using System;
using System.Collections.Generic;

namespace Tibia.Data
{
    public interface IGameConnection
    {
        /// <summary>
        ///     Disconnects the specified reason.
        /// </summary>
        /// <param name="reason">The reason.</param>
        void Disconnect(string reason);

        /// <summary>
        ///     Sends the open channel.
        /// </summary>
        /// <param name="channel">The channel.</param>
        void SendOpenChannel(IChannel channel);

        /// <summary>
        ///     Sends the self teleport.
        /// </summary>
        /// <param name="sourcePosition">The source position.</param>
        /// <param name="sourceStackPosition">The source stack position.</param>
        /// <param name="targetPosition">The target position.</param>
        void SendSelfTeleport(IVector3 sourcePosition, byte sourceStackPosition, IVector3 targetPosition);

        /// <summary>
        ///     Sends the creature move.
        /// </summary>
        /// <param name="sourcePosition">The source position.</param>
        /// <param name="sourceStackPosition">The source stack position.</param>
        /// <param name="targetPosition">The target position.</param>
        void SendCreatureMove(IVector3 sourcePosition, byte sourceStackPosition, IVector3 targetPosition);

        /// <summary>
        ///     Sends the tile artifact remove.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="stackPosition">The stack position.</param>
        void SendTileArtifactRemove(IVector3 position, byte stackPosition);

        /// <summary>
        ///     Sends the self move.
        /// </summary>
        /// <param name="sourcePosition">The source position.</param>
        /// <param name="sourceStackPosition">The source stack position.</param>
        /// <param name="targetPosition">The target position.</param>
        void SendSelfMove(IVector3 sourcePosition, byte sourceStackPosition, IVector3 targetPosition);

        /// <summary>
        ///     Moves the creature.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        /// <param name="targetPosition">The target position.</param>
        /// <returns><c>true</c> if the creature is moved; otherwise, <c>false</c>.</returns>
        bool MoveCreature(ICreatureSpawn creatureSpawn, IVector3 targetPosition);

        /// <summary>
        ///     Sends the close channel.
        /// </summary>
        /// <param name="channelType">Type of the channel.</param>
        void SendCloseChannel(ChannelType channelType);

        /// <summary>
        ///     Sends the close channel.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        void SendCloseChannel(ushort channelId);

        /// <summary>
        ///     Sends the channel message.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="type">The type.</param>
        /// <param name="text">The text.</param>
        void SendChannelMessage(ICharacterSpawn characterSpawn, ushort channelId, SpeechType type, string text);

        /// <summary>
        ///     Sends the channel list.
        /// </summary>
        /// <param name="channels">The channels.</param>
        void SendChannelList(ICollection<IChannel> channels);

        /// <summary>
        ///     Sends the creature speech.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        /// <param name="type">The type.</param>
        /// <param name="text">The text.</param>
        /// <param name="position">The position.</param>
        void SendCreatureSpeech(ICreatureSpawn creatureSpawn, SpeechType type, string text, IVector3 position);

        /// <summary>
        ///     Sends the change creature speed.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        void SendChangeCreatureSpeed(ICreatureSpawn creatureSpawn);

        /// <summary>
        ///     Sends the change creature appearance.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        void SendChangeCreatureAppearance(ICreatureSpawn creatureSpawn);

        /// <summary>
        ///     Sends the appearance window.
        /// </summary>
        void SendAppearanceWindow();

        /// <summary>
        ///     Sends the enter game.
        /// </summary>
        /// <param name="lightInfo">The light information.</param>
        /// <param name="premiumTimeLeft">The premium time left.</param>
        /// <param name="characterSpawn">The character spawn.</param>
        void SendEnterGame(ILightInfo lightInfo, TimeSpan premiumTimeLeft, ICharacterSpawn characterSpawn);

        /// <summary>
        ///     Sends the creature turn.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        void SendCreatureTurn(ICreatureSpawn creatureSpawn);

        /// <summary>
        ///     Sends the effect.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="effect">The effect.</param>
        void SendEffect(IVector3 position, Effect effect);

        /// <summary>
        ///     Sends the friend login.
        /// </summary>
        /// <param name="friendId">The friend identifier.</param>
        void SendFriendLogin(uint friendId);

        /// <summary>
        ///     Sends the text message.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="text">The text.</param>
        /// <param name="position">The position.</param>
        /// <param name="primaryValue">The primary value.</param>
        /// <param name="primaryColor">Color of the primary.</param>
        /// <param name="secondaryValue">The secondary value.</param>
        /// <param name="secondaryColor">Color of the secondary.</param>
        void SendTextMessage(TextMessageType type, string text, IVector3 position = null, uint? primaryValue = null, byte? primaryColor = null, uint? secondaryValue = null, byte? secondaryColor = null);

        /// <summary>
        ///     Determines whether the specified creature spawn is known.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        /// <param name="creatureSpawnService">The creature spawn service.</param>
        /// <param name="removeId">The remove identifier.</param>
        /// <returns>
        ///     <c>true</c> if the specified creature spawn is known; otherwise, <c>false</c>.
        /// </returns>
        bool IsCreatureKnown(ICreatureSpawn creatureSpawn, ICreatureSpawnService creatureSpawnService, out uint removeId);

        /// <summary>
        ///     Sends the tile add creature.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        /// <param name="tilePosition">The tile position.</param>
        /// <param name="stackPosition">The stack position.</param>
        void SendTileAddCreature(ICreatureSpawn creatureSpawn, IVector3 tilePosition, byte stackPosition);

        /// <summary>
        ///     Sends the ping back.
        /// </summary>
        void SendPingBack();

        /// <summary>
        ///     Sends the friend.
        /// </summary>
        /// <param name="friend">The friend.</param>
        void SendFriend(IFriend friend);
    }
}