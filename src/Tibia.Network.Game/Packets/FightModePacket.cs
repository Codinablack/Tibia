using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class FightModePacket : IClientPacket
    {
        /// <summary>
        ///     Gets or sets the battle stance.
        /// </summary>
        /// <value>
        ///     The battle stance.
        /// </value>
        public BattleStance BattleStance { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [chase mode].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [chase mode]; otherwise, <c>false</c>.
        /// </value>
        public bool ChaseMode { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [safe mode].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [safe mode]; otherwise, <c>false</c>.
        /// </value>
        public bool SafeMode { get; set; }

        /// <summary>
        ///     Parses the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The packet.</returns>
        public static FightModePacket Parse(NetworkMessage message)
        {
            FightModePacket packet = new FightModePacket();
            packet.BattleStance = message.ReadBattleStance();
            packet.ChaseMode = message.ReadBoolean();
            packet.SafeMode = message.ReadBoolean();
            return packet;
        }
    }
}