using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Tibia.Collections.Generic;
using Tibia.Items;
using Tibia.Map;
using Tibia.Spawns;

namespace Tibia.Data.Providers.OpenTibia
{
    public class MapReader : MapFileReader, IDisposable
    {
        private readonly ItemService _itemService;
        private readonly TileService _tileService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MapReader" /> class.
        /// </summary>
        /// <param name="itemService">The item service.</param>
        /// <param name="tileService">The tile service.</param>
        public MapReader(ItemService itemService, TileService tileService)
        {
            _itemService = itemService;
            _tileService = tileService;
        }
        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Reader?.Dispose();
            FileStream?.Dispose();
        }

        /// <summary>
        ///     Asynchronously loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The map.</returns>
        public Task<WorldMap> LoadAsync(string fileName)
        {
            return Task.Run(() => Load(fileName));
        }

        /// <summary>
        ///     Loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The map.</returns>
        /// <exception cref="FileFormatException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private WorldMap Load(string fileName)
        {
            string workingFolder = Path.GetDirectoryName(fileName);

            OpenFile(fileName);

            // File version
            Reader.ReadUInt32();

            // Initialize
            InitializeRoot();

            if (Reader.ReadByte() != NodeTags.StartTag)
                throw new FileFormatException("Could not find start node.");

            //if (!ParseNode(Root))
            //    throw new FileFormatException("Could not parse root node.");
            // TODO: This should probably throw an exception if the return is 'false'(?)
            ParseNode(Root);

            Node node = GetRootNode();
            if (!ReadProperty(node, out MapStreamReader mapPropertyReader))
                throw new FileFormatException("Could not parse root node properties.");

            // First byte of OTB is 0
            // TODO: Use propertyReader.Skip(byte.size); - byte.size is unknown
            mapPropertyReader.ReadByte();
            MapMetadata metadata = mapPropertyReader.ReadMetadata();
            if (metadata == null)
                throw new FileFormatException("Could not read OTBM metadata.");

            WorldMap map = new WorldMap
            {
                Width = metadata.Width,
                Height = metadata.Height
            };

            node = node.Child;
            if ((MapNodeType) node.Type != MapNodeType.MapData)
                throw new FileFormatException("Could not find map data node.");
            if (!ReadProperty(node, out mapPropertyReader))
                throw new FileFormatException("Could not parse OTBM attribute.");

            while (mapPropertyReader.PeekChar() != -1)
            {
                MapAttribute attribute = (MapAttribute) mapPropertyReader.ReadByte();
                switch (attribute)
                {
                    case MapAttribute.Description:
                        map.Description = mapPropertyReader.ReadString();
                        break;
                    case MapAttribute.ExtSpawnFile:
                        string spawnFile = mapPropertyReader.ReadString();
                        map.SpawnFile = Path.Combine(workingFolder, spawnFile);
                        break;
                    case MapAttribute.ExtHouseFile:
                        string houseFile = mapPropertyReader.ReadString();
                        map.HouseFile = Path.Combine(workingFolder, houseFile);
                        break;
                    default:
                        throw new ArgumentException($"OTBM attribute '{attribute}' is not supported.");
                }
            }

            Node childNode = node.Child;
            while (childNode != null)
            {
                MapNodeType nodeType = (MapNodeType) childNode.Type;
                switch (nodeType)
                {
                    case MapNodeType.Tiles:
                        if (!ParseTiles(childNode, out IDictionary<IVector3, ITile> tiles))
                            throw new FileFormatException("Could not parse tiles.");
                        map.Tiles.AddRange(tiles);
                        break;
                    case MapNodeType.Towns:
                        if (!TryParseTowns(childNode, out IDictionary<uint, ITown> towns))
                            throw new FileFormatException("Could not parse towns.");
                        map.Towns.AddRange(towns);
                        break;
                    case MapNodeType.Waypoints:
                        if (!TryParseWaypoints(childNode, out IDictionary<string, IWaypoint> waypoints))
                            throw new FileFormatException("Could not parse waypoints.");
                        map.Waypoints.AddRange(waypoints);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(nodeType), nodeType, "OTBM node type is not supported.");
                }

                childNode = childNode.Next;
            }

            return map;
        }

        /// <summary>
        ///     Initializes the root.
        /// </summary>
        private void InitializeRoot()
        {
            Root = new Node
            {
                Start = 4
            };
        }

        /// <summary>
        ///     Tries to parse the waypoints.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="waypoints">The waypoints.</param>
        /// <returns>Whether the waypoints were parsed successfully.</returns>
        /// <exception cref="FileFormatException"></exception>
        private bool TryParseWaypoints(Node node, out IDictionary<string, IWaypoint> waypoints)
        {
            waypoints = new Dictionary<string, IWaypoint>();

            Node childNode = node.Child;
            while (childNode != null)
            {
                if ((MapNodeType) childNode.Type != MapNodeType.Waypoint)
                    throw new FileFormatException("Could not find waypoint node.");
                if (!ReadProperty(childNode, out MapStreamReader propertyReader))
                    throw new FileFormatException("Could not parse waypoint properties.");

                // TODO: This should throw an exception, waypoints without a name must not be allowed.
                string name = propertyReader.ReadString();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    if (!waypoints.ContainsKey(name))
                    {
                        Vector3 position = propertyReader.ReadVector3();
                        if (position == null)
                            throw new FileFormatException("Could not parse waypoint location (vector3).");

                        Waypoint waypoint = new Waypoint(name, position);
                        waypoints.Add(waypoint.Name, waypoint);
                    }
                }

                childNode = childNode.Next;
            }
            return true;
        }

        /// <summary>
        ///     Tries the parse towns.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="towns">The towns.</param>
        /// <returns>Whether the towns were parsed successfully.</returns>
        /// <exception cref="FileFormatException"></exception>
        private bool TryParseTowns(Node node, out IDictionary<uint, ITown> towns)
        {
            towns = new Dictionary<uint, ITown>();

            Node childNode = node.Child;
            while (childNode != null)
            {
                if ((MapNodeType) childNode.Type != MapNodeType.Town)
                    throw new FileFormatException("Could not find town node.");
                if (!ReadProperty(childNode, out MapStreamReader propertyReader))
                    throw new FileFormatException("Could not parse town properties.");

                uint townId = propertyReader.ReadUInt32();
                if (townId <= 0)
                    throw new FileFormatException("Could not parse town ID.");

                if (!towns.ContainsKey(townId))
                {
                    string townName = propertyReader.ReadString();
                    if (string.IsNullOrWhiteSpace(townName))
                        throw new FileFormatException("Could not parse town name.");

                    Vector3 templePosition = propertyReader.ReadVector3();
                    if (templePosition == null)
                        throw new FileFormatException("Could not find town's temple location (vector3).");

                    Town town = new Town(townId, townName, templePosition);
                    towns.Add(town.Id, town);
                }

                childNode = childNode.Next;
            }
            return true;
        }

        /// <summary>
        ///     Parses the tiles.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="tiles">The tiles.</param>
        /// <returns>Whether the tiles are parsed successfully.</returns>
        /// <exception cref="FileFormatException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private bool ParseTiles(Node node, out IDictionary<IVector3, ITile> tiles)
        {
            if (!ReadProperty(node, out MapStreamReader propertyReader))
                throw new FileFormatException("Could not parse tile area properties.");

            Vector3 basePosition = propertyReader.ReadVector3();
            tiles = new Dictionary<IVector3, ITile>();

            Node childNode = node.Child;
            while (childNode != null)
            {
                if (childNode.Type != (long) MapNodeType.Tile && childNode.Type != (long) MapNodeType.HouseTile)
                    throw new FileFormatException("Could not find tile node.");
                if (!ReadProperty(childNode, out propertyReader))
                    throw new FileFormatException("Could not parse tile vector2 properties.");

                Vector2 positionOffset = propertyReader.ReadVector2();
                Vector3 tilePosition = new Vector3(basePosition.X + positionOffset.X, basePosition.Y + positionOffset.Y, basePosition.Z);
                if (!tiles.ContainsKey(tilePosition))
                {
                    Tile tile = new Tile
                    {
                        Position = tilePosition
                    };
                    tiles.Add(tile.Position, tile);

                    if (childNode.Type == (long) MapNodeType.HouseTile)
                    {
                        uint houseId = propertyReader.ReadUInt32();
                        if (houseId == 0)
                            throw new FileFormatException($"Invalid house ID at position [x: {tile.Position.X}, y: {tile.Position.Y}, z: {tile.Position.Z}].");

                        tile.HouseId = houseId;
                    }

                    // Tile attributes
                    while (propertyReader.PeekChar() != -1)
                    {
                        MapAttribute attribute = (MapAttribute) propertyReader.ReadByte();
                        switch (attribute)
                        {
                            case MapAttribute.TileFlags:
                                uint flags = propertyReader.ReadUInt32();
                                if (flags == 0)
                                    throw new FileFormatException("Invalid tile flags.");
                                tile.Flags = (TileFlags) flags;
                                break;
                            case MapAttribute.TileItem:
                                ushort itemId = propertyReader.ReadUInt16();
                                if (itemId == 0)
                                    throw new FileFormatException($"Invalid item ID at position [x: {tile.Position.X}, y: {tile.Position.Y}, z: {tile.Position.Z}].");

                                AddTileItem(tile, itemId);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(attribute), attribute, "OTBM attribute is not supported.");
                        }
                    }

                    // Tile items
                    Node nodeItem = childNode.Child;
                    while (nodeItem != null)
                    {
                        if (nodeItem.Type != (long) MapNodeType.Item)
                            throw new FileFormatException("Could not find item node.");
                        if (!ReadProperty(nodeItem, out propertyReader))
                            throw new FileFormatException("Could not parse item properties.");

                        ushort itemId = propertyReader.ReadUInt16();
                        if (itemId == 0)
                            throw new FileFormatException($"Invalid item ID at position [x: {tile.Position.X}, y: {tile.Position.Y}, z: {tile.Position.Z}].");

                        AddTileItem(tile, itemId);
                        nodeItem = nodeItem.Next;
                    }
                }

                childNode = childNode.Next;
            }
            return true;
        }

        /// <summary>
        ///     Adds the tile item.
        /// </summary>
        /// <param name="tile">The tile.</param>
        /// <param name="itemId">The item identifier.</param>
        private void AddTileItem(ITile tile, ushort itemId)
        {
            IItem item = _itemService.GetItemById(itemId);

            if (item.GroupType == ItemGroupType.Ground && tile.Ground == null)
            {
                _tileService.AddGround(new ItemSpawn
                {
                    Item = item,
                    Tile = tile
                });
                return;
            }

            _tileService.AddItem(new ItemSpawn
            {
                Item = item,
                Tile = tile
            });
        }
    }
}