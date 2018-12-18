using System.ComponentModel;
using Tibia.Data;
using Tibia.Spawns;

namespace Tibia.Network.Game.Events
{
    public class FightModeChangingEventArgs : CancelEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.Events.FightModeChangingEventArgs" /> class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="battleStance">The battle stance.</param>
        /// <param name="chaseMode">if set to <c>true</c> [chase mode].</param>
        /// <param name="safeMode">if set to <c>true</c> [safe mode].</param>
        public FightModeChangingEventArgs(CharacterSpawn characterSpawn, BattleStance battleStance, bool chaseMode, bool safeMode)
        {
            CharacterSpawn = characterSpawn;
            BattleStance = battleStance;
            ChaseMode = chaseMode;
            SafeMode = safeMode;
        }

        /// <summary>
        ///     Gets the character spawn.
        /// </summary>
        /// <value>
        ///     The character spawn.
        /// </value>
        public CharacterSpawn CharacterSpawn { get; }

        /// <summary>
        ///     Gets the battle stance.
        /// </summary>
        /// <value>
        ///     The battle stance.
        /// </value>
        public BattleStance BattleStance { get; }

        /// <summary>
        ///     Gets a value indicating whether [chase mode].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [chase mode]; otherwise, <c>false</c>.
        /// </value>
        public bool ChaseMode { get; }

        /// <summary>
        ///     Gets a value indicating whether [safe mode].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [safe mode]; otherwise, <c>false</c>.
        /// </value>
        public bool SafeMode { get; }
    }
}