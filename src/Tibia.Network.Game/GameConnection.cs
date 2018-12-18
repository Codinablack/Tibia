using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tibia.Data;
using Tibia.Map;
using Tibia.Network.Game.Events;
using Tibia.Network.Game.Packets;
using Tibia.Security.Cryptography;
using Tibia.Spawns;

namespace Tibia.Network.Game
{
    public sealed partial class GameConnection : Connection, IGameConnection
    {
        /// <summary>
        ///     Determines whether the specified creature spawn is known.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        /// <param name="creatureSpawnService">The creature spawn service.</param>
        /// <param name="removeId">The remove identifier.</param>
        /// <returns>
        ///     <c>true</c> if the specified creature spawn is known; otherwise, <c>false</c>.
        /// </returns>
        public bool IsCreatureKnown(ICreatureSpawn creatureSpawn, ICreatureSpawnService creatureSpawnService, out uint removeId)
        {
            if (_knownCreatures.Contains(creatureSpawn.Id))
            {
                removeId = 0;
                return true;
            }

            removeId = 0;

            // TODO: Fix this logic, it never removes entries!
            if (_knownCreatures.Count > 1300)
            {
                foreach (uint knownCreature in _knownCreatures)
                {
                    ICreatureSpawn creature = creatureSpawnService.GetCreatureSpawnById(knownCreature);
                    if (creature != null && CharacterSpawn.CanSee(creature))
                        continue;

                    removeId = knownCreature;
                    _knownCreatures.Remove(knownCreature);
                    return false;
                }

                // All creatures are visible, remove first
                if (removeId == 0)
                {
                    removeId = _knownCreatures.First();
                    _knownCreatures.Remove(removeId);
                    return false;
                }
            }

            _knownCreatures.Add(creatureSpawn.Id);
            removeId = 0;
            return false;
        }
        /// <summary>
        ///     Disconnects the specified reason.
        /// </summary>
        /// <param name="reason">The reason.</param>
        public void Disconnect(string reason)
        {
            NetworkMessage message = new NetworkMessage(Xtea);

            if (!string.IsNullOrWhiteSpace(reason))
            {
                DisconnectPacket.Add(message, reason);
                Send(message);
            }

            Disconnected?.Invoke(this, new DisconnectedEventArgs(CharacterSpawn));
            Close();
        }
        /// <summary>
        ///     Moves the creature.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        /// <param name="targetPosition">The target position.</param>
        public bool MoveCreature(ICreatureSpawn creatureSpawn, IVector3 targetPosition)
        {
            ITile targetTile = _tileService.Tile(targetPosition);
            if (targetTile == null)
                return false;

            // TODO: Have the range defined as static and available to the whole engine => new Vector3(1, 1, 1)
            bool isTeleporting = !creatureSpawn.Tile.Position.IsNextTo(targetTile.Position, new Vector3(1, 1, 1));
            if (!isTeleporting && !targetTile.IsWalkable())
                return false;

            MovingCreatureEventArgs movingCreatureEventArgs = new MovingCreatureEventArgs(creatureSpawn, targetTile);
            MovingCreature?.Invoke(this, movingCreatureEventArgs);
            if (movingCreatureEventArgs.Cancel)
                return false;

            ITile sourceTile = creatureSpawn.Tile;
            byte sourceStackPosition = creatureSpawn.StackPosition;
            Direction targetDirection = creatureSpawn.Tile.Position.GetDirection(targetTile.Position);

            if (!isTeleporting && targetTile.FloorChangeDirection != FloorChangeDirection.None)
            {
                // Target tile has a ladder or similar
                if (targetTile.IsWalkable())
                {
                    IVector3 positionFromFloorChange = targetTile.GetPositionFromFloorChange(_tileService, targetTile.FloorChangeDirection);

                    // TODO: Improve targetDirection detection when going upstairs
                    targetDirection = targetTile.Position.GetDirection(positionFromFloorChange);
                    targetTile = _tileService.Tile(positionFromFloorChange);
                }
            }

            UnregisterTileCreature(creatureSpawn);
            creatureSpawn.Direction = targetDirection;
            creatureSpawn.Tile = targetTile;

            byte newStackPosition = (byte) _tileService.GetItemsBeforeMediumPriority(targetTile).Count(s => s.Item.IsAlwaysOnTop);
            if (targetTile.Ground != null)
                newStackPosition++;

            creatureSpawn.StackPosition = newStackPosition;
            RegisterTileCreature(creatureSpawn);

            // TODO: Close containers that are not nearby
            //var mover = creature as Character;
            //if (mover != null)
            //{
            //    foreach (byte i in mover.Inventory.GetContainersToClose(toLocation))
            //    {
            //        mover.Inventory.CloseContainer(i);
            //        mover.Connection.SendContainerClose(i);
            //    }
            //}

            // TODO: SetPlayerFlags(creature as Character, toTile);

            foreach (ICharacterSpawn spectator in _tileService.Spectators(SpectatorRange, sourceTile.Position, targetTile.Position))
            {
                if (creatureSpawn == spectator)
                {
                    if (isTeleporting)
                        spectator.Connection.SendSelfTeleport(sourceTile.Position, sourceStackPosition, targetTile.Position);
                    else
                        spectator.Connection.SendSelfMove(sourceTile.Position, sourceStackPosition, targetTile.Position);
                }
                else
                {
                    bool canSeeSourcePosition = spectator.Tile.Position.IsInRange(sourceTile.Position, SpectatorRange);
                    bool canSeeTargetPosition = spectator.Tile.Position.IsInRange(targetTile.Position, SpectatorRange);
                    if (canSeeSourcePosition && canSeeTargetPosition)
                    {
                        if (!isTeleporting)
                            spectator.Connection.SendCreatureMove(sourceTile.Position, sourceStackPosition, targetTile.Position);
                        else
                        {
                            spectator.Connection.SendTileArtifactRemove(sourceTile.Position, sourceStackPosition);
                            spectator.Connection.SendTileAddCreature(creatureSpawn, targetTile.Position, creatureSpawn.StackPosition);
                        }
                    }
                    else if (canSeeSourcePosition)
                        spectator.Connection.SendTileArtifactRemove(sourceTile.Position, sourceStackPosition);
                    else if (canSeeTargetPosition)
                        spectator.Connection.SendTileAddCreature(creatureSpawn, targetTile.Position, creatureSpawn.StackPosition);
                }
            }

            CreatureMovedEventArgs creatureMovedEventArgs = new CreatureMovedEventArgs(movingCreatureEventArgs.CreatureSpawn, movingCreatureEventArgs.TargetTile);
            CreatureMoved?.Invoke(this, creatureMovedEventArgs);
            return true;
        }

        /// <summary>
        ///     Moves the artifact.
        /// </summary>
        /// <param name="sourcePosition">The source position.</param>
        /// <param name="sourceStackPosition">The source stack position.</param>
        /// <param name="spriteId">The sprite identifier.</param>
        /// <param name="targetPosition">The target position.</param>
        /// <param name="count">The count.</param>
        /// <returns><c>true</c> if the artifact is moved successfully; otherwise, <c>false</c>.</returns>
        private bool MoveArtifact(IVector3 sourcePosition, byte sourceStackPosition, ushort spriteId, IVector3 targetPosition, byte count)
        {
            ISpawn spawn = GetArtifactSpawn(sourcePosition, sourceStackPosition);
            if (spawn == null)
            {
                CharacterSpawn.Connection.SendTextMessage(TextMessageType.StatusSmall, "Sorry, not possible.");
                return false;
            }

            if (spawn is ICreatureSpawn creatureSpawn)
            {
                ITile targetTile = _tileService.Tile(targetPosition);
                if (targetTile == null)
                {
                    CharacterSpawn.Connection.SendTextMessage(TextMessageType.StatusSmall, "Sorry, not possible.");
                    return false;
                }

                if (CharacterSpawn.Character.Settings.CanTeleportObject(creatureSpawn) || CharacterSpawn.Tile.Position.IsNextTo(creatureSpawn.Tile.Position, MoveCreatureRange) && creatureSpawn.Tile.Position.IsNextTo(targetTile.Position, MoveCreatureRange))
                    return MoveCreature(creatureSpawn, targetPosition);
            }
            else if (spawn is IItemSpawn itemSpawn)
            {
                ITile targetTile = _tileService.Tile(targetPosition);
                if (targetTile == null)
                {
                    CharacterSpawn.Connection.SendTextMessage(TextMessageType.StatusSmall, "Sorry, not possible.");
                    return false;
                }

                if (CharacterSpawn.Character.Settings.CanTeleportObject(itemSpawn) || CharacterSpawn.Tile.Position.IsNextTo(itemSpawn.Tile.Position, MoveItemSourceRange) && itemSpawn.Tile.Position.IsInRange(targetTile.Position, MoveItemTargetRange))
                    return MoveItem(itemSpawn, targetPosition);
            }

            // TODO: IItemSpawn now inherits from ISpawn. We need to make sure that all properties from ISpawn are set correctly;
            // TODO: We need to merge naming into one (thing, artifact, object)
            return false;
        }


        /// <summary>
        ///     Gets the artifact spawn.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="stackPosition">The stack position.</param>
        /// <returns>The artifact spawn.</returns>
        private ISpawn GetArtifactSpawn(IVector3 position, byte stackPosition)
        {
            switch (position.Type)
            {
                case Vector3Type.Ground:
                {
                    // TODO: Creature spawns and item spawns must be placed into one single class (currently separated in TileService.Creatures and Tile.Items)
                    // BUG: Here we should allow to move creature spawns in case there is any in the stack before an item spawn
                    // TODO: This is too ugly! We need to make Tile have a TryGetItemByIndex method and track those items through a dictionary
                    return _tileService.Spawns(position).FirstOrDefault(s => s.StackPosition == stackPosition);
                }
                case Vector3Type.Container:
                {
                    // TODO: This is a magic number that should be properly designed (or at least documented)
                    // NOTE: If the index is outside limits (index < 0 || index >= 16) then it's forced to be inside (e.g.: 16 becomes 0)
                    int index = position.Y & 0x0F;
                    if (!CharacterSpawn.TryGetOpenContainer(index, out IContainerItemSpawn container))
                        return null;

                    // BUG: Casting to byte may throw an exception
                    if (!container.TryGetItemByIndex((byte) position.Z, out IItemSpawn item))
                        return null;

                    return item;
                }
                case Vector3Type.Slot:
                {
                    // TODO: We're missing one case here, whenever Y and Z are zero, then it would be a liquid. It's unclear how this works
                    return CharacterSpawn.GetItemSpawnFromInventorySlotType(position.ToSlotType());
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(position.Type), position.Type, null);
            }
        }

        /// <summary>
        ///     Moves the item.
        /// </summary>
        /// <param name="itemSpawn">The item spawn.</param>
        /// <param name="targetPosition">The target position.</param>
        /// <returns><c>true</c> if the item is moved successfully; otherwise, <c>false</c>.</returns>
        public bool MoveItem(IItemSpawn itemSpawn, IVector3 targetPosition)
        {
            ITile targetTile = _tileService.Tile(targetPosition);
            if (targetTile == null)
                return false;

            MovingItemEventArgs movingCreatureEventArgs = new MovingItemEventArgs(itemSpawn, targetTile);
            MovingItem?.Invoke(this, movingCreatureEventArgs);
            if (movingCreatureEventArgs.Cancel)
                return false;

            ITile sourceTile = itemSpawn.Tile;
            byte sourceStackPosition = itemSpawn.StackPosition;

            UnregisterTileItem(itemSpawn);
            itemSpawn.Tile = targetTile;

            byte newStackPosition = (byte) _tileService.GetItemsBeforeMediumPriority(targetTile).Count(s => s.Item.IsAlwaysOnTop);
            if (targetTile.Ground != null)
                newStackPosition++;

            itemSpawn.StackPosition = newStackPosition;
            RegisterTileItem(itemSpawn);

            foreach (ICharacterSpawn spectator in _tileService.Spectators(SpectatorRange, sourceTile.Position, targetTile.Position))
            {
                bool canSeeSourcePosition = spectator.Tile.Position.IsInRange(sourceTile.Position, SpectatorRange);
                if (canSeeSourcePosition)
                    spectator.Connection.SendTileArtifactRemove(sourceTile.Position, sourceStackPosition);

                bool canSeeTargetPosition = spectator.Tile.Position.IsInRange(targetTile.Position, SpectatorRange);
                if (canSeeTargetPosition)
                    spectator.Connection.SendTileAddItem(itemSpawn, targetTile.Position, itemSpawn.StackPosition);
            }

            ItemMovedEventArgs creatureMovedEventArgs = new ItemMovedEventArgs(movingCreatureEventArgs.ItemSpawn, movingCreatureEventArgs.TargetTile);
            ItemMoved?.Invoke(this, creatureMovedEventArgs);
            return true;
        }

        /// <summary>
        ///     Called when [opening channel].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="OpeningChannelEventArgs" /> instance containing the event data.</param>
        private void OnOpeningChannel(object sender, OpeningChannelEventArgs e)
        {
            if (!(e.Channel is IPrivateChannel privateChannel) || privateChannel.IsInvited(e.CharacterSpawn))
                return;

            e.CharacterSpawn.Connection.SendTextMessage(TextMessageType.StatusSmall, "You are not invited to this channel.");
            e.Cancel = true;
        }

        /// <summary>
        ///     Called when [removing friend].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RemovingFriendEventArgs" /> instance containing the event data.</param>
        private void OnRemovingFriend(object sender, RemovingFriendEventArgs e)
        {
            if (e.Friend != null)
                return;

            e.Cancel = true;
        }

        /// <summary>
        ///     Called when [editing friend].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EditingFriendEventArgs" /> instance containing the event data.</param>
        private void OnEditingFriend(object sender, EditingFriendEventArgs e)
        {
            if (e.Friend != null)
                return;

            e.Cancel = true;
        }

        /// <summary>
        ///     Called when [request ping back].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnRequestPingBack(object sender, EventArgs e)
        {
            CharacterSpawn.Connection.SendPingBack();
        }

        /// <summary>
        ///     Called when [logged in].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="LoggedInEventArgs" /> instance containing the event data.</param>
        private void OnLoggedIn(object sender, LoggedInEventArgs e)
        {
            _chatService.UnregisterFromAllChannels(CharacterSpawn);
            _creatureSpawnService.RegisterCreature(CharacterSpawn);

            foreach (IAccount account in _chatService.GetAccountsWithFriend(CharacterSpawn.Character.Id))
            foreach (IFriend friend in account.Friends.Where(s => s.Character.Id == CharacterSpawn.Character.Id))
            foreach (ICharacterSpawn characterSpawnWithFriend in account.CharacterSpawns.Where(s => s.Connection != null))
            {
                characterSpawnWithFriend.Connection.SendFriendLogin(friend.Id);
                if (friend.NotifyOnLogin)
                    characterSpawnWithFriend.Connection.SendTextMessage(TextMessageType.StatusSmall, $"{CharacterSpawn.Character.Name} has logged in.");
            }

            // TODO: Clear all modal windows
            RegisterTileCreature(CharacterSpawn);

            // TODO: The range of spectatorship should probably be set once and be available through the server => new Vector3(10, 10, 3)
            foreach (ICharacterSpawn spectator in _tileService.Spectators(CharacterSpawn.Tile.Position, new Vector3(10, 10, 3)).Where(s => s != CharacterSpawn))
                spectator.Connection.SendTileAddCreature(CharacterSpawn, CharacterSpawn.Tile.Position, CharacterSpawn.StackPosition);

            // TODO: Improve multiple channel registrations
            _chatService.RegisterCharacterSpawn(CharacterSpawn, _chatService.GetChannelByType(ChannelType.Local));
            _chatService.RegisterCharacterSpawn(CharacterSpawn, _chatService.GetChannelByType(ChannelType.Loot));
            _chatService.RegisterCharacterSpawn(CharacterSpawn, _chatService.GetChannelByType(ChannelType.Advertising));
            _chatService.RegisterCharacterSpawn(CharacterSpawn, _chatService.GetChannelByType(ChannelType.English));
            _chatService.RegisterCharacterSpawn(CharacterSpawn, _chatService.GetChannelByType(ChannelType.Help));
            _chatService.RegisterCharacterSpawn(CharacterSpawn, _chatService.GetChannelByType(ChannelType.World));

            IChannel channel = _chatService.CreatePrivateChannel(CharacterSpawn);
            _chatService.RegisterCharacterSpawn(CharacterSpawn, channel);
        }

        /// <summary>
        ///     Registers the specified creature spawn in a tile.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        private void RegisterTileCreature(ICreatureSpawn creatureSpawn)
        {
            RegisteringTileCreatureEventArgs registeringTileCreatureEventArgs = new RegisteringTileCreatureEventArgs(creatureSpawn);
            RegisteringTileCreature?.Invoke(this, registeringTileCreatureEventArgs);
            if (registeringTileCreatureEventArgs.Cancel)
                return;

            _tileService.AddCreature(creatureSpawn);

            RegisteredTileCreatureEventArgs registeredTileCreatureEventArgs = new RegisteredTileCreatureEventArgs(registeringTileCreatureEventArgs.CreatureSpawn);
            RegisteredTileCreature?.Invoke(this, registeredTileCreatureEventArgs);
        }

        /// <summary>
        ///     Unregisters the specified creature spawn in a tile.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        private void UnregisterTileCreature(ICreatureSpawn creatureSpawn)
        {
            UnregisteringTileCreatureEventArgs unregisteringTileCreatureEventArgs = new UnregisteringTileCreatureEventArgs(creatureSpawn);
            UnregisteringTileCreature?.Invoke(this, unregisteringTileCreatureEventArgs);
            if (unregisteringTileCreatureEventArgs.Cancel)
                return;

            _tileService.RemoveCreature(creatureSpawn);

            UnregisteredTileCreatureEventArgs unregisteredTileCreatureEventArgs = new UnregisteredTileCreatureEventArgs(unregisteringTileCreatureEventArgs.CreatureSpawn);
            UnregisteredTileCreature?.Invoke(this, unregisteredTileCreatureEventArgs);
        }

        /// <summary>
        ///     Registers the specified item spawn in a tile.
        /// </summary>
        /// <param name="itemSpawn">The item spawn.</param>
        private void RegisterTileItem(IItemSpawn itemSpawn)
        {
            RegisteringTileItemEventArgs registeringTileItemEventArgs = new RegisteringTileItemEventArgs(itemSpawn);
            RegisteringTileItem?.Invoke(this, registeringTileItemEventArgs);
            if (registeringTileItemEventArgs.Cancel)
                return;

            _tileService.AddItem(itemSpawn);

            RegisteredTileItemEventArgs registeredTileItemEventArgs = new RegisteredTileItemEventArgs(registeringTileItemEventArgs.ItemSpawn);
            RegisteredTileItem?.Invoke(this, registeredTileItemEventArgs);
        }

        /// <summary>
        ///     Unregisters the specified item spawn in a tile.
        /// </summary>
        /// <param name="itemSpawn">The item spawn.</param>
        private void UnregisterTileItem(IItemSpawn itemSpawn)
        {
            UnregisteringTileItemEventArgs unregisteringTileItemEventArgs = new UnregisteringTileItemEventArgs(itemSpawn);
            UnregisteringTileItem?.Invoke(this, unregisteringTileItemEventArgs);
            if (unregisteringTileItemEventArgs.Cancel)
                return;

            _tileService.RemoveItem(itemSpawn);

            UnregisteredTileItemEventArgs unregisteredTileItemEventArgs = new UnregisteredTileItemEventArgs(unregisteringTileItemEventArgs.ItemSpawn);
            UnregisteredTileItem?.Invoke(this, unregisteredTileItemEventArgs);
        }

        /// <summary>
        ///     Called when [client read handshake message].
        /// </summary>
        /// <param name="result">The result.</param>
        private void OnClientReadHandshakeMessage(IAsyncResult result)
        {
            if (!EndRead(result))
                return;

            try
            {
                if (IncomingMessage.ReadNetworkProtocol() != NetworkProtocol.Game)
                    throw new InvalidNetworkProtocolException();

                ParseLoginPacket(IncomingMessage);
                if (Stream.CanRead)
                    Stream.BeginRead(IncomingMessage.Buffer, 0, 2, OnClientReadProtocolMessage, null);
            }
            catch (InvalidRsaException invalidRsaException)
            {
                Disconnect(invalidRsaException.Message);
                Close();
            }
            catch (AuthenticationFailedException authFailedException)
            {
                Disconnect(authFailedException.Message);
                Close();
            }
            catch
            {
                // TODO: Invalid data from the client, log!
                Close();
            }
        }
        /// <summary>
        ///     Closes this instance.
        /// </summary>
        public override void Close()
        {
            IsRemoved = true;
            base.Close();
        }

        /// <summary>
        ///     Called when [client read protocol message].
        /// </summary>
        /// <param name="result">The result.</param>
        private void OnClientReadProtocolMessage(IAsyncResult result)
        {
            try
            {
                if (!EndRead(result))
                {
                    // Client crashed or disconnected
                    Disconnected?.Invoke(this, new DisconnectedEventArgs(CharacterSpawn));
                    return;
                }

                IncomingMessage.Decrypt(Xtea);
                // TODO: Validate that length is actually the one stated
                ushort length = IncomingMessage.ReadUInt16();
                GameClientPacketType type = IncomingMessage.ReadGameClientPacketType();

                if (!_gamePacketMap.ContainsKey(type))
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);

                _gamePacketMap[type].Invoke(IncomingMessage);

                if (!IsRemoved)
                    Stream.BeginRead(IncomingMessage.Buffer, 0, 2, OnClientReadProtocolMessage, null);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        /// <summary>
        ///     Called when [adding friend].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="AddingFriendEventArgs" /> instance containing the event data.</param>
        private void OnAddingFriend(object sender, AddingFriendEventArgs e)
        {
            // TODO: Permission to bypass friend count limit
            // TODO: Move hard-coded limit to app settings
            if (e.CharacterSpawn.Account.Friends.Count >= 200)
            {
                e.CharacterSpawn.Connection.SendTextMessage(TextMessageType.StatusSmall, "You cannot add more friends.");
                e.Cancel = true;
                return;
            }

            ICollection<ICreatureSpawn> creatureSpawns = _creatureSpawnService.GetCreatureSpawnsByName(e.FriendName).ToList();
            ICollection<ICharacterSpawn> characterSpawns = creatureSpawns.Where(s => s is ICharacterSpawn).Cast<ICharacterSpawn>().ToList();
            if (!characterSpawns.Any())
            {
                e.CharacterSpawn.Connection.SendTextMessage(TextMessageType.StatusSmall, "A player with this name does not exist.");
                e.Cancel = true;
                return;
            }

            // TODO: Permission to bypass adding self
            if (characterSpawns.Any(s => s.Id == e.CharacterSpawn.Creature.Id))
            {
                e.CharacterSpawn.Connection.SendTextMessage(TextMessageType.StatusSmall, "You cannot add this player.");
                e.Cancel = true;
                return;
            }

            if (e.CharacterSpawn.Account.Friends.Any(s => characterSpawns.Any(c => c.Id == s.Character.Id)))
            {
                e.CharacterSpawn.Connection.SendTextMessage(TextMessageType.StatusSmall, "This player is already in your friend list.");
                e.Cancel = true;
                return;
            }

            e.Character = characterSpawns.Select(s => s.Character).FirstOrDefault();
        }

        /// <summary>
        ///     Turns the character.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public void TurnCharacter(Direction direction)
        {
            CreatureTurningEventArgs creatureTurningEventArgs = new CreatureTurningEventArgs(CharacterSpawn, direction);
            CreatureTurning?.Invoke(this, creatureTurningEventArgs);
            if (creatureTurningEventArgs.Cancel)
                return;

            CharacterSpawn.Direction = creatureTurningEventArgs.Direction;

            // TODO: The range of spectatorship should probably be set once and be available through the server
            foreach (ICharacterSpawn spectator in _tileService.Spectators(CharacterSpawn.Tile.Position, new Vector3(10, 10, 3)))
                spectator.Connection.SendCreatureTurn(CharacterSpawn);

            CreatureTurnedEventArgs creatureTurnedEventArgs = new CreatureTurnedEventArgs(CharacterSpawn);
            CreatureTurned?.Invoke(this, creatureTurnedEventArgs);
        }

        /// <summary>
        ///     Walks the character.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public void WalkCharacter(Direction direction)
        {
            CharacterWalkingEventArgs characterWalkingEventArgs = new CharacterWalkingEventArgs(CharacterSpawn, direction);
            CharacterWalking?.Invoke(this, characterWalkingEventArgs);
            if (characterWalkingEventArgs.Cancel)
                return;

            if (!MoveCreature(CharacterSpawn, CharacterSpawn.Tile.Position.Offset(direction)))
                return;

            CharacterWalkedEventArgs characterWalkedEventArgs = new CharacterWalkedEventArgs(CharacterSpawn, characterWalkingEventArgs.Direction);
            CharacterWalked?.Invoke(this, characterWalkedEventArgs);
        }

        /// <summary>
        ///     Walks the character.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        /// <param name="directions">The directions.</param>
        private async void WalkCharacter(ICreatureSpawn creatureSpawn, Queue<Direction> directions)
        {
            if (directions.Count <= 0)
                return;

            while (true)
            {
                Direction direction = directions.First();
                if (!MoveCreature(creatureSpawn, creatureSpawn.Tile.Position.Offset(direction)))
                    return;

                directions.Dequeue();

                // TODO: Updating step cost should be done in MoveCreature method and include costs of moving diagonally
                TimeSpan stepDuration = creatureSpawn.StepDuration();
                await Task.Delay(direction <= Direction.West ? stepDuration : stepDuration.Add(stepDuration));

                if (directions.Count <= 0)
                    return;
            }
        }
    }
}