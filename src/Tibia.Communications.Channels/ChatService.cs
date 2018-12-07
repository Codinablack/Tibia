using System;
using System.Collections.Generic;
using System.Linq;
using Tibia.Data;
using Tibia.Data.Services;

namespace Tibia.Communications.Channels
{
    public class ChatService : IService
    {
        private readonly IDictionary<uint, HashSet<IAccount>> _accountsWithFriend;
        private readonly IDictionary<uint, IGuildChannel> _channelByGuildId;
        private readonly IDictionary<uint, IPartyChannel> _channelByPartyId;
        private readonly IDictionary<ChannelType, IChannel> _channelByType;
        private readonly IDictionary<uint, ICollection<IChannel>> _channelsByCharacterSpawnId;
        private readonly IDictionary<uint, IPrivateChannel> _privateChannelByOwnerId;
        private readonly IDictionary<string, ICollection<IPrivateChannel>> _privateChannelsByOwnerName;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ChatService" /> class.
        /// </summary>
        /// <param name="accounts">The accounts.</param>
        /// <param name="channels">The channels.</param>
        public ChatService(IEnumerable<IAccount> accounts, IDictionary<ChannelType, IChannel> channels)
        {
            // TODO: Replace IEnumerable<IAccount> accounts with IRepository<IAccount>
            _channelByType = new Dictionary<ChannelType, IChannel>(channels);
            _privateChannelByOwnerId = new Dictionary<uint, IPrivateChannel>();
            _privateChannelsByOwnerName = new Dictionary<string, ICollection<IPrivateChannel>>();
            _channelByPartyId = new Dictionary<uint, IPartyChannel>();
            _channelByGuildId = new Dictionary<uint, IGuildChannel>();
            _channelsByCharacterSpawnId = new Dictionary<uint, ICollection<IChannel>>();
            _accountsWithFriend = accounts.SelectMany(s => s.Friends).GroupBy(s => s.Character.Id).ToDictionary(s => s.Key, s => new HashSet<IAccount>(s.Select(r => r.Account)));
        }

        /// <summary>
        ///     Gets a collection of channels.
        /// </summary>
        /// <value>
        ///     The collection of channels.
        /// </value>
        public IEnumerable<IChannel> Channels
        {
            get
            {
                foreach (IChannel channel in _channelByType.Values)
                    yield return channel;

                foreach (IPrivateChannel channel in _privateChannelByOwnerId.Values)
                    yield return channel;

                foreach (IPartyChannel channel in _channelByPartyId.Values)
                    yield return channel;

                foreach (IGuildChannel channel in _channelByGuildId.Values)
                    yield return channel;
            }
        }

        /// <summary>
        ///     Gets the channels by character spawn identifier.
        /// </summary>
        /// <param name="characterSpawnId">The character spawn identifier.</param>
        /// <returns>The collection of channels.</returns>
        public IEnumerable<IChannel> GetChannelsByCharacterSpawnId(uint characterSpawnId)
        {
            if (!_channelsByCharacterSpawnId.ContainsKey(characterSpawnId))
                yield break;

            foreach (IChannel channel in _channelsByCharacterSpawnId[characterSpawnId])
                yield return channel;
        }

        /// <summary>
        ///     Unregisters the specified character spawn from all channels.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        public void UnregisterFromAllChannels(ICharacterSpawn characterSpawn)
        {
            foreach (IChannel channel in _channelByType.Values)
                channel.Unregister(characterSpawn);

            foreach (IPartyChannel channel in _channelByPartyId.Values)
                channel.Unregister(characterSpawn);

            foreach (IGuildChannel channel in _channelByGuildId.Values)
                channel.Unregister(characterSpawn);

            List<IPrivateChannel> privateChannels = _privateChannelByOwnerId.Values.ToList();
            foreach (IPrivateChannel channel in privateChannels)
            {
                channel.Uninvite(characterSpawn);
                channel.Unregister(characterSpawn);

                if (channel.Owner == characterSpawn)
                    ClosePrivateChannel(characterSpawn.Id);
            }
        }

        /// <summary>
        ///     Registers the character spawn to a specific channel.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="channel">The channel.</param>
        public void RegisterCharacterSpawn(ICharacterSpawn characterSpawn, IChannel channel)
        {
            if (!_channelsByCharacterSpawnId.ContainsKey(characterSpawn.Id))
                _channelsByCharacterSpawnId.Add(characterSpawn.Id, new HashSet<IChannel>());

            _channelsByCharacterSpawnId[characterSpawn.Id].Add(channel);
            channel.Register(characterSpawn);
        }

        /// <summary>
        ///     Closes the private channel.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void ClosePrivateChannel(uint id)
        {
            if (!_privateChannelByOwnerId.ContainsKey(id))
                return;

            _privateChannelByOwnerId[id].Close();
            _privateChannelByOwnerId[id] = null;
            _privateChannelByOwnerId.Remove(id);
        }

        /// <summary>
        ///     Gets the accounts with friend.
        /// </summary>
        /// <param name="characterId">The character identifier.</param>
        /// <returns>The accounts.</returns>
        public IEnumerable<IAccount> GetAccountsWithFriend(uint characterId)
        {
            if (!_accountsWithFriend.ContainsKey(characterId))
                yield break;

            foreach (IAccount account in _accountsWithFriend[characterId])
                yield return account;
        }

        /// <summary>
        ///     Removes the friend.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <param name="friend">The friend.</param>
        /// <returns><c>true</c> if the friend is removed; otherwise, <c>false</c>.</returns>
        public bool RemoveFriend(IAccount account, IFriend friend)
        {
            if (!_accountsWithFriend.ContainsKey(friend.Character.Id))
                return false;

            _accountsWithFriend[friend.Character.Id].Remove(account);
            return true;
        }

        /// <summary>
        ///     Gets the channel by type.
        /// </summary>
        /// <param name="channelType">Type of the channel.</param>
        /// <returns>The channel.</returns>
        public IChannel GetChannelByType(ChannelType channelType)
        {
            return _channelByType.ContainsKey(channelType) ? _channelByType[channelType] : null;
        }

        /// <summary>
        ///     Registers the channel.
        /// </summary>
        /// <param name="channelType">Type of the channel.</param>
        /// <param name="channel">The channel.</param>
        /// <returns><c>true</c> if the channel is registered; otherwise, <c>false</c>.</returns>
        public bool RegisterChannel(ChannelType channelType, IChannel channel)
        {
            if (_channelByType.ContainsKey(channelType))
                return false;

            _channelByType.Add(channelType, channel);
            return true;
        }

        /// <summary>
        ///     Gets the private channel by owner identifier.
        /// </summary>
        /// <param name="ownerId">The owner identifier.</param>
        /// <returns>The channel.</returns>
        public IPrivateChannel GetPrivateChannelByOwnerId(uint ownerId)
        {
            return _privateChannelByOwnerId.ContainsKey(ownerId) ? _privateChannelByOwnerId[ownerId] : null;
        }

        /// <summary>
        ///     Gets the channel by guild identifier.
        /// </summary>
        /// <param name="guildId">The guild identifier.</param>
        /// <returns>The channel.</returns>
        public IChannel GetGuildChannelById(uint guildId)
        {
            return _channelByGuildId.ContainsKey(guildId) ? _channelByGuildId[guildId] : null;
        }

        /// <summary>
        ///     Gets the channels by speech.
        /// </summary>
        /// <param name="speechType">Type of the speech.</param>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="owner">The owner.</param>
        /// <returns>The collection of channels.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public IEnumerable<IChannel> GetChannelsBySpeech(SpeechType speechType, ushort channelId, string owner)
        {
            switch (speechType)
            {
                case SpeechType.PrivateFrom:
                case SpeechType.PrivateTo:
                    foreach (IPrivateChannel channel in GetPrivateChannelsByOwnerName(owner))
                        yield return channel;
                    break;
                case SpeechType.Say:
                case SpeechType.Whisper:
                case SpeechType.Yell:
                case SpeechType.Yellow:
                case SpeechType.Orange:
                case SpeechType.Red:
                    yield return GetChannelByType((ChannelType) channelId);
                    break;
                case SpeechType.PrivateNp:
                case SpeechType.PrivatePn:
                    throw new NotImplementedException("NPC Channel is not implemented");
                case SpeechType.Broadcast:
                    throw new NotImplementedException("Broadcast is not implemented");
                case SpeechType.PrivateRedFrom:
                case SpeechType.PrivateRedTo:
                case SpeechType.MonsterSay:
                case SpeechType.MonsterYell:
                case SpeechType.ChannelR2:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException(nameof(speechType), speechType, null);
            }
        }

        /// <summary>
        ///     Gets the private channels by owner.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <returns>The collection of channels.</returns>
        public IEnumerable<IPrivateChannel> GetPrivateChannelsByOwnerName(string owner)
        {
            return _privateChannelsByOwnerName.ContainsKey(owner) ? _privateChannelsByOwnerName[owner] : null;
        }

        /// <summary>
        ///     Creates the private channel.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <returns>The channel.</returns>
        public IChannel CreatePrivateChannel(ICharacterSpawn characterSpawn)
        {
            if (!_privateChannelByOwnerId.ContainsKey(characterSpawn.Id))
            {
                PrivateChannel channel = new PrivateChannel();
                channel.Id = characterSpawn.Id;
                channel.Owner = characterSpawn;
                _privateChannelByOwnerId.Add(characterSpawn.Id, channel);
            }

            return _privateChannelByOwnerId[characterSpawn.Id];
        }
    }
}