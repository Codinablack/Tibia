using Tibia.Data;
using Tibia.Spawns;

namespace Tibia.Parties
{
    public static class CharacterSpawnExtensions
    {
        /// <summary>
        ///     Gets the party shield.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="member">The member.</param>
        /// <returns>The party shield.</returns>
        public static PartyShield GetPartyShield(this ICharacterSpawn self, CharacterSpawn member)
        {
            if (member?.Party == null)
                return PartyShield.None;

            if (self.Party != null)
            {
                if (self.Party.Leader == member)
                {
                    if (!self.Party.IsActivelySharingExp)
                        return PartyShield.Leader;

                    if (self.Party.HasSharedExpEnabled)
                        return PartyShield.LeaderSharedExp;

                    if (self.Party.CanUseSharedExp(member))
                        return PartyShield.LeaderSharedExpInactive;

                    return PartyShield.LeaderNoSharedExp;
                }

                if (self.Party == member.Party)
                {
                    if (!self.Party.IsActivelySharingExp)
                        return PartyShield.Member;

                    if (self.Party.HasSharedExpEnabled)
                        return PartyShield.MemberSharedExp;

                    if (self.Party.CanUseSharedExp(member))
                        return PartyShield.MemberSharedExpInactive;

                    return PartyShield.MemberNoSharedExp;
                }

                if (member.PartyInvitations.Contains(self.Party))
                    return PartyShield.Guest;

                return PartyShield.None;
            }

            if (member.Party.Leader == member && self.PartyInvitations.Contains(member.Party))
                return PartyShield.Host;

            return PartyShield.OtherPartyMember;
        }
    }
}