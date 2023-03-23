// See https://aka.ms/new-console-template for more information
using WeaponClasses;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using FileProcessingParsingReading;
using Microsoft.VisualBasic;

Console.WriteLine("Hello, World!");
Console.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff"));


string filepath = "output__aa-121.png.txt";
string filepath2 = "output__aa-122.png.txt";

Console.WriteLine(FileProcessing.WeaponOutputs.directPythonExecute("CLONKER.png", null, true));

FileProcessing.AllWeaponStrings rek = new(FileProcessing.BuildOptions.All);

Console.WriteLine(rek.AssaultRiflesStrings.Count + "\n" + 
    rek.PersonalDefenseWeaponsStrings.Count + "\n" +
    rek.LightMachineGunsStrings.Count + "\n" +
    rek.SniperRiflesStrings.Count + "\n" +
    rek.DesignatedMarksmanRiflesStrings.Count + "\n" +
    rek.BattleRiflesStrings.Count + "\n" +
    rek.CarbineStrings.Count + "\n" +
    rek.ShotgunsStrings.Count + "\n" +
    rek.PistolsStrings.Count + "\n" +
    rek.MachinePistolsStrings.Count + "\n" +
    rek.RevolversStrings.Count + "\n" +
    rek.OthersStrings.Count + "\n" +
    rek.FragmentationGrenadesStrings.Count + "\n" +
    rek.HighExplosiveGrenadesStrings.Count + "\n" +
    rek.ImpactGrenadesStrings.Count + "\n" +
    rek.OneHandBladeMelees.Count + "\n" +
    rek.OneHandBluntMelees.Count + "\n" +
    rek.TwoHandBladeMelees.Count + "\n" +
    rek.TwoHandBluntMelees.Count + "\n" +
    rek.GetStrings().Count
    );

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

//31 + 20 = 51
//30 + 20 = 50
//27 + 23 = 50
//24 + 26 = 50
//25 + 26 = 51
//29



/*26 e
26 e 
11 e
17 
9 e
9 e
24 e
11 e
25 e
9 e 
6 e
8 e
7 e
6 e
3 e
31 e
24 e
14
15*/

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

        private string version = "1.00";

        public string VERSION { get { return version; } set { version = value; } }

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
            private List<string> assaultRiflesStrings = new() {
                "ak12","an94","as-val","scar-l","aug-a1","m16a4","g36","m16a1","m16a3","type-20","aug-a2","k2","famas-f1",
                "ak47","aug-a3","l85a2","hk416","ak74","akm","ak103","tar-21","type-88","m231","c7a2","stg-44","g11k2" };

            private List<string> personalDefenseWeaponsStrings = new() {
                "mp5k","ump45","g36c","mp7","mac10","p90","colt-mars","mp5","colt-smg-633","l2a3","mp5sd","mp10","m3a1",
                "mp510","uzi","aug-a3-para-xs","k7","ak74u","ppsh-41","fal-para-shorty","kriss-vector","pp-19-bizon","mp40",
                "x95-smg","tommy-gun","rama-1130"};

            private List<string> lightMachineGunsStrings = new() {
                "colt-lmg","m60","aug-hbar","mg36","rpk12","l86-lsw","rpk","hk21e","hamr-iar","rpk74","mg3kws" };

            private List<string> sniperRiflesStrings = new() {
                "intervention","model-700","dragunov-svu","aws","bfg-50","awm","trg-42","mosin-nagant","dragunov-svds",
                "m1903","k14","hecate-ii","ft300","m107","steyr-scout","wa2000","ntw-20"};

            private List<string> battleRiflesStrings = new() {
                "m14","beowulf-ecr","scar-h","ak12br","g3a3","ag-3","hk417","henry-45-70","fal-5000"};

            private List<string> designatedMarksmanRiflesStrings = new() {
                "mk11","sks","sl-8","vss-vintorez","msg90","m21","beowulf-tcr","sa58-spr","scar-ssr"};
            
            private List<string> shotgunsStrings = new() {
                "ksg-12","model-870","dbv12","ks-23m","saiga-12","stevens-db","e-gun","aa-12","spas-12","dt11","usas-12"};

            private List<string> carbineStrings = new() {
                "m4a1","g36k","m4","l22","scar-pdw","aku12","groza-1","ots-126","ak12c","honey-badger","k1a","sr-3m","groza-4",
                "mc51","fal-5063-para","1858-carbine","ak105","jury","kac-srr","gyrojet-carbine","c8a2","x95r","hk51b",
                "can-cannon"};

            private List<string> pistolsStrings = new() {
                "g17","m9","m1911a1","desert-eagle-l5","g21","g23","m45a1","g40","kg-99","g50","five-seven","zip-22","gi-m1",
                "hardballer","izhevsk-pb","makarov-pm","gb-22","desert-eagle-xix","automag-iii","gyrojet-mark-i","gsp",
                "grizzly","m2011","alien","af2011-a1"};

            private List<string> machinePistolsStrings = new() {
                "g18c","93r","pp-2000","tec-9","micro-uzi","skorpion-vz61","asmi","mp1911","arm-pistol"};

            private List<string> revolversStrings = new() {
                "mp412-rex","mateba-6","1858-new-army","redhawk-44","judge","executioner"};

            private List<string> othersStrings = new() {
                "super-shorty","sfg-50","m79-thumper","advanced-coilgun","sawed-off","saiga-12u","obrez","sass-308"};

            private List<string> fragmentationGrenadesStrings = new() { 
                "m67-frag", "mk-2-frag", "m24-stick", "m26-frag", "m560-mini", "v40-mini", "roly-hg"};


            private List<string> highExplosiveGrenadeStrings = new() { 
                "dynamite-3", "dynamite", "rgd-5-he","semtex", "pb-grenade", "bundle-charge"};

            private List<string> impactGrenadeStrings = new() { "t-13-impact", "rgn-udzs", "rgo-udzs" };

            private List<string> oneHandBladeStrings = new() { "cleaver", "tanzanite-blade", "war-fan", "nata-hatchet",
                "mekleth", "karambit", "krampus-kukri", "trench-knife", "knife", "tactical-spatula", "hunting-knife",
                "tanto", "entrencher", "ritual-sickle", "kama", "key", "ice-pick", "machete", "tomahawk", "pocket-knife",
                "havoc-blade", "cutter", "jason", "bridal-brandisher", "darkheart", "streiter", "balisong", "kommando",
                "linked-sword", "classic-knife", "jkey" };

            private List<string> twoHandBladeStrings = new() { 
                "zircon-trident", "nordic-war-axe", "world-buster", "noobslayer", "hattori", "chosen-one", "reaper",
                "zero-cutter", "naginata", "training-bayonet", "longsword", "fire-axe", "harvester", "zweihander" };

            private List<string> oneHandBluntStrings = new() { 
                "asp-baton", "toy-gun", "maglite-club", "crowbar", "mjolnir", "keyboard", "fumelee", "candy-cane", 
                "bare-fists", "tanzanite-pick", "stick-grenade", "bloxy", "holiday-tea", "trench-mace", "clonker", 
                "nightstick", "stun-gun", "uchiwa", "fixer", "brass-knuckle", "cricket-bat", "frying-pan", "arm-cannon", 
                "starlis-funpost"};

            private List<string> twoHandBluntStrings = new() { "sledge-hammer", "hockey-stick", "sweeper", "baseball-bat",
                "paddle", "cursed-shinai", "banjo", "stylis-brush", "kanabo", "stopper", "the-axe", "void-staff",
                "morning-star", "present", "crane"};



            private List<string> stringBuilding = new();


            public List<string> AssaultRiflesStrings { get { return assaultRiflesStrings; } set { assaultRiflesStrings = value; } }
            public List<string> PersonalDefenseWeaponsStrings { get { return personalDefenseWeaponsStrings; } set { personalDefenseWeaponsStrings = value; } }
            public List<string> LightMachineGunsStrings { get { return lightMachineGunsStrings; } set { lightMachineGunsStrings = value; } }
            public List<string> SniperRiflesStrings { get { return sniperRiflesStrings; } set { sniperRiflesStrings = value; } }
            public List<string> BattleRiflesStrings { get { return battleRiflesStrings; } set { battleRiflesStrings = value; } }
            public List<string> DesignatedMarksmanRiflesStrings { get { return designatedMarksmanRiflesStrings; } set { designatedMarksmanRiflesStrings = value; } }
            public List<string> ShotgunsStrings { get { return shotgunsStrings; } set { shotgunsStrings = value; } }
            public List<string> CarbineStrings { get { return carbineStrings; } set { carbineStrings = value; } }
            public List<string> PistolsStrings { get { return pistolsStrings; } set { pistolsStrings = value; } }
            public List<string> MachinePistolsStrings { get { return machinePistolsStrings; } set { machinePistolsStrings = value; } }
            public List<string> RevolversStrings { get { return revolversStrings; } set { revolversStrings = value; } }
            public List<string> OthersStrings { get { return othersStrings; } set { othersStrings = value; } }
            public List<string> ResultString { get { return stringBuilding; } set { stringBuilding = value; } }


            public List<string> FragmentationGrenadesStrings { get { return fragmentationGrenadesStrings; } set { fragmentationGrenadesStrings = value; } }
            public List<string> HighExplosiveGrenadesStrings { get { return highExplosiveGrenadeStrings; } set { highExplosiveGrenadeStrings = value; } }
            public List<string> ImpactGrenadesStrings { get { return impactGrenadeStrings; } set { impactGrenadeStrings = value; } }
            public List<string> OneHandBladeMelees { get { return oneHandBladeStrings; } set { oneHandBladeStrings = value; } }
            public List<string> OneHandBluntMelees { get { return oneHandBluntStrings; } set { oneHandBluntStrings = value; } }
            public List<string> TwoHandBladeMelees { get { return twoHandBladeStrings; } set { twoHandBladeStrings = value; } }
            public List<string> TwoHandBluntMelees { get { return twoHandBluntStrings; } set { twoHandBluntStrings = value; } }

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

            public static string directPythonExecute(string filename, bool? primaryOrSecondary, bool? meleeOrGrenade)
            {
                return executePython(@"C:\Users\peter\source\repos\Phantom Forces Database\ImageParser\dist\ImageParser.exe", @"..\..\..\..\ImageParser\Weapons\" + filename, @"..\..\..\..\ImageParser\",  filename, primaryOrSecondary, meleeOrGrenade);
            }

        }

        public class IndividualLogFiles
        {

        }


        private List<string> filepathStrings;

        //readline, build options, file parsing, all encapsulated in ONE class
        //tuple options: write to console, write to one large log file, write to individual files
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
                            File.WriteAllText("output__" + outputs[j].Filename + ".txt", outputs[j].FileOutput);
                        }
                        else
                        {
                            File.AppendAllText("output__" + outputs[j].Filename + ".txt", outputs[j].FileOutput);
                        }
                    }
                }
                if (overwrite && outputOptions.Item2) File.WriteAllText(logfilepath, logfileoutput);
            }
        }


        public static string executePython(string cmd, string path, string tessbindata, string name, bool? primaryOrSecondary, bool? meleeOrGrenade)
        {
            string startTime = DateTime.Now.ToString("HH:mm:ss:fff");
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
                            string finalOutput = "Started at " + startTime + "\n" + result + "\n" + "Finished at " + finishTime + "\nTime elapsed: " + (DateTime.ParseExact(finishTime, "HH:mm:ss:fff", CultureInfo.InvariantCulture) - DateTime.ParseExact(startTime, "HH:mm:ss:fff", CultureInfo.InvariantCulture)).Microseconds.ToString() + "\n" + @"C:\Users\peter\source\repos\Phantom Forces Database\ImageParser\Weapons\" + name;
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
            List<string> result = new List<string>();
            if (meleeOrGrenades == null)
            {
                foreach (string str in strings)
                {
                    result.Add(str + "1.png");
                    result.Add(str + "2.png");
                }
            }
            else
            {
                foreach (string str in strings)
                {
                    result.Add(str + ".png");
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
                    stream1.Add(str + "1.png");
                    stream2.Add(str + "2.png");
                }
            }
            else
            {
                foreach (string str in filenames)
                {
                    stream1.Add(str + ".png");
                    stream2.Add(str + ".png");
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
            string logfileoutput = "";
            foreach (int j in outputs.Keys)
            {
                if (outputOptions.Item1)
                {
                    Console.WriteLine(outputs[j].FileOutput);
                }
                if (outputOptions.Item2)
                {
                    File.AppendAllText(logfilepath, outputs[j].FileOutput);
                }
                if (outputOptions.Item3)
                {
                    File.AppendAllText("output__" + outputs[j].Filename + ".txt", outputs[j].FileOutput);
                }
            }
            return outputs;

        }

    }

    public class FileParsing
    {
        public enum SearchTargets
        {
            Rank,
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
            StoredCapacity
            //melees
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


        public static string findStatisticInFile(string filepath, SearchTargets targets)
        {
            if (filepath == null) throw new ArgumentNullException(nameof(filepath));
            if (!File.Exists(filepath)) throw new FileNotFoundException("File not found.", nameof(filepath));
            string filetext = File.ReadAllText(filepath);

            string inputWord1 = "";
            string? inputWord2 = null;

            switch(targets)
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


            }

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
            Console.WriteLine(inputWord1 + inputWord2 ?? "");
            Console.WriteLine(result);
            return result;
        }

        
    }

    public class FileReading
    {

        public enum BuildOptions
        {
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
            FG = 4096,
            HEG = 8192,
            IG = 16384,
            OHBT = 32768,
            OHBE = 65536,
            THBT = 131072,
            THBE = 262144


        }

        public FileReading()
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
            Console.WriteLine("confirm build? (this will be resource intensive)");
            string confirm = Console.ReadLine() ?? "false";
            if (confirm == "true") Build();

            //Console.ReadKey();
            Dictionary<int, Weapon> keyValuePairs = new();
        }

        public static void Build()
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



            //TODO: add build options
            Thread ARS = new(() => FileProcessing.multithreadedReadFileList(FileProcessing.StreamOptions.Both, new FileProcessing.AllWeaponStrings(FileProcessing.BuildOptions.AssaultRifles).AssaultRiflesStrings, Tuple.Create(false, true, true), "assaultrifles.txt", true, null));
            Thread PDWS = new(() => FileProcessing.multithreadedReadFileList(FileProcessing.StreamOptions.Both, new FileProcessing.AllWeaponStrings(FileProcessing.BuildOptions.PersonalDefenseWeapons).PersonalDefenseWeaponsStrings, Tuple.Create(false, true, true), "personaldefenseweapons.txt", true, null));
            Thread LMGS = new(() => FileProcessing.multithreadedReadFileList(FileProcessing.StreamOptions.Both, new FileProcessing.AllWeaponStrings(FileProcessing.BuildOptions.LightMachineGuns).LightMachineGunsStrings, Tuple.Create(false, true, true), "lightmachineguns.txt", true, null));
            Thread SRS = new(() => FileProcessing.multithreadedReadFileList(FileProcessing.StreamOptions.Both, new FileProcessing.AllWeaponStrings(FileProcessing.BuildOptions.SniperRifles).SniperRiflesStrings, Tuple.Create(false, true, true), "sniperrifles.txt", true, null));
            Thread CAS = new(() => FileProcessing.multithreadedReadFileList(FileProcessing.StreamOptions.Both, new FileProcessing.AllWeaponStrings(FileProcessing.BuildOptions.Carbines).CarbineStrings, Tuple.Create(false, true, true), "carbines.txt", true, null));
            Thread DMRS = new(() => FileProcessing.multithreadedReadFileList(FileProcessing.StreamOptions.Both, new FileProcessing.AllWeaponStrings(FileProcessing.BuildOptions.DesignatedMarksmanRifles).DesignatedMarksmanRiflesStrings, Tuple.Create(false, true, true), "designatedmarksmanrifles.txt", true, null));
            Thread BRS = new(() => FileProcessing.multithreadedReadFileList(FileProcessing.StreamOptions.Both, new FileProcessing.AllWeaponStrings(FileProcessing.BuildOptions.BattleRifles).BattleRiflesStrings, Tuple.Create(false, true, true), "battlerifles.txt", true, null));
            Thread SHS = new(() => FileProcessing.multithreadedReadFileList(FileProcessing.StreamOptions.Both, new FileProcessing.AllWeaponStrings(FileProcessing.BuildOptions.Shotguns).ShotgunsStrings, Tuple.Create(false, true, true), "shotguns.txt", true, null));

            Thread PS = new(() => FileProcessing.multithreadedReadFileList(FileProcessing.StreamOptions.Both, new FileProcessing.AllWeaponStrings(FileProcessing.BuildOptions.Pistols).PistolsStrings, Tuple.Create(false, true, true), "pistols.txt", false, null));
            Thread MPS = new(() => FileProcessing.multithreadedReadFileList(FileProcessing.StreamOptions.Both, new FileProcessing.AllWeaponStrings(FileProcessing.BuildOptions.MachinePistols).MachinePistolsStrings, Tuple.Create(false, true, true), "machinepistols.txt", false, null));
            Thread RES = new(() => FileProcessing.multithreadedReadFileList(FileProcessing.StreamOptions.Both, new FileProcessing.AllWeaponStrings(FileProcessing.BuildOptions.Revolvers).RevolversStrings, Tuple.Create(false, true, true), "revolvers.txt", false, null));
            Thread OTH = new(() => FileProcessing.multithreadedReadFileList(FileProcessing.StreamOptions.Both, new FileProcessing.AllWeaponStrings(FileProcessing.BuildOptions.Other).OthersStrings, Tuple.Create(false, true, true), "others.txt", false, null));

            //optimizations:
            /*
             battle rifles goes with lmgs
             dmrs goes with shotguns
             
             
             */


            List<Thread> threads = new() { ARS, PDWS, LMGS, SRS, CAS, SHS, DMRS, BRS, PS, MPS, RES, OTH };
            foreach (Thread t in threads)
            {
                //t.IsBackground = true;
                t.Start();
            }
        }


    }

}
