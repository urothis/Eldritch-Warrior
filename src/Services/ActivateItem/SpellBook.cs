using System;
using NWN.API;
using NWN.API.Events;
using NWN.Services;

namespace Services.ActivateItem
{
    [ServiceBinding(typeof(IItemHandler))]
    public class SpellBook : IItemHandler
    {
        public string Tag => "itm_spellbook";

        public void HandleActivateItem(ModuleEvents.OnActivateItem activateItem)
        {
            Random random = new Random();
            int count = 0;
            int spellCount = random.Next(6, 20);
            string scrollName = "";

            while (count < spellCount)
            {
                switch (random.Next(0, 275))
                {
                    case 0: scrollName = "nw_it_sparscr101"; break; //
                    case 1: scrollName = "nw_it_sparscr102"; break; // Protection from Alignment
                    case 2: scrollName = "nw_it_sparscr103"; break; // Grease
                    case 3: scrollName = "nw_it_sparscr104"; break; // Mage Armor
                    case 4: scrollName = "nw_it_sparscr105"; break; // Summon Creature I
                    case 5: scrollName = "nw_it_sparscr106"; break; // Identify
                    case 6: scrollName = "nw_it_sparscr107"; break; // Charm Person
                    case 7: scrollName = "nw_it_sparscr108"; break; // Sleep
                    case 8: scrollName = "nw_it_sparscr109"; break; // Magic Missile
                    case 9: scrollName = "nw_it_sparscr110"; break; // Color Spray
                    case 10: scrollName = "nw_it_sparscr111"; break; // Ray of Enfeeblement
                    case 11: scrollName = "nw_it_sparscr112"; break; // Burning Hands
                    case 12: scrollName = "nw_it_sparscr113"; break; // Negative Energy Ray
                    case 13: scrollName = "nw_it_sparscr201"; break; // Resist Elements
                    case 14: scrollName = "nw_it_sparscr202"; break; // Melf's Acid Arrow
                    case 15: scrollName = "nw_it_sparscr203"; break; // Summon Creature II
                    case 16: scrollName = "nw_it_sparscr204"; break; // Web
                    case 17: scrollName = "nw_it_sparscr205"; break; // See Invisibility
                    case 18: scrollName = "nw_it_sparscr206"; break; // Darkness
                    case 19: scrollName = "nw_it_sparscr207"; break; // Invisibility
                    case 20: scrollName = "nw_it_sparscr208"; break; // Ghostly Visage
                    case 21: scrollName = "nw_it_sparscr209"; break; // Ghoul Touch
                    case 22: scrollName = "nw_it_sparscr210"; break; // Scare
                    case 23: scrollName = "nw_it_sparscr211"; break; // Blindness / Deafness
                    case 24: scrollName = "nw_it_sparscr212"; break; // Bull's Strength
                    case 25: scrollName = "nw_it_sparscr213"; break; // Cat's Grace
                    case 26: scrollName = "nw_it_sparscr214"; break; // Ultravision
                    case 27: scrollName = "nw_it_sparscr215"; break; // Endurance
                    case 28: scrollName = "nw_it_sparscr216"; break; // Knock
                    case 29: scrollName = "nw_it_sparscr217"; break; // Clarity
                    case 30: scrollName = "nw_it_sparscr218"; break; // Lesser Dispel
                    case 31: scrollName = "nw_it_sparscr219"; break; // Eagle's Splendor
                    case 32: scrollName = "nw_it_sparscr220"; break; // Fox's Cunning
                    case 33: scrollName = "nw_it_sparscr221"; break; // Owl's Wisdom
                    case 34: scrollName = "nw_it_sparscr301"; break; // Dispel Magic
                    case 35: scrollName = "nw_it_sparscr302"; break; // Magic Circle against Alignment
                    case 36: scrollName = "nw_it_sparscr303"; break; // Protection from Elements
                    case 37: scrollName = "nw_it_sparscr304"; break; // Flame Arrow
                    case 38: scrollName = "nw_it_sparscr305"; break; // Stinking Cloud
                    case 39: scrollName = "nw_it_sparscr306"; break; // Summon Creature III
                    case 40: scrollName = "nw_it_sparscr307"; break; // Clairaudience & Clairvoyance
                    case 41: scrollName = "nw_it_sparscr308"; break; // Hold Person
                    case 42: scrollName = "nw_it_sparscr309"; break; // Fireball
                    case 43: scrollName = "nw_it_sparscr310"; break; // Lightning Bolt
                    case 44: scrollName = "nw_it_sparscr311"; break; // Vampiric Touch
                    case 45: scrollName = "nw_it_sparscr312"; break; // Haste
                    case 46: scrollName = "nw_it_sparscr313"; break; // Slow
                    case 47: scrollName = "nw_it_sparscr314"; break; // Invisibility Sphere
                    case 48: scrollName = "nw_it_sparscr315"; break; // Negative Energy Burst
                    case 49: scrollName = "nw_it_sparscr401"; break; // Minor Globe of Invulnerability
                    case 50: scrollName = "nw_it_sparscr402"; break; // Remove Curse
                    case 51: scrollName = "nw_it_sparscr403"; break; // Stoneskin
                    case 52: scrollName = "nw_it_sparscr404"; break; // Summon Creature IV
                    case 53: scrollName = "nw_it_sparscr405"; break; // Charm Monster
                    case 54: scrollName = "nw_it_sparscr406"; break; // Confusion
                    case 55: scrollName = "nw_it_sparscr407"; break; // Wall of Fire
                    case 56: scrollName = "nw_it_sparscr408"; break; // Improved Invisibility
                    case 57: scrollName = "nw_it_sparscr409"; break; // Phantasmal Killer
                    case 58: scrollName = "nw_it_sparscr410"; break; // Shadow Conjuration
                    case 59: scrollName = "nw_it_sparscr411"; break; // Contagion
                    case 60: scrollName = "nw_it_sparscr412"; break; // Enervation
                    case 61: scrollName = "nw_it_sparscr413"; break; // Fear
                    case 62: scrollName = "nw_it_sparscr414"; break; // Bestow Curse
                    case 63: scrollName = "nw_it_sparscr415"; break; // Polymorph Self
                    case 64: scrollName = "nw_it_sparscr416"; break; // Elemental Shield
                    case 65: scrollName = "nw_it_sparscr417"; break; // Lesser Spell Breach
                    case 66: scrollName = "nw_it_sparscr418"; break; // Evard's Black Tentacles
                    case 67: scrollName = "nw_it_sparscr501"; break; // Dismissal
                    case 68: scrollName = "nw_it_sparscr502"; break; // Cloudkill
                    case 69: scrollName = "nw_it_sparscr503"; break; // Dominate Person
                    case 70: scrollName = "nw_it_sparscr504"; break; // Feeblemind
                    case 71: scrollName = "nw_it_sparscr505"; break; // Hold Monster
                    case 72: scrollName = "nw_it_sparscr506"; break; // Mind Fog
                    case 73: scrollName = "nw_it_sparscr507"; break; // Cone of Cold
                    case 74: scrollName = "nw_it_sparscr508"; break; // Greater Shadow Conjunction
                    case 75: scrollName = "nw_it_sparscr509"; break; // Animate Dead
                    case 76: scrollName = "nw_it_sparscr510"; break; // Summon Creature V
                    case 77: scrollName = "nw_it_sparscr511"; break; // Lesser Mind Blank
                    case 78: scrollName = "nw_it_sparscr512"; break; // Lesser Planar Binding
                    case 79: scrollName = "nw_it_sparscr513"; break; // Lesser Spell Mantle
                    case 80: scrollName = "nw_it_sparscr601"; break; // Globe of Invulnerability
                    case 81: scrollName = "nw_it_sparscr602"; break; // Greater Dispelling
                    case 82: scrollName = "nw_it_sparscr603"; break; // Acid Fog
                    case 83: scrollName = "nw_it_sparscr604"; break; // Planar Binding
                    case 84: scrollName = "nw_it_sparscr605"; break; // Summon Creature VI
                    case 85: scrollName = "nw_it_sparscr606"; break; // True Seeing
                    case 86: scrollName = "nw_it_sparscr607"; break; // Chain Lightning
                    case 87: scrollName = "nw_it_sparscr608"; break; // Ethereal Visage
                    case 88: scrollName = "nw_it_sparscr609"; break; // Shades
                    case 89: scrollName = "nw_it_sparscr610"; break; // Circle of Death
                    case 90: scrollName = "nw_it_sparscr611"; break; // Mass Haste
                    case 91: scrollName = "nw_it_sparscr612"; break; // Greater Spell Breach
                    case 92: scrollName = "nw_it_sparscr613"; break; // Greater Stoneskin
                    case 93: scrollName = "nw_it_sparscr614"; break; // Tenser's Transformation
                    case 94: scrollName = "nw_it_sparscr701"; break; // Spell Mantle
                    case 95: scrollName = "nw_it_sparscr702"; break; // Power Word, Stun
                    case 96: scrollName = "nw_it_sparscr703"; break; // Summon Creature VII
                    case 97: scrollName = "nw_it_sparscr704"; break; // Delayed Blast Fireball
                    case 98: scrollName = "nw_it_sparscr705"; break; // Mordenkainen's Sword
                    case 99: scrollName = "nw_it_sparscr706"; break; // Prismatic Spray
                    case 100: scrollName = "nw_it_sparscr707"; break; // Control Undead
                    case 101: scrollName = "nw_it_sparscr708"; break; // Finger of Death
                    case 102: scrollName = "nw_it_sparscr801"; break; // Mind Blank
                    case 103: scrollName = "nw_it_sparscr802"; break; // Protection from Spells
                    case 104: scrollName = "nw_it_sparscr803"; break; // Greater Planar Binding
                    case 105: scrollName = "nw_it_sparscr804"; break; // Incendiary Cloud
                    case 106: scrollName = "nw_it_sparscr805"; break; // Summon Creature VIII
                    case 107: scrollName = "nw_it_sparscr806"; break; // Mass Charm
                    case 108: scrollName = "nw_it_sparscr807"; break; // Mass Blindness / Deafness
                    case 109: scrollName = "nw_it_sparscr808"; break; // Premonition
                    case 110: scrollName = "nw_it_sparscr809"; break; // Horrid Wilting
                    case 111: scrollName = "nw_it_sparscr901"; break; // Mordenkainen's Disjunction
                    case 112: scrollName = "nw_it_sparscr902"; break; // Gate
                    case 113: scrollName = "nw_it_sparscr903"; break; // Power Word, Kill
                    case 114: scrollName = "nw_it_sparscr904"; break; // Summon Creature IX
                    case 115: scrollName = "nw_it_sparscr905"; break; // Dominate Monster
                    case 116: scrollName = "nw_it_sparscr906"; break; // Meteor Swarm
                    case 117: scrollName = "nw_it_sparscr907"; break; // Weird
                    case 118: scrollName = "nw_it_sparscr908"; break; // Energy Drain
                    case 119: scrollName = "nw_it_sparscr909"; break; // Wail of the Banshee
                    case 120: scrollName = "nw_it_sparscr910"; break; // Shapechange
                    case 121: scrollName = "nw_it_sparscr911"; break; // Time Stop
                    case 122: scrollName = "nw_it_sparscr912"; break; // Greater Spell Mantle
                    case 123: scrollName = "nw_it_spdvscr201"; break; // Lesser Restoration
                    case 124: scrollName = "nw_it_spdvscr202"; break; // Charm Person or Animal
                    case 125: scrollName = "nw_it_spdvscr203"; break; // Silence
                    case 126: scrollName = "nw_it_spdvscr204"; break; // Sound Burst
                    case 127: scrollName = "nw_it_spdvscr301"; break; // Remove Blindness / Deafness
                    case 128: scrollName = "nw_it_spdvscr302"; break; // Remove Disease
                    case 129: scrollName = "nw_it_spdvscr401"; break; // Restoration
                    case 130: scrollName = "nw_it_spdvscr402"; break; // Neutralize Poison
                    case 131: scrollName = "nw_it_spdvscr501"; break; // Raise Dead
                    case 132: scrollName = "nw_it_spdvscr701"; break; // Greater Restoration
                    case 133: scrollName = "nw_it_spdvscr702"; break; // Resurrection
                    case 134: scrollName = "x1_it_sparscr101"; break; // Expeditious Retreat
                    case 135: scrollName = "x1_it_sparscr102"; break; // Amplify
                    case 136: scrollName = "x1_it_sparscr103"; break; // Shield
                    case 137: scrollName = "x1_it_sparscr104"; break; // True Strike
                    case 138: scrollName = "x1_it_sparscr201"; break; // Balagarn's Iron Horn
                    case 139: scrollName = "x1_it_sparscr202"; break; // Tasha's Hideous Laughter
                    case 140: scrollName = "x1_it_sparscr301"; break; // Displacement
                    case 141: scrollName = "x1_it_sparscr302"; break; // Wounding Whispers
                    case 142: scrollName = "x1_it_sparscr303"; break; // Gust of Wind
                    case 143: scrollName = "x1_it_sparscr401"; break; // Isaac's Lesser Missile Storm
                    case 144: scrollName = "x1_it_sparscr501"; break; // Firebrand
                    case 145: scrollName = "x1_it_sparscr502"; break; // Bigby's Interposing Hand
                    case 146: scrollName = "x1_it_sparscr601"; break; // Dirge
                    case 147: scrollName = "x1_it_sparscr602"; break; // Bigby's Forceful Hand
                    case 148: scrollName = "x1_it_sparscr603"; break; // Isaac's Greater Missile Storm
                    case 149: scrollName = "x1_it_sparscr604"; break; // Stone to Flesh
                    case 150: scrollName = "x1_it_sparscr605"; break; // Flesh to Stone
                    case 151: scrollName = "x1_it_sparscr701"; break; // Bigby's Grasping Hand
                    case 152: scrollName = "x1_it_sparscr801"; break; // Bigby's Clenched Fist
                    case 153: scrollName = "x1_it_sparscr901"; break; // Bigby's Crushing Hand
                    case 154: scrollName = "x1_it_spdvscr101"; break; // Bane
                    case 155: scrollName = "x1_it_spdvscr102"; break; // Divine Favor
                    case 156: scrollName = "x1_it_spdvscr103"; break; // Entropic Shield
                    case 157: scrollName = "x1_it_spdvscr104"; break; // Inflict Light Wounds
                    case 158: scrollName = "x1_it_spdvscr105"; break; // Shield of Faith
                    case 159: scrollName = "x1_it_spdvscr106"; break; // Magic Fang
                    case 160: scrollName = "x1_it_spdvscr107"; break; // Camouflage
                    case 161: scrollName = "x1_it_spdvscr201"; break; // Inflict Moderate Wounds
                    case 162: scrollName = "x1_it_spdvscr202"; break; // Blood Frenzy
                    case 163: scrollName = "x1_it_spdvscr203"; break; // One with the Land
                    case 164: scrollName = "x1_it_spdvscr204"; break; // Aura of Glory
                    case 165: scrollName = "x1_it_spdvscr205"; break; // Flame Lash
                    case 166: scrollName = "x1_it_spdvscr301"; break; // Continual Flame
                    case 167: scrollName = "x1_it_spdvscr302"; break; // Inflict Serious Wounds
                    case 168: scrollName = "x1_it_spdvscr303"; break; // Greater Magic Fang
                    case 169: scrollName = "x1_it_spdvscr304"; break; // Spike Growth
                    case 170: scrollName = "x1_it_spdvscr305"; break; // Quillfire
                    case 171: scrollName = "x1_it_spdvscr401"; break; // Inflict Critical Wounds
                    case 172: scrollName = "x1_it_spdvscr402"; break; // Mass Camouflage
                    case 173: scrollName = "x1_it_spdvscr403"; break; // Flame Strike
                    case 174: scrollName = "x1_it_spdvscr501"; break; // Inferno
                    case 175: scrollName = "x1_it_spdvscr502"; break; // Owl's Insight
                    case 176: scrollName = "x1_it_spdvscr601"; break; // Banishment
                    case 177: scrollName = "x1_it_spdvscr602"; break; // Greater Sanctuary
                    case 178: scrollName = "x1_it_spdvscr603"; break; // Planar Ally
                    case 179: scrollName = "x1_it_spdvscr604"; break; // Drown
                    case 180: scrollName = "x1_it_spdvscr605"; break; // Create Undead
                    case 181: scrollName = "x1_it_spdvscr701"; break; // Aura of Vitality
                    case 182: scrollName = "x1_it_spdvscr702"; break; // Creeping Doom
                    case 183: scrollName = "x1_it_spdvscr703"; break; // Destruction
                    case 184: scrollName = "x1_it_spdvscr704"; break; // Fire Storm
                    case 185: scrollName = "x1_it_spdvscr801"; break; // Earthquake
                    case 186: scrollName = "x1_it_spdvscr802"; break; // Sunburst
                    case 187: scrollName = "x1_it_spdvscr803"; break; // Bombardment
                    case 188: scrollName = "x1_it_spdvscr804"; break; // Create Greater Undead
                    case 189: scrollName = "x1_it_spdvscr901"; break; // Undeath's Eternal Foe
                    case 190: scrollName = "x2_it_sparscr101"; break; // Horizikaul's Boom
                    case 191: scrollName = "x2_it_sparscr102"; break; // Ironguts
                    case 192: scrollName = "x2_it_sparscr103"; break; // Shelgarn's Persistent Blade
                    case 193: scrollName = "x2_it_sparscr104"; break; // Ice Dagger
                    case 194: scrollName = "x2_it_sparscr105"; break; // Magic Weapon
                    case 195: scrollName = "x2_it_sparscr201"; break; // Combust
                    case 196: scrollName = "x2_it_sparscr202"; break; // Death Armor
                    case 197: scrollName = "x2_it_sparscr203"; break; // Gedlee's Electric Loop
                    case 198: scrollName = "x2_it_sparscr204"; break; // Stone Bones
                    case 199: scrollName = "x2_it_sparscr205"; break; // Flame Weapon
                    case 200: scrollName = "x2_it_sparscr206"; break; // Cloud of Bewilderment
                    case 201: scrollName = "x2_it_sparscr207"; break; // Cat's Grace
                    case 202: scrollName = "x2_it_sparscr301"; break; // Mestil's Acid Breath
                    case 203: scrollName = "x2_it_sparscr302"; break; // Scintillating Sphere
                    case 204: scrollName = "x2_it_sparscr303"; break; // Keen Edge
                    case 205: scrollName = "x2_it_sparscr304"; break; // Greater Magic Weapon
                    case 206: scrollName = "x2_it_sparscr305"; break; //  Find Traps
                    case 207: scrollName = "x2_it_sparscr401"; break; // Ice Storm
                    case 208: scrollName = "x2_it_sparscr501"; break; // Ball Lightning
                    case 209: scrollName = "x2_it_sparscr502"; break; // Mestil's Acid Sheath
                    case 210: scrollName = "x2_it_sparscr503"; break; // Energy Buffer
                    case 211: scrollName = "x2_it_sparscr601"; break; // Undeath to Death
                    case 212: scrollName = "x2_it_sparscr602"; break; // Legend Lore
                    case 213: scrollName = "x2_it_sparscr701"; break; // Great Thunderclap
                    case 214: scrollName = "x2_it_sparscr703"; break; // Shadow Shield
                    case 215: scrollName = "x2_it_sparscr801"; break; // Blackstaff
                    case 216: scrollName = "x2_it_sparscr901"; break; // Black Blade of Disaster
                    case 217: scrollName = "x2_it_sparscr902"; break; // Tide of Battle
                    case 218: scrollName = "x2_it_spdvscr101"; break; // Deafening Clang
                    case 219: scrollName = "x2_it_spdvscr102"; break; // Bless Weapon
                    case 220: scrollName = "x2_it_spdvscr103"; break; // Bless
                    case 221: scrollName = "x2_it_spdvscr104"; break; // Cure Light Wounds
                    case 222: scrollName = "x2_it_spdvscr105"; break; // Doom
                    case 223: scrollName = "x2_it_spdvscr106"; break; // Entangle
                    case 224: scrollName = "x2_it_spdvscr107"; break; // Remove Fear
                    case 225: scrollName = "x2_it_spdvscr108"; break; // Sanctuary
                    case 226: scrollName = "x2_it_spdvscr201"; break; // Aid
                    case 227: scrollName = "x2_it_spdvscr202"; break; // Barkskin
                    case 228: scrollName = "x2_it_spdvscr203"; break; // Cure Moderate Wounds
                    case 229: scrollName = "x2_it_spdvscr204"; break; // Hold Animal
                    case 230: scrollName = "x2_it_spdvscr205"; break; // Remove Paralysis
                    case 231: scrollName = "x2_it_spdvscr301"; break; // Infestation of Maggots
                    case 232: scrollName = "x2_it_spdvscr302"; break; // Healing Sting
                    case 233: scrollName = "x2_it_spdvscr303"; break; // Blade Thirst
                    case 234: scrollName = "x2_it_spdvscr304"; break; // Magic Vestment
                    case 235: scrollName = "x2_it_spdvscr305"; break; // Darkfire
                    case 236: scrollName = "x2_it_spdvscr306"; break; // Glyph of Warding
                    case 237: scrollName = "x2_it_spdvscr307"; break; // Call Lightning
                    case 238: scrollName = "x2_it_spdvscr308"; break; // Cure Serious Wounds
                    case 239: scrollName = "x2_it_spdvscr309"; break; // Dominate Animal
                    case 240: scrollName = "x2_it_spdvscr310"; break; // Invisibility Purge
                    case 241: scrollName = "x2_it_spdvscr311"; break; // Negative Energy Protection
                    case 242: scrollName = "x2_it_spdvscr312"; break; // Prayer
                    case 243: scrollName = "x2_it_spdvscr313"; break; // Searing Light
                    case 244: scrollName = "x2_it_spdvscr401"; break; // Holy Sword
                    case 245: scrollName = "x2_it_spdvscr402"; break; // Cure Critical Wounds
                    case 246: scrollName = "x2_it_spdvscr403"; break; // Death Ward
                    case 247: scrollName = "x2_it_spdvscr404"; break; // Divine Power
                    case 248: scrollName = "x2_it_spdvscr405"; break; // Freedom of Movement
                    case 249: scrollName = "x2_it_spdvscr406"; break; // Hammer of the Gods
                    case 250: scrollName = "x2_it_spdvscr407"; break; // Poison
                    case 251: scrollName = "x2_it_spdvscr501"; break; // Battletide
                    case 252: scrollName = "x2_it_spdvscr502"; break; // Monstrous Regeneration
                    case 253: scrollName = "x2_it_spdvscr503"; break; // Vine Mine
                    case 254: scrollName = "x2_it_spdvscr504"; break; // Circle of Doom
                    case 255: scrollName = "x2_it_spdvscr505"; break; // Healing Circle
                    case 256: scrollName = "x2_it_spdvscr506"; break; // Slay Living
                    case 257: scrollName = "x2_it_spdvscr507"; break; // Spell Resistance
                    case 258: scrollName = "x2_it_spdvscr508"; break; // Awaken
                    case 259: scrollName = "x2_it_spdvscr509"; break; // Evil Blight
                    case 260: scrollName = "x2_it_spdvscr601"; break; // Crumble
                    case 261: scrollName = "x2_it_spdvscr602"; break; // Stonehold
                    case 262: scrollName = "x2_it_spdvscr603"; break; // Blade Barrier
                    case 263: scrollName = "x2_it_spdvscr604"; break; // Harm
                    case 264: scrollName = "x2_it_spdvscr605"; break; // Heal
                    case 265: scrollName = "x2_it_spdvscr606"; break; // Greater Sanctuary
                    case 266: scrollName = "x2_it_spdvscr701"; break; // Word of Faith
                    case 267: scrollName = "x2_it_spdvscr702"; break; // Regenerate
                    case 268: scrollName = "x2_it_spdvscr801"; break; // Mass Heal
                    case 269: scrollName = "x2_it_spdvscr802"; break; // Nature's Balance
                    case 270: scrollName = "x2_it_spdvscr803"; break; // Sunbeam
                    case 271: scrollName = "x2_it_spdvscr804"; break; // Aura versus Alignment
                    case 272: scrollName = "x2_it_spdvscr901"; break; // Elemental Swarm
                    case 273: scrollName = "x2_it_spdvscr902"; break; // Implosion
                    case 274: scrollName = "x2_it_spdvscr903"; break; // Storm of Vengeance
                    default: break;
                }

                NwItem item = NwItem.Create(scrollName, activateItem.ItemActivator, random.Next(1, 6));
                count++;
            }
            activateItem.ActivatedItem.Destroy();
        }
    }
}