using System.Collections.Generic;
using System.Linq;
using Tibia.Data;
using Tibia.Data.Services;

namespace Tibia.Map
{
    public class TileService : IService
    {
        private readonly IDictionary<IVector3, ICollection<ICharacterSpawn>> _characterSpawnsByPosition;
        private readonly IDictionary<IVector3, ICollection<ICreatureSpawn>> _creatureSpawnsByPosition;
        private readonly IDictionary<IVector3, ITile> _tileByPosition;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TileService" /> class.
        /// </summary>
        /// <param name="tiles">The tiles.</param>
        public TileService(IEnumerable<ITile> tiles)
        {
            _creatureSpawnsByPosition = new Dictionary<IVector3, ICollection<ICreatureSpawn>>();
            _characterSpawnsByPosition = new Dictionary<IVector3, ICollection<ICharacterSpawn>>();
            _tileByPosition = tiles.ToDictionary(s => s.Position);
        }

        /// <summary>
        ///     Gets the tile by position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>The tile.</returns>
        public ITile GetTileByPosition(IVector3 position)
        {
            return _tileByPosition.ContainsKey(position) ? _tileByPosition[position] : null;
        }

        /// <summary>
        ///     Gets the spectators.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="range">The range.</param>
        /// <returns>The spectators.</returns>
        public IEnumerable<ICharacterSpawn> GetSpectators(IVector3 position, IVector3 range)
        {
            int startX = position.X - range.X;
            int endX = position.X + range.X;
            int startY = position.Y - range.Y;
            int endY = position.Y + range.Y;
            int startZ = position.Z - range.Z;
            int endZ = position.Z + range.Z;

            for (int nx = startX; nx < endX; nx++)
            for (int ny = startY; ny < endY; ny++)
            for (int nz = startZ; nz < endZ; nz++)
            {
                IVector3 spectatorPosition = new Vector3(nx, ny, nz);
                if (!_characterSpawnsByPosition.ContainsKey(spectatorPosition))
                    continue;

                foreach (ICharacterSpawn spectator in _characterSpawnsByPosition[spectatorPosition])
                    yield return spectator;
            }
        }

        /// <summary>
        ///     Gets the spectators.
        /// </summary>
        /// <param name="positions">The positions.</param>
        /// <param name="range">The range.</param>
        /// <returns>The spectators.</returns>
        public IEnumerable<ICharacterSpawn> GetSpectators(IEnumerable<IVector3> positions, IVector3 range)
        {
            return positions.SelectMany(position => GetSpectators(position, range)).Distinct();
        }

        /// <summary>
        ///     Gets the creatures by position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>The creatures.</returns>
        public IEnumerable<ICreatureSpawn> GetCreaturesByPosition(IVector3 position)
        {
            if (!_creatureSpawnsByPosition.ContainsKey(position))
                yield break;
            foreach (ICreatureSpawn creatureSpawn in _creatureSpawnsByPosition[position])
                yield return creatureSpawn;
        }

        /// <summary>
        ///     Registers the creature.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        public void RegisterCreature(ICreatureSpawn creatureSpawn)
        {
            if (!_creatureSpawnsByPosition.ContainsKey(creatureSpawn.Tile.Position))
                _creatureSpawnsByPosition.Add(creatureSpawn.Tile.Position, new HashSet<ICreatureSpawn>());
            _creatureSpawnsByPosition[creatureSpawn.Tile.Position].Add(creatureSpawn);

            if (creatureSpawn is ICharacterSpawn characterSpawn)
            {
                if (!_characterSpawnsByPosition.ContainsKey(creatureSpawn.Tile.Position))
                    _characterSpawnsByPosition.Add(creatureSpawn.Tile.Position, new HashSet<ICharacterSpawn>());
                _characterSpawnsByPosition[creatureSpawn.Tile.Position].Add(characterSpawn);
            }
        }

        /// <summary>
        ///     Unregisters the creature.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        public void UnregisterCreature(ICreatureSpawn creatureSpawn)
        {
            _creatureSpawnsByPosition[creatureSpawn.Tile.Position].Remove(creatureSpawn);

            if (creatureSpawn is ICharacterSpawn characterSpawn)
                _characterSpawnsByPosition[creatureSpawn.Tile.Position].Remove(characterSpawn);
        }
    }
}