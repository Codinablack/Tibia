using System;
using System.Collections.Generic;
using System.Linq;
using Tibia.Data;
using Tibia.Map;
using Tibia.Spawns;

namespace Tibia.Communications.Commands
{
    public class TeleportCommand : CommandBase
    {
        private readonly CreatureSpawnService _creatureSpawnService;
        private readonly TownService _townService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TeleportCommand" /> class.
        /// </summary>
        /// <param name="townService">The town service.</param>
        /// <param name="creatureSpawnService">The creature spawn service.</param>
        public TeleportCommand(TownService townService, CreatureSpawnService creatureSpawnService)
        {
            _townService = townService;
            _creatureSpawnService = creatureSpawnService;
        }

        /// <summary>
        ///     Gets the keyword.
        /// </summary>
        /// <value>
        ///     The keyword.
        /// </value>
        /// <inheritdoc />
        public override string Keyword { get; } = "/goto";

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
            return !caster.Creature.Settings.CanTeleport;
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
        /// <inheritdoc />
        public override bool CanExecute(ICharacterSpawn caster, params string[] args)
        {
            return caster.Creature.Settings.CanTeleport;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Executes the command.
        /// </summary>
        /// <param name="caster">The caster.</param>
        /// <param name="args">The arguments.</param>
        protected override void ExecuteCommand(ICharacterSpawn caster, string[] args)
        {
            if (args.Length == 0)
                return;

            string target = args.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(target))
                return;

            target = target.Trim().ToLower();
            IVector3 targetPosition;
            switch (target)
            {
                case "xyz":
                {
                    string position = args.Skip(1).FirstOrDefault();
                    if (string.IsNullOrWhiteSpace(position))
                        return;

                    string[] xyz = position.Split(',');
                    targetPosition = new Vector3(int.Parse(xyz[0].Trim()), int.Parse(xyz[1].Trim()), int.Parse(xyz[2].Trim()));
                    break;
                }
                case "temple":
                {
                    targetPosition = caster.Town.TemplePosition;
                    break;
                }
                case "town":
                {
                    string townName = args.Skip(1).FirstOrDefault();
                    if (string.IsNullOrWhiteSpace(townName))
                        return;

                    ITown town = _townService.GetTownByName(townName.Trim().ToLower());
                    targetPosition = town.TemplePosition;
                    break;
                }
                case "creature":
                {
                    string creatureName = args.Skip(1).FirstOrDefault();
                    if (string.IsNullOrWhiteSpace(creatureName))
                        return;

                    IEnumerable<ICreatureSpawn> creatureSpawns = _creatureSpawnService.GetCreatureSpawnsByName(creatureName.Trim().ToLower());
                    ICreatureSpawn firstMatch = creatureSpawns.FirstOrDefault();
                    if (firstMatch == null)
                    {
                        caster.Connection.SendTextMessage(TextMessageType.StatusSmall, "A creature with this name does not exist.");
                        return;
                    }

                    targetPosition = firstMatch.Tile.Position;
                    break;
                }
                case "up":
                {
                    IVector3 currentPosition = caster.Tile.Position;
                    targetPosition = new Vector3(currentPosition.X, currentPosition.Y, currentPosition.Z - 1);
                    break;
                }
                case "down":
                {
                    IVector3 currentPosition = caster.Tile.Position;
                    targetPosition = new Vector3(currentPosition.X, currentPosition.Y, currentPosition.Z + 1);
                    break;
                }
                case "north":
                {
                    IVector3 currentPosition = caster.Tile.Position;
                    targetPosition = new Vector3(currentPosition.X, currentPosition.Y - 1, currentPosition.Z);
                    break;
                }
                case "south":
                {
                    IVector3 currentPosition = caster.Tile.Position;
                    targetPosition = new Vector3(currentPosition.X, currentPosition.Y + 1, currentPosition.Z);
                    break;
                }
                case "east":
                {
                    IVector3 currentPosition = caster.Tile.Position;
                    targetPosition = new Vector3(currentPosition.X + 1, currentPosition.Y, currentPosition.Z);
                    break;
                }
                case "west":
                {
                    IVector3 currentPosition = caster.Tile.Position;
                    targetPosition = new Vector3(currentPosition.X - 1, currentPosition.Y, currentPosition.Z);
                    break;
                }
                default: { throw new ArgumentOutOfRangeException(nameof(target), target, null); }
            }

            caster.Connection.MoveCreature(caster, targetPosition);
            caster.Connection.SendEffect(targetPosition, Effect.Teleport);
        }
    }
}