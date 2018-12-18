using System;
using System.Collections.Generic;
using Tibia.Data;

namespace Tibia.Spawns
{
    public class CharacterSpawn : CreatureSpawn, ICharacterSpawn
    {
        private readonly Dictionary<int, IContainerItemSpawn> _openContainers;
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Spawns.CharacterSpawn" /> class.
        /// </summary>
        public CharacterSpawn()
        {
            _openContainers = new Dictionary<int, IContainerItemSpawn>();

            LastLogin = new LoginInfo();
            MagicLevel = new MagicLevel();
            Mana = new Mana();
            Mounts = new HashSet<IMount>();
            OfflineTraining = new OfflineTrainingInfo();
            Outfits = new HashSet<IOutfit>();
            PartyInvitations = new HashSet<IParty>();
            Quests = new HashSet<IQuestInfo>();
        }
        /// <summary>
        ///     Gets or sets the town.
        /// </summary>
        /// <value>
        ///     The town.
        /// </value>
        public ITown Town { get; set; }
        /// <summary>
        ///     Gets or sets a value indicating whether [safe mode].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [safe mode]; otherwise, <c>false</c>.
        /// </value>
        public bool SafeMode { get; set; }
        /// <summary>
        ///     Gets or sets the connection.
        /// </summary>
        /// <value>
        ///     The connection.
        /// </value>
        public IGameConnection Connection { get; set; }
        /// <summary>
        ///     Gets or sets the account.
        /// </summary>
        /// <value>
        ///     The account.
        /// </value>
        public IAccount Account { get; set; }
        /// <summary>
        ///     Gets or sets the vocation.
        /// </summary>
        /// <value>
        ///     The vocation.
        /// </value>
        public IVocation Vocation { get; set; }
        /// <summary>
        ///     Gets or sets the party.
        /// </summary>
        /// <value>
        ///     The party.
        /// </value>
        public IParty Party { get; set; }
        /// <summary>
        ///     Gets or sets the outfits.
        /// </summary>
        /// <value>
        ///     The outfits.
        /// </value>
        public ICollection<IOutfit> Outfits { get; set; }
        /// <summary>
        ///     Gets or sets the mounts.
        /// </summary>
        /// <value>
        ///     The mounts.
        /// </value>
        public ICollection<IMount> Mounts { get; set; }
        /// <summary>
        ///     Gets the quests.
        /// </summary>
        /// <value>
        ///     The quests.
        /// </value>
        public ICollection<IQuestInfo> Quests { get; set; }
        /// <summary>
        ///     Gets or sets the party invitations.
        /// </summary>
        /// <value>
        ///     The party invitations.
        /// </value>
        public ICollection<IParty> PartyInvitations { get; set; }
        /// <summary>
        ///     Gets or sets the shield skill.
        /// </summary>
        /// <value>
        ///     The shield skill.
        /// </value>
        public ISkill ShieldSkill { get; set; }
        /// <summary>
        ///     Gets or sets the sword skill.
        /// </summary>
        /// <value>
        ///     The sword skill.
        /// </value>
        public ISkill SwordSkill { get; set; }
        /// <summary>
        ///     Gets or sets the head.
        /// </summary>
        /// <value>
        ///     The head.
        /// </value>
        public IItemSpawn Head { get; set; }
        /// <summary>
        ///     Gets or sets the amulet.
        /// </summary>
        /// <value>
        ///     The amulet.
        /// </value>
        public IItemSpawn Amulet { get; set; }
        /// <summary>
        ///     Gets or sets the ring.
        /// </summary>
        /// <value>
        ///     The ring.
        /// </value>
        public IItemSpawn Ring { get; set; }
        /// <summary>
        ///     Gets or sets the legs.
        /// </summary>
        /// <value>
        ///     The legs.
        /// </value>
        public IItemSpawn Legs { get; set; }
        /// <summary>
        ///     Gets or sets the torso.
        /// </summary>
        /// <value>
        ///     The torso.
        /// </value>
        public IItemSpawn Torso { get; set; }
        /// <summary>
        ///     Gets or sets the shield.
        /// </summary>
        /// <value>
        ///     The shield.
        /// </value>
        public IItemSpawn Shield { get; set; }
        /// <summary>
        ///     Gets or sets the belt.
        /// </summary>
        /// <value>
        ///     The belt.
        /// </value>
        public IItemSpawn Belt { get; set; }
        /// <summary>
        ///     Gets or sets the weapon.
        /// </summary>
        /// <value>
        ///     The weapon.
        /// </value>
        public IItemSpawn Weapon { get; set; }
        /// <summary>
        ///     Gets or sets the container.
        /// </summary>
        /// <value>
        ///     The container.
        /// </value>
        public IItemSpawn Container { get; set; }
        /// <summary>
        ///     Gets or sets the feet.
        /// </summary>
        /// <value>
        ///     The feet.
        /// </value>
        public IItemSpawn Feet { get; set; }
        /// <summary>
        ///     Gets or sets the character.
        /// </summary>
        /// <value>
        ///     The character.
        /// </value>
        public ICharacter Character { get; set; }
        /// <summary>
        ///     Gets or sets the character identifier.
        /// </summary>
        /// <value>
        ///     The character identifier.
        /// </value>
        public int CharacterId { get; set; }
        /// <summary>
        ///     Gets or sets the axe skill.
        /// </summary>
        /// <value>
        ///     The axe skill.
        /// </value>
        public ISkill AxeSkill { get; set; }
        /// <summary>
        ///     Gets or sets the balance.
        /// </summary>
        /// <value>
        ///     The balance.
        /// </value>
        public ulong Balance { get; set; }
        /// <summary>
        ///     Gets or sets the blessings.
        /// </summary>
        /// <value>
        ///     The blessings.
        /// </value>
        public int Blessings { get; set; }
        /// <summary>
        ///     Gets or sets the capacity.
        /// </summary>
        /// <value>
        ///     The capacity.
        /// </value>
        public uint Capacity { get; set; }
        /// <summary>
        ///     Gets or sets the club skill.
        /// </summary>
        /// <value>
        ///     The club skill.
        /// </value>
        public ISkill ClubSkill { get; set; }
        /// <summary>
        ///     Gets or sets the distance skill.
        /// </summary>
        /// <value>
        ///     The distance skill.
        /// </value>
        public ISkill DistanceSkill { get; set; }
        /// <summary>
        ///     Gets or sets the fight mode.
        /// </summary>
        /// <value>
        ///     The fight mode.
        /// </value>
        public BattleStance BattleStance { get; set; }
        /// <summary>
        ///     Gets or sets the fishing skill.
        /// </summary>
        /// <value>
        ///     The fishing skill.
        /// </value>
        public ISkill FishingSkill { get; set; }
        /// <summary>
        ///     Gets or sets the fist skill.
        /// </summary>
        /// <value>
        ///     The fist skill.
        /// </value>
        public ISkill FistSkill { get; set; }
        /// <summary>
        ///     Gets or sets a value indicating whether [follow opponent].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [follow opponent]; otherwise, <c>false</c>.
        /// </value>
        public bool FollowOpponent { get; set; }
        /// <summary>
        ///     Gets or sets the free capacity.
        /// </summary>
        /// <value>
        ///     The free capacity.
        /// </value>
        public uint FreeCapacity { get; set; }
        /// <summary>
        ///     Gets or sets the guild.
        /// </summary>
        /// <value>
        ///     The guild.
        /// </value>
        public IGuild Guild { get; set; }
        /// <summary>
        ///     Gets or sets the guild level.
        /// </summary>
        /// <value>
        ///     The guild level.
        /// </value>
        public byte GuildLevel { get; set; }
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is mount toggle exhausted.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is mount toggle exhausted; otherwise, <c>false</c>.
        /// </value>
        public bool IsMountToggleExhausted { get; set; }
        /// <summary>
        ///     Gets or sets the last login.
        /// </summary>
        /// <value>
        ///     The last login.
        /// </value>
        public ILoginInfo LastLogin { get; set; }
        /// <summary>
        ///     Gets or sets the last logout.
        /// </summary>
        /// <value>
        ///     The last logout.
        /// </value>
        public DateTime? LastLogout { get; set; }
        /// <summary>
        ///     Gets or sets the last ping.
        /// </summary>
        /// <value>
        ///     The last ping.
        /// </value>
        public DateTime LastPing { get; set; }
        /// <summary>
        ///     Gets or sets the magic level.
        /// </summary>
        /// <value>
        ///     The magic level.
        /// </value>
        public IMagicLevel MagicLevel { get; set; }
        /// <summary>
        ///     Gets or sets the mana.
        /// </summary>
        /// <value>
        ///     The mana.
        /// </value>
        public IMana Mana { get; set; }
        /// <summary>
        ///     Gets or sets the offline training.
        /// </summary>
        /// <value>
        ///     The offline training.
        /// </value>
        public IOfflineTrainingInfo OfflineTraining { get; set; }
        /// <summary>
        ///     Gets or sets the online time ticks.
        /// </summary>
        /// <value>
        ///     The online time ticks.
        /// </value>
        public TimeSpan OnlineTime { get; set; }

        /// <summary>
        ///     Gets the open containers.
        /// </summary>
        /// <value>
        ///     The open containers.
        /// </value>
        public IEnumerable<IContainerItemSpawn> OpenContainers => _openContainers.Values;
        /// <summary>
        ///     Gets or sets the party identifier.
        /// </summary>
        /// <value>
        ///     The party identifier.
        /// </value>
        public int? PartyId { get; set; }
        /// <summary>
        ///     Gets or sets the sex.
        /// </summary>
        /// <value>
        ///     The sex.
        /// </value>
        public Sex Sex { get; set; }
        /// <summary>
        ///     Gets or sets the soul.
        /// </summary>
        /// <value>
        ///     The soul.
        /// </value>
        public byte Soul { get; set; }
        /// <summary>
        ///     Gets or sets the conditions.
        /// </summary>
        /// <value>
        ///     The conditions.
        /// </value>
        public Conditions Conditions { get; set; }
        /// <summary>
        ///     Gets or sets the stamina.
        /// </summary>
        /// <value>
        ///     The stamina.
        /// </value>
        public ushort Stamina { get; set; }
        /// <summary>
        ///     Gets or sets the town identifier.
        /// </summary>
        /// <value>
        ///     The town identifier.
        /// </value>
        public uint TownId { get; set; }
        /// <summary>
        ///     Gets or sets a value indicating whether [use secure mode].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [use secure mode]; otherwise, <c>false</c>.
        /// </value>
        public bool UseSecureMode { get; set; }
        /// <summary>
        ///     Gets or sets the account identifier.
        /// </summary>
        /// <value>
        ///     The account identifier.
        /// </value>
        public int AccountId { get; set; }
        /// <summary>
        ///     Gets or sets the vocation identifier.
        /// </summary>
        /// <value>
        ///     The vocation identifier.
        /// </value>
        public byte VocationId { get; set; }
        /// <summary>
        ///     Gets or sets the world identifier.
        /// </summary>
        /// <value>
        ///     The world identifier.
        /// </value>
        public byte WorldId { get; set; }
        /// <summary>
        ///     Gets or sets the creation time.
        /// </summary>
        /// <value>
        ///     The creation time.
        /// </value>
        public DateTime CreationTime { get; set; }
        /// <summary>
        ///     Gets or sets the deletion time.
        /// </summary>
        /// <value>
        ///     The deletion time.
        /// </value>
        public DateTime? DeletionTime { get; set; }
        /// <summary>
        ///     Gets or sets the head identifier.
        /// </summary>
        /// <value>
        ///     The head identifier.
        /// </value>
        public int? HeadId { get; set; }
        /// <summary>
        ///     Gets or sets the amulet identifier.
        /// </summary>
        /// <value>
        ///     The amulet identifier.
        /// </value>
        public int? AmuletId { get; set; }
        /// <summary>
        ///     Gets or sets the ring identifier.
        /// </summary>
        /// <value>
        ///     The ring identifier.
        /// </value>
        public int? RingId { get; set; }
        /// <summary>
        ///     Gets or sets the legs identifier.
        /// </summary>
        /// <value>
        ///     The legs identifier.
        /// </value>
        public int? LegsId { get; set; }
        /// <summary>
        ///     Gets or sets the torso identifier.
        /// </summary>
        /// <value>
        ///     The torso identifier.
        /// </value>
        public int? TorsoId { get; set; }
        /// <summary>
        ///     Gets or sets the shield identifier.
        /// </summary>
        /// <value>
        ///     The shield identifier.
        /// </value>
        public int? ShieldId { get; set; }
        /// <summary>
        ///     Gets or sets the belt identifier.
        /// </summary>
        /// <value>
        ///     The belt identifier.
        /// </value>
        public int? BeltId { get; set; }
        /// <summary>
        ///     Gets or sets the weapon identifier.
        /// </summary>
        /// <value>
        ///     The weapon identifier.
        /// </value>
        public int? WeaponId { get; set; }
        /// <summary>
        ///     Gets or sets the container identifier.
        /// </summary>
        /// <value>
        ///     The container identifier.
        /// </value>
        public int? ContainerId { get; set; }
        /// <summary>
        ///     Gets or sets the feet identifier.
        /// </summary>
        /// <value>
        ///     The feet identifier.
        /// </value>
        public int? FeetId { get; set; }
        /// <summary>
        ///     Gets or sets a value indicating whether [chase mode].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [chase mode]; otherwise, <c>false</c>.
        /// </value>
        public bool ChaseMode { get; set; }

        /// <summary>
        ///     Attempts to get the container from the collection of open containers with the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="containerItemSpawn">The container item spawn.</param>
        /// <returns><c>true</c> if the container is successfully obtained.</returns>
        public bool TryGetOpenContainer(int index, out IContainerItemSpawn containerItemSpawn)
        {
            return _openContainers.TryGetValue(index, out containerItemSpawn);
        }
    }
}