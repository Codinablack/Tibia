using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tibia.Items;

namespace Tibia.Data.Providers.OpenTibia
{
    public class ItemReader : ItemFileReader, IDisposable
    {
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
        /// <returns>The collection of items.</returns>
        public Task<IEnumerable<IItem>> LoadAsync(string fileName)
        {
            return Task.Run(() => Load(fileName));
        }

        /// <summary>
        ///     Loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The collection of items.</returns>
        /// <exception cref="FileFormatException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private IEnumerable<IItem> Load(string fileName)
        {
            OpenFile(fileName);

            // File version
            Reader.ReadUInt32();

            // Initialize
            InitializeRoot();

            //if (!ParseNode(Root))
            //    throw new FileFormatException("Could not parse root node.");
            // TODO: This should probably throw an exception if the return is 'false'(?)
            ParseNode(Root);

            Node node = GetRootNode();
            if (!ReadProperty(node, out ItemStreamReader itemStreamReader))
                throw new FileFormatException("Could not parse root node properties.");

            // First byte of OTB is 0
            // TODO: Use propertyReader.Skip(byte.size); - byte.size is unknown
            itemStreamReader.ReadByte();

            // Flags (4 bytes) unused
            itemStreamReader.ReadUInt32();

            RootAttribute rootAttribute = (RootAttribute) itemStreamReader.ReadByte();
            if (rootAttribute != RootAttribute.Version)
                throw new FileFormatException("Could not find item version.");

            ItemsMetadata metadata = itemStreamReader.ReadMetadata();
            if (metadata == null)
                throw new FileFormatException("Could not read OTBI metadata.");

            Node childNode = node.Child;
            while (childNode != null)
            {
                if (!ReadProperty(childNode, out ItemStreamReader propertyReader))
                    throw new FileFormatException("Could not parse waypoint properties.");

                IItem item = new Item();
                item.GroupType = (ItemGroupType)childNode.Type;

                ItemFlags itemFlags = (ItemFlags) propertyReader.ReadUInt32();

                if (itemFlags.HasFlag(ItemFlags.Stackable))
                    item.IsStackable = true;

                if (itemFlags.HasFlag(ItemFlags.Pickupable))
                    item.IsPickupable = true;

                if (itemFlags.HasFlag(ItemFlags.SolidBlock))
                    item.IsSolidBlock = true;

                if (itemFlags.HasFlag(ItemFlags.ProjectileBlocker))
                    item.IsProjectileBlocker = true;

                if (itemFlags.HasFlag(ItemFlags.PathBlocker))
                    item.IsPathBlocker = true;

                if (itemFlags.HasFlag(ItemFlags.HasHeight))
                    item.HasHeight = true;

                if (itemFlags.HasFlag(ItemFlags.Useable))
                    item.IsUseable = true;

                if (itemFlags.HasFlag(ItemFlags.Moveable))
                    item.IsMoveable = true;

                if (itemFlags.HasFlag(ItemFlags.AlwaysOnTop))
                    item.IsAlwaysOnTop = true;

                if (itemFlags.HasFlag(ItemFlags.Vertical))
                    item.IsVertical = true;

                if (itemFlags.HasFlag(ItemFlags.Horizontal))
                    item.IsHorizontal = true;

                if (itemFlags.HasFlag(ItemFlags.Hangable))
                    item.IsHangable = true;

                if (itemFlags.HasFlag(ItemFlags.DistanceReadable))
                    item.IsDistanceReadable = true;

                if (itemFlags.HasFlag(ItemFlags.Rotatable))
                    item.IsRotatable = true;

                if (itemFlags.HasFlag(ItemFlags.Readable))
                    item.IsReadable = true;

                if (itemFlags.HasFlag(ItemFlags.LookThrough))
                    item.IsLookThrough = true;

                if (itemFlags.HasFlag(ItemFlags.Animation))
                    item.IsAnimation = true;

                if (itemFlags.HasFlag(ItemFlags.ForceUse))
                    item.IsForceUse = true;

                while (propertyReader.PeekChar() != -1)
                {
                    ItemAttribute attribute = (ItemAttribute) propertyReader.ReadByte();
                    ushort dataLength = propertyReader.ReadUInt16();
                    switch (attribute)
                    {
                        case ItemAttribute.Id:
                            ushort serverId = propertyReader.ReadUInt16();
                            if (serverId > 30000 && serverId < 30100)
                                serverId -= 30000;
                            item.Id = serverId;
                            break;
                        case ItemAttribute.SpriteId:
                            item.SpriteId = propertyReader.ReadUInt16();
                            break;
                        case ItemAttribute.Speed:
                            item.Speed = propertyReader.ReadUInt16();
                            break;
                        case ItemAttribute.TopOrder:
                            // TODO: item.TileStackOrder = propertyReader.ReadTileStackOrder();
                            propertyReader.Skip(dataLength);
                            // propertyRader.ReadByte();
                            break;
                        case ItemAttribute.WareId:
                            // TODO: This should probably use a service to have a reference (e.g: item.WriteOnceItem [IItem])
                            item.WriteOnceItemId = propertyReader.ReadUInt16();
                            break;
                        case ItemAttribute.Name:
                            // TODO: item.Name = propertyReader.ReadString();
                            propertyReader.Skip(dataLength);
                            //propertyReader.ReadString();
                            break;
                        case ItemAttribute.SpriteHash:
                            // TODO: item.SpriteHash = propertyReader.ReadBytes(datalen);
                            propertyReader.Skip(dataLength);
                            //propertyReader.ReadBytes(dataLength);
                            break;
                        case ItemAttribute.Light2:
                            // TODO: item.LightLevel = propertyReader.ReadUInt16();
                            // TODO: item.LightColor = propertyReader.ReadUInt16();
                            propertyReader.Skip(dataLength);
                            //propertyReader.ReadUInt16();
                            //propertyReader.ReadUInt16();
                            break;
                        case ItemAttribute.MinimapColor:
                            // TODO: item.MinimapColor = propertyReader.ReadUInt16();
                            propertyReader.Skip(dataLength);
                            //propertyReader.ReadUInt16();
                            break;
                        case ItemAttribute.Unknown1:
                            throw new NotImplementedException();
                        case ItemAttribute.Unknown2:
                            throw new NotImplementedException();
                        case ItemAttribute.Unknown3:
                            throw new NotImplementedException();
                        case ItemAttribute.Unknown4:
                            throw new NotImplementedException();
                        case ItemAttribute.Unknown5:
                            throw new NotImplementedException();
                        case ItemAttribute.Unknown6:
                            throw new NotImplementedException();
                        case ItemAttribute.Unknown7:
                            throw new NotImplementedException();
                        case ItemAttribute.Unknown8:
                            throw new NotImplementedException();
                        case ItemAttribute.Unknown9:
                            throw new NotImplementedException();
                        case ItemAttribute.Unknown10:
                            throw new NotImplementedException();
                        case ItemAttribute.Unknown11:
                            throw new NotImplementedException();
                        case ItemAttribute.Unknown12:
                            throw new NotImplementedException();
                        case ItemAttribute.Unknown13:
                            throw new NotImplementedException();
                        case ItemAttribute.Unknown14:
                            throw new NotImplementedException();
                        case ItemAttribute.Unknown15:
                            throw new NotImplementedException();
                        case ItemAttribute.Unknown16:
                            throw new NotImplementedException();
                        case ItemAttribute.Description:
                            throw new NotImplementedException();
                        case ItemAttribute.Slot:
                            throw new NotImplementedException();
                        case ItemAttribute.MaxItems:
                            throw new NotImplementedException();
                        case ItemAttribute.Weight:
                            throw new NotImplementedException();
                        case ItemAttribute.Weapon:
                            throw new NotImplementedException();
                        case ItemAttribute.Ammo:
                            throw new NotImplementedException();
                        case ItemAttribute.Armor:
                            throw new NotImplementedException();
                        case ItemAttribute.Magiclevel:
                            throw new NotImplementedException();
                        case ItemAttribute.MagicFieldType:
                            throw new NotImplementedException();
                        case ItemAttribute.Writeable:
                            throw new NotImplementedException();
                        case ItemAttribute.Rotateable:
                            throw new NotImplementedException();
                        case ItemAttribute.Decay:
                            throw new NotImplementedException();
                        case ItemAttribute.Unknown17:
                            propertyReader.Skip(dataLength);
                            break;
                        case ItemAttribute.Unknown18:
                            propertyReader.Skip(dataLength);
                            break;
                        case ItemAttribute.Light:
                            throw new NotImplementedException();
                        case ItemAttribute.Decay2:
                            throw new NotImplementedException();
                        case ItemAttribute.Weapon2:
                            throw new NotImplementedException();
                        case ItemAttribute.Ammo2:
                            throw new NotImplementedException();
                        case ItemAttribute.Armor2:
                            throw new NotImplementedException();
                        case ItemAttribute.Writeable2:
                            throw new NotImplementedException();
                        case ItemAttribute.Writeable3:
                            throw new NotImplementedException();
                        default:
                            // TODO: Review whether this is necessary propertyReader.Skip(dataLength);
                            throw new ArgumentOutOfRangeException(nameof(attribute), attribute, "OTBI attribute is not supported.");
                    }
                }

                yield return item;
                childNode = childNode.Next;
            }
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
    }
}