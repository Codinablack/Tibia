using System.Collections.Generic;
using System.Linq;
using Tibia.Data;
using Tibia.Spawns;

namespace Tibia.Parties
{
    public static class PartyExtensions
    {
        /// <summary>
        ///     Determines whether this instance can use shared experience with the specified member.
        /// </summary>
        /// <param name="party">The party.</param>
        /// <param name="member">The member.</param>
        /// <returns>
        ///     <c>true</c> if this instance can use shared experience withthe specified member; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanUseSharedExp(this IParty party, CharacterSpawn member)
        {
            if (party.Members.Count == 0)
                return false;

            if (!party.IsMemberActive(member))
                return false;

            return party.IsInsideLevelRange(member) && party.IsInsideSharedExpRange(member);
        }

        /// <summary>
        ///     Determines whether the specified member is inside level range.
        /// </summary>
        /// <param name="party">The party.</param>
        /// <param name="member">The member.</param>
        /// <returns>
        ///     <c>true</c> if the specified member is inside level range; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInsideLevelRange(this IParty party, CharacterSpawn member)
        {
            int highestLevel = party.GetHighestLevel();
            int maxLevelDifference = party.GetMaxLevelDifference(highestLevel);
            int minimumLevel = highestLevel - maxLevelDifference;

            return member.Level.Current >= minimumLevel;
        }

        /// <summary>
        ///     Gets the experience bonus.
        /// </summary>
        /// <param name="party">The party.</param>
        /// <returns>The experience bonus.</returns>
        public static Percent GetExpBonus(this IParty party)
        {
            HashSet<IVocation> vocations = new HashSet<IVocation>();

            if (party.Leader.Vocation != null)
                vocations.Add(party.Leader.Vocation);

            foreach (ICharacterSpawn member in party.Members)
                vocations.Add(member.Vocation);

            if (vocations.Count > 1)
                return party.SharedExpMultipleVocationsBonus;

            return Percent.MinValue;
        }

        /// <summary>
        ///     Gets the highest level.
        /// </summary>
        /// <param name="party">The party.</param>
        /// <returns>The level.</returns>
        public static int GetHighestLevel(this IParty party)
        {
            int highestLevel = party.Leader.Level.Current;
            foreach (CharacterSpawn member in party.Members.Cast<CharacterSpawn>())
            {
                if (member.Level.Current <= highestLevel)
                    continue;

                highestLevel = member.Level.Current;
            }

            return highestLevel;
        }

        /// <summary>
        ///     Gets the maximum level difference.
        /// </summary>
        /// <param name="party">The party.</param>
        /// <param name="highestLevel">The highest level.</param>
        /// <returns>The maximum level difference.</returns>
        public static int GetMaxLevelDifference(this IParty party, int highestLevel)
        {
            // TODO: This could probably be replaced with a method in the Percent class
            return highestLevel * party.SharedExpLevel.Value / 100;
        }

        /// <summary>
        ///     Determines whether the specified member is inside shared experience range.
        /// </summary>
        /// <param name="party">The party.</param>
        /// <param name="member">The member.</param>
        /// <returns>
        ///     <c>true</c> if the specified member is inside shared experience range; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInsideSharedExpRange(this IParty party, CharacterSpawn member)
        {
            if (party.Leader.Tile == null || member.Tile == null)
                return false;

            return party.Leader.Tile.Position.IsInRange(member.Tile.Position, party.SharedExpMaximumRange);
        }

        /// <summary>
        ///     Determines whether the specified member is active.
        /// </summary>
        /// <param name="party">The party.</param>
        /// <param name="member">The member.</param>
        /// <returns>
        ///     <c>true</c> if the specified member is active; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsMemberActive(this IParty party, CharacterSpawn member)
        {
            // TODO: Check if member is in combat
            return true;
        }
    }
}