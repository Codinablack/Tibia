using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tibia.Creatures;

namespace Tibia.Data.Providers.OpenTibia
{
    public class NpcXmlReader
    {
        /// <summary>
        ///     Asynchronously loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The task.</returns>
        public async Task<IEnumerable<INpc>> LoadAsync(string fileName)
        {
            return await Task.Run(() => Load(fileName));
        }

        /// <summary>
        ///     Loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The collection of NPC's.</returns>
        private IEnumerable<INpc> Load(string fileName)
        {
            XDocument document = XDocument.Load(fileName);

            foreach (XElement element in document.Element("npcs").Elements("npc"))
            {
                INpc npc = new Npc();
                XDocument monsterDocument = XDocument.Load(Path.Combine(Path.GetDirectoryName(fileName), element.Attribute("file").Value));
                XElement npcElement = monsterDocument.Element("npc");

                npc.Name = npcElement.Attribute("name").Value;

                if (npcElement.Element("health") is XElement healthElement)
                    if (healthElement.Attribute("max") is XAttribute maxHealthAttribute)
                        npc.MaxHealth = uint.Parse(maxHealthAttribute.Value);

                if (npcElement.Element("look") is XElement outfitElement)
                {
                    if (outfitElement.Attribute("type") is XAttribute outfitLookTypeAttribute)
                        npc.Outfit.SpriteId = ushort.Parse(outfitLookTypeAttribute.Value);
                }

                yield return npc;
            }
        }
    }
}