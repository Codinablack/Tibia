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
        private readonly IDictionary<IVector3, ICollection<IItemSpawn>> _itemSpawnsByPosition;
        private readonly IDictionary<IVector3, ICollection<ISpawn>> _spawnsByPosition;
        private readonly IDictionary<IVector3, ITile> _tileByPosition;
        private readonly IDictionary<IVector3, IItemSpawn> _groundByPosition;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TileService" /> class.
        /// </summary>
        public TileService()
        {
            _creatureSpawnsByPosition = new Dictionary<IVector3, ICollection<ICreatureSpawn>>();
            _characterSpawnsByPosition = new Dictionary<IVector3, ICollection<ICharacterSpawn>>();
            _itemSpawnsByPosition = new Dictionary<IVector3, ICollection<IItemSpawn>>();
            _spawnsByPosition = new Dictionary<IVector3, ICollection<ISpawn>>();
            _tileByPosition = new Dictionary<IVector3, ITile>();
            _groundByPosition = new Dictionary<IVector3, IItemSpawn>();
        }

        /// <summary>
        ///     Adds the tile.
        /// </summary>
        /// <param name="tile">The tile.</param>
        public void AddTile(ITile tile)
        {
            if (_tileByPosition.ContainsKey(tile.Position))
                return;

            _tileByPosition.Add(tile.Position, tile);
        }

        /// <summary>
        ///     Gets the collection of spawns in the specified position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>The collection of spawns in the specified position.</returns>
        public IEnumerable<ISpawn> Spawns(IVector3 position)
        {
            if (!_spawnsByPosition.ContainsKey(position))
                yield break;

            foreach (ISpawn spawn in _spawnsByPosition[position])
                yield return spawn;
        }

        /// <summary>
        ///     Gets the tile by position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>The tile.</returns>
        public ITile Tile(IVector3 position)
        {
            return _tileByPosition.ContainsKey(position) ? _tileByPosition[position] : null;
        }

        /// <summary>
        ///     Gets the spectators.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="range">The range.</param>
        /// <returns>The spectators.</returns>
        public IEnumerable<ICharacterSpawn> Spectators(IVector3 position, IVector3 range)
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
        /// <param name="range">The range.</param>
        /// <param name="positions">The positions.</param>
        /// <returns>The spectators.</returns>
        public IEnumerable<ICharacterSpawn> Spectators(IVector3 range, params IVector3[] positions)
        {
            return positions.SelectMany(position => Spectators(position, range)).Distinct();
        }

        /// <summary>
        ///     Gets the creatures by position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>The creatures.</returns>
        public IEnumerable<ICreatureSpawn> Creatures(IVector3 position)
        {
            if (!_creatureSpawnsByPosition.ContainsKey(position))
                yield break;

            foreach (ICreatureSpawn creatureSpawn in _creatureSpawnsByPosition[position])
                yield return creatureSpawn;
        }

        /// <summary>
        ///     adds the creature.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        public void AddCreature(ICreatureSpawn creatureSpawn)
        {
            AddSpawn(creatureSpawn);

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
        ///     Adds the spawn.
        /// </summary>
        /// <param name="spawn">The spawn.</param>
        private void AddSpawn(ISpawn spawn)
        {
            if (!_spawnsByPosition.ContainsKey(spawn.Tile.Position))
                _spawnsByPosition.Add(spawn.Tile.Position, new HashSet<ISpawn>());
            _spawnsByPosition[spawn.Tile.Position].Add(spawn);
        }

        /// <summary>
        ///     Removes the creature.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        public void RemoveCreature(ICreatureSpawn creatureSpawn)
        {
            RemoveSpawn(creatureSpawn);

            _creatureSpawnsByPosition[creatureSpawn.Tile.Position].Remove(creatureSpawn);

            if (creatureSpawn is ICharacterSpawn characterSpawn)
                _characterSpawnsByPosition[creatureSpawn.Tile.Position].Remove(characterSpawn);
        }

        /// <summary>
        ///     Adds the item.
        /// </summary>
        /// <param name="itemSpawn">The item spawn.</param>
        public void AddItem(IItemSpawn itemSpawn)
        {
            AddSpawn(itemSpawn);

            if (!_itemSpawnsByPosition.ContainsKey(itemSpawn.Tile.Position))
                _itemSpawnsByPosition.Add(itemSpawn.Tile.Position, new HashSet<IItemSpawn>());
            _itemSpawnsByPosition[itemSpawn.Tile.Position].Add(itemSpawn);
        }

        /// <summary>
        ///     Removes the item.
        /// </summary>
        /// <param name="itemSpawn">The item spawn.</param>
        public void RemoveItem(IItemSpawn itemSpawn)
        {
            RemoveSpawn(itemSpawn);

            _itemSpawnsByPosition[itemSpawn.Tile.Position].Remove(itemSpawn);
        }

        /// <summary>
        ///     Remove the spawn.
        /// </summary>
        /// <param name="spawn">The spawn.</param>
        public void RemoveSpawn(ISpawn spawn)
        {
            _spawnsByPosition[spawn.Tile.Position].Remove(spawn);
        }

        /// <summary>
        ///     Registers the ground.
        /// </summary>
        /// <param name="itemSpawn">The item spawn.</param>
        public void AddGround(IItemSpawn itemSpawn)
        {
            if (itemSpawn == null)
                return;

            if (!_groundByPosition.ContainsKey(itemSpawn.Tile.Position))
                _groundByPosition.Add(itemSpawn.Tile.Position, itemSpawn);

            if (itemSpawn.Item.FloorChangeDirection != FloorChangeDirection.None)
                itemSpawn.Tile.FloorChangeDirection = itemSpawn.Item.FloorChangeDirection;
        }

        /// <summary>
        ///     Returns the collection of items in the specified position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>The collection of items in the specified position.</returns>
        public IEnumerable<IItemSpawn> Items(IVector3 position)
        {
            if (!_itemSpawnsByPosition.ContainsKey(position))
                yield break;

            foreach (IItemSpawn item in _itemSpawnsByPosition[position])
                yield return item;
        }
    }
}