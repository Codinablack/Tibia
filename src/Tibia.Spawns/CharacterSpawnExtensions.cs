using Tibia.Data;

namespace Tibia.Spawns
{
    public static class CharacterSpawnExtensions
    {
        /// <summary>
        ///     Determines whether this instance can see the specified creature spawn.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="creatureSpawn">The creature spawn.</param>
        /// <returns>
        ///     <c>true</c> if this instance can see the specified creature spawn; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanSee(this ICharacterSpawn self, ICreatureSpawn creatureSpawn)
        {
            if (self == creatureSpawn)
                return true;

            // TODO: Ghost Mode
            //if (creature.isInGhostMode() && !Group.UnrestrictedAccess)
            //    return false;

            // TODO: Implement invisibility immunity
            return !creatureSpawn.IsInvisible && ((ICreatureSpawn) self).CanSee(creatureSpawn);
        }
    }
}