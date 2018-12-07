using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tibia.Outfits;

namespace Tibia.Data.Providers.Ultimate
{
    public class DrkOutfitReader
    {
        /// <summary>
        ///     Asynchronously loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The collection of outfits.</returns>
        public Task<IEnumerable<IOutfit>> LoadAsync(string fileName)
        {
            return Task.Run(() => Load(fileName));
        }

        /// <summary>
        ///     Loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The collection of outfits.</returns>
        private IEnumerable<IOutfit> Load(string fileName)
        {
            XDocument document = XDocument.Load(fileName);

            foreach (XElement element in document.Element("outfits").Elements("outfit"))
            {
                Outfit outfit = new Outfit();
                outfit.Id = uint.Parse(element.Attribute("id").Value);
                outfit.Sex = (Sex) byte.Parse(element.Attribute("sex").Value);
                outfit.SpriteId = ushort.Parse(element.Attribute("spriteid").Value);
                outfit.Name = element.Attribute("name").Value;
                outfit.IsPremium = string.Equals(bool.TrueString, element.Attribute("premium").Value, StringComparison.InvariantCultureIgnoreCase);
                outfit.IsUnlocked = string.Equals(bool.TrueString, element.Attribute("unlocked").Value, StringComparison.InvariantCultureIgnoreCase);
                outfit.IsEnabled = string.Equals(bool.TrueString, element.Attribute("enabled").Value, StringComparison.InvariantCultureIgnoreCase);

                yield return outfit;
            }
        }
    }
}