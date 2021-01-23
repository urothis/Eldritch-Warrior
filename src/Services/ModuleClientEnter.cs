using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using NLog;

using NWN.API;
using NWN.API.Constants;
using NWN.API.Events;
using NWN.Services;

using NWNX.API;

namespace Module
{
    [ServiceBinding(typeof(ModuleClientEnter))]
    public class ModuleClientEnter
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public ModuleClientEnter(NativeEventService nativeEventService) =>
            nativeEventService.Subscribe<NwModule, ModuleEvents.OnClientEnter>(NwModule.Instance, OnClientEnter);

        private async void OnClientEnter(ModuleEvents.OnClientEnter enter)
        {
            /* Check player name and boot if its inappropriate */
            ClientCheckName(enter, enter.Player.Name);

            /* Add default journal entries */
            ClientEnterJournal(enter);

            await ClientPrintLogin(enter);

            /* This is to short circuit the rest of this code if we are DM */
            if (enter.Player.IsDM)
            {
                return;
            }

            /* Check if we are brand new player. */
            ClientFirstLogin(enter);
        }

        private static void ClientFirstLogin(ModuleEvents.OnClientEnter enter)
        {
            if (!enter.Player.Items.Any(x => x.ResRef == "item_recall"))
            {
                DestroyAllItems(enter);
            }
        }

        private static void DestroyAllItems(ModuleEvents.OnClientEnter enter)
        {
            /* Iterate all items in inventory and destroy them. */
            foreach (NwItem item in enter.Player.Items)
            {
                item.Destroy();
            }
        }

        private static void ClientCheckName(ModuleEvents.OnClientEnter enter, string text)
        {
            string[] censoredText = text.Split(' ');

            foreach (string censoredWord in censoredText)
            {
                if (WordFilter.Contains(censoredWord.ToLower()))
                {
                    enter.Player.BootPlayer($"BOOTED - Inappropriate character name {censoredWord} in {enter.Player.Name}");
                    Log.Info($"BOOTED - Inappropriate character name {censoredWord} in {enter.Player.Name}");
                    Administration.DeletePlayerCharacter(enter.Player, false);
                    return;
                }
            }
        }

        private static void ClientEnterJournal(ModuleEvents.OnClientEnter enter) => enter.Player.AddJournalQuestEntry("test", 1, false);

        /*
            https://gist.github.com/Jorteck/f7049ca1995ccea4dd5d4886f8c4254e

            Print to shout of client logging in if we are PC.
            Print to dm channel if dm logs in.
        */

        /* List of DM Public Keys */
        private static readonly Dictionary<string, string> dmID = new()
        {
            { "QR4JFL9A", "milliorn" },
            { "QRMXQ6GM", "milliorn" },
        };
        
        private static async Task ClientPrintLogin(ModuleEvents.OnClientEnter enter)
        {
            string colorString = $"\n{"NAME".ColorString(Color.GREEN)}:{enter.Player.Name.ColorString(Color.WHITE)}\n{"ID".ColorString(Color.GREEN)}:{enter.Player.CDKey.ColorString(Color.WHITE)}\n{"BIC".ColorString(Color.GREEN)}:{Player.GetBicFileName(enter.Player).ColorString(Color.WHITE)}";
            string clientDM = $"NAME:{enter.Player.Name} ID:{enter.Player.CDKey}";

            if (enter.Player.IsDM && dmID.ContainsKey(enter.Player.CDKey))
            {
                NwModule.Instance.SendMessageToAllDMs($"\n{"Entering DM ID VERIFIED".ColorString(Color.GREEN)}:{colorString}");
                Log.Info($"DM VERIFIED:{clientDM}.");

            }
            else if (enter.Player.IsDM)
            {
                NwModule.Instance.SendMessageToAllDMs($"\n{"Entering DM ID DENIED".ColorString(Color.RED)}:{colorString}");
                Log.Info($"DM DENIED:{clientDM}.");
                enter.Player.BootPlayer("DENIED DM Access.");
            }
            else
            {
                await NwModule.Instance.SpeakString($"\n{"LOGIN".ColorString(Color.GREEN)}:{colorString}", TalkVolume.Shout);
                Log.Info($"LOGIN:{$"NAME:{enter.Player.Name} ID:{enter.Player.CDKey} BIC:{Player.GetBicFileName(enter.Player)}"}.");
            }
        }

        /* Google list of explicit words */
        private static IList<string> WordFilter => new List<string>
        {
            "ahole",
            "anus",
            "ash0le",
            "ash0les",
            "asholes",
            "ass",
            "Ass Monkey",
            "Assface",
            "assh0le",
            "assh0lez",
            "asshole",
            "assholes",
            "assholz",
            "asswipe",
            "azzhole",
            "bassterds",
            "bastard",
            "bastards",
            "bastardz",
            "basterds",
            "basterdz",
            "Biatch",
            "bitch",
            "bitches",
            "Blow Job",
            "boffing",
            "butthole",
            "buttwipe",
            "c0ck",
            "c0cks",
            "c0k",
            "Carpet Muncher",
            "cawk",
            "cawks",
            "Clit",
            "cnts",
            "cntz",
            "cock",
            "cockhead",
            "cock-head",
            "cocks",
            "CockSucker",
            "cock-sucker",
            "crap",
            "cum",
            "cunt",
            "cunts",
            "cuntz",
            "dick",
            "dild0",
            "dild0s",
            "dildo",
            "dildos",
            "dilld0",
            "dilld0s",
            "dominatricks",
            "dominatrics",
            "dominatrix",
            "dyke",
            "enema",
            "f u c k",
            "f u c k e r",
            "fag",
            "fag1t",
            "faget",
            "fagg1t",
            "faggit",
            "faggot",
            "fagg0t",
            "fagit",
            "fags",
            "fagz",
            "faig",
            "faigs",
            "fart",
            "flipping the bird",
            "fuck",
            "fucker",
            "fuckin",
            "fucking",
            "fucks",
            "Fudge Packer",
            "fuk",
            "Fukah",
            "Fuken",
            "fuker",
            "Fukin",
            "Fukk",
            "Fukkah",
            "Fukken",
            "Fukker",
            "Fukkin",
            "g00k",
            "God-damned",
            "h00r",
            "h0ar",
            "h0re",
            "hells",
            "hoar",
            "hoor",
            "hoore",
            "jackoff",
            "jap",
            "japs",
            "jerk-off",
            "jisim",
            "jiss",
            "jizm",
            "jizz",
            "knob",
            "knobs",
            "knobz",
            "kunt",
            "kunts",
            "kuntz",
            "Lezzian",
            "Lipshits",
            "Lipshitz",
            "masochist",
            "masokist",
            "massterbait",
            "masstrbait",
            "masstrbate",
            "masterbaiter",
            "masterbate",
            "masterbates",
            "Motha Fucker",
            "Motha Fuker",
            "Motha Fukkah",
            "Motha Fukker",
            "Mother Fucker",
            "Mother Fukah",
            "Mother Fuker",
            "Mother Fukkah",
            "Mother Fukker",
            "mother-fucker",
            "Mutha Fucker",
            "Mutha Fukah",
            "Mutha Fuker",
            "Mutha Fukkah",
            "Mutha Fukker",
            "n1gr",
            "nastt",
            "nigger;",
            "nigur;",
            "niiger;",
            "niigr;",
            "orafis",
            "orgasim;",
            "orgasm",
            "orgasum",
            "oriface",
            "orifice",
            "orifiss",
            "packi",
            "packie",
            "packy",
            "paki",
            "pakie",
            "paky",
            "pecker",
            "peeenus",
            "peeenusss",
            "peenus",
            "peinus",
            "pen1s",
            "penas",
            "penis",
            "penis-breath",
            "penus",
            "penuus",
            "Phuc",
            "Phuck",
            "Phuk",
            "Phuker",
            "Phukker",
            "polac",
            "polack",
            "polak",
            "Poonani",
            "pr1c",
            "pr1ck",
            "pr1k",
            "pusse",
            "pussee",
            "pussy",
            "puuke",
            "puuker",
            "qweir",
            "recktum",
            "rectum",
            "retard",
            "sadist",
            "scank",
            "schlong",
            "screwing",
            "semen",
            "sex",
            "sexy",
            "Sh!t",
            "sh1t",
            "sh1ter",
            "sh1ts",
            "sh1tter",
            "sh1tz",
            "shit",
            "shits",
            "shitter",
            "Shitty",
            "Shity",
            "shitz",
            "Shyt",
            "Shyte",
            "Shytty",
            "Shyty",
            "skanck",
            "skank",
            "skankee",
            "skankey",
            "skanks",
            "Skanky",
            "slag",
            "slut",
            "sluts",
            "Slutty",
            "slutz",
            "son-of-a-bitch",
            "tit",
            "turd",
            "va1jina",
            "vag1na",
            "vagiina",
            "vagina",
            "vaj1na",
            "vajina",
            "vullva",
            "vulva",
            "w0p",
            "wh00r",
            "wh0re",
            "whore",
            "xrated",
            "xxx",
            "b!+ch",
            "bitch",
            "blowjob",
            "clit",
            "arschloch",
            "fuck",
            "shit",
            "ass",
            "asshole",
            "b!tch",
            "b17ch",
            "b1tch",
            "bastard",
            "bi+ch",
            "boiolas",
            "buceta",
            "c0ck",
            "cawk",
            "chink",
            "cipa",
            "clits",
            "cock",
            "cum",
            "cunt",
            "dildo",
            "dirsa",
            "ejakulate",
            "fatass",
            "fcuk",
            "fuk",
            "fux0r",
            "hoer",
            "hore",
            "jism",
            "kawk",
            "l3itch",
            "l3i+ch",
            "masturbate",
            "masterbat*",
            "masterbat3",
            "motherfucker",
            "s.o.b.",
            "mofo",
            "nazi",
            "nigga",
            "nigger",
            "nutsack",
            "phuck",
            "pimpis",
            "pusse",
            "pussy",
            "scrotum",
            "sh!t",
            "shemale",
            "shi+",
            "sh!+",
            "slut",
            "smut",
            "teets",
            "tits",
            "boobs",
            "b00bs",
            "teez",
            "testical",
            "testicle",
            "titt",
            "w00se",
            "jackoff",
            "wank",
            "whoar",
            "whore",
            "*damn",
            "*dyke",
            "*fuck*",
            "*shit*",
            "@$$",
            "amcik",
            "andskota",
            "arse*",
            "assrammer",
            "ayir",
            "bi7ch",
            "bitch*",
            "bollock*",
            "breasts",
            "butt-pirate",
            "cabron",
            "cazzo",
            "chraa",
            "chuj",
            "Cock*",
            "cunt*",
            "d4mn",
            "daygo",
            "dego",
            "dick*",
            "dike*",
            "dupa",
            "dziwka",
            "ejackulate",
            "Ekrem*",
            "Ekto",
            "enculer",
            "faen",
            "fag*",
            "fanculo",
            "fanny",
            "feces",
            "feg",
            "Felcher",
            "ficken",
            "fitt*",
            "Flikker",
            "foreskin",
            "Fotze",
            "Fu(*",
            "fuk*",
            "futkretzn",
            "gook",
            "guiena",
            "h0r",
            "h4x0r",
            "hell",
            "helvete",
            "hoer*",
            "honkey",
            "Huevon",
            "hui",
            "injun",
            "jizz",
            "kanker*",
            "kike",
            "klootzak",
            "kraut",
            "knulle",
            "kuk",
            "kuksuger",
            "Kurac",
            "kurwa",
            "kusi*",
            "kyrpa*",
            "lesbo",
            "mamhoon",
            "masturbat*",
            "merd*",
            "mibun",
            "monkleigh",
            "mouliewop",
            "muie",
            "mulkku",
            "muschi",
            "nazis",
            "nepesaurio",
            "nigger*",
            "orospu",
            "paska*",
            "perse",
            "picka",
            "pierdol*",
            "pillu*",
            "pimmel",
            "piss*",
            "pizda",
            "poontsee",
            "poop",
            "porn",
            "p0rn",
            "pr0n",
            "preteen",
            "pula",
            "pule",
            "puta",
            "puto",
            "qahbeh",
            "queef*",
            "rautenberg",
            "schaffer",
            "scheiss*",
            "schlampe",
            "schmuck",
            "screw",
            "sh!t*",
            "sharmuta",
            "sharmute",
            "shipal",
            "shiz",
            "skribz",
            "skurwysyn",
            "sphencter",
            "spic",
            "spierdalaj",
            "splooge",
            "suka",
            "b00b*",
            "testicle*",
            "titt*",
            "twat",
            "vittu",
            "wank*",
            "wetback*",
            "wichser",
            "wop*",
            "yed",
            "zabourah"
        };
    }
}
