using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tibia.Combat;
using Tibia.Creatures;
using Tibia.Spells;

namespace Tibia.Data.Providers.OpenTibia
{
    public class MonsterXmlReader
    {
        private readonly SpellService _spellService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Spawns.MonsterXmlReader" /> class.
        /// </summary>
        /// <param name="spellService">The spell service.</param>
        public MonsterXmlReader(SpellService spellService)
        {
            _spellService = spellService;
        }

        /// <summary>
        ///     Asynchronously loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The task.</returns>
        public async Task<IEnumerable<IMonster>> LoadAsync(string fileName)
        {
            return await Task.Run(() => Load(fileName));
        }

        /// <summary>
        ///     Loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The collection of monsters.</returns>
        private IEnumerable<IMonster> Load(string fileName)
        {
            XDocument document = XDocument.Load(fileName);

            if (!(document.Element("monsters") is XElement monstersElement))
                throw new ArgumentNullException(nameof(monstersElement));

            foreach (XElement element in monstersElement.Elements("monster"))
            {
                IMonster monster = new Monster();
                XDocument monsterDocument = XDocument.Load(Path.Combine(Path.GetDirectoryName(fileName), element.Attribute("file").Value));
                XElement monsterElement = monsterDocument.Element("monster");

                monster.UniqueName = element.Attribute("name").Value;
                monster.Name = monsterElement.Attribute("name").Value;

                if (monsterElement.Attribute("nameDescription") is XAttribute nameDescriptionAttribute)
                    monster.Description = nameDescriptionAttribute.Value;

                monster.RaceType = ParseRaceType(monsterElement.Attribute("race").Value);

                if (monsterElement.Attribute("experience") is XAttribute expAttribute)
                    monster.Experience = uint.Parse(expAttribute.Value);

                monster.BaseSpeed.BaseSpeed = ushort.Parse(monsterElement.Attribute("speed").Value);

                if (monsterElement.Attribute("manacost") is XAttribute summonCostAttribute)
                    monster.SummonCost = uint.Parse(summonCostAttribute.Value);

                XElement healthElement = monsterElement.Element("health");
                monster.MaxHealth = uint.Parse(healthElement.Attribute("max").Value);

                if (monsterElement.Element("look") is XElement outfitElement)
                {
                    if (outfitElement.Attribute("type") is XAttribute outfitLookTypeAttribute)
                        monster.Outfit.SpriteId = ushort.Parse(outfitLookTypeAttribute.Value);

                    if (outfitElement.Attribute("corpse") is XAttribute corpseAttribute)
                        monster.Corpse.SpriteId = ushort.Parse(corpseAttribute.Value);
                }

                if (monsterElement.Element("targetchange") is XElement targetChangeElement)
                {
                    if (targetChangeElement.Attribute("interval") is XAttribute attributeTargetChangeInterval)
                        monster.TargetChangeSettings.Interval = TimeSpan.FromMilliseconds(long.Parse(attributeTargetChangeInterval.Value));

                    if (targetChangeElement.Attribute("chance") is XAttribute attributeTargetChangeChance)
                        monster.TargetChangeSettings.Chance = new Percent(byte.Parse(attributeTargetChangeChance.Value));
                }

                ParseFlags(monster, monsterElement.Element("flags").Elements("flag"));

                if (monsterElement.Element("attacks") is XElement attacksElement)
                {
                    foreach (XElement defenseElement in attacksElement.Elements("attack"))
                        monster.Attacks.Add(ParseCombat(defenseElement));
                }

                if (monsterElement.Element("defenses") is XElement defensesElement)
                {
                    if (defensesElement.Attribute("armor") is XAttribute armorAttribute)
                        monster.Armor = short.Parse(armorAttribute.Value);

                    if (defensesElement.Attribute("defense") is XAttribute defenseAttribute)
                        monster.Defense = short.Parse(defenseAttribute.Value);

                    foreach (XElement defenseElement in defensesElement.Elements("defense"))
                        monster.Defenses.Add(ParseCombat(defenseElement));
                }

                yield return monster;
            }
        }

        /// <summary>
        ///     Parses the combat.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The combat.</returns>
        private ICombat ParseCombat(XElement element)
        {
            ICombat combat;
            if (!(element.Attribute("name") is XAttribute nameAttribute))
                throw new ArgumentNullException(nameof(nameAttribute));

            string combatName = nameAttribute.Value;
            switch (combatName)
            {
                case "healing":
                    combat = ParseHealingCombatData(element);
                    break;
                case "invisible":
                    combat = ParseInvisibleCombatData(element);
                    break;
                case "melee":
                    combat = ParseMeleeCombatData(element);
                    break;
                case "physical":
                    combat = ParsePhysicalDamage(element);
                    break;
                case "speed":
                    combat = ParseSpeedCombatData(element);
                    break;
                case "paralyze":
                    combat = ParseParalyzeCombatData(element);
                    break;
                case "earth":
                    combat = ParseEarthCombatData(element);
                    break;
                case "manadrain":
                    combat = ParseManaDrainCombatData(element);
                    break;
                case "fire":
                    combat = ParseFireCombatData(element);
                    break;
                case "death":
                    combat = ParseDeathCombatData(element);
                    break;
                case "energy":
                    combat = ParseEnergyCombatData(element);
                    break;
                case "ice":
                    combat = ParseIceCombatData(element);
                    break;
                case "poison":
                    combat = ParsePoisonCombatData(element);
                    break;
                case "poisonfield":
                    combat = ParsePoisonFieldCombatData(element);
                    break;
                case "firefield":
                    combat = ParseFireFieldCombatData(element);
                    break;
                case "energyfield":
                    combat = ParseEnergyFieldCombatData(element);
                    break;
                case "drunk":
                    combat = ParseDrunkCombatData(element);
                    break;
                case "holy":
                    combat = ParseHolyCombatData(element);
                    break;
                case "lifedrain":
                    combat = ParseLifeDrainCombatData(element);
                    break;
                case "drown":
                    combat = ParseDrowningCombatData(element);
                    break;
                case "outfit":
                    combat = ParseOutfitCombatData(element);
                    break;
                case "poisoncondition":
                    combat = ParsePoisonConditionCombatData(element);
                    break;
                case "drowncondition":
                    combat = ParseDrownConditionCombatData(element);
                    break;
                case "firecondition":
                    combat = ParseFireConditionCombatData(element);
                    break;
                case "energycondition":
                    combat = ParseEnergyConditionCombatData(element);
                    break;
                case "cursecondition":
                    combat = ParseCurseConditionCombatData(element);
                    break;
                case "freezecondition":
                    combat = ParseFreezeConditionCombatData(element);
                    break;
                case "bleedcondition":
                    combat = ParseBleedConditionCombatData(element);
                    break;
                case "strength":
                    combat = ParseStrengthCombatData(element);
                    break;
                case "fireball":
                    combat = ParseFireballCombatData(element);
                    break;
                case "icicle":
                    combat = ParseIcicleCombatData(element);
                    break;
                case "soulfire":
                    combat = ParseSoulfireCombatData(element);
                    break;
                case "thunderstorm":
                    combat = ParseThunderstormCombatData(element);
                    break;
                case "effect":
                    combat = ParseEffectCombatData(element);
                    break;
                default:
                    ICombat spell = _spellService.Spells.OfType<InstantSpell>().FirstOrDefault(s => string.Equals(s.Name, combatName, StringComparison.InvariantCultureIgnoreCase));
                    combat = spell ?? throw new ArgumentOutOfRangeException(nameof(combatName), combatName, null);
                    break;
            }

            if (element.Attribute("interval") is XAttribute intervalAttribute)
                combat.Interval = TimeSpan.FromMilliseconds(long.Parse(intervalAttribute.Value));

            return combat;
        }

        private static Melee ParseMeleeCombatData(XElement element)
        {
            Melee melee = new Melee();

            if (element.Attribute("min") is XAttribute minAttribute)
                melee.Min = short.Parse(minAttribute.Value);

            if (element.Attribute("max") is XAttribute maxAttribute)
                melee.Max = short.Parse(maxAttribute.Value);

            if (element.Attribute("skill") is XAttribute skillAttribute)
                melee.Skill = byte.Parse(skillAttribute.Value);

            if (element.Attribute("attack") is XAttribute attackAttribute)
                melee.Attack = ushort.Parse(attackAttribute.Value);

            return melee;
        }

        private EffectCombat ParseEffectCombatData(XElement element)
        {
            EffectCombat effectCombat = new EffectCombat();

            foreach (XElement attributeElement in element.Elements("attributes"))
            {
                if (!(attributeElement.Attribute("key") is XAttribute keyAttribute))
                    throw new ArgumentNullException(nameof(keyAttribute));

                string key = keyAttribute.Value;
                switch (key)
                {
                    case "areaEffect":
                        effectCombat.AreaEffect = ParseEffect(attributeElement.Attribute("value"));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(key), key, null);
                }
            }

            return effectCombat;
        }

        private Effect ParseEffect(XAttribute attribute)
        {
            string effectName = attribute.Value;
            switch (effectName)
            {
                case "redsparks":
                    return Effect.RedSparks;
                case "bluerings":
                    return Effect.BlueRings;
                case "puff":
                    return Effect.Puff;
                case "yellowspark":
                    return Effect.YellowSpark;
                case "explosionarea":
                    return Effect.ExplosionArea;
                case "explosiondamage":
                    return Effect.ExplosionDamage;
                case "firearea":
                    return Effect.FireArea;
                case "yellowrings":
                    return Effect.YellowRings;
                case "greenrings":
                    return Effect.GreenRings;
                case "blackspark":
                    return Effect.BlackSpark;
                case "teleport":
                    return Effect.Teleport;
                case "energydamage":
                    return Effect.EnergyDamage;
                case "blueshimmer":
                    return Effect.BlueShimmer;
                case "redshimmer":
                    return Effect.RedShimmer;
                case "greenshimmer":
                    return Effect.GreenShimmer;
                case "firestrike":
                    return Effect.FireStrike;
                case "greensparks":
                    return Effect.GreenSparks;
                case "mortarea":
                    return Effect.MortArea;
                case "greennotes":
                    return Effect.GreenNotes;
                case "rednotes":
                    return Effect.RedNotes;
                case "poisonarea":
                    return Effect.PoisonArea;
                case "yellownotes":
                    return Effect.YellowNotes;
                case "purplenotes":
                    return Effect.PurpleNotes;
                case "bluenotes":
                    return Effect.BlueNotes;
                case "whitenotes":
                    return Effect.WhiteNotes;
                case "bubbles":
                    return Effect.Bubbles;
                case "dieroll":
                    return Effect.DieRoll;
                case "giftwraps":
                    return Effect.GiftWraps;
                case "yellowfirework":
                    return Effect.YellowFirework;
                case "redfirework":
                    return Effect.RedFirework;
                case "bluefirework":
                    return Effect.BlueFirework;
                case "stun":
                    return Effect.Stun;
                case "sleep":
                    return Effect.Sleep;
                case "watercreature":
                    return Effect.WaterCreature;
                case "groundshaker":
                    return Effect.Groundshaker;
                case "hearts":
                    return Effect.Hearts;
                case "fireattack":
                    return Effect.FireAttack;
                case "energyarea":
                    return Effect.EnergyArea;
                case "smallclouds":
                    return Effect.SmallClouds;
                case "holydamage":
                    return Effect.HolyDamage;
                case "bigclouds":
                    return Effect.BigClouds;
                case "icearea":
                    return Effect.IceArea;
                case "icetornado":
                    return Effect.IceTornado;
                case "iceattack":
                    return Effect.IceAttack;
                case "stones":
                    return Effect.Stones;
                case "smallplants":
                    return Effect.SmallPlants;
                case "carniphila":
                    return Effect.Carniphila;
                case "purpleenergy":
                    return Effect.PurpleEnergy;
                case "yellowenergy":
                    return Effect.YellowEnergy;
                case "holyarea":
                    return Effect.HolyArea;
                case "bigplants":
                    return Effect.BigPlants;
                case "cake":
                    return Effect.Cake;
                case "giantice":
                    return Effect.GiantIce;
                case "watersplash":
                    return Effect.WaterSplash;
                case "plantattack":
                    return Effect.PlantAttack;
                case "tutorialarrow":
                    return Effect.TutorialArrow;
                case "tutorialsquare":
                    return Effect.TutorialSquare;
                case "horizontalmirror":
                    return Effect.HorizontalMirror;
                case "verticalmirror":
                    return Effect.VerticalMirror;
                case "horizontalskull":
                    return Effect.HorizontalSkull;
                case "verticalskull":
                    return Effect.VerticalSkull;
                case "assassin":
                    return Effect.Assassin;
                case "horizontalsteps":
                    return Effect.HorizontalSteps;
                case "bloodysteps":
                    return Effect.BloodySteps;
                case "verticalsteps":
                    return Effect.VerticalSteps;
                case "yalaharighost":
                    return Effect.YalahariGhost;
                case "bats":
                    return Effect.Bats;
                case "smoke":
                    return Effect.Smoke;
                case "insects":
                    return Effect.Insects;
                case "dragonhead":
                    return Effect.DragonHead;
                case "orcshaman":
                    return Effect.OrcShaman;
                case "orcshamanfire":
                    return Effect.OrcShamanFire;
                case "thunder":
                    return Effect.Thunder;
                case "ferumbras":
                    return Effect.Ferumbras;
                case "horizontalconfetti":
                    return Effect.HorizontalConfetti;
                case "verticalconfetti":
                    return Effect.VerticalConfetti;
                case "blacksmoke":
                    return Effect.BlackSmoke;
                case "redsmoke":
                    return Effect.RedSmoke;
                case "yellowsmoke":
                    return Effect.YellowSmoke;
                case "greensmoke":
                    return Effect.GreenSmoke;
                case "purplesmoke":
                    return Effect.PurpleSmoke;
                default:
                    throw new ArgumentOutOfRangeException(nameof(effectName), effectName, null);
            }
        }

        private static Speed ParseSpeedCombatData(XElement element)
        {
            Speed speed = new Speed();

            if (element.Attribute("chance") is XAttribute minAttribute)
                speed.Chance = new Percent(byte.Parse(minAttribute.Value));

            if (element.Attribute("speedchange") is XAttribute maxAttribute)
                speed.Change = short.Parse(maxAttribute.Value);

            if (element.Attribute("duration") is XAttribute chanceAttribute)
                speed.Duration = TimeSpan.FromMilliseconds(long.Parse(chanceAttribute.Value));

            return speed;
        }

        private static PoisonCondition ParsePoisonConditionCombatData(XElement element)
        {
            PoisonCondition poisonCondition = new PoisonCondition();

            if (element.Attribute("min") is XAttribute minAttribute)
                poisonCondition.Min = short.Parse(minAttribute.Value);

            if (element.Attribute("max") is XAttribute maxAttribute)
                poisonCondition.Max = short.Parse(maxAttribute.Value);

            return poisonCondition;
        }

        private static DrownCondition ParseDrownConditionCombatData(XElement element)
        {
            DrownCondition drownCondition = new DrownCondition();

            if (element.Attribute("min") is XAttribute minAttribute)
                drownCondition.Min = short.Parse(minAttribute.Value);

            if (element.Attribute("max") is XAttribute maxAttribute)
                drownCondition.Max = short.Parse(maxAttribute.Value);

            return drownCondition;
        }

        private static FireCondition ParseFireConditionCombatData(XElement element)
        {
            FireCondition fireCondition = new FireCondition();

            if (element.Attribute("min") is XAttribute minAttribute)
                fireCondition.Min = short.Parse(minAttribute.Value);

            if (element.Attribute("max") is XAttribute maxAttribute)
                fireCondition.Max = short.Parse(maxAttribute.Value);

            return fireCondition;
        }

        private static EnergyCondition ParseEnergyConditionCombatData(XElement element)
        {
            EnergyCondition energyCondition = new EnergyCondition();

            if (element.Attribute("min") is XAttribute minAttribute)
                energyCondition.Min = short.Parse(minAttribute.Value);

            if (element.Attribute("max") is XAttribute maxAttribute)
                energyCondition.Max = short.Parse(maxAttribute.Value);

            return energyCondition;
        }

        private static CurseCondition ParseCurseConditionCombatData(XElement element)
        {
            CurseCondition curseCondition = new CurseCondition();

            if (element.Attribute("min") is XAttribute minAttribute)
                curseCondition.Min = short.Parse(minAttribute.Value);

            if (element.Attribute("max") is XAttribute maxAttribute)
                curseCondition.Max = short.Parse(maxAttribute.Value);

            return curseCondition;
        }

        private static FreezeCondition ParseFreezeConditionCombatData(XElement element)
        {
            FreezeCondition freezeCondition = new FreezeCondition();

            if (element.Attribute("min") is XAttribute minAttribute)
                freezeCondition.Min = short.Parse(minAttribute.Value);

            if (element.Attribute("max") is XAttribute maxAttribute)
                freezeCondition.Max = short.Parse(maxAttribute.Value);

            return freezeCondition;
        }

        private static BleedCondition ParseBleedConditionCombatData(XElement element)
        {
            BleedCondition bleedCondition = new BleedCondition();

            if (element.Attribute("min") is XAttribute minAttribute)
                bleedCondition.Min = short.Parse(minAttribute.Value);

            if (element.Attribute("max") is XAttribute maxAttribute)
                bleedCondition.Max = short.Parse(maxAttribute.Value);

            return bleedCondition;
        }

        private static Strength ParseStrengthCombatData(XElement element)
        {
            Strength strength = new Strength();

            if (element.Attribute("min") is XAttribute minAttribute)
                strength.Min = short.Parse(minAttribute.Value);

            if (element.Attribute("max") is XAttribute maxAttribute)
                strength.Max = short.Parse(maxAttribute.Value);

            return strength;
        }

        private static Fireball ParseFireballCombatData(XElement element)
        {
            Fireball fireball = new Fireball();

            if (element.Attribute("min") is XAttribute minAttribute)
                fireball.Min = short.Parse(minAttribute.Value);

            if (element.Attribute("max") is XAttribute maxAttribute)
                fireball.Max = short.Parse(maxAttribute.Value);

            return fireball;
        }

        private static Icicle ParseIcicleCombatData(XElement element)
        {
            Icicle icicle = new Icicle();

            if (element.Attribute("min") is XAttribute minAttribute)
                icicle.Min = short.Parse(minAttribute.Value);

            if (element.Attribute("max") is XAttribute maxAttribute)
                icicle.Max = short.Parse(maxAttribute.Value);

            return icicle;
        }

        private static Thunderstorm ParseThunderstormCombatData(XElement element)
        {
            Thunderstorm thunderstorm = new Thunderstorm();

            if (element.Attribute("min") is XAttribute minAttribute)
                thunderstorm.Min = short.Parse(minAttribute.Value);

            if (element.Attribute("max") is XAttribute maxAttribute)
                thunderstorm.Max = short.Parse(maxAttribute.Value);

            return thunderstorm;
        }

        private static Soulfire ParseSoulfireCombatData(XElement element)
        {
            Soulfire soulfire = new Soulfire();

            if (element.Attribute("chance") is XAttribute minAttribute)
                soulfire.Chance = new Percent(byte.Parse(minAttribute.Value));

            return soulfire;
        }

        private static OutfitCombat ParseOutfitCombatData(XElement element)
        {
            OutfitCombat outfitCombat = new OutfitCombat();

            if (element.Attribute("duration") is XAttribute durationAttribute)
                outfitCombat.Duration = TimeSpan.FromMilliseconds(long.Parse(durationAttribute.Value));

            return outfitCombat;
        }

        private static Drowning ParseDrowningCombatData(XElement element)
        {
            Drowning drowning = new Drowning();

            if (element.Attribute("min") is XAttribute minAttribute)
                drowning.Min = short.Parse(minAttribute.Value);

            if (element.Attribute("max") is XAttribute maxAttribute)
                drowning.Max = short.Parse(maxAttribute.Value);

            return drowning;
        }

        private static LifeDrain ParseLifeDrainCombatData(XElement element)
        {
            LifeDrain lifeDrain = new LifeDrain();

            if (element.Attribute("min") is XAttribute minAttribute)
                lifeDrain.Min = short.Parse(minAttribute.Value);

            if (element.Attribute("max") is XAttribute maxAttribute)
                lifeDrain.Max = short.Parse(maxAttribute.Value);

            return lifeDrain;
        }

        private static Holy ParseHolyCombatData(XElement element)
        {
            Holy holy = new Holy();

            if (element.Attribute("min") is XAttribute minAttribute)
                holy.Min = short.Parse(minAttribute.Value);

            if (element.Attribute("max") is XAttribute maxAttribute)
                holy.Max = short.Parse(maxAttribute.Value);

            return holy;
        }

        private static Drunk ParseDrunkCombatData(XElement element)
        {
            Drunk drunk = new Drunk();

            if (element.Attribute("duration") is XAttribute durationAttribute)
                drunk.Duration = TimeSpan.FromMilliseconds(long.Parse(durationAttribute.Value));

            return drunk;
        }

        private static FireField ParseFireFieldCombatData(XElement element)
        {
            FireField fireField = new FireField();

            if (element.Attribute("radius") is XAttribute radiusAttribute)
                fireField.Radius = byte.Parse(radiusAttribute.Value);

            return fireField;
        }

        private static EnergyField ParseEnergyFieldCombatData(XElement element)
        {
            EnergyField energyField = new EnergyField();

            if (element.Attribute("radius") is XAttribute radiusAttribute)
                energyField.Radius = byte.Parse(radiusAttribute.Value);

            return energyField;
        }

        private static PoisonField ParsePoisonFieldCombatData(XElement element)
        {
            PoisonField poisonField = new PoisonField();

            if (element.Attribute("radius") is XAttribute radiusAttribute)
                poisonField.Radius = byte.Parse(radiusAttribute.Value);

            return poisonField;
        }

        private static Poison ParsePoisonCombatData(XElement element)
        {
            Poison poison = new Poison();

            if (element.Attribute("min") is XAttribute minAttribute)
                poison.Min = short.Parse(minAttribute.Value);

            if (element.Attribute("max") is XAttribute maxAttribute)
                poison.Max = short.Parse(maxAttribute.Value);

            return poison;
        }

        private static Ice ParseIceCombatData(XElement element)
        {
            Ice ice = new Ice();

            if (element.Attribute("min") is XAttribute minAttribute)
                ice.Min = short.Parse(minAttribute.Value);

            if (element.Attribute("max") is XAttribute maxAttribute)
                ice.Max = short.Parse(maxAttribute.Value);

            return ice;
        }

        private static Energy ParseEnergyCombatData(XElement element)
        {
            Energy energy = new Energy();

            if (element.Attribute("min") is XAttribute minAttribute)
                energy.Min = short.Parse(minAttribute.Value);

            if (element.Attribute("max") is XAttribute maxAttribute)
                energy.Max = short.Parse(maxAttribute.Value);

            return energy;
        }

        private static Death ParseDeathCombatData(XElement element)
        {
            Death death = new Death();

            if (element.Attribute("min") is XAttribute minAttribute)
                death.Min = short.Parse(minAttribute.Value);

            if (element.Attribute("max") is XAttribute maxAttribute)
                death.Max = short.Parse(maxAttribute.Value);

            return death;
        }

        private static Fire ParseFireCombatData(XElement element)
        {
            Fire fire = new Fire();

            if (element.Attribute("min") is XAttribute minAttribute)
                fire.Min = short.Parse(minAttribute.Value);

            if (element.Attribute("max") is XAttribute maxAttribute)
                fire.Max = short.Parse(maxAttribute.Value);

            return fire;
        }

        private static ManaDrain ParseManaDrainCombatData(XElement element)
        {
            ManaDrain manaDrain = new ManaDrain();

            if (element.Attribute("min") is XAttribute minAttribute)
                manaDrain.Min = short.Parse(minAttribute.Value);

            if (element.Attribute("max") is XAttribute maxAttribute)
                manaDrain.Max = short.Parse(maxAttribute.Value);

            return manaDrain;
        }

        private static Earth ParseEarthCombatData(XElement element)
        {
            Earth earth = new Earth();

            if (element.Attribute("min") is XAttribute minAttribute)
                earth.Min = short.Parse(minAttribute.Value);

            if (element.Attribute("max") is XAttribute maxAttribute)
                earth.Max = short.Parse(maxAttribute.Value);

            return earth;
        }

        private static Paralyze ParseParalyzeCombatData(XElement element)
        {
            Paralyze paralyze = new Paralyze();

            if (element.Attribute("chance") is XAttribute minAttribute)
                paralyze.Chance = new Percent(byte.Parse(minAttribute.Value));

            if (element.Attribute("speedchange") is XAttribute maxAttribute)
                paralyze.Change = short.Parse(maxAttribute.Value);

            if (element.Attribute("duration") is XAttribute chanceAttribute)
                paralyze.Duration = TimeSpan.FromMilliseconds(long.Parse(chanceAttribute.Value));

            return paralyze;
        }

        private static Physical ParsePhysicalDamage(XElement element)
        {
            Physical physical = new Physical();

            if (element.Attribute("min") is XAttribute minAttribute)
                physical.Min = short.Parse(minAttribute.Value);

            if (element.Attribute("max") is XAttribute maxAttribute)
                physical.Max = short.Parse(maxAttribute.Value);

            return physical;
        }

        private static Invisible ParseInvisibleCombatData(XElement element)
        {
            Invisible invisible = new Invisible();

            if (element.Attribute("chance") is XAttribute chanceAttribute)
                invisible.Chance = new Percent(byte.Parse(chanceAttribute.Value));

            if (element.Attribute("duration") is XAttribute durationAttribute)
                invisible.Duration = TimeSpan.FromMilliseconds(long.Parse(durationAttribute.Value));

            return invisible;
        }

        private static Healing ParseHealingCombatData(XElement element)
        {
            Healing healing = new Healing();

            if (element.Attribute("min") is XAttribute minAttribute)
                healing.Min = int.Parse(minAttribute.Value);

            if (element.Attribute("max") is XAttribute maxAttribute)
                healing.Max = int.Parse(maxAttribute.Value);

            if (element.Attribute("chance") is XAttribute chanceAttribute)
                healing.Chance = new Percent(byte.Parse(chanceAttribute.Value));

            return healing;
        }

        /// <summary>
        ///     Parses the flags.
        /// </summary>
        /// <param name="monster">The monster.</param>
        /// <param name="flagElements">The flag elements.</param>
        private static void ParseFlags(IMonster monster, IEnumerable<XElement> flagElements)
        {
            foreach (XElement flagElement in flagElements)
            {
                XAttribute flagAttribute = flagElement.Attribute("summonable");
                if (flagAttribute != null)
                    monster.Settings.IsSummonable = bool.Parse(flagAttribute.Value);

                flagAttribute = flagElement.Attribute("attackable");
                if (flagAttribute != null)
                    monster.Settings.IsAttackable = bool.Parse(flagAttribute.Value);

                flagAttribute = flagElement.Attribute("hostile");
                if (flagAttribute != null)
                    monster.Settings.IsHostile = bool.Parse(flagAttribute.Value);

                flagAttribute = flagElement.Attribute("illusionable");
                if (flagAttribute != null)
                    monster.Settings.IsIllusionable = bool.Parse(flagAttribute.Value);

                flagAttribute = flagElement.Attribute("convinceable");
                if (flagAttribute != null)
                {
                    monster.Settings.IsConvinceable = bool.Parse(flagAttribute.Value);

                    if (flagElement.Attribute("manacost") is XAttribute convinceCostAttribute)
                        monster.ConvinceCost = int.Parse(convinceCostAttribute.Value);
                }

                flagAttribute = flagElement.Attribute("pushable");
                if (flagAttribute != null)
                    monster.Settings.IsPushable = bool.Parse(flagAttribute.Value);

                flagAttribute = flagElement.Attribute("canpushitems");
                if (flagAttribute != null)
                    monster.Settings.CanPushItems = bool.Parse(flagAttribute.Value);

                flagAttribute = flagElement.Attribute("canpushcreatures");
                if (flagAttribute != null)
                    monster.Settings.CanPushCreatures = bool.Parse(flagAttribute.Value);

                flagAttribute = flagElement.Attribute("targetdistance");
                if (flagAttribute != null)
                {
                    monster.Range = uint.Parse(flagAttribute.Value);
                    monster.Settings.IsRanged = monster.Range > 1;
                }

                flagAttribute = flagElement.Attribute("staticattack");
                if (flagAttribute != null)
                {
                    // TODO: Implement position change interval
                    //monster.PositionChange.Interval = TimeSpan.FromMilliseconds(long.Parse(targetChangeElement.Attribute("interval").Value));
                    monster.PositionChangeSettings.Chance = new Percent(byte.Parse(flagAttribute.Value));
                }

                flagAttribute = flagElement.Attribute("runonhealth");
                if (flagAttribute != null)
                    monster.FleeHealth = uint.Parse(flagAttribute.Value);
            }
        }

        /// <summary>
        ///     Parses the type of the race.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The race type.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        private static RaceType ParseRaceType(string value)
        {
            switch (value)
            {
                case "blood":
                    return RaceType.Blood;
                case "energy":
                    return RaceType.Energy;
                case "fire":
                    return RaceType.Fire;
                case "undead":
                    return RaceType.Undead;
                case "venom":
                    return RaceType.Venom;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }
    }
}