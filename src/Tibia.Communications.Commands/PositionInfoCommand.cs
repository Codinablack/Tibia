using System.Text;
using Tibia.Data;

namespace Tibia.Communications.Commands
{
    public class PositionInfoCommand : CommandBase
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets the keyword.
        /// </summary>
        /// <value>
        ///     The keyword.
        /// </value>
        public override string Keyword { get; } = "/position";

        /// <inheritdoc />
        /// <summary>
        ///     Determines whether this instance can be executed by the caster.
        /// </summary>
        /// <param name="caster">The caster.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>
        ///     <see langword="true" /> if this instance can be executed by the specified caster; otherwise,
        ///     <see langword="false" />.
        /// </returns>
        public override bool CanExecute(ICharacterSpawn caster, params string[] args)
        {
            return caster.Creature.Settings.CanSeeDiagnostics;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets a value indicating whether the text should be posted in the channel.
        /// </summary>
        /// <param name="caster">The caster.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>
        ///     <see langword="true" /> if [post in channel]; otherwise, <see langword="false" />
        /// </returns>
        public override bool PostInChannel(ICharacterSpawn caster, params string[] args)
        {
            if (!caster.Creature.Settings.CanSeeDiagnostics)
                return false;

            ITile currentTile = caster.Tile;
            return currentTile?.Position != null && currentTile.Ground?.Item != null;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Executes the command.
        /// </summary>
        /// <param name="caster">The caster.</param>
        /// <param name="args">The arguments.</param>
        protected override void ExecuteCommand(ICharacterSpawn caster, string[] args)
        {
            ITile currentTile = caster.Tile;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("\n========== POSITION INFO ==========\n");
            stringBuilder.Append($"Position: {currentTile.Position.X}, {currentTile.Position.Y}, {currentTile.Position.Z}\n");
            stringBuilder.Append($"Ground: {(!string.IsNullOrWhiteSpace(currentTile.Ground.Item.Name) ? currentTile.Ground.Item.Name : "Undefined")}, Speed: {currentTile.Ground.Item.Speed ?? 0}\n");
            stringBuilder.Append($"Flags: {currentTile.Flags}");

            caster.Connection.SendTextMessage(TextMessageType.InformationDescription, stringBuilder.ToString());
        }
    }
}