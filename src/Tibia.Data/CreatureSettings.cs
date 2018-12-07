namespace Tibia.Data
{
    public class CreatureSettings
    {
        /// <summary>
        ///     Gets or sets a value indicating whether this instance can see ghosts.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance can see ghosts; otherwise, <see langword="false" />.
        /// </value>
        public bool CanSeeGhosts { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance can teleport.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance can teleport; otherwise, <see langword="false" />.
        /// </value>
        public bool CanTeleport { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is pushable.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is pushable; otherwise, <c>false</c>.
        /// </value>
        public bool IsPushable { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance can see diagnostics.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance can see diagnostics; otherwise, <see langword="false" />.
        /// </value>
        public bool CanSeeDiagnostics { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance can broadcast.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance can broadcast; otherwise, <see langword="false" />.
        /// </value>
        public bool CanBroadcast { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance can change sex.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance can change sex; otherwise, <see langword="false" />.
        /// </value>
        public bool CanChangeSex { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance can push creatures.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance can push creatures; otherwise, <c>false</c>.
        /// </value>
        public bool CanPushCreatures { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance can push items.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance can push items; otherwise, <c>false</c>.
        /// </value>
        public bool CanPushItems { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is attackable.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is attackable; otherwise, <c>false</c>.
        /// </value>
        public bool IsAttackable { get; set; }
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is convinceable.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is convinceable; otherwise, <c>false</c>.
        /// </value>
        public bool IsConvinceable { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is hostile.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is hostile; otherwise, <c>false</c>.
        /// </value>
        public bool IsHostile { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is illusionable.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is illusionable; otherwise, <c>false</c>.
        /// </value>
        public bool IsIllusionable { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is ranged.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is ranged; otherwise, <c>false</c>.
        /// </value>
        public bool IsRanged { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is summonable.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is summonable; otherwise, <c>false</c>.
        /// </value>
        public bool IsSummonable { get; set; }
    }
}