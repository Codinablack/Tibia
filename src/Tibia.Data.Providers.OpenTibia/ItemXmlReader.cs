using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tibia.Items;

namespace Tibia.Data.Providers.OpenTibia
{
    public class ItemXmlReader
    {
        private readonly ItemService _itemService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ItemXmlReader" /> class.
        /// </summary>
        /// <param name="itemService">The item service.</param>
        public ItemXmlReader(ItemService itemService)
        {
            _itemService = itemService;
        }

        /// <summary>
        ///     Asynchronously loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The task.</returns>
        public async Task LoadAsync(string fileName)
        {
            await Task.Run(() => Load(fileName));
        }

        /// <summary>
        ///     Loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        private void Load(string fileName)
        {
            XDocument document = XDocument.Load(fileName);

            foreach (XElement element in document.Element("items").Elements("item"))
            {
                XAttribute startItem;
                if ((startItem = element.Attribute("fromid")) != null)
                {
                    XAttribute endItem;
                    if ((endItem = element.Attribute("toid")) != null)
                    {
                        uint startItemId = uint.Parse(startItem.Value);
                        uint endItemId = uint.Parse(endItem.Value);
                        for (uint itemId = startItemId; itemId < endItemId; itemId++)
                            ParseItem(itemId, element);

                        continue;
                    }
                }

                XAttribute item;
                if ((item = element.Attribute("id")) != null)
                    ParseItem(uint.Parse(item.Value), element);
            }
        }

        /// <summary>
        ///     Parses the item.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        /// <param name="element">The element.</param>
        private void ParseItem(uint itemId, XElement element)
        {
            IItem item = _itemService.GetItemById(itemId);

            // TODO: This should throw an exception because it means it's a different format
            if (item == null)
                return;

            item.Name = element.Attribute("name").Value;

            foreach (XElement attribute in element.Elements("attribute"))
            {
                switch (attribute.Attribute("key").Value)
                {
                    case "floorchange":
                        item.FloorChangeDirection = ParseFloorChangeDirection(attribute.Attribute("value").Value);
                        break;
                }
            }
        }

        /// <summary>
        ///     Parses the floor change direction.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        private FloorChangeDirection ParseFloorChangeDirection(string value)
        {
            switch (value)
            {
                case "up":
                    return FloorChangeDirection.Up;
                case "down":
                    return FloorChangeDirection.Down;
                case "north":
                    return FloorChangeDirection.North;
                case "south":
                    return FloorChangeDirection.South;
                case "east":
                    return FloorChangeDirection.East;
                case "west":
                    return FloorChangeDirection.West;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }
    }
}