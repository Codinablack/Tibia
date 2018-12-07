using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tibia.Vocations;

namespace Tibia.Data.Providers.OpenTibia
{
    public class VocationXmlReader
    {
        /// <summary>
        ///     Asynchronously loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The task.</returns>
        public async Task<IEnumerable<IVocation>> LoadAsync(string fileName)
        {
            return await Task.Run(() => Load(fileName));
        }

        /// <summary>
        ///     Loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The collection of vocations.</returns>
        private IEnumerable<IVocation> Load(string fileName)
        {
            XDocument document = XDocument.Load(fileName);

            foreach (XElement element in document.Element("vocations").Elements("vocation"))
            {
                IVocation vocation = new Vocation();

                if (element.Attribute("id") is XAttribute idAttribute)
                    vocation.Id = byte.Parse(idAttribute.Value);

                if (element.Attribute("name") is XAttribute nameAttribute)
                    vocation.Name = nameAttribute.Value;

                yield return vocation;
            }
        }
    }
}