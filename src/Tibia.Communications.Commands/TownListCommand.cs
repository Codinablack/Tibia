using System.Linq;
using Tibia.Data;
using Tibia.Map;

namespace Tibia.Communications.Commands
{
    public class TownListCommand : CommandBase
    {
        private readonly TownService _townService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TownListCommand" /> class.
        /// </summary>
        /// <param name="townService">The town service.</param>
        public TownListCommand(TownService townService)
        {
            _townService = townService;
        }

        /// <summary>
        ///     Gets the keyword.
        /// </summary>
        /// <value>
        ///     The keyword.
        /// </value>
        public override string Keyword { get; } = "/towns";
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
            return !caster.Creature.Settings.CanSeeDiagnostics;
        }
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
        /// <summary>
        ///     Executes the command.
        /// </summary>
        /// <param name="caster">The caster.</param>
        /// <param name="args">The arguments.</param>
        protected override void ExecuteCommand(ICharacterSpawn caster, string[] args)
        {
            caster.Connection.SendTextMessage(TextMessageType.InformationDescription, string.Join("\n", _townService.Towns.Select(s => s.Name)));
        }
    }
}