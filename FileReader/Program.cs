// See https://aka.ms/new-console-template for more information
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading;
using sd = System.Diagnostics;
using FileProcessingParsingReading;
using System.Threading.Tasks;
using System.Diagnostics;
using WeaponClasses;

Console.WriteLine("Hello, World!");
Console.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff"));

//FileReading r = new(FileReading.BuildOptions.IGS, true, false);

//Console.WriteLine(FileProcessing.WeaponOutputs.directPythonExecute("SCAR-L2.png", true, null));

string filepath = "output__rgn-udzs.png.txt";
string filepath2 = "output__rgn-udzs.png.txt";
Console.WriteLine(FileProcessing.Filenames.convertFileNameToGunName(filepath));
FileReading reading = new(FileReading.BuildOptions.NONE, true, null, null);
/*
List<Dictionary<int, FileProcessing.WeaponOutputs>> valuePairs = new(FileReading.thread1Async().Result);
foreach(Dictionary<int, FileProcessing.WeaponOutputs> pair in valuePairs)
{
    foreach(int j in pair.Keys)
    {
        Console.WriteLine(pair[j].Filename);
    }
}
*/
/*
FileParsing.findStatisticInFile(filepath, FileParsing.SearchTargets.DamageRange);
FileParsing.findStatisticInFile(filepath, FileParsing.SearchTargets.AmmoCapacity);
FileParsing.findStatisticInFile(filepath, FileParsing.SearchTargets.HeadMultiplier);
FileParsing.findStatisticInFile(filepath, FileParsing.SearchTargets.TorsoMultiplier);
FileParsing.findStatisticInFile(filepath, FileParsing.SearchTargets.LimbMultiplier);

FileParsing.findStatisticInFile(filepath, FileParsing.SearchTargets.MuzzleVelocity);
FileParsing.findStatisticInFile(filepath, FileParsing.SearchTargets.PenetrationDepth);

FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.ReloadTime);
FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.EmptyReloadTime);

FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.AimingWalkspeed);
FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.WeaponWalkspeed);

FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.AmmoType);
FileParsing.findStatisticInFile(filepath, FileParsing.SearchTargets.FireModes);
FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.FireModes);


FileParsing.findStatisticInFile(filepath, FileParsing.SearchTargets.Damage); //only __1.png files have the correct suppression
FileParsing.findStatisticInFile(filepath, FileParsing.SearchTargets.Rank);
FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.Rank);
FileParsing.findStatisticInFile(filepath, FileParsing.SearchTargets.Firerate);
FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.Firerate);*/


Console.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff"));

Console.ReadKey();





namespace FileProcessingParsingReading
{

    [Serializable]
    public class WordNotFoundException : Exception
    {
        public WordNotFoundException() { }
        public WordNotFoundException(string message) : base(message) { }
        public WordNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected WordNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class FileProcessing
    {

        private static string version = "1.00";

        public static string VERSION { get { return version; } }

        //build options
        public enum BuildOptions
        {
            None,
            AssaultRifles,
            PersonalDefenseWeapons,
            LightMachineGuns,
            SniperRifles,
            DesignatedMarksmanRifles,
            BattleRifles,
            Carbines,
            Shotguns,
            Pistols,
            MachinePistols,
            Revolvers,
            Other,
            FragmentationGrenades,
            HighExplosiveGrenades,
            ImpactGrenades,
            OneHandBladeMelees,
            OneHandBluntMelees,
            TwoHandBladeMelees,
            TwoHandBluntMelees,
            All
        }

        public enum StreamOptions
        {
            First,
            Second,
            Both
        }

        public class AllWeaponStrings
        {
            private static List<string> assaultRiflesStrings = new() {
                "ak12","an94","as-val","scar-l","aug-a1","m16a4","g36","m16a1","m16a3","type-20","aug-a2","k2","famas-f1",
                "ak47","aug-a3","l85a2","hk416","ak74","akm","ak103","tar-21","type-88","m231","c7a2","stg-44","g11k2" };

            private static List<string> personalDefenseWeaponsStrings = new() {
                "mp5k","ump45","g36c","mp7","mac10","p90","colt-mars","mp5","colt-smg-633","l2a3","mp5sd","mp10","m3a1",
                "mp510","uzi","aug-a3-para-xs","k7","ak74u","ppsh-41","fal-para-shorty","kriss-vector","pp-19-bizon","mp40",
                "x95-smg","tommy-gun","rama-1130"};

            private static List<string> lightMachineGunsStrings = new() {
                "colt-lmg","m60","aug-hbar","mg36","rpk12","l86-lsw","rpk","hk21e","hamr-iar","rpk74","mg3kws" };

            private static List<string> sniperRiflesStrings = new() {
                "intervention","model-700","dragunov-svu","aws","bfg-50","awm","trg-42","mosin-nagant","dragunov-svds",
                "m1903","k14","hecate-ii","ft300","m107","steyr-scout","wa2000","ntw-20"};

            private static List<string> battleRiflesStrings = new() {
                "m14","beowulf-ecr","scar-h","ak12br","g3a3","ag-3","hk417","henry-45-70","fal-5000"};

            private static List<string> designatedMarksmanRiflesStrings = new() {
                "mk11","sks","sl-8","vss-vintorez","msg90","m21","beowulf-tcr","sa58-spr","scar-ssr"};
            
            private static List<string> shotgunsStrings = new() {
                "ksg-12","model-870","dbv12","ks-23m","saiga-12","stevens-db","e-gun","aa-12","spas-12","dt11","usas-12"};

            private static List<string> carbineStrings = new() {
                "m4a1","g36k","m4","l22","scar-pdw","aku12","groza-1","ots-126","ak12c","honey-badger","k1a","sr-3m","groza-4",
                "mc51","fal-5063-para","1858-carbine","ak105","jury","kac-srr","gyrojet-carbine","c8a2","x95r","hk51b",
                "can-cannon"};

            private static List<string> pistolsStrings = new() {
                "g17","m9","m1911a1","desert-eagle-l5","g21","g23","m45a1","g40","kg-99","g50","five-seven","zip-22","gi-m1",
                "hardballer","izhevsk-pb","makarov-pm","gb-22","desert-eagle-xix","automag-iii","gyrojet-mark-i","gsp",
                "grizzly","m2011","alien","af2011-a1"};

            private static List<string> machinePistolsStrings = new() {
                "g18c","93r","pp-2000","tec-9","micro-uzi","skorpion-vz61","asmi","mp1911","arm-pistol"};

            private static List<string> revolversStrings = new() {
                "mp412-rex","mateba-6","1858-new-army","redhawk-44","judge","executioner"};

            private static List<string> othersStrings = new() {
                "super-shorty","sfg-50","m79-thumper","advanced-coilgun","sawed-off","saiga-12u","obrez","sass-308"};

            private static List<string> fragmentationGrenadesStrings = new() { 
                "m67-frag", "mk-2-frag", "m24-stick", "m26-frag", "m560-mini", "v40-mini", "roly-hg"};


            private static List<string> highExplosiveGrenadeStrings = new() { 
                "dynamite-3", "dynamite", "rgd-5-he","semtex", "pb-grenade", "bundle-charge"};

            private static List<string> impactGrenadeStrings = new() { "t-13-impact", "rgn-udzs", "rgo-udzs" };

            private static List<string> oneHandBladeStrings = new() { "cleaver", "tanzanite-blade", "war-fan", "nata-hatchet",
                "mekleth", "karambit", "krampus-kukri", "trench-knife", "knife", "tactical-spatula", "hunting-knife",
                "tanto", "entrencher", "ritual-sickle", "kama", "key", "ice-pick", "machete", "tomahawk", "pocket-knife",
                "havoc-blade", "cutter", "jason", "bridal-brandisher", "darkheart", "streiter", "balisong", "kommando",
                "linked-sword", "classic-knife", "jkey" };

            private static List<string> twoHandBladeStrings = new() { 
                "zircon-trident", "nordic-war-axe", "world-buster", "noobslayer", "hattori", "chosen-one", "reaper",
                "zero-cutter", "naginata", "training-bayonet", "longsword", "fire-axe", "harvester", "zweihander" };

            private static List<string> oneHandBluntStrings = new() { 
                "asp-baton", "toy-gun", "maglite-club", "crowbar", "mjolnir", "keyboard", "fumelee", "candy-cane", 
                "bare-fists", "tanzanite-pick", "stick-grenade", "bloxy", "holiday-tea", "trench-mace", "clonker", 
                "nightstick", "stun-gun", "uchiwa", "fixer", "brass-knuckle", "cricket-bat", "frying-pan", "arm-cannon", 
                "starlis-funpost"};

            private static List<string> twoHandBluntStrings = new() { "sledge-hammer", "hockey-stick", "sweeper", "baseball-bat",
                "paddle", "cursed-shinai", "banjo", "stylis-brush", "kanabo", "stopper", "the-axe", "void-staff",
                "morning-star", "present", "crane"};



            private List<string> stringBuilding = new();


            public static List<string> AssaultRiflesStrings { get { return assaultRiflesStrings; }}
            public static List<string> PersonalDefenseWeaponsStrings { get { return personalDefenseWeaponsStrings; }  }
            public static List<string> LightMachineGunsStrings { get { return lightMachineGunsStrings; }  }
            public static List<string> SniperRiflesStrings { get { return sniperRiflesStrings; } }
            public static List<string> BattleRiflesStrings { get { return battleRiflesStrings; } }
            public static List<string> DesignatedMarksmanRiflesStrings { get { return designatedMarksmanRiflesStrings; } }
            public static List<string> ShotgunsStrings { get { return shotgunsStrings; } }
            public static List<string> CarbineStrings { get { return carbineStrings; } }
            public static List<string> PistolsStrings { get { return pistolsStrings; } }
            public static List<string> MachinePistolsStrings { get { return machinePistolsStrings; } }
            public static List<string> RevolversStrings { get { return revolversStrings; } }
            public static List<string> OthersStrings { get { return othersStrings; } }
            //public List<string> ResultString { get { return stringBuilding; } set { stringBuilding = value; } } //redundant, but whatevs


            public static List<string> FragmentationGrenadesStrings { get { return fragmentationGrenadesStrings; } }
            public static List<string> HighExplosiveGrenadesStrings { get { return highExplosiveGrenadeStrings; } }
            public static List<string> ImpactGrenadesStrings { get { return impactGrenadeStrings; } }
            public static List<string> OneHandBladeMelees { get { return oneHandBladeStrings; } }
            public static List<string> OneHandBluntMelees { get { return oneHandBluntStrings; } }
            public static List<string> TwoHandBladeMelees { get { return twoHandBladeStrings; } }
            public static List<string> TwoHandBluntMelees { get { return twoHandBluntStrings; } }

            public AllWeaponStrings(BuildOptions buildOptions)
            {
                switch (buildOptions)
                {

                    case BuildOptions.None:
                        {
                            //empty
                            break;
                        }
                    case BuildOptions.AssaultRifles:
                        {
                            foreach (string s in assaultRiflesStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            break;
                        }
                    case BuildOptions.PersonalDefenseWeapons:
                        {
                            foreach (string s in personalDefenseWeaponsStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            break;
                        }
                    case BuildOptions.LightMachineGuns:
                        {
                            foreach (string s in lightMachineGunsStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            break;
                        }
                    case BuildOptions.SniperRifles:
                        {
                            foreach (string s in sniperRiflesStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            break;
                        }
                    case BuildOptions.DesignatedMarksmanRifles:
                        {
                            foreach (string s in designatedMarksmanRiflesStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            break;
                        }
                    case BuildOptions.BattleRifles:
                        {
                            foreach (string s in battleRiflesStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            break;
                        }
                    case BuildOptions.Carbines:
                        {
                            foreach (string s in carbineStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            break;
                        }
                    case BuildOptions.Shotguns:
                        {
                            foreach (string s in shotgunsStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            break;
                        }
                    case BuildOptions.Pistols:
                        {
                            foreach (string s in pistolsStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            break;
                        }
                    case BuildOptions.MachinePistols:
                        {
                            foreach (string s in machinePistolsStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            break;
                        }
                    case BuildOptions.Revolvers:
                        {
                            foreach (string s in revolversStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            break;
                        }
                    case BuildOptions.Other:
                        {
                            foreach (string s in othersStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            break;
                        }
                    case BuildOptions.FragmentationGrenades:
                        {
                            foreach (string s in fragmentationGrenadesStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            break;
                        }
                    case BuildOptions.HighExplosiveGrenades:
                        {
                            foreach (string s in highExplosiveGrenadeStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            break;
                        }
                    case BuildOptions.ImpactGrenades:
                        {
                            foreach (string s in impactGrenadeStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            break;
                        }
                    case BuildOptions.OneHandBladeMelees:
                        {
                            foreach (string s in oneHandBladeStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            break;
                        }
                    case BuildOptions.OneHandBluntMelees:
                        {
                            foreach (string s in oneHandBluntStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            break;
                        }
                    case BuildOptions.TwoHandBladeMelees:
                        {
                            foreach (string s in twoHandBladeStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            break;
                        }
                    case BuildOptions.TwoHandBluntMelees:
                        {
                            foreach (string s in twoHandBluntStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            break;
                        }
                    case BuildOptions.All:
                        {
                            foreach (string s in assaultRiflesStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            foreach (string s in personalDefenseWeaponsStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            foreach (string s in lightMachineGunsStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            foreach (string s in sniperRiflesStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            foreach (string s in designatedMarksmanRiflesStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            foreach (string s in battleRiflesStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            foreach (string s in carbineStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            foreach (string s in shotgunsStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            foreach (string s in pistolsStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            foreach (string s in machinePistolsStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            foreach (string s in revolversStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            foreach (string s in othersStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            foreach (string s in fragmentationGrenadesStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            foreach (string s in highExplosiveGrenadeStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            foreach (string s in impactGrenadeStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            foreach (string s in oneHandBladeStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            foreach (string s in oneHandBluntStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            foreach (string s in twoHandBladeStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            foreach (string s in twoHandBluntStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            break;
                        }
                }
            }

            public List<string> GetStrings()
            {
                return stringBuilding;
            }

        }

        public class WeaponOutputs
        {
            private string fileOutput;
            private string filename;

            public string FileOutput
            {
                get { return fileOutput; }
                set { fileOutput = value; }
            }
            public string Filename
            {
                get { return filename; }
                set { filename = value; }
            }


            public WeaponOutputs(string filename, bool? primaryOrSecondary, bool? meleeOrGrenade)
            {
                this.filename = filename;
                this.fileOutput = executePython(@"C:\Users\peter\source\repos\Phantom Forces Database\ImageParser\dist\ImageParser.exe", @"..\..\..\..\ImageParser\Weapons\" + filename, @"..\..\..\..\ImageParser\", filename, primaryOrSecondary, meleeOrGrenade);
            }

            public WeaponOutputs(string filename, string fileOutput)
            {
                this.filename = filename;
                this.fileOutput= fileOutput;
            }

            public WeaponOutputs(WeaponOutputs outputs)
            {
                filename = outputs.Filename;
                fileOutput = outputs.FileOutput;
            }

            public static string directPythonExecute(string filename, bool? primaryOrSecondary, bool? meleeOrGrenade)
            {
                return executePython(@"C:\Users\peter\source\repos\Phantom Forces Database\ImageParser\dist\ImageParser.exe", @"..\..\..\..\ImageParser\Weapons\" + filename, @"..\..\..\..\ImageParser\",  filename, primaryOrSecondary, meleeOrGrenade);
            }

        }

        public class Filenames
        {

            private string filename;
            private string filecontents;
            private static string fileprefix = "output__";
            private static string fileextension1 = ".png";
            private static string fileextension2 = ".txt";

            public string Filecontents
            {
                get { return filecontents; }
                set { filecontents = value; }
            }
            public string Filename
            {
                get { return filename; }
                set { filename = value; }
            }

            public static string FilePrefix { get { return fileprefix; } }
            public static string FileExtension1 { get { return fileextension1; } }
            public static string FileExtension2 { get { return fileextension2; } }

            public Filenames(string filename, string? filecontents)
            {

                this.filename = filename;
                this.filecontents = filecontents ?? "";
            }

            public Filenames(WeaponOutputs outputs)
            {
                this.filename = outputs.Filename;
                this.filecontents = outputs.FileOutput;
            }

            //to save a file (non-static)
            public void SetFilenames(Tuple<bool, bool, bool> options, string? logfilepath, bool noExtensions)
            {
                if (options.Item1)
                {
                    Console.WriteLine(filecontents);
                }
                if (options.Item2)
                {
                    if (logfilepath != null && logfilepath != "") File.AppendAllText(logfilepath, filecontents);

                }
                if (options.Item3)
                {
                    File.AppendAllText(!noExtensions ? (fileprefix + filename + fileextension2) : filename, filecontents);
                }
            }


            public static WeaponOutputs GetFilenames(string filepath)
            {
                WeaponOutputs output = new(filepath, "");
                try
                {
                    output.FileOutput = File.ReadAllText(filepath);
                }catch(FileNotFoundException)
                {
                    string gunName = convertFileNameToGunName(filepath);
                    List<List<string>> strings = new() {
                        AllWeaponStrings.AssaultRiflesStrings, AllWeaponStrings.PersonalDefenseWeaponsStrings, AllWeaponStrings.LightMachineGunsStrings, AllWeaponStrings.SniperRiflesStrings, AllWeaponStrings.CarbineStrings, AllWeaponStrings.BattleRiflesStrings, AllWeaponStrings.DesignatedMarksmanRiflesStrings, AllWeaponStrings.ShotgunsStrings,
                        AllWeaponStrings.PistolsStrings, AllWeaponStrings.MachinePistolsStrings, AllWeaponStrings.RevolversStrings, AllWeaponStrings.OthersStrings,
                        AllWeaponStrings.FragmentationGrenadesStrings, AllWeaponStrings.HighExplosiveGrenadesStrings, AllWeaponStrings.ImpactGrenadesStrings,
                        AllWeaponStrings.OneHandBluntMelees, AllWeaponStrings.OneHandBladeMelees,AllWeaponStrings.TwoHandBluntMelees,AllWeaponStrings.TwoHandBladeMelees
                    };



                    foreach(List<string> category in strings)
                    {
                        foreach(string str in category)
                        {
                            if(gunName == str)
                            {
                                Console.WriteLine(category.ToString() + " needs to be rebuilt. Missing " + gunName);
                            }
                        }
                    }
                }
                output.Filename = filepath;
                return output;
            }

            public static string convertFileNameToGunName(string filename)
            {
                string result = "";
                if (filename.StartsWith(fileprefix))
                {
                    string removed1 = filename.Remove(0, fileprefix.Length);
                    string removed2 = removed1.Remove(removed1.IndexOf(fileextension2));
                    result = removed2.Remove(removed2.IndexOf(fileextension1));
                }
                return result;
            }

            public static string convertFileNameToImageName(string filename)
            {
                string result = "";
                if (filename.StartsWith(fileprefix))
                {
                    string removed1 = filename.Remove(0, fileprefix.Length);
                    result += removed1.Remove(removed1.IndexOf(fileextension2));
                }
                return result;
            }

            public static string convertGunNameToFileName(string filename)
            {
                string result1 = "";
                result1 += fileprefix + filename + fileextension1 + fileextension2;
                return result1;
            }

            public static Tuple<string, string, string> convertGunNameToFileNames(string filename)
            {
                string result1 = "", result2 = "", result3 = "";
                result1 += fileprefix + filename + fileextension1 + fileextension2;
                result2 += fileprefix + filename + "1" + fileextension1 + fileextension2;
                result3 += fileprefix + filename + "2" + fileextension1 + fileextension2;
                return Tuple.Create(result1, result2, result3);
            }

            public static string convertGunNameToImageName(string filename)
            {
                string result = "";
                result += filename + fileextension1;
                return result;
            }

            public static Tuple<string, string, string> convertGunNameToImageNames(string filename)
            {
                string result1 = "", result2 = "", result3 = "";
                result1 += filename + fileextension1;
                result2 += filename + "1" + fileextension1;
                result3 += filename + "2" + fileextension1;
                return Tuple.Create(result1, result2, result3);
            }

            public static string convertGunNameToUppercaseImageName(string filename)
            {
                string result = "";
                result += filename.ToUpperInvariant() + fileextension1;
                return result;
            }

            public static Tuple<string, string, string> convertGunNameToUppercaseImageNames(string filename)
            {
                string result1 = "", result2 = "", result3 = "";
                result1 += filename.ToUpperInvariant() + fileextension1;
                result2 += filename.ToUpperInvariant() + "1" + fileextension1;
                result3 += filename.ToUpperInvariant() + "2" + fileextension1;
                return Tuple.Create(result1, result2, result3);
            }

        }



        private List<string> filepathStrings;

        private Dictionary<int, WeaponOutputs> weaponOutputsList;

        public Dictionary<int, WeaponOutputs> WeaponOutputsList { get { return weaponOutputsList; } set { weaponOutputsList = value; } }

        //readline, build options, file parsing, all encapsulated in ONE class
        //tuple options: write to console, write to one large log file, write to individual files
        //for redundancy
        public FileProcessing(BuildOptions options, Tuple<bool, bool, bool> outputOptions, string logfilepath, bool overwrite)
        {
            AllWeaponStrings main = new(options);
            filepathStrings = new(main.GetStrings());
            bool? primaryOrSecondary = true;
            bool? meleeOrGrenade = true;

            if (options == BuildOptions.AssaultRifles || options == BuildOptions.PersonalDefenseWeapons || options == BuildOptions.LightMachineGuns || options == BuildOptions.SniperRifles
                || options == BuildOptions.DesignatedMarksmanRifles || options == BuildOptions.BattleRifles || options == BuildOptions.Carbines
                || options == BuildOptions.Shotguns)
            {
                meleeOrGrenade = null;
            }

            //add melees and grenades
            if (options == BuildOptions.Pistols || options == BuildOptions.MachinePistols || options == BuildOptions.Revolvers || options == BuildOptions.Other) 
            { 
                primaryOrSecondary = false;
                meleeOrGrenade = null;
            }


            if(options == BuildOptions.FragmentationGrenades || options == BuildOptions.HighExplosiveGrenades || options == BuildOptions.ImpactGrenades)
            {
                primaryOrSecondary = null;
                meleeOrGrenade = false;
            }

            if (options == BuildOptions.OneHandBladeMelees || options == BuildOptions.OneHandBluntMelees || options == BuildOptions.TwoHandBluntMelees || options == BuildOptions.TwoHandBladeMelees)
            {
                primaryOrSecondary = null;
                meleeOrGrenade = true;
            }

            if (filepathStrings.Count <= 0)
            {
                //do nothing
            }
            else
            {
                Dictionary<int, WeaponOutputs> outputs = new(readFilelist(filepathStrings, primaryOrSecondary, meleeOrGrenade));
                //calls readfilelist(), which initializes the weaponoutputs objects, each of which have a constructro that calls executepython()
                string logfileoutput = "";
                foreach (int j in outputs.Keys)
                {
                    if (outputOptions.Item1)
                    {
                        Console.WriteLine(outputs[j].FileOutput);
                    }
                    if (outputOptions.Item2)
                    {
                        if (overwrite)
                        {
                            logfileoutput += outputs[j].FileOutput;
                        }
                        else
                        {
                            File.AppendAllText(logfilepath, outputs[j].FileOutput);
                        }
                    }
                    if (outputOptions.Item3)
                    {
                        if (overwrite)
                        {
                            File.WriteAllText(Filenames.FilePrefix + outputs[j].Filename + Filenames.FileExtension2, outputs[j].FileOutput);
                        }
                        else
                        {
                            File.AppendAllText(Filenames.FilePrefix + outputs[j].Filename + Filenames.FileExtension2, outputs[j].FileOutput);
                        }
                    }
                    weaponOutputsList = outputs;
                }
                if (overwrite && outputOptions.Item2) File.WriteAllText(logfilepath, logfileoutput);
            }
        }

        public static string executePython(string cmd, string path, string tessbindata, string name, bool? primaryOrSecondary, bool? meleeOrGrenade)
        {
            string startTime = DateTime.Now.ToString("HH:mm:ss:fff");
            var watch = sd.Stopwatch.StartNew();
            string POS = primaryOrSecondary?.ToString() ?? "k";
            string MOG = meleeOrGrenade?.ToString() ?? "k";
            
            ProcessStartInfo start = new(cmd, string.Format("{0} {1} {2} {3}", path, tessbindata, POS, MOG));

            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            if (start != null)
            {
                using (Process process = Process.Start(start) ?? Process.Start(new ProcessStartInfo(cmd, string.Format("{0} {1}", path, tessbindata))))
                {
                    if (process != null)
                    {
                        using (StreamReader reader = process.StandardOutput)
                        {
                            string result = reader.ReadToEnd();
                            string finishTime = DateTime.Now.ToString("HH:mm:ss:fff");
                            //version is the ONLY non-static member
                            string finalOutput = "VERSION:" + VERSION + "\nStarted at " + startTime + "\n" + result + "\n" + "Finished at " + finishTime + "\nTime elapsed: " + watch.ElapsedMilliseconds.ToString() + "\n" + @"C:\Users\peter\source\repos\Phantom Forces Database\ImageParser\Weapons\" + name;
                            Console.WriteLine(name);
                            return finalOutput;
                        }
                    }
                }
            }
            return "Failed. Time = " + DateTime.Now.ToString("HH:mm:ss:fff") + ".";
            //Process.Start(start);
        }

        public static List<string> imagePaths(List<string> strings, bool? meleeOrGrenades)
        {
            List<string> result = new();
            if (meleeOrGrenades == null)
            {
                foreach (string str in strings)
                {
                    result.Add(Filenames.convertGunNameToImageNames(str).Item2);
                    result.Add(Filenames.convertGunNameToImageNames(str).Item3);
                }
            }
            else
            {
                foreach (string str in strings)
                {
                    result.Add(Filenames.convertGunNameToImageNames(str).Item1);
                }
            }
            return result;
        }

        public static Dictionary<int, WeaponOutputs> readFilelist(List<string> filenames, bool? primaryOrSecondary, bool? meleeOrGrenade)
        {
            List<string> filepaths = (meleeOrGrenade != null && (meleeOrGrenade == true || meleeOrGrenade == false))?new(filenames): new(imagePaths(filenames, meleeOrGrenade));
            Dictionary<int, WeaponOutputs> result = new();
            for (int i = 0; i < filepaths.Count; i++)
            {
                result.Add(i, new WeaponOutputs(filepaths[i], primaryOrSecondary, meleeOrGrenade));
            }
            return result;
        }

        public static Tuple<List<string>, List<string>> multithreadedImagePaths(List<string> filenames, bool? meleeOrGrenade)
        {
            List<string> stream1 = new();
            List<string> stream2 = new();
            if (meleeOrGrenade == null)
            {
                foreach (string str in filenames)
                {
                    stream1.Add(Filenames.convertGunNameToImageNames(str).Item2);
                    stream2.Add(Filenames.convertGunNameToImageNames(str).Item3);
                }
            }
            else
            {
                foreach (string str in filenames)
                {
                    stream1.Add(Filenames.convertGunNameToImageNames(str).Item1);
                    stream2.Add(Filenames.convertGunNameToImageNames(str).Item1);
                }
            }
            return Tuple.Create(stream1, stream2);
        }

        public static Dictionary<int, WeaponOutputs> multithreadedReadFileList(StreamOptions streamChoice, List<string> strings, Tuple<bool, bool, bool> outputOptions, string logfilepath, bool? primaryOrSecondary, bool? meleeOrGrenade)
        {
            List<string> threadBuilds;
            switch (streamChoice)
            {
                case StreamOptions.First:
                    {
                        threadBuilds = new(multithreadedImagePaths(strings, meleeOrGrenade).Item1);
                        break;
                    }
                case StreamOptions.Second:
                    {
                        threadBuilds = new(multithreadedImagePaths(strings, meleeOrGrenade).Item2);
                        break;
                    }
                case StreamOptions.Both:
                    {
                        threadBuilds = new(imagePaths(strings, meleeOrGrenade));
                        break;
                    }
                default:
                    {
                        threadBuilds = new();
                        break;
                    }
            }
            Dictionary<int, WeaponOutputs> outputs = new();
            for (int i = 0; i < threadBuilds.Count; i++)
            {
                outputs.Add(i, new WeaponOutputs(threadBuilds[i], primaryOrSecondary, meleeOrGrenade));
            }
            //calls readfilelist(), which initializes the weaponoutputs objects, each of which have a constructro that calls executepython()
            //string logfileoutput = "";
            foreach (int j in outputs.Keys)
            {
                Filenames filenames = new(outputs[j]);
                filenames.SetFilenames(outputOptions, logfilepath, true);
            }
            return outputs;

        }

        public static async Task<Dictionary<int, WeaponOutputs>> multithreadedReadFileListAsync(StreamOptions streamChoice, List<string> strings, Tuple<bool, bool, bool> outputOptions, string logfilepath, bool? primaryOrSecondary, bool? meleeOrGrenade, Dictionary<int, WeaponOutputs> outDictionary)
    {
            List<string> threadBuilds;
            switch (streamChoice)
            {
                case StreamOptions.First:
                    {
                        threadBuilds = new(multithreadedImagePaths(strings, meleeOrGrenade).Item1);
                        break;
                    }
                case StreamOptions.Second:
                    {
                        threadBuilds = new(multithreadedImagePaths(strings, meleeOrGrenade).Item2);
                        break;
                    }
                case StreamOptions.Both:
                    {
                        threadBuilds = new(imagePaths(strings, meleeOrGrenade));
                        break;
                    }
                default:
                    {
                        threadBuilds = new();
                        break;
                    }
            }
            Dictionary<int, WeaponOutputs> outputs = outDictionary;
            for (int i = 0; i < threadBuilds.Count; i++)
            {
                outputs.Add(i, new WeaponOutputs(threadBuilds[i], primaryOrSecondary, meleeOrGrenade));
            }
            //calls readfilelist(), which initializes the weaponoutputs objects, each of which have a constructro that calls executepython()
            //string logfileoutput = "";
            foreach (int j in outputs.Keys)
            {
                Filenames filenames = new(outputs[j]);
                filenames.SetFilenames(outputOptions, logfilepath, true);
            }
            return outputs;
        }

    }

    public class FileParsing
    {
        public enum SearchTargets
        {
            Version,
            
            Rank,
            //guns
            Damage,
            Firerate,
            AmmoCapacity,
            FireModes,
            HeadMultiplier,
            TorsoMultiplier,
            LimbMultiplier,
            DamageRange,
            MuzzleVelocity,
            PenetrationDepth,
            Suppression,
            ReloadTime,
            EmptyReloadTime,
            WeaponWalkspeed,
            AimingWalkspeed,
            AmmoType,
            //grenade
            BlastRadius,
            KillingRadius,
            MaxiumumDamage,
            TriggerMechanism,
            SpecialEffects,
            StoredCapacity,
            //melees
            BladeLength,
            FrontStabDamage,
            BackStabDamage,
            Walkspeed
        }


        private static List<int> indexFinder(string filetext, string word)
        {

            List<int> output = new();
            for (; filetext.Contains(word, StringComparison.CurrentCultureIgnoreCase);)
            {
                output.Add(filetext.LastIndexOf(word, StringComparison.CurrentCultureIgnoreCase));
                try
                {
                    filetext = filetext.Remove(filetext.LastIndexOf(word, StringComparison.CurrentCultureIgnoreCase), word.Length);
                }
                catch (ArgumentOutOfRangeException) { break; }
            }
            return output;
        }

        public static string insertAndIterate(string original, string input, List<int> index, int counter, int finished)
        {

            int changed = counter--;
            if (counter >= finished)
            {
                return original.Insert(index[counter], input);
            }
            else
            {
                return insertAndIterate(original.Insert(index[counter], input), input, index, changed, finished);
            }
        }

        private static void inputWordsSelection(SearchTargets targets, ref string inputWord1, ref string? inputWord2)
        {

            switch (targets)
            {
                case SearchTargets.Rank:
                    {
                        inputWord1 = "rank";
                        break;
                    }
                case SearchTargets.Damage:
                    {
                        inputWord1 = "damage";
                        inputWord2 = "range"; //to be selected AGAINST
                        break;
                    }
                case SearchTargets.Firerate:
                    {
                        inputWord1 = "firerate";
                        break;
                    }
                case SearchTargets.AmmoCapacity:
                    {
                        inputWord1 = "ammo";
                        inputWord2 = "capacity";
                        break;
                    }
                case SearchTargets.FireModes:
                    {
                        inputWord1 = "fire";
                        inputWord2 = "modes";
                        break;
                    }
                case SearchTargets.HeadMultiplier:
                    {
                        inputWord1 = "head";
                        inputWord2 = "multiplier";
                        break;
                    }
                case SearchTargets.TorsoMultiplier:
                    {
                        inputWord1 = "torso";
                        inputWord2 = "multiplier";
                        break;
                    }
                case SearchTargets.LimbMultiplier:
                    {
                        inputWord1 = "limb";
                        inputWord2 = "multiplier";
                        break;
                    }
                case SearchTargets.DamageRange:
                    {
                        inputWord1 = "Damage";
                        inputWord2 = "Range"; //to be selected FOR
                        break;
                    }
                case SearchTargets.MuzzleVelocity:
                    {
                        inputWord1 = "muzzle";
                        inputWord2 = "velocity";
                        break;
                    }
                case SearchTargets.PenetrationDepth:
                    {
                        inputWord1 = "penetration";
                        inputWord2 = "depth";
                        break;
                    }
                case SearchTargets.Suppression:
                    {
                        inputWord1 = "suppression";
                        break;
                    }
                case SearchTargets.ReloadTime:
                    {
                        inputWord1 = "reload";
                        inputWord2 = "time";
                        break;
                    }
                case SearchTargets.EmptyReloadTime:
                    {
                        inputWord1 = "empty";
                        inputWord2 = "reload time";
                        break;
                    }
                case SearchTargets.WeaponWalkspeed:
                    {
                        inputWord1 = "weapon";
                        inputWord2 = "walkspeed";
                        break;
                    }
                case SearchTargets.AimingWalkspeed:
                    {
                        inputWord1 = "aiming";
                        inputWord2 = "walkspeed";
                        break;
                    }
                case SearchTargets.AmmoType:
                    {
                        inputWord1 = "ammo";
                        inputWord2 = "type";
                        break;
                    }
                case SearchTargets.BlastRadius:
                    {
                        inputWord1 = "blast";
                        inputWord2 = "radius";
                        break;
                    }
                case SearchTargets.KillingRadius:
                    {
                        inputWord1 = "killing";
                        inputWord2 = "radius";
                        break;
                    }
                case SearchTargets.TriggerMechanism:
                    {
                        inputWord1 = "trigger";
                        inputWord2 = "mechanism";
                        break;
                    }
                case SearchTargets.SpecialEffects:
                    {
                        inputWord1 = "special";
                        inputWord2 = "effects";
                        break;
                    }
                case SearchTargets.StoredCapacity:
                    {
                        inputWord1 = "stored";
                        inputWord2 = "capacity";
                        break;
                    }
                case SearchTargets.BladeLength:
                    {
                        inputWord1 = "blade";
                        inputWord2 = "length";
                        break;
                    }
                case SearchTargets.FrontStabDamage:
                    {
                        inputWord1 = "front stab";
                        inputWord2 = "damage";
                        break;
                    }
                case SearchTargets.BackStabDamage:
                    {
                        inputWord1 = "back stab";
                        inputWord2 = "damage";
                        break;
                    }
                case SearchTargets.Walkspeed:
                    {
                        inputWord1 = "walkspeed";
                        break;
                    }
                case SearchTargets.Version:
                    {
                        inputWord1 = "version";
                        break;
                    }


            }
        }

        public static string findStatisticInFile(string filepath, SearchTargets targets)
        {
            if (filepath == null) throw new ArgumentNullException(nameof(filepath));
            if (!File.Exists(filepath)) throw new FileNotFoundException("File not found.", nameof(filepath));
            string filetext = File.ReadAllText(filepath);

            string inputWord1 = "";
            string? inputWord2 = null;

            inputWordsSelection(targets, ref inputWord1, ref inputWord2);

            List<int> inputWord1Locations = new(indexFinder(filetext, inputWord1));
            List<int> inputWord2Locations = new();

            bool found = false;
            int currentPosition = 0;
            int foundCounter = 0;
            string result = "";

            if (inputWord2 != null)
            {
                 inputWord2Locations = indexFinder(filetext, inputWord2);
            }
            //targets == SearchTargets.LimbMultiplier || targets == SearchTargets.WeaponWalkspeed || targets == SearchTargets.AimingWalkspeed
            foreach (int indexI in inputWord1Locations)
            {
                if (inputWord2 != null) //2 words
                {
                    foreach (int indexJ in inputWord2Locations)
                    {
                        if (targets == SearchTargets.Damage)
                        {
                            //special
                            if(filetext.Substring(indexI, inputWord1.Length + inputWord2.Length + 4).Contains(inputWord2))
                            {
                                //Console.WriteLine("found damage + range");
                            }
                            else
                            {
                                //Console.WriteLine("found only damage");
                                found = true;
                                foundCounter++;
                                currentPosition = indexI + inputWord1.Length;
                                break;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < inputWord1.Length + inputWord2.Length + 3; i++)
                            {
                                if (indexI + i == indexJ)
                                {
                                    //Console.WriteLine("found!! at: " + indexJ.ToString());
                                    found = true;
                                    foundCounter++;
                                    currentPosition = indexJ + inputWord2.Length;
                                    break;
                                }
                            }
                        }
                    }
                }
                else //1 word
                {
                    //rank, suppression, firerate
                    found = true;
                    currentPosition = indexI + inputWord1.Length;
                    break;
                }
            }

            if (found)
            {
                for(int j = currentPosition; filetext[j] != 10 && filetext[j] != 12 && filetext[j] != 13; j++)
                {
                    result += filetext[j];
                }
            }
            //Console.WriteLine(inputWord1 + inputWord2 ?? "");
            Console.WriteLine(result);
            return result;
        }

        public async static Task<string> findStatisticInFileAsync(string filepath, SearchTargets targets)
        {
            if (filepath == null) throw new ArgumentNullException(nameof(filepath));
            if (!File.Exists(filepath)) throw new FileNotFoundException("File not found.", nameof(filepath));
            string filetext = File.ReadAllText(filepath);

            string inputWord1 = "";
            string? inputWord2 = null;

            inputWordsSelection(targets, ref inputWord1, ref inputWord2);

            List<int> inputWord1Locations = new(indexFinder(filetext, inputWord1));
            List<int> inputWord2Locations = new();

            bool found = false;
            int currentPosition = 0;
            int foundCounter = 0;
            string result = "";

            if (inputWord2 != null)
            {
                inputWord2Locations = indexFinder(filetext, inputWord2);
            }
            //targets == SearchTargets.LimbMultiplier || targets == SearchTargets.WeaponWalkspeed || targets == SearchTargets.AimingWalkspeed
            foreach (int indexI in inputWord1Locations)
            {
                if (inputWord2 != null) //2 words
                {
                    foreach (int indexJ in inputWord2Locations)
                    {
                        if (targets == SearchTargets.Damage)
                        {
                            //special
                            if (filetext.Substring(indexI, inputWord1.Length + inputWord2.Length + 4).Contains(inputWord2))
                            {
                                //Console.WriteLine("found damage + range");
                            }
                            else
                            {
                                //Console.WriteLine("found only damage");
                                found = true;
                                foundCounter++;
                                currentPosition = indexI + inputWord1.Length;
                                break;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < inputWord1.Length + inputWord2.Length + 3; i++)
                            {
                                if (indexI + i == indexJ)
                                {
                                    //Console.WriteLine("found!! at: " + indexJ.ToString());
                                    found = true;
                                    foundCounter++;
                                    currentPosition = indexJ + inputWord2.Length;
                                    break;
                                }
                            }
                        }
                    }
                }
                else //1 word
                {
                    //rank, suppression, firerate
                    found = true;
                    currentPosition = indexI + inputWord1.Length;
                    break;
                }
            }

            if (found)
            {
                for (int j = currentPosition; filetext[j] != 10 && filetext[j] != 12 && filetext[j] != 13; j++)
                {
                    result += filetext[j];
                }
            }
            //Console.WriteLine(inputWord1 + inputWord2 ?? "");
            Console.WriteLine(result);
            return result;
        }
    }

    public class FileReading
    {
        private Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>> result = new();


        public enum BuildOptions
        {
            NONE,
            ARS = 1,
            PDWS = 2,
            LMGS = 4,
            SRS = 8,
            CAS = 16,
            DMRS = 32,
            BRS = 64,
            SHS = 128,
            PS = 256,
            MPS = 512,
            RES = 1024,
            OTH = 2048,
            FGS = 4096,
            HEGS = 8192,
            IGS = 16384,
            OHBT = 32768,
            OHBE = 65536,
            THBT = 131072,
            THBE = 262144


        }

        public Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>> Result { get { return result; } }


        public FileReading(BuildOptions options, bool read, bool? optimizedBuild, bool? fullBuild)
        {
            /*
             process the image -> parse the image -> return as a list of guns + categories
            error handling

            1. create a fileprocessing object that will run custom build commands
            2. multithreading support
                ideally a thread per weapon type ie pistols, light machine guns, etc.
                and also a thread per image (so "g36k1.png" and "g36k2.png" get different threads, but "m41.png" and "g36k1.png" get the same thread)
            3. return categories and guns and classes and all that stuff

             */



            //Gun
            ControlPath(options, read,  optimizedBuild, fullBuild);

            //Console.ReadKey();
        }

        private static Dictionary<int, FileProcessing.WeaponOutputs> GetStrings(List<string> strings, bool twoFiles)
        {
            string folder = @"all build options v3\";

            Dictionary<int, FileProcessing.WeaponOutputs> output = new();
            for (int j = 0; j < strings.Count; j++)
            {
                if (twoFiles)
                {
                    output.Add(j*2, new FileProcessing.WeaponOutputs(FileProcessing.Filenames.GetFilenames(folder + FileProcessing.Filenames.convertGunNameToFileNames(strings[j]).Item2)));
                    output.Add(j*2+1, new FileProcessing.WeaponOutputs(FileProcessing.Filenames.GetFilenames(folder + FileProcessing.Filenames.convertGunNameToFileNames(strings[j]).Item3)));
                }
                else
                {
                    output.Add(j, new FileProcessing.WeaponOutputs(FileProcessing.Filenames.GetFilenames(folder + FileProcessing.Filenames.convertGunNameToFileNames(strings[j]).Item1)));
                }
            }
            return output;
        }

        public async void ControlPath(BuildOptions options, bool read, bool? optimizedBuild, bool? fullBuild)
        {
            Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>> awaiter = new();
            if (read)
            {
                //FileProcessing.AllWeaponStrings.
                awaiter = new()
                {
                    {BuildOptions.ARS, GetStrings(FileProcessing.AllWeaponStrings.AssaultRiflesStrings, true) },
                    {BuildOptions.PDWS, GetStrings(FileProcessing.AllWeaponStrings.PersonalDefenseWeaponsStrings, true) },
                    {BuildOptions.LMGS, GetStrings(FileProcessing.AllWeaponStrings.LightMachineGunsStrings, true)},
                    {BuildOptions.SRS, GetStrings(FileProcessing.AllWeaponStrings.SniperRiflesStrings, true) },

                    {BuildOptions.BRS, GetStrings(FileProcessing.AllWeaponStrings.BattleRiflesStrings, true) },
                    {BuildOptions.CAS, GetStrings(FileProcessing.AllWeaponStrings.CarbineStrings, true) },
                    {BuildOptions.DMRS, GetStrings(FileProcessing.AllWeaponStrings.DesignatedMarksmanRiflesStrings, true) },
                    {BuildOptions.SHS, GetStrings(FileProcessing.AllWeaponStrings.ShotgunsStrings, true) },

                    {BuildOptions.PS, GetStrings(FileProcessing.AllWeaponStrings.PistolsStrings, true) },
                    {BuildOptions.MPS, GetStrings(FileProcessing.AllWeaponStrings.MachinePistolsStrings, true) },
                    {BuildOptions.RES, GetStrings(FileProcessing.AllWeaponStrings.RevolversStrings, true) },
                    {BuildOptions.OTH, GetStrings(FileProcessing.AllWeaponStrings.OthersStrings, true) },

                    {BuildOptions.FGS, GetStrings(FileProcessing.AllWeaponStrings.FragmentationGrenadesStrings, false) },
                    {BuildOptions.HEGS, GetStrings(FileProcessing.AllWeaponStrings.HighExplosiveGrenadesStrings, false) },
                    {BuildOptions.IGS, GetStrings(FileProcessing.AllWeaponStrings.ImpactGrenadesStrings, false) },

                    {BuildOptions.OHBE, GetStrings(FileProcessing.AllWeaponStrings.OneHandBladeMelees, false) },

                    {BuildOptions.OHBT, GetStrings(FileProcessing.AllWeaponStrings.OneHandBluntMelees, false) },

                    {BuildOptions.THBE, GetStrings(FileProcessing.AllWeaponStrings.TwoHandBladeMelees, false) },

                    {BuildOptions.THBT, GetStrings(FileProcessing.AllWeaponStrings.TwoHandBluntMelees, false) }

                };
            }
            else if(optimizedBuild != null && fullBuild != null)
            {
                if ((bool)fullBuild)
                {
                    if ((bool)optimizedBuild)
                    {
                        awaiter = await OptimizedFullBuild();
                    }
                    else if(!(bool)optimizedBuild)
                    {
                        awaiter = await FullBuild();
                    }
                }
                else if(!(bool)fullBuild)
                {
                    if ((bool)optimizedBuild)
                    {
                        awaiter = await OptimizedBuild(options);
                    }
                    else if (!(bool)optimizedBuild)
                    {
                        awaiter = await Build(options);
                    }
                }
            }
            else
            {
                throw new ArgumentException("\"read\" cannot be false while \"optimizedBuild\" and \"fullBuild\" are both null");
            }

            Proofread(awaiter);

        }

        public void Proofread(Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>> keyValuePairs)
        {

        }


        public BuildOptions BuildOptionsConvert(FileProcessing.BuildOptions options)
        {
            BuildOptions result = 0;
            switch (options)
            {

                case FileProcessing.BuildOptions.AssaultRifles:
                    {
                        result = BuildOptions.ARS;
                        break;
                    }
                case FileProcessing.BuildOptions.PersonalDefenseWeapons:
                    {
                        result = BuildOptions.PDWS;
                        break;
                    }
                case FileProcessing.BuildOptions.LightMachineGuns:
                    {
                        result = BuildOptions.LMGS;
                        break;
                    }
                case FileProcessing.BuildOptions.SniperRifles:
                    {
                        result = BuildOptions.SRS;
                        break;
                    }
                case FileProcessing.BuildOptions.Shotguns:
                    {
                        result = BuildOptions.SHS;
                        break;
                    }
                case FileProcessing.BuildOptions.Carbines:
                    {
                        result = BuildOptions.CAS;
                        break;
                    }
                case FileProcessing.BuildOptions.DesignatedMarksmanRifles:
                    {
                        result = BuildOptions.DMRS;
                        break;
                    }
                case FileProcessing.BuildOptions.BattleRifles:
                    {
                        result = BuildOptions.BRS;
                        break;
                    }
                case FileProcessing.BuildOptions.Pistols:
                    {
                        result = BuildOptions.PS;
                        break;
                    }
                case FileProcessing.BuildOptions.MachinePistols:
                    {
                        result = BuildOptions.MPS;
                        break;
                    }
                case FileProcessing.BuildOptions.Revolvers:
                    {
                        result = BuildOptions.RES;
                        break;
                    }
                case FileProcessing.BuildOptions.Other:
                    {
                        result = BuildOptions.OTH;
                        break;
                    }
                case FileProcessing.BuildOptions.FragmentationGrenades:
                    {
                        result = BuildOptions.FGS;
                        break;
                    }
                case FileProcessing.BuildOptions.ImpactGrenades:
                    {
                        result = BuildOptions.IGS;
                        break;
                    }
                case FileProcessing.BuildOptions.HighExplosiveGrenades:
                    {
                        result = BuildOptions.HEGS;
                        break;
                    }
                case FileProcessing.BuildOptions.OneHandBladeMelees:
                    {
                        result = BuildOptions.OHBE;
                        break;
                    }
                case FileProcessing.BuildOptions.OneHandBluntMelees:
                    {
                        result = BuildOptions.OHBT;
                        break;
                    }
                case FileProcessing.BuildOptions.TwoHandBladeMelees:
                    {
                        result = BuildOptions.THBE;
                        break;
                    }
                case FileProcessing.BuildOptions.TwoHandBluntMelees:
                    {
                        result = BuildOptions.THBT;
                        break;
                    }
                default:
                    break;

            }
            return result;
        }

        public async Task<Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>>> Build(BuildOptions options)
        {

            Func<BuildOptions, int, bool> decoder = (en, id) => ((int)en & id) != 0;

            Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>> result = new();

            Action<FileProcessing.BuildOptions, FileProcessing.StreamOptions, bool, bool, bool, string, bool?, bool?, Dictionary<BuildOptions,Dictionary<int, FileProcessing.WeaponOutputs>>> builders = (option, stream, consoleLogging, largeFileLogging, individualFileLogging, largeFileLogName, primaryOrSecondary, meleeOrGrenade, res) => {res.Add(BuildOptionsConvert(option),FileProcessing.multithreadedReadFileList(stream, new FileProcessing.AllWeaponStrings(option).GetStrings(), Tuple.Create(consoleLogging, largeFileLogging, individualFileLogging), largeFileLogName, primaryOrSecondary, meleeOrGrenade)); };


            //TODO: add build options
            Thread ARS = new(() => builders(FileProcessing.BuildOptions.AssaultRifles, FileProcessing.StreamOptions.Both, false, true, true, "assaultrifles.txt", true, null, result));
            Thread PDWS = new(() => builders(FileProcessing.BuildOptions.PersonalDefenseWeapons, FileProcessing.StreamOptions.Both, false, true, true, "personaldefenseweapons.txt", true, null, result));
            Thread LMGS = new(() => builders(FileProcessing.BuildOptions.LightMachineGuns, FileProcessing.StreamOptions.Both, false, true, true, "lightmachineguns.txt", true, null, result));
            Thread SRS = new(() => builders(FileProcessing.BuildOptions.SniperRifles, FileProcessing.StreamOptions.Both, false, true, true, "sniperrifles.txt", true, null, result));
            Thread CAS = new(() => builders(FileProcessing.BuildOptions.Carbines, FileProcessing.StreamOptions.Both, false, true, true, "carbines.txt", true, null, result));
            Thread DMRS = new(() => builders(FileProcessing.BuildOptions.DesignatedMarksmanRifles, FileProcessing.StreamOptions.Both, false, true, true, "designatedmarksmanrifles.txt", true, null, result));
            Thread BRS = new(() => builders(FileProcessing.BuildOptions.BattleRifles, FileProcessing.StreamOptions.Both, false, true, true, "battlerifles.txt", true, null, result));
            Thread SHS = new(() => builders(FileProcessing.BuildOptions.Shotguns, FileProcessing.StreamOptions.Both, false, true, true, "shotguns.txt", true, null, result));


            Thread PS = new(() => builders(FileProcessing.BuildOptions.Pistols, FileProcessing.StreamOptions.Both, false, true, true, "pistols.txt", false, null, result));
            Thread MPS = new(() => builders(FileProcessing.BuildOptions.MachinePistols, FileProcessing.StreamOptions.Both, false, true, true, "machinepistols.txt", false, null, result));
            Thread RES = new(() => builders(FileProcessing.BuildOptions.Revolvers, FileProcessing.StreamOptions.Both, false, true, true, "revolvers.txt", false, null, result));
            Thread OTH = new(() => builders(FileProcessing.BuildOptions.Other, FileProcessing.StreamOptions.Both, false, true, true, "others.txt", false, null, result));


            Thread FGS = new(() => builders(FileProcessing.BuildOptions.FragmentationGrenades, FileProcessing.StreamOptions.Both, false, true, true, "fraggrenades.txt", null, false, result));
            Thread HEGS = new(() => builders(FileProcessing.BuildOptions.HighExplosiveGrenades, FileProcessing.StreamOptions.Both, false, true, true, "highexplosivegrenades.txt", null, false, result));
            Thread IGS = new(() => builders(FileProcessing.BuildOptions.ImpactGrenades, FileProcessing.StreamOptions.Both, false, true, true, "impactgrenades.txt", null, false, result));


            Thread OHBT = new(() => builders(FileProcessing.BuildOptions.OneHandBluntMelees, FileProcessing.StreamOptions.Both, false, true, true, "onehandbluntmelees.txt", null, true, result));
            Thread OHBE = new(() => builders(FileProcessing.BuildOptions.OneHandBladeMelees, FileProcessing.StreamOptions.Both, false, true, true, "onehandblademelees.txt", null, true, result));
            Thread THBT = new(() => builders(FileProcessing.BuildOptions.TwoHandBluntMelees, FileProcessing.StreamOptions.Both, false, true, true, "twohandbluntmelees.txt", null, true, result));
            Thread THBE = new(() => builders(FileProcessing.BuildOptions.TwoHandBladeMelees, FileProcessing.StreamOptions.Both, false, true, true, "twohandblademelees.txt", null, true, result));

            //optimizations:
            /*
             battle rifles goes with lmgs
             dmrs goes with shotguns
             
             
             */


            //List<Thread> threads = new() { ARS, PDWS, LMGS, SRS, CAS, SHS, DMRS, BRS, PS, MPS, RES, OTH };
            /*foreach (Thread t in threads)
            {
                //t.IsBackground = true;
                t.Start();
            }*/

            if (decoder(options, (int)BuildOptions.ARS)) ARS.Start();
            if (decoder(options, (int)BuildOptions.PDWS)) PDWS.Start();
            if (decoder(options, (int)BuildOptions.DMRS)) DMRS.Start();
            if (decoder(options, (int)BuildOptions.BRS)) BRS.Start();
            if (decoder(options, (int)BuildOptions.LMGS)) LMGS.Start();
            if (decoder(options, (int)BuildOptions.SRS)) SRS.Start();
            if (decoder(options, (int)BuildOptions.CAS)) CAS.Start();
            if (decoder(options, (int)BuildOptions.SHS)) SHS.Start();


            if (decoder(options, (int)BuildOptions.PS)) PS.Start();
            if (decoder(options, (int)BuildOptions.MPS)) MPS.Start();
            if (decoder(options, (int)BuildOptions.RES)) RES.Start();
            if (decoder(options, (int)BuildOptions.OTH)) OTH.Start();


            if (decoder(options, (int)BuildOptions.FGS)) FGS.Start();
            if (decoder(options, (int)BuildOptions.HEGS)) HEGS.Start();
            if (decoder(options, (int)BuildOptions.IGS)) IGS.Start();



            if (decoder(options, (int)BuildOptions.OHBT)) OHBT.Start();
            if (decoder(options, (int)BuildOptions.OHBE)) OHBE.Start();
            if (decoder(options, (int)BuildOptions.THBT)) THBT.Start();
            if (decoder(options, (int)BuildOptions.THBE)) THBE.Start();

            this.result = result;
            return result;

        }

        public async Task<Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>>> OptimizedFullBuild()
        {

            //dmrs + lmgs = 20 f
            //brs + shotguns = 20 f
            //snipers + revolvers = 23 f
            //assault = 26
            //pdws = 26
            //carbines = 24
            //pistols = 25
            //one hand blunt + impact = 27 f
            // one hand blade = 31 f
            // machine pistols + others + frags + highexplosive = 30 f
            //two handed = 14 +15 = 29

            //31 + 20 = 51 dmrs, lmgs, one hand blade
            //30 + 20 = 50 brs, shotguns, machine pistols, others, frags, highexplosive
            //27 + 23 = 50 ohbt, impact, snipers, revolvers
            //24 + 26 = 50 pdws, carbines
            //25 + 26 = 51 assault + pistols
            //29 //two handed stuff
            return await OptimizedBuild(BuildOptions.ARS | BuildOptions.PDWS | BuildOptions.LMGS | BuildOptions.SRS | BuildOptions.CAS | BuildOptions.DMRS | BuildOptions.BRS | BuildOptions.SHS | BuildOptions.PS | BuildOptions.MPS | BuildOptions.RES | BuildOptions.OTH | BuildOptions.FGS | BuildOptions.HEGS | BuildOptions.IGS | BuildOptions.OHBT | BuildOptions.OHBE | BuildOptions.THBT | BuildOptions.THBE);
        }
        
        public async Task<Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>>> OptimizedBuild(BuildOptions options)
        {

            Func<BuildOptions, int, bool> decoder = (en, id) => ((int)en & id) != 0;

            bool DMRS = false, LMGS = false, OHBE = false;
            if (decoder(options, (int)BuildOptions.DMRS)) DMRS = true;
            if (decoder(options, (int)BuildOptions.LMGS)) LMGS = true;
            if (decoder(options, (int)BuildOptions.OHBE)) OHBE = true;

            bool BRS = false, SHS = false, MPS = false, OTH = false, FGS = false, HEGS = false;
            if (decoder(options, (int)BuildOptions.BRS)) BRS = true;
            if (decoder(options, (int)BuildOptions.SHS)) SHS = true;
            if (decoder(options, (int)BuildOptions.MPS)) MPS = true;
            if (decoder(options, (int)BuildOptions.OTH)) OTH = true;
            if (decoder(options, (int)BuildOptions.FGS)) FGS = true;
            if (decoder(options, (int)BuildOptions.HEGS)) HEGS = true;

            bool OHBT = false, IGS = false, SRS = false, RES = false;
            if (decoder(options, (int)BuildOptions.OHBT)) OHBT = true;
            if (decoder(options, (int)BuildOptions.IGS)) IGS = true;
            if (decoder(options, (int)BuildOptions.SRS)) SRS = true;
            if (decoder(options, (int)BuildOptions.RES)) RES = true;

            bool PDWS = false, CAS = false;
            if (decoder(options, (int)BuildOptions.PDWS)) PDWS = true;
            if (decoder(options, (int)BuildOptions.CAS)) CAS = true;

            bool ARS = false, PS = false;
            if (decoder(options, (int)BuildOptions.ARS)) ARS = true;
            if (decoder(options, (int)BuildOptions.PS)) PS = true;

            bool THBT = false, THBE = false;
            if (decoder(options, (int)BuildOptions.THBT)) THBT = true;
            if (decoder(options, (int)BuildOptions.THBE)) THBE = true;


            Thread T1 = new(async () => await thread1Async(DMRS, LMGS, OHBE));
            Thread T2 = new(async () => await thread2Async(BRS, SHS, MPS, OTH, FGS, HEGS));
            Thread T3 = new(async () => await thread3Async(OHBT, IGS, SRS, RES));
            Thread T4 = new(async () => await thread4Async(PDWS, CAS));
            Thread T5 = new(async () => await thread5Async(ARS, PS));
            Thread T6 = new(async () => await thread6Async(THBT,THBE));

            T1.Start();
            T2.Start();
            T3.Start();
            T4.Start();
            T5.Start();
            T6.Start();

            if(T1.IsAlive == false && T2.IsAlive == false && T3.IsAlive == false && T4.IsAlive == false && T5.IsAlive == false && T6.IsAlive == false)
            {
                return result;
            }
            else
            {
                return new Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>>();
            }
            //) thread1Async().Start();

        }

        //31 + 20 = 51 dmrs, lmgs, one hand blade
        public async Task<Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>>> thread1Async(bool DMRS, bool LMGS, bool OHBE)
        {
            Dictionary<BuildOptions,Dictionary<int, FileProcessing.WeaponOutputs>> result = new();

            Dictionary<int, FileProcessing.WeaponOutputs>? output1 = null; 
            if(DMRS)output1 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.DesignatedMarksmanRiflesStrings, Tuple.Create(false, true, true), "designatedmarksmanrifles.txt", true, null, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.Add(BuildOptions.DMRS, output1 ?? new());


            Dictionary<int, FileProcessing.WeaponOutputs>? output2 = null; 
            if(LMGS)output2 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.LightMachineGunsStrings, Tuple.Create(false, true, true), "lightmachineguns.txt", true, null, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.Add(BuildOptions.LMGS, output2 ?? new());


            Dictionary<int, FileProcessing.WeaponOutputs>? output3 = null;
            if (OHBE) output3 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.OneHandBladeMelees, Tuple.Create(false, true, true), "onehandedblademelees.txt", null, true, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.Add(BuildOptions.OHBE, output3 ?? new());

            result.Add(BuildOptions.DMRS, output1 ?? new());
            result.Add(BuildOptions.LMGS, output2 ?? new());
            result.Add(BuildOptions.OHBE, output3 ?? new());
            return result;
        }

        //30 + 20 = 50 brs, shotguns, machine pistols, others, frags, highexplosive
        public async Task<Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>>> thread2Async(bool BRS, bool SHS, bool MPS, bool OTH, bool FGS, bool HEGS)
        {
            Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>> result = new();

            Dictionary<int, FileProcessing.WeaponOutputs>? output1 = null;
            if (BRS) output1 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.BattleRiflesStrings, Tuple.Create(false, true, true), "battlerifles.txt", true, null, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.Add(BuildOptions.BRS, output1 ?? new());


            Dictionary<int, FileProcessing.WeaponOutputs>? output2 = null;
            if (SHS) output2 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.ShotgunsStrings, Tuple.Create(false, true, true), "shotguns.txt", true, null, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.Add(BuildOptions.SHS, output2 ?? new());


            Dictionary<int, FileProcessing.WeaponOutputs>? output3 = null;
            if (MPS) output3 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.MachinePistolsStrings, Tuple.Create(false, true, true), "machinepistols.txt", false, null, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.Add(BuildOptions.MPS, output3 ?? new());


            Dictionary<int, FileProcessing.WeaponOutputs>? output4 = null;
            if (OTH) output4 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both,FileProcessing.AllWeaponStrings.OthersStrings, Tuple.Create(false, true, true), "others.txt", false, null, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.Add(BuildOptions.OTH, output4 ?? new());


            Dictionary<int, FileProcessing.WeaponOutputs>? output5 = null;
            if (FGS) output5 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.FragmentationGrenadesStrings, Tuple.Create(false, true, true), "fragmentationgrenades.txt", null, false, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.Add(BuildOptions.FGS, output5 ?? new());


            Dictionary<int, FileProcessing.WeaponOutputs>? output6 = null;
            if (HEGS) output6 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.HighExplosiveGrenadesStrings, Tuple.Create(false, true, true), "highexplosivegrenades.txt", null, false, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.Add(BuildOptions.HEGS, output6 ?? new());

            result.Add(BuildOptions.BRS, output1 ?? new());
            result.Add(BuildOptions.SHS, output2 ?? new());
            result.Add(BuildOptions.MPS, output3 ?? new());
            result.Add(BuildOptions.OTH, output4 ?? new());
            result.Add(BuildOptions.FGS, output5 ?? new());
            result.Add(BuildOptions.HEGS, output6 ?? new());
            return result;
        }
        
        //27 + 23 = 50 ohbt, impact, snipers, revolvers
        public async Task<Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>>> thread3Async(bool OHBT, bool IGS, bool SRS, bool RES)
        {
            Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>> result = new();

            Dictionary<int, FileProcessing.WeaponOutputs>? output1 = null;
            if (OHBT) output1 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both,FileProcessing.AllWeaponStrings.OneHandBluntMelees, Tuple.Create(false, true, true), "onehandedbluntmelees.txt", null, true, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.Add(BuildOptions.OHBT, output1 ?? new());


            Dictionary<int, FileProcessing.WeaponOutputs>? output2 = null;
            if (IGS) output2 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.ImpactGrenadesStrings, Tuple.Create(false, true, true), "impactgrenades.txt", null, false, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.Add(BuildOptions.IGS, output2 ?? new());


            Dictionary<int, FileProcessing.WeaponOutputs>? output3 = null;
            if (SRS) output3 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.SniperRiflesStrings, Tuple.Create(false, true, true), "sniperrifles.txt", true, null, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.Add(BuildOptions.SRS, output3 ?? new());

            Dictionary<int, FileProcessing.WeaponOutputs>? output4 = null;
            if (RES) output4 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.RevolversStrings, Tuple.Create(false, true, true), "revolvers.txt", false, null, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.Add(BuildOptions.RES, output4 ?? new());

            result.Add(BuildOptions.OHBT, output1 ?? new());
            result.Add(BuildOptions.IGS, output2 ?? new());
            result.Add(BuildOptions.SRS, output3 ?? new());
            result.Add(BuildOptions.RES, output4 ?? new());
            return result;
        }

        //24 + 26 = 50 pdws, carbines
        public async Task<Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>>> thread4Async(bool PDWS, bool CAS)
        {
            Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>> result = new();

            Dictionary<int, FileProcessing.WeaponOutputs>? output1 = null;
            if (PDWS) output1 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.PersonalDefenseWeaponsStrings, Tuple.Create(false, true, true), "personaldefenseweapons.txt", true, null, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.Add(BuildOptions.PDWS, output1 ?? new());


            Dictionary<int, FileProcessing.WeaponOutputs>? output2 = null;
            if (CAS) output2 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.CarbineStrings, Tuple.Create(false, true, true), "carbines.txt", true, null, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.Add(BuildOptions.CAS, output2 ?? new());

            result.Add(BuildOptions.PDWS, output1 ?? new());
            result.Add(BuildOptions.CAS, output2 ?? new());
            return result;
        }

        //25 + 26 = 51 assault + pistols
        public async Task<Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>>> thread5Async(bool ARS, bool PS)
        {
            Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>> result = new();

            Dictionary<int, FileProcessing.WeaponOutputs>? output1 = null;
            if (ARS) output1 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both,FileProcessing.AllWeaponStrings.AssaultRiflesStrings, Tuple.Create(false, true, true), "assaultrifles.txt", true, null, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.Add(BuildOptions.ARS, output1 ?? new());


            Dictionary<int, FileProcessing.WeaponOutputs>? output2 = null;
            if (PS) output2 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both,FileProcessing.AllWeaponStrings.PistolsStrings, Tuple.Create(false, true, true), "carbines.txt", false, null, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.Add(BuildOptions.PS, output2 ?? new());

            result.Add(BuildOptions.ARS, output1 ?? new());
            result.Add(BuildOptions.PS, output2 ?? new());
            return result;
        }

        //29 //two handed stuff
        public async Task<Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>>> thread6Async(bool THBT, bool THBE)
        {
            Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>> result = new();

            Dictionary<int, FileProcessing.WeaponOutputs>? output1 = null;
            if (THBT) output1 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.TwoHandBluntMelees, Tuple.Create(false, true, true), "twohandedbluntmelees.txt", null, true, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.Add(BuildOptions.THBT, output1 ?? new());


            Dictionary<int, FileProcessing.WeaponOutputs>? output2 = null;
            if (THBE) output2 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.TwoHandBladeMelees, Tuple.Create(false, true, true), "twohandedblademelees.txt", null, true, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.Add(BuildOptions.THBE, output2 ?? new());

            result.Add(BuildOptions.THBT, output1 ?? new());
            result.Add(BuildOptions.THBE, output2 ?? new());
            return result;
        }

        public async Task<Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>>> FullBuild()
        {
            return await Build(BuildOptions.ARS | BuildOptions.PDWS | BuildOptions.LMGS | BuildOptions.SRS | BuildOptions.CAS | BuildOptions.DMRS | BuildOptions.BRS | BuildOptions.SHS | BuildOptions.PS | BuildOptions.MPS | BuildOptions.RES | BuildOptions.OTH | BuildOptions.FGS | BuildOptions.HEGS | BuildOptions.IGS | BuildOptions.OHBT | BuildOptions.OHBE | BuildOptions.THBT | BuildOptions.THBE);
        }


    }

}
