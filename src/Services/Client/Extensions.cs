using System;
using System.Collections.Generic;
using System.Linq;
using NLog.Fluent;
using NWN.API;
using NWN.API.Constants;

namespace Services.Client
{
    public static class Extensions
    {
        /* Auto-Kill if we logout while in combat state */
        public static int DeathLog(this NwPlayer leave) => leave.IsInCombat ? leave.HP = -1 : leave.HP;
        public static string StripDashes(string uuid) => uuid = uuid.Replace("-", "");
        public static void StoreHitPoints(this NwPlayer player) => player.GetCampaignVariable<int>("Hit_Points", StripDashes(player.UUID.ToUUIDString())).Value = player.HP;
        public static void RestoreHitPoints(this NwPlayer player)
        {
            var id = player.UUID.ToUUIDString();

            if (player.GetCampaignVariable<int>("Hit_Points", id) == null)
            {
                player.GetCampaignVariable<int>("Hit_Points", id).Value = player.HP;
            }
            else
            {
                player.GetCampaignVariable<int>("Hit_Points", id).Value = player.HP;
            }
        }
        
        public static async void PrintLogout(this NwPlayer leave)
        {
            string colorString = $"\n{"NAME".ColorString(Color.GREEN)}:{leave.Name.ColorString(Color.WHITE)}\n{"ID".ColorString(Color.GREEN)}:{leave.CDKey.ColorString(Color.WHITE)}\n{"BIC".ColorString(Color.GREEN)}:{leave.BicFileName.ColorString(Color.WHITE)}";

            if (leave.IsDM)
            {
                NwModule.Instance.SendMessageToAllDMs($"\n{"Exiting DM".ColorString(Color.GREEN)}:{colorString}");
                Log.Info($"DM Exiting:{$"NAME:{leave.Name} ID:{leave.CDKey}"}.");
            }
            else
            {
                await NwModule.Instance.SpeakString($"\n{"LOGOUT".ColorString(Color.LIME)}:{colorString}", TalkVolume.Shout);
                Log.Info($"LOGOUT:{$"NAME:{leave.Name} ID:{leave.CDKey} BIC:{leave.BicFileName}"}.");
            }
        }

        public static bool ClientCheckName(this NwPlayer enter, string text)
        {
            foreach (var censoredWord in text.Split(' ').Where(censoredWord => Extensions.WordFilter.Contains(censoredWord.ToLower())))
            {
                enter.BootPlayer($"BOOTED - Inappropriate character name {censoredWord} in {enter.Name}");
                Log.Info($"BOOTED - Inappropriate character name {censoredWord} in {enter.Name}");
                return true;
            }

            return false;
        }

        public static void ValidateDM(this NwPlayer enter)
        {
            string clientDM = $"NAME:{enter.Name} ID:{enter.CDKey}";
            string colorString = $"\n{"NAME".ColorString(Color.GREEN)}:{enter.Name.ColorString(Color.WHITE)}\n{"ID".ColorString(Color.GREEN)}:{enter.CDKey.ColorString(Color.WHITE)}\n{"BIC".ColorString(Color.GREEN)}:{enter.BicFileName.ColorString(Color.WHITE)}";

            if (enter.IsDM && Module.Extensions.DMList.ContainsKey(enter.CDKey))
            {
                NwModule.Instance.SendMessageToAllDMs($"\n{"Entering DM ID VERIFIED".ColorString(Color.GREEN)}:{colorString}");
                Log.Info($"DM VERIFIED:{clientDM}.");

            }
            else
            {
                NwModule.Instance.SendMessageToAllDMs($"\n{"Entering DM ID DENIED".ColorString(Color.RED)}:{colorString}");
                Log.Info($"DM DENIED:{clientDM}.");
                enter.BootPlayer("DENIED DM Access.");
            }
        }

        public static void WelcomeMessage(this NwPlayer enter)
        {
            enter.SendServerMessage("Welcome to the server!".ColorString(SelectRandomColor(new(0, 0, 0), (Random)(new()))));
            string colorString = $"\n{"NAME".ColorString(Color.GREEN)}:{enter.Name.ColorString(Color.WHITE)}\n{"ID".ColorString(Color.GREEN)}:{enter.CDKey.ColorString(Color.WHITE)}\n{"BIC".ColorString(Color.GREEN)}:{enter.BicFileName.ColorString(Color.WHITE)}";
            NwModule.Instance.SpeakString($"\n{"LOGIN".ColorString(Color.LIME)}:{colorString}", TalkVolume.Shout);
            Log.Info($"LOGIN:{$"NAME:{enter.Name} ID:{enter.CDKey} BIC:{enter.BicFileName}"}.");
        }

        private static Color SelectRandomColor(Color color, Random random)
        {
            switch (random.Next(0, 16))
            {
                case 0: color = Color.BLUE; break;
                case 1: color = Color.BROWN; break;
                case 2: color = Color.CYAN; break;
                case 3: color = Color.GRAY; break;
                case 4: color = Color.GREEN; break;
                case 5: color = Color.LIME; break;
                case 6: color = Color.MAGENTA; break;
                case 7: color = Color.MAROON; break;
                case 8: color = Color.NAVY; break;
                case 9: color = Color.OLIVE; break;
                case 10: color = Color.ORANGE; break;
                case 11: color = Color.PINK; break;
                case 12: color = Color.PURPLE; break;
                case 13: color = Color.RED; break;
                case 14: color = Color.ROSE; break;
                case 15: color = Color.SILVER; break;
                case 16: color = Color.TEAL; break;
            }
            return color;
        }

        /* Google list of explicit words */
        public static IList<string> WordFilter => new List<string>
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