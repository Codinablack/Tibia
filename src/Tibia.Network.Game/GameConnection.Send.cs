using System;
using System.Collections.Generic;
using System.Linq;
using Tibia.Data;
using Tibia.Map;
using Tibia.Network.Game.Packets;
using Tibia.Quests;
using Tibia.Spawns;

namespace Tibia.Network.Game
{
    public partial class GameConnection
    {
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
            TileCreatureAddPacket.Add(message, CharacterSpawn, position, stackPosition, creatureSpawn, known, remove);
            Send(message);
        }

        /// <summary>
        ///     Sends the tile add item.
        /// </summary>
        /// <param name="itemSpawn">The item spawn.</param>
        /// <param name="targetTilePosition">The position.</param>
        /// <param name="stackPosition">The stack position.</param>
        public void SendTileAddItem(IItemSpawn itemSpawn, IVector3 targetTilePosition, byte stackPosition)
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            TileItemAddPacket.Add(message, targetTilePosition, stackPosition, itemSpawn);
            Send(message);
        }
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
        /// <summary>
        ///     Sends the ping back.
        /// </summary>
        public void SendPingBack()
        {
            NetworkMessage message = new NetworkMessage(Xtea);
            GameServerPingBackPacket.Add(message);
            Send(message);
        }
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