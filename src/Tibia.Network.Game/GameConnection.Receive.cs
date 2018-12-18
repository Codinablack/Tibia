using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Tibia.Communications;
using Tibia.Data;
using Tibia.Map;
using Tibia.Network.Game.Events;
using Tibia.Network.Game.Packets;
using Tibia.Security.Cryptography;

namespace Tibia.Network.Game
{
    public partial class GameConnection
    {
        /// <summary>
        ///     Called when [message received].
        /// </summary>
        /// <param name="result">The result.</param>
        public void OnMessageReceived(IAsyncResult result)
        {
            TcpListener gameListener = (TcpListener)result.AsyncState;
            Socket = gameListener.EndAcceptSocket(result);
            Stream = new NetworkStream(Socket);

            OnlineTimeEventArgs onlineTimeEventArgs = new OnlineTimeEventArgs(TimeSpan.Zero);
            RequestOnlineTime?.Invoke(this, onlineTimeEventArgs);

            SendConnectionPacket(onlineTimeEventArgs.TimeSpan);
            Stream.BeginRead(IncomingMessage.Buffer, 0, 2, OnClientReadHandshakeMessage, null);
        }

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
            foreach (ICharacterSpawn spectator in _tileService.Spectators(CharacterSpawn.Tile.Position, new Vector3(10, 10, 3)))
                spectator.Connection.SendEffect(CharacterSpawn.Tile.Position, Effect.Teleport);

            // TODO: DateTime.UtcNow should be server time to avoid such operation consuming resources
            LightInfoEventArgs lightInfoEventArgs = new LightInfoEventArgs();
            RequestLightInfo?.Invoke(this, lightInfoEventArgs);

            CharacterSpawn.Connection.SendEnterGame(lightInfoEventArgs.LightInfo, CharacterSpawn.Account.PremiumExpirationDate - DateTime.UtcNow, CharacterSpawn);

            EnteredGameEventArgs enteredGameEventArgs = new EnteredGameEventArgs(CharacterSpawn);
            EnteredGame?.Invoke(this, enteredGameEventArgs);
        }

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
        ///     Parses the self walk north.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseSelfWalkNorth(NetworkMessage message)
        {
            WalkCharacter(Direction.North);
        }

        /// <summary>
        ///     Parses the self walk south.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseSelfWalkSouth(NetworkMessage message)
        {
            WalkCharacter(Direction.South);
        }

        /// <summary>
        ///     Parses the self walk east.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseSelfWalkEast(NetworkMessage message)
        {
            WalkCharacter(Direction.East);
        }

        /// <summary>
        ///     Parses the self walk west.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseSelfWalkWest(NetworkMessage message)
        {
            WalkCharacter(Direction.West);
        }

        /// <summary>
        ///     Parses the self walk north east.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseSelfWalkNorthEast(NetworkMessage message)
        {
            WalkCharacter(Direction.NorthEast);
        }

        /// <summary>
        ///     Parses the self walk north west.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseMoveNorthWest(NetworkMessage message)
        {
            WalkCharacter(Direction.NorthWest);
        }

        /// <summary>
        ///     Parses the self walk south east.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseSelfWalkSouthEast(NetworkMessage message)
        {
            WalkCharacter(Direction.SouthEast);
        }

        /// <summary>
        ///     Parses the self walk south west.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseSelfWalkSouthWest(NetworkMessage message)
        {
            WalkCharacter(Direction.SouthWest);
        }

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
                foreach (ICharacterSpawn spectator in _tileService.Spectators(CharacterSpawn.Tile.Position, new Vector3(10, 10, 3)))
                    spectator.Connection.SendChangeCreatureAppearance(CharacterSpawn);
            }

            RequestedAppearanceChangeEventArgs requestedAppearanceChangeEventArgs = new RequestedAppearanceChangeEventArgs(CharacterSpawn);
            RequestedAppearanceChange?.Invoke(this, requestedAppearanceChangeEventArgs);
        }

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
            foreach (ICharacterSpawn spectator in _tileService.Spectators(CharacterSpawn.Tile.Position, new Vector3(10, 10, 3)))
            {
                spectator.Connection.SendChangeCreatureSpeed(CharacterSpawn);

                if (!CharacterSpawn.IsInvisible)
                    spectator.Connection.SendChangeCreatureAppearance(CharacterSpawn);
            }

            ToggledMountEventArgs toggledMountEventArgs = new ToggledMountEventArgs(CharacterSpawn);
            ToggledMount?.Invoke(this, toggledMountEventArgs);
        }

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
        ///     Parses the battle list look at.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseBattleListLookAt(NetworkMessage message)
        {
            BattleListLookPacket packet = BattleListLookPacket.Parse(message);
            // TODO: Game.CharacterBattleListLookAt(CharacterSpawn, (int) packet.CreatureId);
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
        ///     Parses the artifact move.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ParseArtifactMove(NetworkMessage message)
        {
            ArtifactMovePacket packet = ArtifactMovePacket.Parse(message);
            ArtifactMovingEventArgs artifactMovingEventArgs = new ArtifactMovingEventArgs(packet.SourcePosition, packet.SourceStackPosition, packet.SpriteId, packet.TargetPosition, packet.Count);
            ArtifactMoving?.Invoke(this, artifactMovingEventArgs);
            if (artifactMovingEventArgs.Cancel)
                return;

            if (!MoveArtifact(artifactMovingEventArgs.SourcePosition, artifactMovingEventArgs.SourceStackPosition, artifactMovingEventArgs.SpriteId, artifactMovingEventArgs.TargetPosition, artifactMovingEventArgs.Count))
                return;

            ArtifactMovedEventArgs characterWalkedEventArgs = new ArtifactMovedEventArgs(artifactMovingEventArgs.SourcePosition, artifactMovingEventArgs.SourceStackPosition, artifactMovingEventArgs.SpriteId, artifactMovingEventArgs.TargetPosition, artifactMovingEventArgs.Count);
            ArtifactMoved?.Invoke(this, characterWalkedEventArgs);
        }
    }
}