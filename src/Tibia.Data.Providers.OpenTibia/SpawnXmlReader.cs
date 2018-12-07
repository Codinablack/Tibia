using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tibia.Creatures;
using Tibia.Map;
using Tibia.Spawns;

namespace Tibia.Data.Providers.OpenTibia
{
    public class SpawnXmlReader
    {
        private readonly MonsterService _monsterService;
        private readonly NpcService _npcService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SpawnXmlReader" /> class.
        /// </summary>
        /// <param name="monsterService">The monster service.</param>
        /// <param name="npcService">The NPC service.</param>
        public SpawnXmlReader(MonsterService monsterService, NpcService npcService)
        {
            _monsterService = monsterService;
            _npcService = npcService;
        }

        /// <summary>
        ///     Asynchronously loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The task.</returns>
        public async Task<IEnumerable<SpawnSource>> LoadAsync(string fileName)
        {
            return await Task.Run(() => Load(fileName));
        }

        /// <summary>
        ///     Loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The collection of spawn sources.</returns>
        private IEnumerable<SpawnSource> Load(string fileName)
        {
            XDocument document = XDocument.Load(fileName);

            if (!(document.Element("spawns") is XElement spawnsElement))
                throw new ArgumentNullException(nameof(spawnsElement));

            foreach (XElement element in spawnsElement.Elements("spawn"))
            {
                SpawnSource spawnSource = new SpawnSource();
                spawnSource.CenterPosition = ParseCenterPosition(element);
                spawnSource.Radius = ParseRadius(element);

                foreach (XElement monsterElement in element.Elements("monster"))
                {
                    IMonster monster = _monsterService.GetMonsterByUniqueName(ParseName(monsterElement));
                    MonsterSpawnSettings monsterSpawnSettings = new MonsterSpawnSettings();
                    monsterSpawnSettings.Monster = monster;
                    monsterSpawnSettings.AbsolutePosition = ParseAbsolutePosition(spawnSource.CenterPosition, monsterElement);
                    monsterSpawnSettings.SpawnSource = spawnSource;
                    spawnSource.Spawns.Add(monsterSpawnSettings);
                }

                foreach (XElement npcElement in element.Elements("npc"))
                {
                    foreach (INpc npc in _npcService.GetNpcsByName(ParseName(npcElement)))
                    {
                        NpcSpawnSettings npcSpawnSettings = new NpcSpawnSettings();
                        npcSpawnSettings.Npc = npc;
                        npcSpawnSettings.AbsolutePosition = ParseAbsolutePosition(spawnSource.CenterPosition, npcElement);
                        npcSpawnSettings.SpawnSource = spawnSource;
                        spawnSource.Spawns.Add(npcSpawnSettings);
                    }
                }

                yield return spawnSource;
            }
        }

        /// <summary>
        ///     Parses the radius.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The radius.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        private static int ParseRadius(XElement element)
        {
            if (!(element.Attribute("radius") is XAttribute radiusAttribute))
                throw new ArgumentNullException(nameof(radiusAttribute));

            return int.Parse(radiusAttribute.Value);
        }

        /// <summary>
        ///     Parses the center position.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The center position.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        private static IVector3 ParseCenterPosition(XElement element)
        {
            if (!(element.Attribute("centerx") is XAttribute centerXAttribute))
                throw new ArgumentNullException(nameof(centerXAttribute));

            if (!(element.Attribute("centery") is XAttribute centerYAttribute))
                throw new ArgumentNullException(nameof(centerYAttribute));

            if (!(element.Attribute("centerz") is XAttribute centerZAttribute))
                throw new ArgumentNullException(nameof(centerZAttribute));

            return new Vector3(int.Parse(centerXAttribute.Value), int.Parse(centerYAttribute.Value), int.Parse(centerZAttribute.Value));
        }

        /// <summary>
        ///     Parses the name.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The name.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        private static string ParseName(XElement element)
        {
            if (!(element.Attribute("name") is XAttribute nameAttribute))
                throw new ArgumentNullException(nameof(nameAttribute));

            return nameAttribute.Value;
        }

        /// <summary>
        ///     Parses the absolute position.
        /// </summary>
        /// <param name="centerPosition">The center position.</param>
        /// <param name="element">The element.</param>
        /// <returns>The absolute position.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        private static IVector3 ParseAbsolutePosition(IVector3 centerPosition, XElement element)
        {
            if (!(element.Attribute("x") is XAttribute xAttribute))
                throw new ArgumentNullException(nameof(xAttribute));

            if (!(element.Attribute("y") is XAttribute yAttribute))
                throw new ArgumentNullException(nameof(yAttribute));

            if (!(element.Attribute("z") is XAttribute zAttribute))
                throw new ArgumentNullException(nameof(zAttribute));

            // The actual Z of Vector3 should be the sum of centerPosition.Z + int.Parse(zAttribute.Value)
            // but this is OpenTibia we're talking about. In OpenTibia Z can be actually different than centerPosition.Z
            // which means that the spawn's center position is just a reference and the creature can be in any floor
            // TODO: The value in the XML file should indicate the offset of the creature in relation to its spawn's
            // center position, not an absolute Z value. This fix probably must be done in the MapEditor before
            // implementing it here
            // TODO: Replace the below line with -> return new Vector3(centerPosition.X + int.Parse(xAttribute.Value), centerPosition.Y + int.Parse(yAttribute.Value), centerPosition.Z + int.Parse(zAttribute.Value));
            return new Vector3(centerPosition.X + int.Parse(xAttribute.Value), centerPosition.Y + int.Parse(yAttribute.Value), int.Parse(zAttribute.Value));
        }
    }
}