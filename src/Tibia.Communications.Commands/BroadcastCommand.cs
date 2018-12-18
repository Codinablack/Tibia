using Tibia.Data;
using Tibia.Spawns;

namespace Tibia.Communications.Commands
{
    public class BroadcastCommand : CommandBase
    {
        private readonly CreatureSpawnService _creatureSpawnService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BroadcastCommand" /> class.
        /// </summary>
        /// <param name="creatureSpawnService">The creature spawn service.</param>
        public BroadcastCommand(CreatureSpawnService creatureSpawnService)
        {
            _creatureSpawnService = creatureSpawnService;
        }

        /// <summary>
        ///     Gets the keyword.
        /// </summary>
        /// <value>
        ///     The keyword.
        /// </value>
        public override string Keyword { get; } = "/b";
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
            return !caster.Creature.Settings.CanBroadcast;
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
            return caster.Creature.Settings.CanBroadcast;
        }
        /// <summary>
        ///     Executes the command.
        /// </summary>
        /// <param name="caster">The caster.</param>
        /// <param name="args">The arguments.</param>
        protected override void ExecuteCommand(ICharacterSpawn caster, string[] args)
        {
            if (args.Length == 0)
                return;

            foreach (ICharacterSpawn characterSpawn in _creatureSpawnService.CharacterSpawns)
                characterSpawn.Connection.SendTextMessage(TextMessageType.StatusWarning, string.Join(" ", args));
        }
    }
}