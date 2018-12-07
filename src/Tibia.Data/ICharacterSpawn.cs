using System;
using System.Collections.Generic;

namespace Tibia.Data
{
    public interface ICharacterSpawn : ICreatureSpawn
    {
        /// <summary>
        ///     Gets or sets a value indicating whether [chase mode].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [chase mode]; otherwise, <c>false</c>.
        /// </value>
        bool ChaseMode { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [safe mode].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [safe mode]; otherwise, <c>false</c>.
        /// </value>
        bool SafeMode { get; set; }

        /// <summary>
        ///     Gets or sets the connection.
        /// </summary>
        /// <value>
        ///     The connection.
        /// </value>
        IGameConnection Connection { get; set; }

        /// <summary>
        ///     Gets or sets the party.
        /// </summary>
        /// <value>
        ///     The party.
        /// </value>
        IParty Party { get; set; }

        /// <summary>
        ///     Gets or sets the outfits.
        /// </summary>
        /// <value>
        ///     The outfits.
        /// </value>
        ICollection<IOutfit> Outfits { get; set; }

        /// <summary>
        ///     Gets or sets the mounts.
        /// </summary>
        /// <value>
        ///     The mounts.
        /// </value>
        ICollection<IMount> Mounts { get; set; }

        /// <summary>
        ///     Gets or sets the account identifier.
        /// </summary>
        /// <value>
        ///     The account identifier.
        /// </value>
        int AccountId { get; set; }

        /// <summary>
        ///     Gets or sets the account.
        /// </summary>
        /// <value>
        ///     The account.
        /// </value>
        IAccount Account { get; set; }

        /// <summary>
        ///     Gets or sets the amulet.
        /// </summary>
        /// <value>
        ///     The amulet.
        /// </value>
        IItemSpawn Amulet { get; set; }

        /// <summary>
        ///     Gets or sets the amulet identifier.
        /// </summary>
        /// <value>
        ///     The amulet identifier.
        /// </value>
        int? AmuletId { get; set; }

        /// <summary>
        ///     Gets or sets the axe skill.
        /// </summary>
        /// <value>
        ///     The axe skill.
        /// </value>
        ISkill AxeSkill { get; set; }

        /// <summary>
        ///     Gets or sets the balance.
        /// </summary>
        /// <value>
        ///     The balance.
        /// </value>
        ulong Balance { get; set; }

        /// <summary>
        ///     Gets or sets the fight mode.
        /// </summary>
        /// <value>
        ///     The fight mode.
        /// </value>
        BattleStance BattleStance { get; set; }

        /// <summary>
        ///     Gets or sets the belt.
        /// </summary>
        /// <value>
        ///     The belt.
        /// </value>
        IItemSpawn Belt { get; set; }

        /// <summary>
        ///     Gets or sets the belt identifier.
        /// </summary>
        /// <value>
        ///     The belt identifier.
        /// </value>
        int? BeltId { get; set; }

        /// <summary>
        ///     Gets or sets the blessings.
        /// </summary>
        /// <value>
        ///     The blessings.
        /// </value>
        int Blessings { get; set; }

        /// <summary>
        ///     Gets or sets the capacity.
        /// </summary>
        /// <value>
        ///     The capacity.
        /// </value>
        uint Capacity { get; set; }

        /// <summary>
        ///     Gets or sets the character.
        /// </summary>
        /// <value>
        ///     The character.
        /// </value>
        ICharacter Character { get; set; }

        /// <summary>
        ///     Gets or sets the character identifier.
        /// </summary>
        /// <value>
        ///     The character identifier.
        /// </value>
        int CharacterId { get; set; }

        /// <summary>
        ///     Gets or sets the club skill.
        /// </summary>
        /// <value>
        ///     The club skill.
        /// </value>
        ISkill ClubSkill { get; set; }

        /// <summary>
        ///     Gets or sets the conditions.
        /// </summary>
        /// <value>
        ///     The conditions.
        /// </value>
        Conditions Conditions { get; set; }

        /// <summary>
        ///     Gets or sets the container.
        /// </summary>
        /// <value>
        ///     The container.
        /// </value>
        IItemSpawn Container { get; set; }

        /// <summary>
        ///     Gets or sets the container identifier.
        /// </summary>
        /// <value>
        ///     The container identifier.
        /// </value>
        int? ContainerId { get; set; }

        /// <summary>
        ///     Gets or sets the creation time.
        /// </summary>
        /// <value>
        ///     The creation time.
        /// </value>
        DateTime CreationTime { get; set; }

        /// <summary>
        ///     Gets or sets the deletion time.
        /// </summary>
        /// <value>
        ///     The deletion time.
        /// </value>
        DateTime? DeletionTime { get; set; }

        /// <summary>
        ///     Gets or sets the distance skill.
        /// </summary>
        /// <value>
        ///     The distance skill.
        /// </value>
        ISkill DistanceSkill { get; set; }

        /// <summary>
        ///     Gets or sets the feet.
        /// </summary>
        /// <value>
        ///     The feet.
        /// </value>
        IItemSpawn Feet { get; set; }

        /// <summary>
        ///     Gets or sets the feet identifier.
        /// </summary>
        /// <value>
        ///     The feet identifier.
        /// </value>
        int? FeetId { get; set; }

        /// <summary>
        ///     Gets or sets the fishing skill.
        /// </summary>
        /// <value>
        ///     The fishing skill.
        /// </value>
        ISkill FishingSkill { get; set; }

        /// <summary>
        ///     Gets or sets the fist skill.
        /// </summary>
        /// <value>
        ///     The fist skill.
        /// </value>
        ISkill FistSkill { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [follow opponent].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [follow opponent]; otherwise, <c>false</c>.
        /// </value>
        bool FollowOpponent { get; set; }

        /// <summary>
        ///     Gets or sets the free capacity.
        /// </summary>
        /// <value>
        ///     The free capacity.
        /// </value>
        uint FreeCapacity { get; set; }

        /// <summary>
        ///     Gets or sets the guild.
        /// </summary>
        /// <value>
        ///     The guild.
        /// </value>
        IGuild Guild { get; set; }

        /// <summary>
        ///     Gets or sets the guild level.
        /// </summary>
        /// <value>
        ///     The guild level.
        /// </value>
        byte GuildLevel { get; set; }

        /// <summary>
        ///     Gets or sets the head.
        /// </summary>
        /// <value>
        ///     The head.
        /// </value>
        IItemSpawn Head { get; set; }

        /// <summary>
        ///     Gets or sets the head identifier.
        /// </summary>
        /// <value>
        ///     The head identifier.
        /// </value>
        int? HeadId { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is mount toggle exhausted.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is mount toggle exhausted; otherwise, <c>false</c>.
        /// </value>
        bool IsMountToggleExhausted { get; set; }

        /// <summary>
        ///     Gets or sets the last login.
        /// </summary>
        /// <value>
        ///     The last login.
        /// </value>
        ILoginInfo LastLogin { get; set; }

        /// <summary>
        ///     Gets or sets the last logout.
        /// </summary>
        /// <value>
        ///     The last logout.
        /// </value>
        DateTime? LastLogout { get; set; }

        /// <summary>
        ///     Gets or sets the last ping.
        /// </summary>
        /// <value>
        ///     The last ping.
        /// </value>
        DateTime LastPing { get; set; }

        /// <summary>
        ///     Gets or sets the legs.
        /// </summary>
        /// <value>
        ///     The legs.
        /// </value>
        IItemSpawn Legs { get; set; }

        /// <summary>
        ///     Gets or sets the legs identifier.
        /// </summary>
        /// <value>
        ///     The legs identifier.
        /// </value>
        int? LegsId { get; set; }

        /// <summary>
        ///     Gets or sets the magic level.
        /// </summary>
        /// <value>
        ///     The magic level.
        /// </value>
        IMagicLevel MagicLevel { get; set; }

        /// <summary>
        ///     Gets or sets the mana.
        /// </summary>
        /// <value>
        ///     The mana.
        /// </value>
        IMana Mana { get; set; }

        /// <summary>
        ///     Gets or sets the offline training.
        /// </summary>
        /// <value>
        ///     The offline training.
        /// </value>
        IOfflineTrainingInfo OfflineTraining { get; set; }

        /// <summary>
        ///     Gets or sets the online time ticks.
        /// </summary>
        /// <value>
        ///     The online time ticks.
        /// </value>
        TimeSpan OnlineTime { get; set; }

        /// <summary>
        ///     Gets the open containers.
        /// </summary>
        /// <value>
        ///     The open containers.
        /// </value>
        Dictionary<int, IContainerItemSpawn> OpenContainers { get; }

        /// <summary>
        ///     Gets or sets the party identifier.
        /// </summary>
        /// <value>
        ///     The party identifier.
        /// </value>
        int? PartyId { get; set; }

        /// <summary>
        ///     Gets or sets the party invitations.
        /// </summary>
        /// <value>
        ///     The party invitations.
        /// </value>
        ICollection<IParty> PartyInvitations { get; set; }

        /// <summary>
        ///     Gets the quests.
        /// </summary>
        /// <value>
        ///     The quests.
        /// </value>
        ICollection<IQuestInfo> Quests { get; set; }

        /// <summary>
        ///     Gets or sets the ring.
        /// </summary>
        /// <value>
        ///     The ring.
        /// </value>
        IItemSpawn Ring { get; set; }

        /// <summary>
        ///     Gets or sets the ring identifier.
        /// </summary>
        /// <value>
        ///     The ring identifier.
        /// </value>
        int? RingId { get; set; }

        /// <summary>
        ///     Gets or sets the sex.
        /// </summary>
        /// <value>
        ///     The sex.
        /// </value>
        Sex Sex { get; set; }

        /// <summary>
        ///     Gets or sets the shield.
        /// </summary>
        /// <value>
        ///     The shield.
        /// </value>
        IItemSpawn Shield { get; set; }

        /// <summary>
        ///     Gets or sets the shield identifier.
        /// </summary>
        /// <value>
        ///     The shield identifier.
        /// </value>
        int? ShieldId { get; set; }

        /// <summary>
        ///     Gets or sets the shield skill.
        /// </summary>
        /// <value>
        ///     The shield skill.
        /// </value>
        ISkill ShieldSkill { get; set; }

        /// <summary>
        ///     Gets or sets the soul.
        /// </summary>
        /// <value>
        ///     The soul.
        /// </value>
        byte Soul { get; set; }

        /// <summary>
        ///     Gets or sets the stamina.
        /// </summary>
        /// <value>
        ///     The stamina.
        /// </value>
        ushort Stamina { get; set; }

        /// <summary>
        ///     Gets or sets the sword skill.
        /// </summary>
        /// <value>
        ///     The sword skill.
        /// </value>
        ISkill SwordSkill { get; set; }

        /// <summary>
        ///     Gets or sets the torso.
        /// </summary>
        /// <value>
        ///     The torso.
        /// </value>
        IItemSpawn Torso { get; set; }

        /// <summary>
        ///     Gets or sets the torso identifier.
        /// </summary>
        /// <value>
        ///     The torso identifier.
        /// </value>
        int? TorsoId { get; set; }

        /// <summary>
        ///     Gets or sets the town identifier.
        /// </summary>
        /// <value>
        ///     The town identifier.
        /// </value>
        uint TownId { get; set; }

        /// <summary>
        ///     Gets or sets the town.
        /// </summary>
        /// <value>
        ///     The town.
        /// </value>
        ITown Town { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [use secure mode].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [use secure mode]; otherwise, <c>false</c>.
        /// </value>
        bool UseSecureMode { get; set; }

        /// <summary>
        ///     Gets or sets the vocation identifier.
        /// </summary>
        /// <value>
        ///     The vocation identifier.
        /// </value>
        byte VocationId { get; set; }

        /// <summary>
        ///     Gets or sets the vocation.
        /// </summary>
        /// <value>
        ///     The vocation.
        /// </value>
        IVocation Vocation { get; set; }

        /// <summary>
        ///     Gets or sets the weapon.
        /// </summary>
        /// <value>
        ///     The weapon.
        /// </value>
        IItemSpawn Weapon { get; set; }

        /// <summary>
        ///     Gets or sets the weapon identifier.
        /// </summary>
        /// <value>
        ///     The weapon identifier.
        /// </value>
        int? WeaponId { get; set; }

        /// <summary>
        ///     Gets or sets the world identifier.
        /// </summary>
        /// <value>
        ///     The world identifier.
        /// </value>
        byte WorldId { get; set; }
    }
}