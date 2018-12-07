using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tibia.Mounts;

namespace Tibia.Data.Providers.Ultimate
{
    public class DrkMountReader
    {
        /// <summary>
        ///     Asynchronously loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The collection of mounts.</returns>
        public Task<IEnumerable<IMount>> LoadAsync(string fileName)
        {
            return Task.Run(() => Load(fileName));
        }

        /// <summary>
        ///     Loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The collection of mounts.</returns>
        private IEnumerable<IMount> Load(string fileName)
        {
            XDocument document = XDocument.Load(fileName);

            foreach (XElement element in document.Element("mounts").Elements("mount"))
            {
                Mount mount = new Mount();
                mount.Id = uint.Parse(element.Attribute("id").Value);
                mount.SpriteId = ushort.Parse(element.Attribute("spriteid").Value);
                mount.Name = element.Attribute("name").Value;
                mount.Speed = ushort.Parse(element.Attribute("speed").Value);
                mount.IsPremium = string.Equals(bool.TrueString, element.Attribute("premium").Value, StringComparison.InvariantCultureIgnoreCase);

                yield return mount;
            }
        }
    }
}