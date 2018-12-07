using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tibia.Data;
using Tibia.Data.Services;

namespace Tibia.Spells
{
    public class SpellXmlReader : IService
    {
        /// <summary>
        ///     Asynchronously loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The task.</returns>
        public async Task<IEnumerable<ISpell>> LoadAsync(string fileName)
        {
            return await Task.Run(() => Load(fileName));
        }

        /// <summary>
        ///     Loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The collection of spells.</returns>
        private IEnumerable<ISpell> Load(string fileName)
        {
            XDocument document = XDocument.Load(fileName);

            foreach (XElement element in document.Element("spells").Elements())
            {
                ISpell spell;
                string elementName = element.Name.LocalName;
                switch (elementName)
                {
                    case "instant":
                        spell = ParseInstantSpell(element);
                        break;
                    case "conjure":
                        spell = ParseConjureSpell(element);
                        break;
                    case "rune":
                        spell = ParseRuneSpell(element);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(elementName), elementName, null);
                }

                spell.Id = ParseIdAttribute(element);
                spell.Name = ParseNameAttribute(element);

                yield return spell;
            }
        }

        /// <summary>
        ///     Parses the rune spell.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The rune spell.</returns>
        private static ISpell ParseRuneSpell(XElement element)
        {
            RuneSpell runeSpell = new RuneSpell();
            return runeSpell;
        }

        /// <summary>
        ///     Parses the conjure spell.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The conjure spell.</returns>
        private static ISpell ParseConjureSpell(XElement element)
        {
            ConjureSpell conjureSpell = new ConjureSpell();
            return conjureSpell;
        }

        /// <summary>
        ///     Parses the instant spell.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The instant spell.</returns>
        private static ISpell ParseInstantSpell(XElement element)
        {
            InstantSpell instantSpell = new InstantSpell();
            return instantSpell;
        }

        /// <summary>
        ///     Parses the name attribute.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The name.</returns>
        private static string ParseNameAttribute(XElement element)
        {
            if (element.Attribute("name") is XAttribute nameAttribute)
                return nameAttribute.Value;

            return null;
        }

        /// <summary>
        ///     Parses the identifier attribute.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The identifier.</returns>
        private static byte ParseIdAttribute(XElement element)
        {
            if (element.Attribute("uid") is XAttribute idAttribute)
                return byte.Parse(idAttribute.Value);

            return 0;
        }
    }
}