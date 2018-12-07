using Tibia.Data;

namespace Tibia.Communications.Commands
{
    public class ChangeSexCommand : CommandBase
    {
        /// <summary>
        ///     Gets the keyword.
        /// </summary>
        /// <value>
        ///     The keyword.
        /// </value>
        /// <inheritdoc />
        public override string Keyword { get; } = "/changesex";

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
            return !caster.Creature.Settings.CanChangeSex;
        }

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
            return caster.Creature.Settings.CanChangeSex;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Executes the command.
        /// </summary>
        /// <param name="caster">The caster.</param>
        /// <param name="args"></param>
        protected override void ExecuteCommand(ICharacterSpawn caster, string[] args)
        {
            caster.Sex = caster.Sex == Sex.Female ? Sex.Male : Sex.Female;
            caster.Connection.SendEffect(caster.Tile.Position, Effect.GiftWraps);
        }
    }
}