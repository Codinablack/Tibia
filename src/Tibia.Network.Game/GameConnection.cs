using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Tibia.Communications;
using Tibia.Communications.Channels;
using Tibia.Communications.Commands;
using Tibia.Data;
using Tibia.Map;
using Tibia.Mounts;
using Tibia.Network.Game.Packets;
using Tibia.Outfits;
using Tibia.Quests;
using Tibia.Security.Cryptography;
using Tibia.Spawns;
using Tibia.Spells;

namespace Tibia.Network.Game
{
    public sealed class GameConnection : Connection, IGameConnection
    {
        private readonly ChatService _chatService;
        private readonly CommandService _commandService;
        private readonly CreatureSpawnService _creatureSpawnService;
        private readonly IDictionary<GameClientPacketType, Action<NetworkMessage>> _gamePacketMap;
        private readonly ICollection<uint> _knownCreatures;
        private readonly MountService _mountService;
        private readonly OutfitService _outfitService;
        private readonly SpellService _spellService;
        private readonly TileService _tileService;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.GameConnection" /> class.
        /// </summary>
        /// <param name="xtea">The XTEA key.</param>
        /// <param name="chatService">The chat service.</param>
        /// <param name="creatureSpawnService">The creature spawn service.</param>
        /// <param name="tileService">The tile service.</param>
        /// <param name="spellService">The spell service.</param>
        /// <param name="commandService">The command service.</param>
        /// <param name="outfitService">The outfit service.</param>
        /// <param name="mountService">The mount service.</param>
        public GameConnection(Xtea xtea, ChatService chatService, CreatureSpawnService creatureSpawnService, TileService tileService, SpellService spellService, CommandService commandService, OutfitService outfitService, MountService mountService)
            : base(xtea)
        {
            _chatService = chatService;
            _creatureSpawnService = creatureSpawnService;
            _tileService = tileService;
            _spellService = spellService;
            _commandService = commandService;
            _outfitService = outfitService;
            _mountService = mountService;

            LoggedIn += OnLoggedIn;
            RequestPingBack += OnRequestPingBack;
            AddingFriend += OnAddingFriend;
            EditingFriend += OnEditingFriend;
            RemovingFriend += OnRemovingFriend;
            OpeningChannel += OnOpeningChannel;

            IsRemoved = false;
            _knownCreatures = new HashSet<uint>();
            _gamePacketMap = new Dictionary<GameClientPacketType, Action<NetworkMessage>>();

            Initialize();
        }

        /// <summary>
        ///     Gets or sets the character spawn.
        /// </summary>
        /// <value>
        ///     The character spawn.
        /// </value>
        public CharacterSpawn CharacterSpawn { get; set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is removed.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is removed; otherwise, <c>false</c>.
        /// </value>
        public bool IsRemoved { get; private set; }

        /// <inheritdoc />
        /// <summary>
        ///     Sends the text message.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="text">The text.</param>
        /// <param name="position">The position.</param>
        /// <param name="primaryValue">The primary value.</param>
        /// <param name="primaryColor">The primary color..</param>
        /// <param name="secondaryValue">The secondary value.</param>
        /// <param name="secondaryColor">The secondary color.</param>
        public void SendTextMessage(TextMessageType type, string text, IVector3 position = null, uint? primaryValue = null, byte? primaryColor = null, uint? secondaryValue = null, byte? secondaryColor = null)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            TextMessagePacket.Add(message, type, text, position, primaryValue, primaryColor, secondaryValue, secondaryColor);
            Send(message);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Sends the friend login.
        /// </summary>
        /// <param name="friendId">The friend identifier.</param>
        public void SendFriendLogin(uint friendId)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            FriendStatusPacket.Add(message, friendId, SessionStatus.Online);
            Send(message);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        /// <summary>
        ///     Sends the tile add creature.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        /// <param name="position">The position.</param>
        /// <param name="stackPosition">The stack position.</param>
        public void SendTileAddCreature(ICreatureSpawn creatureSpawn, IVector3 position, byte stackPosition)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            bool known = IsCreatureKnown(creatureSpawn, _creatureSpawnService, out uint remove);
            TileAddCreaturePacket.Add(message, CharacterSpawn, position, stackPosition, creatureSpawn, known, remove);
            Send(message);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Sends the effect.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="effect">The effect.</param>
        public void SendEffect(IVector3 position, Effect effect)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            EffectPacket.Add(message, position, effect);
            Send(message);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Sends the enter game.
        /// </summary>
        /// <param name="lightInfo">The light information.</param>
        /// <param name="premiumTimeLeft">The premium time left.</param>
        /// <param name="characterSpawn">The character spawn.</param>
        public void SendEnterGame(ILightInfo lightInfo, TimeSpan premiumTimeLeft, ICharacterSpawn characterSpawn)
        {
            NetworkMessage message = new NetworkMessage(Xtea);

            EnterGamePacket.Add(message);
            MapDescriptionPacket.Add(message, characterSpawn, characterSpawn.Tile.Position, _creatureSpawnService, _tileService);

            if (characterSpawn.Outfit != null && characterSpawn.Mount != null)
                CreatureChangeAppearancePacket.Add(message, characterSpawn.Id, characterSpawn.Outfit, characterSpawn.IsRiding ? characterSpawn.Mount : null);

            InventoryItemPacket.Add(message, characterSpawn.Head, SlotType.Head);
            InventoryItemPacket.Add(message, characterSpawn.Amulet, SlotType.Amulet);
            InventoryItemPacket.Add(message, characterSpawn.Container, SlotType.Container);
            InventoryItemPacket.Add(message, characterSpawn.Torso, SlotType.Torso);
            InventoryItemPacket.Add(message, characterSpawn.Shield, SlotType.Shield);
            InventoryItemPacket.Add(message, characterSpawn.Weapon, SlotType.Weapon);
            InventoryItemPacket.Add(message, characterSpawn.Legs, SlotType.Legs);
            InventoryItemPacket.Add(message, characterSpawn.Feet, SlotType.Feet);
            InventoryItemPacket.Add(message, characterSpawn.Ring, SlotType.Ring);
            InventoryItemPacket.Add(message, characterSpawn.Belt, SlotType.Belt);

            SelfStatsPacket.Add(message, characterSpawn);
            SelfSkillsPacket.Add(message, characterSpawn);
            AmbientPacket.Add(message, lightInfo);
            CreatureLightPacket.Add(message, characterSpawn.Id, characterSpawn.LightInfo);

            SelfBasicStatsPacket.Add(message, premiumTimeLeft, characterSpawn.Vocation, _spellService.Spells.ToList());
            SelfSpecialConditionsPacket.Add(message, characterSpawn.Conditions);

            Send(message);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Sends the ping back.
        /// </summary>
        public void SendPingBack()
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            GameServerPingBackPacket.Add(message);
            Send(message);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Sends the friend.
        /// </summary>
        /// <param name="friend">The friend.</param>
        public void SendFriend(IFriend friend)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            FriendsPacket.Add(message, friend);
            Send(message);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        /// <summary>
        ///     Sends the creature turn.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        public void SendCreatureTurn(ICreatureSpawn creatureSpawn)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            TileCreatureTransformPacket.Add(message, creatureSpawn);
            Send(message);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Sends the appearance window.
        /// </summary>
        public void SendAppearanceWindow()
        {
            // TODO: This should probably send default values since the user should select their appearance
            if (CharacterSpawn.Outfit == null || CharacterSpawn.Mount == null)
                return;

            NetworkMessage message = new NetworkMessage(Xtea);

            // TODO: Order outfits and mounts by name EVERY TIME the list of those available changes, not here. Then pass here the list already sorted ;)
            AppearanceWindowPacket.Add(message, CharacterSpawn.Outfit, CharacterSpawn.Mount, CharacterSpawn.Outfits, CharacterSpawn.Mounts);
            Send(message);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Sends the change creature speed.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        public void SendChangeCreatureSpeed(ICreatureSpawn creatureSpawn)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            CreatureChangeSpeedPacket.Add(message, creatureSpawn.Id, creatureSpawn.Speed);
            Send(message);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Sends the change creature appearance.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        public void SendChangeCreatureAppearance(ICreatureSpawn creatureSpawn)
        {
            // TODO: This should probably send default values instead of returning
            if (CharacterSpawn.Outfit == null || CharacterSpawn.Mount == null)
                return;

            NetworkMessage message = new NetworkMessage(Xtea);
            CreatureChangeAppearancePacket.Add(message, creatureSpawn.Id, CharacterSpawn.Outfit, creatureSpawn.IsRiding ? CharacterSpawn.Mount : null);
            Send(message);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Sends the creature say.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        /// <param name="type">The type.</param>
        /// <param name="text">The text.</param>
        /// <param name="position">The position.</param>
        public void SendCreatureSpeech(ICreatureSpawn creatureSpawn, SpeechType type, string text, IVector3 position)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            // TODO: static uint statementId = 0x00 and statementId++; (Logging messages?)
            CreatureSpeechPacket.Add(message, creatureSpawn, type, 0x00, text, position);
            Send(message);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Sends the channel message.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="type">The type.</param>
        /// <param name="text">The text.</param>
        public void SendChannelMessage(ICharacterSpawn characterSpawn, ushort channelId, SpeechType type, string text)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            // TODO: static uint statementId = 0x00 and statementId++; (Logging messages?)
            ChannelTextPacket.Add(message, characterSpawn, channelId, type, 0x00, text);
            Send(message);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Sends the channel list.
        /// </summary>
        /// <param name="channels">The channels.</param>
        public void SendChannelList(ICollection<IChannel> channels)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            ChannelsListPacket.Add(message, channels);
            Send(message);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Sends the open channel.
        /// </summary>
        /// <param name="channel">The channel.</param>
        public void SendOpenChannel(IChannel channel)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            ChannelOpenPacket.Add(message, channel);
            Send(message);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Sends the close channel.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        public void SendCloseChannel(ushort channelId)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            ChannelClosePacket.Add(message, channelId);
            Send(message);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Sends the close channel.
        /// </summary>
        /// <param name="channelType">Type of the channel.</param>
        public void SendCloseChannel(ChannelType channelType)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            ChannelClosePacket.Add(message, channelType);
            Send(message);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Sends the self move.
        /// </summary>
        /// <param name="sourcePosition">The source position.</param>
        /// <param name="sourceStackPosition">The source stack position.</param>
        /// <param name="targetPosition">The target position.</param>
        public void SendSelfMove(IVector3 sourcePosition, byte sourceStackPosition, IVector3 targetPosition)
        {
            NetworkMessage message = new NetworkMessage(Xtea);

            // TODO: Magic numbers should not exist!
            if (sourcePosition.Z == 7 && targetPosition.Z >= 8)
                TileRemoveArtifactPacket.Add(message, sourcePosition, sourceStackPosition);
            else
                CreatureMovePacket.Add(message, sourcePosition, sourceStackPosition, targetPosition);

            if (targetPosition.Z > sourcePosition.Z)
                MapFloorChangeDownPacket.Add(message, CharacterSpawn, sourcePosition, sourceStackPosition, targetPosition, _creatureSpawnService, _tileService);
            else if (targetPosition.Z < sourcePosition.Z)
                MapFloorChangeUpPacket.Add(message, CharacterSpawn, sourcePosition, sourceStackPosition, targetPosition, _creatureSpawnService, _tileService);

            MapSlicePacket.Add(message, CharacterSpawn, sourcePosition, targetPosition, _creatureSpawnService, _tileService);
            Send(message);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Sends the self teleport.
        /// </summary>
        /// <param name="sourcePosition">The source position.</param>
        /// <param name="sourceStackPosition">The source stack position.</param>
        /// <param name="targetPosition">The target position.</param>
        public void SendSelfTeleport(IVector3 sourcePosition, byte sourceStackPosition, IVector3 targetPosition)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            TileRemoveArtifactPacket.Add(message, sourcePosition, sourceStackPosition);
            MapDescriptionPacket.Add(message, CharacterSpawn, targetPosition, _creatureSpawnService, _tileService);
            Send(message);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Sends the tile artifact remove.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="stackPosition">The stack position.</param>
        public void SendTileArtifactRemove(IVector3 position, byte stackPosition)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            TileRemoveArtifactPacket.Add(message, position, stackPosition);
            Send(message);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Sends the creature move.
        /// </summary>
        /// <param name="sourcePosition">The source position.</param>
        /// <param name="sourceStackPosition">The source stack position.</param>
        /// <param name="targetPosition">The target position.</param>
        public void SendCreatureMove(IVector3 sourcePosition, byte sourceStackPosition, IVector3 targetPosition)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            CreatureMovePacket.Add(message, sourcePosition, sourceStackPosition, targetPosition);
            Send(message);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Moves the creature.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        /// <param name="targetPosition">The target position.</param>
        public bool MoveCreature(ICreatureSpawn creatureSpawn, IVector3 targetPosition)
        {
            ITile targetTile = _tileService.GetTileByPosition(targetPosition);

            // TODO: Have the range defined as static and available to the whole engine => new Vector3(1, 1, 1)
            bool isTeleporting = !creatureSpawn.Tile.Position.IsNextTo(targetTile.Position, new Vector3(1, 1, 1));
            if (!isTeleporting && !targetTile.IsWalkable())
                return false;

            MovingCreatureEventArgs movingCreatureEventArgs = new MovingCreatureEventArgs(CharacterSpawn, targetTile);
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
                    targetTile = _tileService.GetTileByPosition(positionFromFloorChange);
                }
            }

            UnregisterTileCreature(creatureSpawn);
            creatureSpawn.Direction = targetDirection;
            creatureSpawn.Tile = targetTile;
            creatureSpawn.StackPosition = (byte) (creatureSpawn.Tile.Items.Count + 1);
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

            // TODO: The range of spectatorship should probably be set once and be available through the server => new Vector3(10, 10, 3)
            IVector3 range = new Vector3(10, 10, 3);
            foreach (ICharacterSpawn spectator in _tileService.GetSpectators(new[] { sourceTile.Position, targetTile.Position }, range))
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
                    bool canSeeSourcePosition = spectator.Tile.Position.IsInRange(sourceTile.Position, range);
                    bool canSeeTargetPosition = spectator.Tile.Position.IsInRange(targetTile.Position, range);
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

            CreatureMovedEventArgs creatureMovedEventArgs = new CreatureMovedEventArgs(CharacterSpawn, movingCreatureEventArgs.TargetTile);
            CreatureMoved?.Invoke(this, creatureMovedEventArgs);
            return true;
        }

        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            // TODO: Map all packets
            //_gamePacketMap.Add(GameClientPacketType.Packet0, ParsePacket0);
            //_gamePacketMap.Add(GameClientPacketType.LoginServerRequest, ParseLoginServerRequest);
            //_gamePacketMap.Add(GameClientPacketType.Packet2, ParsePacket2);
            //_gamePacketMap.Add(GameClientPacketType.Packet3, ParsePacket3);
            //_gamePacketMap.Add(GameClientPacketType.Packet4, ParsePacket4);
            //_gamePacketMap.Add(GameClientPacketType.Packet5, ParsePacket5);
            //_gamePacketMap.Add(GameClientPacketType.Packet6, ParsePacket6);
            //_gamePacketMap.Add(GameClientPacketType.Packet7, ParsePacket7);
            //_gamePacketMap.Add(GameClientPacketType.Packet8, ParsePacket8);
            //_gamePacketMap.Add(GameClientPacketType.Packet9, ParsePacket9);
            //_gamePacketMap.Add(GameClientPacketType.GameServerRequest, ParseGameServerRequest);
            //_gamePacketMap.Add(GameClientPacketType.Packet11, ParsePacket11);
            //_gamePacketMap.Add(GameClientPacketType.Packet12, ParsePacket12);
            //_gamePacketMap.Add(GameClientPacketType.Packet13, ParsePacket13);
            //_gamePacketMap.Add(GameClientPacketType.Packet14, ParsePacket14);
            _gamePacketMap.Add(GameClientPacketType.EnterGame, ParseEnterGame);
            //_gamePacketMap.Add(GameClientPacketType.Packet16, ParsePacket16);
            //_gamePacketMap.Add(GameClientPacketType.Packet17, ParsePacket17);
            //_gamePacketMap.Add(GameClientPacketType.Packet18, ParsePacket18);
            //_gamePacketMap.Add(GameClientPacketType.Packet19, ParsePacket19);
            _gamePacketMap.Add(GameClientPacketType.Logout, ParseLogout);
            //_gamePacketMap.Add(GameClientPacketType.Packet21, ParsePacket21);
            //_gamePacketMap.Add(GameClientPacketType.Packet22, ParsePacket22);
            //_gamePacketMap.Add(GameClientPacketType.Packet23, ParsePacket23);
            //_gamePacketMap.Add(GameClientPacketType.Packet24, ParsePacket24);
            //_gamePacketMap.Add(GameClientPacketType.Packet25, ParsePacket25);
            //_gamePacketMap.Add(GameClientPacketType.Packet26, ParsePacket26);
            //_gamePacketMap.Add(GameClientPacketType.Packet27, ParsePacket27);
            //_gamePacketMap.Add(GameClientPacketType.Packet28, ParsePacket28);
            _gamePacketMap.Add(GameClientPacketType.PingBack, ParsePingBack);
            _gamePacketMap.Add(GameClientPacketType.Ping, ParsePing);
            //_gamePacketMap.Add(GameClientPacketType.Packet31, ParsePacket31);
            //_gamePacketMap.Add(GameClientPacketType.Packet32, ParsePacket32);
            //_gamePacketMap.Add(GameClientPacketType.Packet33, ParsePacket33);
            //_gamePacketMap.Add(GameClientPacketType.Packet34, ParsePacket34);
            //_gamePacketMap.Add(GameClientPacketType.Packet35, ParsePacket35);
            //_gamePacketMap.Add(GameClientPacketType.Packet36, ParsePacket36);
            //_gamePacketMap.Add(GameClientPacketType.Packet37, ParsePacket37);
            //_gamePacketMap.Add(GameClientPacketType.Packet38, ParsePacket38);
            //_gamePacketMap.Add(GameClientPacketType.Packet39, ParsePacket39);
            //_gamePacketMap.Add(GameClientPacketType.Packet40, ParsePacket40);
            //_gamePacketMap.Add(GameClientPacketType.Packet41, ParsePacket41);
            //_gamePacketMap.Add(GameClientPacketType.Packet42, ParsePacket42);
            //_gamePacketMap.Add(GameClientPacketType.Packet43, ParsePacket43);
            //_gamePacketMap.Add(GameClientPacketType.Packet44, ParsePacket44);
            //_gamePacketMap.Add(GameClientPacketType.Packet45, ParsePacket45);
            //_gamePacketMap.Add(GameClientPacketType.Packet46, ParsePacket46);
            //_gamePacketMap.Add(GameClientPacketType.Packet47, ParsePacket47);
            //_gamePacketMap.Add(GameClientPacketType.Packet48, ParsePacket48);
            //_gamePacketMap.Add(GameClientPacketType.Packet49, ParsePacket49);
            //_gamePacketMap.Add(GameClientPacketType.ExtendedOpenPacketCode, ParseExtendedOpenPacketCode);
            //_gamePacketMap.Add(GameClientPacketType.Packet51, ParsePacket51);
            //_gamePacketMap.Add(GameClientPacketType.Packet52, ParsePacket52);
            //_gamePacketMap.Add(GameClientPacketType.Packet53, ParsePacket53);
            //_gamePacketMap.Add(GameClientPacketType.Packet54, ParsePacket54);
            //_gamePacketMap.Add(GameClientPacketType.Packet55, ParsePacket55);
            //_gamePacketMap.Add(GameClientPacketType.Packet56, ParsePacket56);
            //_gamePacketMap.Add(GameClientPacketType.Packet57, ParsePacket57);
            //_gamePacketMap.Add(GameClientPacketType.Packet58, ParsePacket58);
            //_gamePacketMap.Add(GameClientPacketType.Packet59, ParsePacket59);
            //_gamePacketMap.Add(GameClientPacketType.Packet60, ParsePacket60);
            //_gamePacketMap.Add(GameClientPacketType.Packet61, ParsePacket61);
            //_gamePacketMap.Add(GameClientPacketType.Packet62, ParsePacket62);
            //_gamePacketMap.Add(GameClientPacketType.Packet63, ParsePacket63);
            //_gamePacketMap.Add(GameClientPacketType.Packet64, ParsePacket64);
            //_gamePacketMap.Add(GameClientPacketType.Packet65, ParsePacket65);
            //_gamePacketMap.Add(GameClientPacketType.Packet66, ParsePacket66);
            //_gamePacketMap.Add(GameClientPacketType.Packet67, ParsePacket67);
            //_gamePacketMap.Add(GameClientPacketType.Packet68, ParsePacket68);
            //_gamePacketMap.Add(GameClientPacketType.Packet69, ParsePacket69);
            //_gamePacketMap.Add(GameClientPacketType.Packet70, ParsePacket70);
            //_gamePacketMap.Add(GameClientPacketType.Packet71, ParsePacket71);
            //_gamePacketMap.Add(GameClientPacketType.Packet72, ParsePacket72);
            //_gamePacketMap.Add(GameClientPacketType.Packet73, ParsePacket73);
            //_gamePacketMap.Add(GameClientPacketType.Packet74, ParsePacket74);
            //_gamePacketMap.Add(GameClientPacketType.Packet75, ParsePacket75);
            //_gamePacketMap.Add(GameClientPacketType.Packet76, ParsePacket76);
            //_gamePacketMap.Add(GameClientPacketType.Packet77, ParsePacket77);
            //_gamePacketMap.Add(GameClientPacketType.Packet78, ParsePacket78);
            //_gamePacketMap.Add(GameClientPacketType.Packet79, ParsePacket79);
            //_gamePacketMap.Add(GameClientPacketType.Packet80, ParsePacket80);
            //_gamePacketMap.Add(GameClientPacketType.Packet81, ParsePacket81);
            //_gamePacketMap.Add(GameClientPacketType.Packet82, ParsePacket82);
            //_gamePacketMap.Add(GameClientPacketType.Packet83, ParsePacket83);
            //_gamePacketMap.Add(GameClientPacketType.Packet84, ParsePacket84);
            //_gamePacketMap.Add(GameClientPacketType.Packet85, ParsePacket85);
            //_gamePacketMap.Add(GameClientPacketType.Packet86, ParsePacket86);
            //_gamePacketMap.Add(GameClientPacketType.Packet87, ParsePacket87);
            //_gamePacketMap.Add(GameClientPacketType.Packet88, ParsePacket88);
            //_gamePacketMap.Add(GameClientPacketType.Packet89, ParsePacket89);
            //_gamePacketMap.Add(GameClientPacketType.Packet90, ParsePacket90);
            //_gamePacketMap.Add(GameClientPacketType.Packet91, ParsePacket91);
            //_gamePacketMap.Add(GameClientPacketType.Packet92, ParsePacket92);
            //_gamePacketMap.Add(GameClientPacketType.Packet93, ParsePacket93);
            //_gamePacketMap.Add(GameClientPacketType.Packet94, ParsePacket94);
            //_gamePacketMap.Add(GameClientPacketType.Packet95, ParsePacket95);
            //_gamePacketMap.Add(GameClientPacketType.Packet96, ParsePacket96);
            //_gamePacketMap.Add(GameClientPacketType.Packet97, ParsePacket97);
            //_gamePacketMap.Add(GameClientPacketType.Packet98, ParsePacket98);
            //_gamePacketMap.Add(GameClientPacketType.Packet99, ParsePacket99);
            _gamePacketMap.Add(GameClientPacketType.AutoWalk, ParseAutoWalk);
            _gamePacketMap.Add(GameClientPacketType.MoveNorth, ParseMoveNorth);
            _gamePacketMap.Add(GameClientPacketType.MoveEast, ParseMoveEast);
            _gamePacketMap.Add(GameClientPacketType.MoveSouth, ParseMoveSouth);
            _gamePacketMap.Add(GameClientPacketType.MoveWest, ParseMoveWest);
            //_gamePacketMap.Add(GameClientPacketType.AutoWalkCancel, ParseAutoWalkCancel);
            _gamePacketMap.Add(GameClientPacketType.MoveNorthEast, ParseMoveNorthEast);
            _gamePacketMap.Add(GameClientPacketType.MoveSouthEast, ParseMoveSouthEast);
            _gamePacketMap.Add(GameClientPacketType.MoveSouthWest, ParseMoveSouthWest);
            _gamePacketMap.Add(GameClientPacketType.MoveNorthWest, ParseMoveNorthWest);
            //_gamePacketMap.Add(GameClientPacketType.Packet110, ParsePacket110);
            _gamePacketMap.Add(GameClientPacketType.TurnNorth, ParseTurnNorth);
            _gamePacketMap.Add(GameClientPacketType.TurnWest, ParseTurnWest);
            _gamePacketMap.Add(GameClientPacketType.TurnSouth, ParseTurnSouth);
            _gamePacketMap.Add(GameClientPacketType.TurnEast, ParseTurnEast);
            //_gamePacketMap.Add(GameClientPacketType.Packet115, ParsePacket115);
            //_gamePacketMap.Add(GameClientPacketType.Packet116, ParsePacket116);
            //_gamePacketMap.Add(GameClientPacketType.Packet117, ParsePacket117);
            //_gamePacketMap.Add(GameClientPacketType.Packet118, ParsePacket118);
            //_gamePacketMap.Add(GameClientPacketType.Packet119, ParsePacket119);
            //_gamePacketMap.Add(GameClientPacketType.ItemMove, ParseItemMove);
            //_gamePacketMap.Add(GameClientPacketType.ShopLookAt, ParseShopLookAt);
            //_gamePacketMap.Add(GameClientPacketType.ShopBuy, ParseShopBuy);
            //_gamePacketMap.Add(GameClientPacketType.ShopSell, ParseShopSell);
            //_gamePacketMap.Add(GameClientPacketType.ShopClose, ParseShopClose);
            //_gamePacketMap.Add(GameClientPacketType.TradeRequest, ParseTradeRequest);
            //_gamePacketMap.Add(GameClientPacketType.TradeLookAt, ParseTradeLookAt);
            //_gamePacketMap.Add(GameClientPacketType.TradeAccept, ParseTradeAccept);
            //_gamePacketMap.Add(GameClientPacketType.TradeClose, ParseTradeClose);
            //_gamePacketMap.Add(GameClientPacketType.Packet129, ParsePacket129);
            //_gamePacketMap.Add(GameClientPacketType.ItemUse, ParseItemUse);
            //_gamePacketMap.Add(GameClientPacketType.ItemUseOn, ParseItemUseOn);
            //_gamePacketMap.Add(GameClientPacketType.ItemUseOnBattleList, ParseItemUseOnBattleList);
            //_gamePacketMap.Add(GameClientPacketType.ItemRotate, ParseItemRotate);
            //_gamePacketMap.Add(GameClientPacketType.Packet134, ParsePacket134);
            //_gamePacketMap.Add(GameClientPacketType.ContainerClose, ParseContainerClose);
            //_gamePacketMap.Add(GameClientPacketType.ContainerOpenParent, ParseContainerOpenParent);
            //_gamePacketMap.Add(GameClientPacketType.WriteItem, ParseWriteItem);
            //_gamePacketMap.Add(GameClientPacketType.WriteHousePermissions, ParseWriteHousePermissions);
            //_gamePacketMap.Add(GameClientPacketType.Packet139, ParsePacket139);
            //_gamePacketMap.Add(GameClientPacketType.LookAt, ParseLookAt);
            //_gamePacketMap.Add(GameClientPacketType.LookInBattleList, ParseLookInBattleList);
            //_gamePacketMap.Add(GameClientPacketType.JoinAggression, ParseJoinAggression);
            //_gamePacketMap.Add(GameClientPacketType.Packet143, ParsePacket143);
            //_gamePacketMap.Add(GameClientPacketType.Packet144, ParsePacket144);
            //_gamePacketMap.Add(GameClientPacketType.Packet145, ParsePacket145);
            //_gamePacketMap.Add(GameClientPacketType.Packet146, ParsePacket146);
            //_gamePacketMap.Add(GameClientPacketType.Packet147, ParsePacket147);
            //_gamePacketMap.Add(GameClientPacketType.Packet148, ParsePacket148);
            //_gamePacketMap.Add(GameClientPacketType.Packet149, ParsePacket149);
            _gamePacketMap.Add(GameClientPacketType.ChatSpeech, ParseChatSpeech);
            _gamePacketMap.Add(GameClientPacketType.ChannelList, ParseChannelListRequest);
            _gamePacketMap.Add(GameClientPacketType.ChannelOpen, ParseChannelOpen);
            _gamePacketMap.Add(GameClientPacketType.ChannelClose, ParseChannelClose);
            //_gamePacketMap.Add(GameClientPacketType.ChannelPrivateOpen, ParseChannelPrivateOpen);
            //_gamePacketMap.Add(GameClientPacketType.Packet155, ParsePacket155);
            //_gamePacketMap.Add(GameClientPacketType.Packet156, ParsePacket156);
            //_gamePacketMap.Add(GameClientPacketType.Packet157, ParsePacket157);

            // TODO: ChannelType does not contain a value for NPC channel
            // TODO: Implement NPC channel, this will be replaced by normal channel ID but the channel will be listed as NPC
            //_gamePacketMap.Add(GameClientPacketType.ChannelNpcClose, ParseChannelNpcClose);

            //_gamePacketMap.Add(GameClientPacketType.Packet159, ParsePacket159);
            _gamePacketMap.Add(GameClientPacketType.CombatFightMode, ParseCombatFightMode);
            //_gamePacketMap.Add(GameClientPacketType.CombatAttack, ParseCombatAttack);
            //_gamePacketMap.Add(GameClientPacketType.CombatFollow, ParseCombatFollow);
            _gamePacketMap.Add(GameClientPacketType.PartyInvitation, ParsePartyInvitation);
            _gamePacketMap.Add(GameClientPacketType.PartyJoin, ParsePartyJoin);
            _gamePacketMap.Add(GameClientPacketType.PartyRevokeInvitation, ParsePartyRevokeInvitation);
            _gamePacketMap.Add(GameClientPacketType.PartyPassLeadership, ParsePartyPassLeadership);
            _gamePacketMap.Add(GameClientPacketType.PartyLeave, ParsePartyLeave);
            _gamePacketMap.Add(GameClientPacketType.PartySwitchSharedExperience, ParsePartySwitchSharedExperience);
            //_gamePacketMap.Add(GameClientPacketType.Packet169, ParsePacket169);
            //_gamePacketMap.Add(GameClientPacketType.ChannelPrivateCreate, ParseChannelPrivateCreate);
            //_gamePacketMap.Add(GameClientPacketType.ChannelInvite, ParseChannelInvite);
            //_gamePacketMap.Add(GameClientPacketType.ChannelRevokeAccess, ParseChannelRevokeAccess);
            //_gamePacketMap.Add(GameClientPacketType.Packet173, ParsePacket173);
            //_gamePacketMap.Add(GameClientPacketType.Packet174, ParsePacket174);
            //_gamePacketMap.Add(GameClientPacketType.Packet175, ParsePacket175);
            //_gamePacketMap.Add(GameClientPacketType.Packet176, ParsePacket176);
            //_gamePacketMap.Add(GameClientPacketType.Packet177, ParsePacket177);
            //_gamePacketMap.Add(GameClientPacketType.Packet178, ParsePacket178);
            //_gamePacketMap.Add(GameClientPacketType.Packet179, ParsePacket179);
            //_gamePacketMap.Add(GameClientPacketType.Packet180, ParsePacket180);
            //_gamePacketMap.Add(GameClientPacketType.Packet181, ParsePacket181);
            //_gamePacketMap.Add(GameClientPacketType.Packet182, ParsePacket182);
            //_gamePacketMap.Add(GameClientPacketType.Packet183, ParsePacket183);
            //_gamePacketMap.Add(GameClientPacketType.Packet184, ParsePacket184);
            //_gamePacketMap.Add(GameClientPacketType.Packet185, ParsePacket185);
            //_gamePacketMap.Add(GameClientPacketType.Packet186, ParsePacket186);
            //_gamePacketMap.Add(GameClientPacketType.Packet187, ParsePacket187);
            //_gamePacketMap.Add(GameClientPacketType.Packet188, ParsePacket188);
            //_gamePacketMap.Add(GameClientPacketType.Packet189, ParsePacket189);
            //_gamePacketMap.Add(GameClientPacketType.CombatCancelAll, ParseCombatCancelAll);
            //_gamePacketMap.Add(GameClientPacketType.Packet191, ParsePacket191);
            //_gamePacketMap.Add(GameClientPacketType.Packet192, ParsePacket192);
            //_gamePacketMap.Add(GameClientPacketType.Packet193, ParsePacket193);
            //_gamePacketMap.Add(GameClientPacketType.Packet194, ParsePacket194);
            //_gamePacketMap.Add(GameClientPacketType.Packet195, ParsePacket195);
            //_gamePacketMap.Add(GameClientPacketType.Packet196, ParsePacket196);
            //_gamePacketMap.Add(GameClientPacketType.Packet197, ParsePacket197);
            //_gamePacketMap.Add(GameClientPacketType.Packet198, ParsePacket198);
            //_gamePacketMap.Add(GameClientPacketType.Packet199, ParsePacket199);
            //_gamePacketMap.Add(GameClientPacketType.Packet200, ParsePacket200);
            //_gamePacketMap.Add(GameClientPacketType.TileUpdate, ParseTileUpdate);
            //_gamePacketMap.Add(GameClientPacketType.ContainerUpdate, ParseContainerUpdate);
            //_gamePacketMap.Add(GameClientPacketType.BrowseField, ParseBrowseField);
            //_gamePacketMap.Add(GameClientPacketType.ContainerSeekPage, ParseContainerSeekPage);
            //_gamePacketMap.Add(GameClientPacketType.Packet205, ParsePacket205);
            //_gamePacketMap.Add(GameClientPacketType.Packet206, ParsePacket206);
            //_gamePacketMap.Add(GameClientPacketType.Packet207, ParsePacket207);
            //_gamePacketMap.Add(GameClientPacketType.Packet208, ParsePacket208);
            //_gamePacketMap.Add(GameClientPacketType.Packet209, ParsePacket209);
            _gamePacketMap.Add(GameClientPacketType.WindowAppearanceRequest, ParseWindowAppearanceRequest);
            _gamePacketMap.Add(GameClientPacketType.AppearanceChange, ParseAppearanceChange);
            _gamePacketMap.Add(GameClientPacketType.MountToggle, ParseMountToggle);
            //_gamePacketMap.Add(GameClientPacketType.Packet213, ParsePacket213);
            //_gamePacketMap.Add(GameClientPacketType.Packet214, ParsePacket214);
            //_gamePacketMap.Add(GameClientPacketType.Packet215, ParsePacket215);
            //_gamePacketMap.Add(GameClientPacketType.Packet216, ParsePacket216);
            //_gamePacketMap.Add(GameClientPacketType.Packet217, ParsePacket217);
            //_gamePacketMap.Add(GameClientPacketType.Packet218, ParsePacket218);
            //_gamePacketMap.Add(GameClientPacketType.Packet219, ParsePacket219);
            //_gamePacketMap.Add(GameClientPacketType.FriendAdd, ParseFriendAdd);
            //_gamePacketMap.Add(GameClientPacketType.FriendRemove, ParseFriendRemove);
            //_gamePacketMap.Add(GameClientPacketType.FriendEdit, ParseFriendEdit);
            //_gamePacketMap.Add(GameClientPacketType.Packet223, ParsePacket223);
            //_gamePacketMap.Add(GameClientPacketType.Packet224, ParsePacket224);
            //_gamePacketMap.Add(GameClientPacketType.Packet225, ParsePacket225);
            //_gamePacketMap.Add(GameClientPacketType.Packet226, ParsePacket226);
            //_gamePacketMap.Add(GameClientPacketType.Packet227, ParsePacket227);
            //_gamePacketMap.Add(GameClientPacketType.Packet228, ParsePacket228);
            //_gamePacketMap.Add(GameClientPacketType.Packet229, ParsePacket229);
            //_gamePacketMap.Add(GameClientPacketType.ReportBug, ParseReportBug);
            //_gamePacketMap.Add(GameClientPacketType.ThankYou, ParseThankYou);
            //_gamePacketMap.Add(GameClientPacketType.ReportDebugAssert, ParseReportDebugAssert);
            //_gamePacketMap.Add(GameClientPacketType.Packet233, ParsePacket233);
            //_gamePacketMap.Add(GameClientPacketType.Packet234, ParsePacket234);
            //_gamePacketMap.Add(GameClientPacketType.Packet235, ParsePacket235);
            //_gamePacketMap.Add(GameClientPacketType.Packet236, ParsePacket236);
            //_gamePacketMap.Add(GameClientPacketType.Packet237, ParsePacket237);
            //_gamePacketMap.Add(GameClientPacketType.Packet238, ParsePacket238);
            //_gamePacketMap.Add(GameClientPacketType.Packet239, ParsePacket239);
            _gamePacketMap.Add(GameClientPacketType.QuestLogWindowRequest, ParseQuestLogWindowRequest);
            _gamePacketMap.Add(GameClientPacketType.QuestLogQuestLine, ParseQuestLogQuestLine);
            //_gamePacketMap.Add(GameClientPacketType.ReportRuleViolation, ParseReportRuleViolation);
            //_gamePacketMap.Add(GameClientPacketType.ObjectInfoRequest, ParseObjectInfoRequest);
            //_gamePacketMap.Add(GameClientPacketType.MarketLeave, ParseMarketLeave);
            //_gamePacketMap.Add(GameClientPacketType.MarketBrowse, ParseMarketBrowse);
            //_gamePacketMap.Add(GameClientPacketType.MarketCreateOffer, ParseMarketCreateOffer);
            //_gamePacketMap.Add(GameClientPacketType.MarketCancelOffer, ParseMarketCancelOffer);
            //_gamePacketMap.Add(GameClientPacketType.MarketAcceptOffer, ParseMarketAcceptOffer);
            //_gamePacketMap.Add(GameClientPacketType.OfflineTrainingWindowAnswer, ParseOfflineTrainingWindowAnswer);
            //_gamePacketMap.Add(GameClientPacketType.Packet250, ParsePacket250);
            //_gamePacketMap.Add(GameClientPacketType.Packet251, ParsePacket251);
            //_gamePacketMap.Add(GameClientPacketType.Packet252, ParsePacket252);
            //_gamePacketMap.Add(GameClientPacketType.Packet253, ParsePacket253);
            //_gamePacketMap.Add(GameClientPacketType.Packet254, ParsePacket254);
            //_gamePacketMap.Add(GameClientPacketType.Packet255, ParsePacket255);
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
            foreach (ICharacterSpawn spectator in _tileService.GetSpectators(CharacterSpawn.Tile.Position, new Vector3(10, 10, 3)))
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
        ///     Occurs when registering a creature in a tile.
        /// </summary>
        public event EventHandler<RegisteringTileCreatureEventArgs> RegisteringTileCreature;

        /// <summary>
        ///     Occurs when a creature is registered in a tile.
        /// </summary>
        public event EventHandler<RegisteredTileCreatureEventArgs> RegisteredTileCreature;

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

            _tileService.RegisterCreature(creatureSpawn);

            RegisteredTileCreatureEventArgs registeredTileCreatureEventArgs = new RegisteredTileCreatureEventArgs(registeringTileCreatureEventArgs.CreatureSpawn);
            RegisteredTileCreature?.Invoke(this, registeredTileCreatureEventArgs);
        }


        /// <summary>
        ///     Occurs when unregistering a tile creature.
        /// </summary>
        public event EventHandler<UnregisteringTileCreatureEventArgs> UnregisteringTileCreature;

        /// <summary>
        ///     Occurs when unregistered a tile creature.
        /// </summary>
        public event EventHandler<UnregisteredTileCreatureEventArgs> UnregisteredTileCreature;

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

            _tileService.UnregisterCreature(creatureSpawn);

            UnregisteredTileCreatureEventArgs unregisteredTileCreatureEventArgs = new UnregisteredTileCreatureEventArgs(unregisteringTileCreatureEventArgs.CreatureSpawn);
            UnregisteredTileCreature?.Invoke(this, unregisteredTileCreatureEventArgs);
        }

        /// <summary>
        ///     Occurs when the client gets authenticated.
        /// </summary>
        public event EventHandler<AuthenticationEventArgs> Authenticate;

        /// <summary>
        ///     Occurs when the client is logging in.
        /// </summary>
        public event EventHandler<LoggingInEventArgs> LoggingIn;

        /// <summary>
        ///     Occurs when the client is logged in.
        /// </summary>
        public event EventHandler<LoggedInEventArgs> LoggedIn;

        /// <summary>
        ///     Occurs when the client is disconnected.
        /// </summary>
        public event EventHandler<DisconnectedEventArgs> Disconnected;

        /// <summary>
        ///     Occurs when the server's online time is requested.
        /// </summary>
        public event EventHandler<OnlineTimeEventArgs> RequestOnlineTime;

        /// <summary>
        ///     Called when [message received].
        /// </summary>
        /// <param name="result">The result.</param>
        public void OnMessageReceived(IAsyncResult result)
        {
            TcpListener gameListener = (TcpListener) result.AsyncState;
            Socket = gameListener.EndAcceptSocket(result);
            Stream = new NetworkStream(Socket);

            OnlineTimeEventArgs onlineTimeEventArgs = new OnlineTimeEventArgs(TimeSpan.Zero);
            RequestOnlineTime?.Invoke(this, onlineTimeEventArgs);

            SendConnectionPacket(onlineTimeEventArgs.TimeSpan);
            Stream.BeginRead(IncomingMessage.Buffer, 0, 2, OnClientReadHandshakeMessage, null);
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

        /// <inheritdoc />
        /// <summary>
        ///     Closes this instance.
        /// </summary>
        public override void Close()
        {
            IsRemoved = true;
            base.Close();
        }

        /// <summary>
        ///     Parses the login packet.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <exception cref="InvalidRsaException"></exception>
        /// <exception cref="AuthenticationFailedException"></exception>
        private void ParseLoginPacket(NetworkMessage message)
        {
            LoginPacket loginPacket = LoginPacket.Parse(message);
            if (!loginPacket.IsRsaValid)
                throw new InvalidRsaException();

            AuthenticationEventArgs authenticationEventArgs = new AuthenticationEventArgs(loginPacket.Username, loginPacket.Password);
            Authenticate?.Invoke(this, authenticationEventArgs);
            if (authenticationEventArgs.Cancel)
                throw new AuthenticationFailedException();

            Xtea = loginPacket.Xtea;
            LoggingInEventArgs loggingInEventArgs = new LoggingInEventArgs(loginPacket.CharacterName);
            LoggingIn?.Invoke(this, loggingInEventArgs);
            if (loggingInEventArgs.Cancel)
                throw new ConnectionRefusedException();

            SendInitialPacket();
            CharacterSpawn.Connection = this;
            CharacterSpawn.LastLogin.Time = DateTime.UtcNow;
            CharacterSpawn.LastLogin.IpAddress = IpAddress;

            LoggedInEventArgs loggedInEventArgs = new LoggedInEventArgs(CharacterSpawn);
            LoggedIn?.Invoke(this, loggedInEventArgs);
        }

        /// <summary>
        ///     Sends the initial packet.
        /// </summary>
        private void SendInitialPacket()
        {
            NetworkMessage message = new NetworkMessage(Xtea);

            // TODO: Permission to report bugs
            SelfAppearPacket.Add(message, CharacterSpawn, true);
            FriendsPacket.Add(message, CharacterSpawn.Account.Friends);
            PendingStateEnteredPacket.Add(message);
            Send(message);
        }

        /// <summary>
        ///     Sends the connection packet.
        /// </summary>
        /// <param name="onlineTime">The online time.</param>
        private void SendConnectionPacket(TimeSpan onlineTime)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            GameServerConnectPacket.Add(message, onlineTime);
            Send(message, false);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        ///     Occurs when the client requests a ping back.
        /// </summary>
        public event EventHandler<EventArgs> RequestPingBack;

        /// <summary>
        ///     Parses the ping back.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParsePingBack(NetworkMessage message)
        {
            RequestPingBack?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Parses the ping.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParsePing(NetworkMessage message)
        {
            // TODO: Game.CharacterReceivePing(CharacterSpawn);
        }

        /// <summary>
        ///     Occurs when the client is logging out.
        /// </summary>
        public event EventHandler<LoggingOutEventArgs> LoggingOut;

        /// <summary>
        ///     Parses the logout.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseLogout(NetworkMessage message)
        {
            LoggingOutEventArgs loggingOutEventArgs = new LoggingOutEventArgs(CharacterSpawn);
            LoggingOut?.Invoke(this, loggingOutEventArgs);
            if (loggingOutEventArgs.Cancel)
                return;

            CharacterSpawn.Connection.Disconnect(string.Empty);

            LoggedOutEventArgs loggedOutEventArgs = new LoggedOutEventArgs(CharacterSpawn);
            LoggedOut?.Invoke(this, loggedOutEventArgs);
        }

        /// <summary>
        ///     Occurs when the client is logged out.
        /// </summary>
        public event EventHandler<LoggedOutEventArgs> LoggedOut;

        /// <summary>
        ///     Occurs when the client is entering game.
        /// </summary>
        public event EventHandler<EnteringGameEventArgs> EnteringGame;

        /// <summary>
        ///     Parses the enter game.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseEnterGame(NetworkMessage message)
        {
            // TODO: Game.CharacterEnterGame(CharacterSpawn);
            EnteringGameEventArgs enteringGameEventArgs = new EnteringGameEventArgs(CharacterSpawn);
            EnteringGame?.Invoke(this, enteringGameEventArgs);
            if (enteringGameEventArgs.Cancel)
                return;

            // TODO: Range should probably be accessible as a constant through the server
            foreach (ICharacterSpawn spectator in _tileService.GetSpectators(CharacterSpawn.Tile.Position, new Vector3(10, 10, 3)))
                spectator.Connection.SendEffect(CharacterSpawn.Tile.Position, Effect.Teleport);

            // TODO: DateTime.UtcNow should be server time to avoid such operation consuming resources
            LightInfoEventArgs lightInfoEventArgs = new LightInfoEventArgs();
            RequestLightInfo?.Invoke(this, lightInfoEventArgs);

            CharacterSpawn.Connection.SendEnterGame(lightInfoEventArgs.LightInfo, CharacterSpawn.Account.PremiumExpirationDate - DateTime.UtcNow, CharacterSpawn);

            EnteredGameEventArgs enteredGameEventArgs = new EnteredGameEventArgs(CharacterSpawn);
            EnteredGame?.Invoke(this, enteredGameEventArgs);
        }

        /// <summary>
        ///     Occurs when the server's light info is requested.
        /// </summary>
        public event EventHandler<LightInfoEventArgs> RequestLightInfo;

        /// <summary>
        ///     Occurs when the client has entered game.
        /// </summary>
        public event EventHandler<EnteredGameEventArgs> EnteredGame;

        /// <summary>
        ///     Occurs when the fight mode is changing.
        /// </summary>
        public event EventHandler<FightModeChangingEventArgs> FightModeChanging;

        /// <summary>
        ///     Occurs when the fight mode changed.
        /// </summary>
        public event EventHandler<FightModeChangedEventArgs> FightModeChanged;

        /// <summary>
        ///     Parses the fight mode.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseCombatFightMode(NetworkMessage message)
        {
            FightModePacket packet = FightModePacket.Parse(message);
            FightModeChangingEventArgs fightModeChangingEventArgs = new FightModeChangingEventArgs(CharacterSpawn, packet.BattleStance, packet.ChaseMode, packet.SafeMode);
            FightModeChanging?.Invoke(this, fightModeChangingEventArgs);
            if (fightModeChangingEventArgs.Cancel)
                return;

            CharacterSpawn.BattleStance = fightModeChangingEventArgs.BattleStance;

            // TODO: Chase mode should include new options (e.g.: diagonal, 3 sqm, 5 sqm, 7 sqm, etc. check XenoBot options)
            CharacterSpawn.ChaseMode = fightModeChangingEventArgs.ChaseMode;

            // TODO: Safe mode should include new options (e.g.: ??? Check XenoBot options)
            CharacterSpawn.SafeMode = fightModeChangingEventArgs.SafeMode;

            FightModeChangedEventArgs fightModeChangedEventArgs = new FightModeChangedEventArgs(CharacterSpawn, fightModeChangingEventArgs.BattleStance, fightModeChangingEventArgs.ChaseMode, fightModeChangingEventArgs.SafeMode);
            FightModeChanged?.Invoke(this, fightModeChangedEventArgs);
        }

        /// <summary>
        ///     Parses the attack creature.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseAttackCreature(NetworkMessage message)
        {
            AttackCreaturePacket packet = AttackCreaturePacket.Parse(message);
            // TODO: Game.CharacterChangeAttackedCreature(CharacterSpawn, packet.CreatureId);
        }

        /// <summary>
        ///     Parses the follow creature.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseFollowCreature(NetworkMessage message)
        {
            FollowCreaturePacket packet = FollowCreaturePacket.Parse(message);
            // TODO: Game.CharacterChangeFollowedCreature(CharacterSpawn, packet.CreatureId);
        }

        /// <summary>
        ///     Parses the cancel all combat.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseCancelAllCombat(NetworkMessage message)
        {
            // TODO: Game.CharacterCancelAll(CharacterSpawn);
        }

        /// <summary>
        ///     Occurs when the client is adding a friend.
        /// </summary>
        public event EventHandler<AddingFriendEventArgs> AddingFriend;

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
        ///     Parses the add friend.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseAddFriend(NetworkMessage message)
        {
            FriendAddPacket packet = FriendAddPacket.Parse(message);
            AddingFriendEventArgs addingFriendEventArgs = new AddingFriendEventArgs(CharacterSpawn, packet.FriendName);
            AddingFriend?.Invoke(this, addingFriendEventArgs);
            if (addingFriendEventArgs.Cancel)
                return;

            IFriend friend = new Friend
            {
                Character = addingFriendEventArgs.Character,
                CharacterId = addingFriendEventArgs.Character.Id,
                Description = string.Empty,
                Icon = 0,
                NotifyOnLogin = false
            };
            CharacterSpawn.Account.Friends.Add(friend);
            CharacterSpawn.Connection.SendFriend(friend);

            AddedFriendEventArgs addedFriendEventArgs = new AddedFriendEventArgs(CharacterSpawn, friend);
            AddedFriend?.Invoke(this, addedFriendEventArgs);
        }

        /// <summary>
        ///     Occurs when the client added a friend.
        /// </summary>
        public event EventHandler<AddedFriendEventArgs> AddedFriend;

        /// <summary>
        ///     Parses the remove friend.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseRemoveFriend(NetworkMessage message)
        {
            FriendRemovePacket packet = FriendRemovePacket.Parse(message);

            IFriend friend = CharacterSpawn.Account.Friends.FirstOrDefault(s => s.Id == packet.FriendId);
            RemovingFriendEventArgs removingFriendEventArgs = new RemovingFriendEventArgs(CharacterSpawn, friend, packet.FriendId);
            RemovingFriend?.Invoke(this, removingFriendEventArgs);
            if (removingFriendEventArgs.Cancel)
                return;

            CharacterSpawn.Account.Friends.Remove(friend);
            _chatService.RemoveFriend(CharacterSpawn.Account, friend);

            RemovedFriendEventArgs removedFriendEventArgs = new RemovedFriendEventArgs(CharacterSpawn, friend);
            RemovedFriend?.Invoke(this, removedFriendEventArgs);
        }

        /// <summary>
        ///     Occurs when the client removed a friend.
        /// </summary>
        public event EventHandler<RemovedFriendEventArgs> RemovedFriend;

        /// <summary>
        ///     Occurs when the client is removing a friend.
        /// </summary>
        public event EventHandler<RemovingFriendEventArgs> RemovingFriend;

        /// <summary>
        ///     Occurs when the client is editing a friend.
        /// </summary>
        public event EventHandler<EditingFriendEventArgs> EditingFriend;

        /// <summary>
        ///     Parses the edit friend.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseEditFriend(NetworkMessage message)
        {
            FriendEditPacket packet = FriendEditPacket.Parse(message);

            IFriend friend = CharacterSpawn.Account.Friends.FirstOrDefault(s => s.Id == packet.FriendId);
            EditingFriendEventArgs editingFriendEventArgs = new EditingFriendEventArgs(CharacterSpawn, friend, packet.FriendId, packet.Description, packet.Icon, packet.NotifyOnLogin);
            EditingFriend?.Invoke(this, editingFriendEventArgs);
            if (editingFriendEventArgs.Cancel)
                return;

            friend.Description = editingFriendEventArgs.Description;
            friend.Icon = editingFriendEventArgs.Icon;
            friend.NotifyOnLogin = editingFriendEventArgs.NotifyOnLogin;

            EditedFriendEventArgs editedFriendEventArgs = new EditedFriendEventArgs(CharacterSpawn, friend);
            EditedFriend?.Invoke(this, editedFriendEventArgs);
        }

        /// <summary>
        ///     Occurs when the client edited a friend.
        /// </summary>
        public event EventHandler<EditedFriendEventArgs> EditedFriend;

        /// <summary>
        ///     Occurs when a creature is talking.
        /// </summary>
        public event EventHandler<CreatureTalkingEventArgs> CreatureTalking;

        /// <summary>
        ///     Parses the chat speech.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ParseChatSpeech(NetworkMessage message)
        {
            CharacterSpeechPacket packet = CharacterSpeechPacket.Parse(message);
            CreatureTalkingEventArgs creatureTalkingEventArgs = new CreatureTalkingEventArgs(CharacterSpawn, packet.SpeechType, packet.Receiver, packet.ChannelId, packet.Message);
            CreatureTalking?.Invoke(this, creatureTalkingEventArgs);
            if (creatureTalkingEventArgs.Cancel)
                return;

            // TODO: Individual/base channel muting mechanism
            // TODO: Casting spells
            // TODO: NPC channel
            if (_commandService.Execute(CharacterSpawn, packet.Message))
            {
                IEnumerable<IChannel> channels = _chatService.GetChannelsBySpeech(packet.SpeechType, packet.ChannelId, packet.Receiver);
                foreach (IChannel channel in channels)
                    channel.Post(CharacterSpawn, creatureTalkingEventArgs.Message, creatureTalkingEventArgs.SpeechType);
            }

            CreatureTalkedEventArgs creatureTalkedEventArgs = new CreatureTalkedEventArgs(CharacterSpawn, creatureTalkingEventArgs.SpeechType, creatureTalkingEventArgs.Receiver, creatureTalkingEventArgs.ChannelId, creatureTalkingEventArgs.Message);
            CreatureTalked?.Invoke(this, creatureTalkedEventArgs);
        }

        /// <summary>
        ///     Occurs when a creature talked.
        /// </summary>
        public event EventHandler<CreatureTalkedEventArgs> CreatureTalked;

        /// <summary>
        ///     Parses the open private channel.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseOpenPrivateChannel(NetworkMessage message)
        {
            ChannelPrivateOpenPacket packet = ChannelPrivateOpenPacket.Parse(message);
            IEnumerable<IPrivateChannel> channels = _chatService.GetPrivateChannelsByOwnerName(packet.ReceiverName);
            foreach (IPrivateChannel channel in channels)
            {
                OpeningChannelEventArgs openingChannelEventArgs = new OpeningChannelEventArgs(CharacterSpawn, channel);
                OpeningChannel?.Invoke(this, openingChannelEventArgs);
                if (openingChannelEventArgs.Cancel)
                    return;

                channel.Register(CharacterSpawn);
                CharacterSpawn.Connection.SendOpenChannel(channel);

                OpenedChannelEventArgs openedChannelEventArgs = new OpenedChannelEventArgs(CharacterSpawn, openingChannelEventArgs.Channel);
                OpenedChannel?.Invoke(this, openedChannelEventArgs);
            }
        }

        /// <summary>
        ///     Occurs when the client is closing a channel.
        /// </summary>
        public event EventHandler<ClosingChannelEventArgs> ClosingChannel;

        /// <summary>
        ///     Occurs when the client closed a channel.
        /// </summary>
        public event EventHandler<ClosedChannelEventArgs> ClosedChannel;

        /// <summary>
        ///     Parses the channel close.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseChannelClose(NetworkMessage message)
        {
            ChannelClosePacket packet = ChannelClosePacket.Parse(message);

            // TODO: Close channel with Id (e.g.: Private, Guild, Party, etc.)
            IChannel channel = _chatService.GetChannelByType(packet.ChannelType);
            ClosingChannelEventArgs closingChannelEventArgs = new ClosingChannelEventArgs(CharacterSpawn, channel);
            ClosingChannel?.Invoke(this, closingChannelEventArgs);
            if (closingChannelEventArgs.Cancel)
                return;

            channel.Unregister(CharacterSpawn);
            CharacterSpawn.Connection.SendCloseChannel(packet.ChannelType);

            ClosedChannelEventArgs closedChannelEventArgs = new ClosedChannelEventArgs(CharacterSpawn, closingChannelEventArgs.Channel);
            ClosedChannel?.Invoke(this, closedChannelEventArgs);
        }

        /// <summary>
        ///     Parses the channel open.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseChannelOpen(NetworkMessage message)
        {
            ChannelOpenPacket packet = ChannelOpenPacket.Parse(message);
            IChannel channel = _chatService.GetChannelByType(packet.ChannelType);
            OpeningChannelEventArgs openingChannelEventArgs = new OpeningChannelEventArgs(CharacterSpawn, channel);
            OpeningChannel?.Invoke(this, openingChannelEventArgs);
            if (openingChannelEventArgs.Cancel)
                return;

            channel.Register(CharacterSpawn);
            CharacterSpawn.Connection.SendOpenChannel(channel);

            OpenedChannelEventArgs openedChannelEventArgs = new OpenedChannelEventArgs(CharacterSpawn, openingChannelEventArgs.Channel);
            OpenedChannel?.Invoke(this, openedChannelEventArgs);
        }

        /// <summary>
        ///     Occurs when the client opened a channel.
        /// </summary>
        public event EventHandler<OpenedChannelEventArgs> OpenedChannel;

        /// <summary>
        ///     Occurs when the client is opening a channel.
        /// </summary>
        public event EventHandler<OpeningChannelEventArgs> OpeningChannel;

        /// <summary>
        ///     Occurs when sending the channel list.
        /// </summary>
        public event EventHandler<SendingChannelListEventArgs> SendingChannelList;

        /// <summary>
        ///     Occurs when sent the channel list.
        /// </summary>
        public event EventHandler<SentChannelListEventArgs> SentChannelList;

        /// <summary>
        ///     Parses the channel list request.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseChannelListRequest(NetworkMessage message)
        {
            SendingChannelListEventArgs sendingChannelListEventArgs = new SendingChannelListEventArgs(CharacterSpawn);
            SendingChannelList?.Invoke(this, sendingChannelListEventArgs);
            if (sendingChannelListEventArgs.Cancel)
                return;

            CharacterSpawn.Connection.SendChannelList(_chatService.GetChannelsByCharacterSpawnId(CharacterSpawn.Id).Where(s => s is IQueryableChannel).ToList());

            SentChannelListEventArgs sentChannelListEventArgs = new SentChannelListEventArgs(CharacterSpawn);
            SentChannelList?.Invoke(this, sentChannelListEventArgs);
        }

        /// <summary>
        ///     Occurs when a creature is turning.
        /// </summary>
        public event EventHandler<CreatureTurningEventArgs> CreatureTurning;

        /// <summary>
        ///     Occurs when a creature turned.
        /// </summary>
        public event EventHandler<CreatureTurnedEventArgs> CreatureTurned;

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
            foreach (ICharacterSpawn spectator in _tileService.GetSpectators(CharacterSpawn.Tile.Position, new Vector3(10, 10, 3)))
                spectator.Connection.SendCreatureTurn(CharacterSpawn);

            CreatureTurnedEventArgs creatureTurnedEventArgs = new CreatureTurnedEventArgs(CharacterSpawn);
            CreatureTurned?.Invoke(this, creatureTurnedEventArgs);
        }

        /// <summary>
        ///     Parses the turn south.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseTurnSouth(NetworkMessage message)
        {
            TurnCharacter(Direction.South);
        }

        /// <summary>
        ///     Parses the turn north.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseTurnNorth(NetworkMessage message)
        {
            TurnCharacter(Direction.North);
        }

        /// <summary>
        ///     Parses the turn east.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseTurnEast(NetworkMessage message)
        {
            TurnCharacter(Direction.East);
        }

        /// <summary>
        ///     Parses the turn west.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseTurnWest(NetworkMessage message)
        {
            TurnCharacter(Direction.West);
        }

        /// <summary>
        ///     Occurs when a creature is moving.
        /// </summary>
        public event EventHandler<MovingCreatureEventArgs> MovingCreature;

        /// <summary>
        ///     Occurs when a creature moved.
        /// </summary>
        public event EventHandler<CreatureMovedEventArgs> CreatureMoved;

        /// <summary>
        ///     Occurs when a character is walking.
        /// </summary>
        public event EventHandler<CharacterWalkingEventArgs> CharacterWalking;

        /// <summary>
        ///     Occurs when a character walked.
        /// </summary>
        public event EventHandler<CharacterWalkedEventArgs> CharacterWalked;

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
        ///     Occurs when the client is creating a channel.
        /// </summary>
        public event EventHandler<CreatingChannelEventArgs> CreatingChannel;

        /// <summary>
        ///     Occurs when the client created a channel.
        /// </summary>
        public event EventHandler<CreatedChannelEventArgs> CreatedChannel;

        /// <summary>
        ///     Occurs when the client is inviting.
        /// </summary>
        public event EventHandler<InvitingEventArgs> Inviting;

        /// <summary>
        ///     Occurs when the client has invited.
        /// </summary>
        public event EventHandler<InvitedEventArgs> Invited;

        /// <summary>
        ///     Occurs when the client is uninviting.
        /// </summary>
        public event EventHandler<UninvitingEventArgs> Uninviting;

        /// <summary>
        ///     Occurs when the client has uninvited.
        /// </summary>
        public event EventHandler<UninvitedEventArgs> Uninvited;

        /// <summary>
        ///     Parses the create private channel.
        /// </summary>
        private void ParseCreatePrivateChannel()
        {
            IPrivateChannel channel = _chatService.GetPrivateChannelByOwnerId(CharacterSpawn.Id);
            CreatingChannelEventArgs creatingChannelEventArgs = new CreatingChannelEventArgs(CharacterSpawn, channel);
            CreatingChannel?.Invoke(this, creatingChannelEventArgs);
            if (creatingChannelEventArgs.Cancel)
                return;

            CharacterSpawn.Connection.SendOpenChannel(creatingChannelEventArgs.Channel);

            CreatedChannelEventArgs createdChannelEventArgs = new CreatedChannelEventArgs(CharacterSpawn, creatingChannelEventArgs.Channel);
            CreatedChannel?.Invoke(this, createdChannelEventArgs);
        }

        /// <summary>
        ///     Parses the item use on packet.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseItemUseOnPacket(NetworkMessage message)
        {
            ItemUseOnPacket packet = ItemUseOnPacket.Parse(message);
            // TODO: Game.CharacterItemUseOn(CharacterSpawn, packet.FromPosition, packet.FromStackPosition, packet.FromSpriteId, packet.ToPosition, packet.ToStackPosition, packet.ToSpriteId);
        }

        /// <summary>
        ///     Occurs when using an item.
        /// </summary>
        public event EventHandler<UsingItemEventArgs> UsingItem;

        /// <summary>
        ///     Occurs when used an item.
        /// </summary>
        public event EventHandler<UsedItemEventArgs> UsedItem;

        /// <summary>
        ///     Parses the item use packet.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseItemUsePacket(NetworkMessage message)
        {
            ItemUsePacket packet = ItemUsePacket.Parse(message);
            UsingItemEventArgs usingItemEventArgs = new UsingItemEventArgs(CharacterSpawn, packet.Position, packet.StackPosition, packet.Index, packet.SpriteId);
            UsingItem?.Invoke(this, usingItemEventArgs);
            if (usingItemEventArgs.Cancel)
                return;

            // TODO: Game.CharacterItemUse(CharacterSpawn, packet.Position, packet.StackPosition, packet.Index, packet.SpriteId);
            // TODO: Handle hotkey

            UsedItemEventArgs usedItemEventArgs = new UsedItemEventArgs(usingItemEventArgs.CharacterSpawn, usingItemEventArgs.Position, usingItemEventArgs.StackPosition, usingItemEventArgs.Index, usingItemEventArgs.SpriteId);
            UsedItem?.Invoke(this, usedItemEventArgs);
        }

        /// <summary>
        ///     Parses the move north.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseMoveNorth(NetworkMessage message)
        {
            WalkCharacter(Direction.North);
        }

        /// <summary>
        ///     Parses the move south.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseMoveSouth(NetworkMessage message)
        {
            WalkCharacter(Direction.South);
        }

        /// <summary>
        ///     Parses the move east.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseMoveEast(NetworkMessage message)
        {
            WalkCharacter(Direction.East);
        }

        /// <summary>
        ///     Parses the move west.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseMoveWest(NetworkMessage message)
        {
            WalkCharacter(Direction.West);
        }

        /// <summary>
        ///     Parses the move north east.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseMoveNorthEast(NetworkMessage message)
        {
            WalkCharacter(Direction.NorthEast);
        }

        /// <summary>
        ///     Parses the move north west.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseMoveNorthWest(NetworkMessage message)
        {
            WalkCharacter(Direction.NorthWest);
        }

        /// <summary>
        ///     Parses the move south east.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseMoveSouthEast(NetworkMessage message)
        {
            WalkCharacter(Direction.SouthEast);
        }

        /// <summary>
        ///     Parses the move south west.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseMoveSouthWest(NetworkMessage message)
        {
            WalkCharacter(Direction.SouthWest);
        }

        /// <summary>
        ///     Occurs when automatic walking.
        /// </summary>
        public event EventHandler<AutoWalkingEventArgs> AutoWalking;

        /// <summary>
        ///     Occurs when automatic walked.
        /// </summary>
        public event EventHandler<AutoWalkedEventArgs> AutoWalked;

        /// <summary>
        ///     Parses the automatic walk.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseAutoWalk(NetworkMessage message)
        {
            AutoWalkPacket packet = AutoWalkPacket.Parse(message);
            AutoWalkingEventArgs autoWalkingEventArgs = new AutoWalkingEventArgs(CharacterSpawn, packet.Directions);
            AutoWalking?.Invoke(this, autoWalkingEventArgs);
            if (autoWalkingEventArgs.Cancel)
                return;

            WalkCharacter(CharacterSpawn, autoWalkingEventArgs.Directions);

            AutoWalkedEventArgs autoWalkedEventArgs = new AutoWalkedEventArgs(CharacterSpawn, autoWalkingEventArgs.Directions);
            AutoWalked?.Invoke(this, autoWalkedEventArgs);
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

        /// <summary>
        ///     Parses the party switch shared experience.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParsePartySwitchSharedExperience(NetworkMessage message)
        {
            PartySwitchSharedExperiencePacket packet = PartySwitchSharedExperiencePacket.Parse(message);
            // TODO: Game.PartySwitchSharedExperience(CharacterSpawn, packet.IsActive);
        }

        /// <summary>
        ///     Parses the party leave.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParsePartyLeave(NetworkMessage message)
        {
            // TODO: Game.PartyLeave(CharacterSpawn);
        }

        /// <summary>
        ///     Parses the party pass leadership.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParsePartyPassLeadership(NetworkMessage message)
        {
            PartyPassLeadershipPacket packet = PartyPassLeadershipPacket.Parse(message);
            // TODO: Game.PartyPassLeadership(CharacterSpawn, (int) packet.TargetCharacterId);
        }

        /// <summary>
        ///     Parses the party join.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParsePartyJoin(NetworkMessage message)
        {
            PartyJoinPacket packet = PartyJoinPacket.Parse(message);
            // TODO: Game.PartyJoin(CharacterSpawn, (int) packet.TargetCharacterId);
        }

        /// <summary>
        ///     Parses the party revoke invitation.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParsePartyRevokeInvitation(NetworkMessage message)
        {
            PartyRevokeInvitationPacket packet = PartyRevokeInvitationPacket.Parse(message);
            // TODO: Game.PartyRevokeInvitation(CharacterSpawn, (int) packet.TargetCharacterId);
        }

        /// <summary>
        ///     Parses the party invitation.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParsePartyInvitation(NetworkMessage message)
        {
            PartyInvitationPacket packet = PartyInvitationPacket.Parse(message);
            // TODO: Game.PartyInvite(CharacterSpawn, (int) packet.TargetCharacterId);
        }

        /// <summary>
        ///     Parses the quest log quest line.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseQuestLogQuestLine(NetworkMessage message)
        {
            QuestLogQuestLinePacket packet = QuestLogQuestLinePacket.Parse(message);
            // TODO: Game.QuestLogQuestLine(CharacterSpawn, packet.QuestId);
        }

        /// <summary>
        ///     Parses the quest log window request.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseQuestLogWindowRequest(NetworkMessage message)
        {
            // TODO: Game.CharacterQuestLogWindowRequest(CharacterSpawn);
        }

        /// <summary>
        ///     Parses the appearance change.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseAppearanceChange(NetworkMessage message)
        {
            AppearanceChangePacket packet = AppearanceChangePacket.Parse(message, _outfitService, _mountService);
            RequestingAppearanceChangeEventArgs requestingAppearanceChangeEventArgs = new RequestingAppearanceChangeEventArgs(CharacterSpawn, packet.Outfit, packet.Mount);
            RequestingAppearanceChange?.Invoke(this, requestingAppearanceChangeEventArgs);
            if (requestingAppearanceChangeEventArgs.Cancel)
                return;

            CharacterSpawn.Outfit = packet.Outfit;
            CharacterSpawn.Mount = packet.Mount;

            // TODO: The range of spectatorship should probably be set once and be available through the server
            if (!CharacterSpawn.IsInvisible)
            {
                foreach (ICharacterSpawn spectator in _tileService.GetSpectators(CharacterSpawn.Tile.Position, new Vector3(10, 10, 3)))
                    spectator.Connection.SendChangeCreatureAppearance(CharacterSpawn);
            }

            RequestedAppearanceChangeEventArgs requestedAppearanceChangeEventArgs = new RequestedAppearanceChangeEventArgs(CharacterSpawn);
            RequestedAppearanceChange?.Invoke(this, requestedAppearanceChangeEventArgs);
        }

        /// <summary>
        ///     Occurs when the client requested an appearance change.
        /// </summary>
        public event EventHandler<RequestedAppearanceChangeEventArgs> RequestedAppearanceChange;

        /// <summary>
        ///     Occurs when the client is requesting an appearance change.
        /// </summary>
        public event EventHandler<RequestingAppearanceChangeEventArgs> RequestingAppearanceChange;

        /// <summary>
        ///     Occurs when the client is toggling mount.
        /// </summary>
        public event EventHandler<TogglingMountEventArgs> TogglingMount;

        /// <summary>
        ///     Parses the mount toggle.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseMountToggle(NetworkMessage message)
        {
            MountTogglePacket packet = MountTogglePacket.Parse(message);
            TogglingMountEventArgs togglingMountEventArgs = new TogglingMountEventArgs(CharacterSpawn, packet.IsRiding);
            TogglingMount?.Invoke(this, togglingMountEventArgs);
            if (togglingMountEventArgs.Cancel)
                return;

            if (!packet.IsRiding)
            {
                if (CharacterSpawn.IsRiding)
                    CharacterSpawn.Speed.BonusSpeed -= CharacterSpawn.Mount.Speed;
            }
            else
            {
                if (CharacterSpawn.Mount == null)
                {
                    CharacterSpawn.Connection.SendAppearanceWindow();
                    return;
                }

                if (!CharacterSpawn.IsRiding)
                    CharacterSpawn.Speed.BonusSpeed += CharacterSpawn.Mount.Speed;
            }

            CharacterSpawn.IsRiding = packet.IsRiding;

            // TODO: The range of spectatorship should probably be set once and be available through the server
            foreach (ICharacterSpawn spectator in _tileService.GetSpectators(CharacterSpawn.Tile.Position, new Vector3(10, 10, 3)))
            {
                spectator.Connection.SendChangeCreatureSpeed(CharacterSpawn);

                if (!CharacterSpawn.IsInvisible)
                    spectator.Connection.SendChangeCreatureAppearance(CharacterSpawn);
            }

            ToggledMountEventArgs toggledMountEventArgs = new ToggledMountEventArgs(CharacterSpawn);
            ToggledMount?.Invoke(this, toggledMountEventArgs);
        }

        /// <summary>
        ///     Occurs when the client toggled mount.
        /// </summary>
        public event EventHandler<ToggledMountEventArgs> ToggledMount;

        /// <summary>
        ///     Requests the appearance window.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseWindowAppearanceRequest(NetworkMessage message)
        {
            RequestingAppearanceWindowEventArgs requestingAppearanceWindowEventArgs = new RequestingAppearanceWindowEventArgs(CharacterSpawn);
            RequestingAppearanceWindow?.Invoke(this, requestingAppearanceWindowEventArgs);
            if (requestingAppearanceWindowEventArgs.Cancel)
                return;

            CharacterSpawn.Connection.SendAppearanceWindow();

            RequestedAppearanceWindowEventArgs requestedAppearanceWindowEventArgs = new RequestedAppearanceWindowEventArgs(CharacterSpawn);
            RequestedAppearanceWindow?.Invoke(this, requestedAppearanceWindowEventArgs);
        }

        /// <summary>
        ///     Occurs when the client requested the appearance window.
        /// </summary>
        public event EventHandler<RequestedAppearanceWindowEventArgs> RequestedAppearanceWindow;

        /// <summary>
        ///     Occurs when the client is requesting the appearance window.
        /// </summary>
        public event EventHandler<RequestingAppearanceWindowEventArgs> RequestingAppearanceWindow;

        /// <summary>
        ///     Parses the battle list look at.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseBattleListLookAt(NetworkMessage message)
        {
            BattleListLookPacket packet = BattleListLookPacket.Parse(message);
            // TODO: Game.CharacterBattleListLookAt(CharacterSpawn, (int) packet.CreatureId);
        }

        /// <summary>
        ///     Sends the self conditions.
        /// </summary>
        public void SendSelfConditions()
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            SelfSpecialConditionsPacket.Add(message, CharacterSpawn.Conditions);
            Send(message);
        }

        /// <summary>
        ///     Sends the private message.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="type">The type.</param>
        /// <param name="text">The text.</param>
        public void SendPrivateMessage(CharacterSpawn characterSpawn, SpeechType type, string text)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            // TODO: static uint statementId = 0x00 and statementId++; (Logging messages?)
            PrivateMessagePacket.Add(message, characterSpawn, type, 0x00, text);
            Send(message);
        }

        /// <summary>
        ///     Sends the channel event.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="channelName">Name of the channel.</param>
        /// <param name="channelEventType">Type of the channel event.</param>
        public void SendChannelEvent(int channelId, string channelName, ChannelEventType channelEventType)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            ChannelEventPacket.Add(message, (ushort) channelId, channelName, channelEventType);
            Send(message);
        }

        /// <summary>
        ///     Sends the open private channel.
        /// </summary>
        /// <param name="receiverName">Name of the receiver.</param>
        public void SendOpenPrivateChannel(string receiverName)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            ChannelPrivateOpenPacket.Add(message, receiverName);
            Send(message);
        }

        /// <summary>
        ///     Sends the quest log window.
        /// </summary>
        public void SendQuestLogWindow()
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            // TODO: Sort quests with OrderBy(s => s.Quest.Name)
            QuestLogWindowPacket.Add(message, CharacterSpawn.Quests);
            Send(message);
        }

        /// <summary>
        ///     Sends the quest log quest line.
        /// </summary>
        /// <param name="questId">The quest identifier.</param>
        /// <param name="missions">The missions.</param>
        public void SendQuestLogQuestLine(ushort questId, ICollection<MissionInfo> missions)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            QuestLogQuestLinePacket.Add(message, questId, missions);
            Send(message);
        }

        /// <summary>
        ///     Sends the character shield.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="partyShield">The party shield.</param>
        public void SendCharacterShield(CharacterSpawn characterSpawn, PartyShield partyShield)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            CharacterShieldPacket.Add(message, characterSpawn, partyShield);
            Send(message);
        }

        /// <summary>
        ///     Sends the friend logout.
        /// </summary>
        /// <param name="friendId">The friend identifier.</param>
        public void SendFriendLogout(uint friendId)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            FriendStatusPacket.Add(message, friendId, SessionStatus.Offline);
            Send(message);
        }

        /// <summary>
        ///     Sends the add item to tile.
        /// </summary>
        /// <param name="itemSpawn">The item spawn.</param>
        /// <param name="position">The position.</param>
        /// <param name="stackPosition">The stack position.</param>
        public void SendAddItemToTile(ItemSpawn itemSpawn, Vector3 position, byte stackPosition)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            TileItemAddPacket.Add(message, position, stackPosition, itemSpawn);
            Send(message);
        }

        /// <summary>
        ///     Sends the transform tile item.
        /// </summary>
        /// <param name="itemSpawn">The item spawn.</param>
        /// <param name="location">The location.</param>
        /// <param name="stackPosition">The stack position.</param>
        public void SendTransformTileItem(ItemSpawn itemSpawn, Vector3 location, byte stackPosition)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            TileItemTransformPacket.Add(message, location, stackPosition, itemSpawn);
            Send(message);
        }

        /// <summary>
        ///     Sends the inventory item.
        /// </summary>
        /// <param name="slot">The slot.</param>
        /// <param name="inventoryItem">The inventory item.</param>
        public void SendInventoryItem(SlotType slot, InventoryItem inventoryItem)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            InventoryItemPacket.Add(message, inventoryItem, slot);
            Send(message);
        }

        /// <summary>
        ///     Sends the creature health.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        public void SendCreatureHealth(CreatureSpawn creatureSpawn)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            CreatureHealthPacket.Add(message, creatureSpawn);

            if (creatureSpawn == CharacterSpawn)
                SelfStatsPacket.Add(message, CharacterSpawn);

            Send(message);
        }

        /// <summary>
        ///     Sends the character resource.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        public void SendCharacterResource(CharacterSpawn characterSpawn)
        {
            NetworkMessage message = new NetworkMessage(Xtea);

            // TODO: This packet must be implemented in the new client CharacterResourcePacket.Add(message, character);
            if (characterSpawn == CharacterSpawn)
                SelfStatsPacket.Add(message, CharacterSpawn);

            Send(message);
        }
    }
}