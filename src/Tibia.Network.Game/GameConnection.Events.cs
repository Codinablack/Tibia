using System;
using Tibia.Data;
using Tibia.Network.Game.Events;

namespace Tibia.Network.Game
{
    public partial class GameConnection
    {
        /// <summary>
        ///     Occurs when [registering tile item].
        /// </summary>
        public event EventHandler<RegisteringTileItemEventArgs> RegisteringTileItem;

        /// <summary>
        ///     Occurs when [unregistering tile item].
        /// </summary>
        public event EventHandler<UnregisteringTileItemEventArgs> UnregisteringTileItem;

        /// <summary>
        ///     Occurs when [unregistered tile item].
        /// </summary>
        public event EventHandler<UnregisteredTileItemEventArgs> UnregisteredTileItem;

        /// <summary>
        ///     Occurs when [registered tile item].
        /// </summary>
        public event EventHandler<RegisteredTileItemEventArgs> RegisteredTileItem;

        /// <summary>
        ///     Occurs when [moving item].
        /// </summary>
        public event EventHandler<MovingItemEventArgs> MovingItem;

        /// <summary>
        ///     Occurs when [item moved].
        /// </summary>
        public event EventHandler<ItemMovedEventArgs> ItemMoved;

        /// <summary>
        ///     Occurs when registering a creature in a tile.
        /// </summary>
        public event EventHandler<RegisteringTileCreatureEventArgs> RegisteringTileCreature;

        /// <summary>
        ///     Occurs when a creature is registered in a tile.
        /// </summary>
        public event EventHandler<RegisteredTileCreatureEventArgs> RegisteredTileCreature;

        /// <summary>
        ///     Occurs when unregistering a tile creature.
        /// </summary>
        public event EventHandler<UnregisteringTileCreatureEventArgs> UnregisteringTileCreature;

        /// <summary>
        ///     Occurs when unregistered a tile creature.
        /// </summary>
        public event EventHandler<UnregisteredTileCreatureEventArgs> UnregisteredTileCreature;

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
        ///     Occurs when the client requests a ping back.
        /// </summary>
        public event EventHandler<EventArgs> RequestPingBack;

        /// <summary>
        ///     Occurs when the client is logging out.
        /// </summary>
        public event EventHandler<LoggingOutEventArgs> LoggingOut;

        /// <summary>
        ///     Occurs when the client is logged out.
        /// </summary>
        public event EventHandler<LoggedOutEventArgs> LoggedOut;

        /// <summary>
        ///     Occurs when the client is entering game.
        /// </summary>
        public event EventHandler<EnteringGameEventArgs> EnteringGame;

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
        ///     Occurs when the client is adding a friend.
        /// </summary>
        public event EventHandler<AddingFriendEventArgs> AddingFriend;

        /// <summary>
        ///     Occurs when the client added a friend.
        /// </summary>
        public event EventHandler<AddedFriendEventArgs> AddedFriend;

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
        ///     Occurs when the client edited a friend.
        /// </summary>
        public event EventHandler<EditedFriendEventArgs> EditedFriend;

        /// <summary>
        ///     Occurs when a creature is talking.
        /// </summary>
        public event EventHandler<CreatureTalkingEventArgs> CreatureTalking;

        /// <summary>
        ///     Occurs when a creature talked.
        /// </summary>
        public event EventHandler<CreatureTalkedEventArgs> CreatureTalked;

        /// <summary>
        ///     Occurs when the client is closing a channel.
        /// </summary>
        public event EventHandler<ClosingChannelEventArgs> ClosingChannel;

        /// <summary>
        ///     Occurs when the client closed a channel.
        /// </summary>
        public event EventHandler<ClosedChannelEventArgs> ClosedChannel;

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
        ///     Occurs when a creature is turning.
        /// </summary>
        public event EventHandler<CreatureTurningEventArgs> CreatureTurning;

        /// <summary>
        ///     Occurs when a creature turned.
        /// </summary>
        public event EventHandler<CreatureTurnedEventArgs> CreatureTurned;

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
        ///     Occurs when [artifact moving].
        /// </summary>
        public event EventHandler<ArtifactMovingEventArgs> ArtifactMoving;

        /// <summary>
        ///     Occurs when [artifact moved].
        /// </summary>
        public event EventHandler<ArtifactMovedEventArgs> ArtifactMoved;

        /// <summary>
        ///     Occurs when a character walked.
        /// </summary>
        public event EventHandler<CharacterWalkedEventArgs> CharacterWalked;

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
        ///     Occurs when using an item.
        /// </summary>
        public event EventHandler<UsingItemEventArgs> UsingItem;

        /// <summary>
        ///     Occurs when used an item.
        /// </summary>
        public event EventHandler<UsedItemEventArgs> UsedItem;

        /// <summary>
        ///     Occurs when automatic walking.
        /// </summary>
        public event EventHandler<AutoWalkingEventArgs> AutoWalking;

        /// <summary>
        ///     Occurs when automatic walked.
        /// </summary>
        public event EventHandler<AutoWalkedEventArgs> AutoWalked;

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
        ///     Occurs when the client toggled mount.
        /// </summary>
        public event EventHandler<ToggledMountEventArgs> ToggledMount;

        /// <summary>
        ///     Occurs when the client requested the appearance window.
        /// </summary>
        public event EventHandler<RequestedAppearanceWindowEventArgs> RequestedAppearanceWindow;

        /// <summary>
        ///     Occurs when the client is requesting the appearance window.
        /// </summary>
        public event EventHandler<RequestingAppearanceWindowEventArgs> RequestingAppearanceWindow;
    }
}