﻿// See https://aka.ms/new-console-template for more information
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using sd = System.Diagnostics;
using System.Text;
using System.Data.SQLite;


using FileProcessingParsingReading;
using WeaponClasses;
using System.Collections.Concurrent;
using System.Linq;
using System.Data;
using static FileProcessingParsingReading.FileReading;

Console.WriteLine("Hello, World!");
Console.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff"));
Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
/*
new FileProcessing.Filenames("M601.png", FileProcessing.WeaponOutputs.directPythonExecute("M601.png", true, null)).SetFilenames(Tuple.Create(false, false, true),null,false);
new FileProcessing.Filenames("M602.png", FileProcessing.WeaponOutputs.directPythonExecute("M602.png", true, null)).SetFilenames(Tuple.Create(false, false, true), null, false);

new FileProcessing.Filenames("STONER-961.png", FileProcessing.WeaponOutputs.directPythonExecute("STONER-961.png", true, null)).SetFilenames(Tuple.Create(false, false, true), null, false);
new FileProcessing.Filenames("STONER-962.png", FileProcessing.WeaponOutputs.directPythonExecute("STONER-962.png", true, null)).SetFilenames(Tuple.Create(false, false, true), null, false);
new FileProcessing.Filenames("MGV-1761.png", FileProcessing.WeaponOutputs.directPythonExecute("MGV-1761.png", true, null)).SetFilenames(Tuple.Create(false, false, true), null, false);
new FileProcessing.Filenames("MGV-1762.png", FileProcessing.WeaponOutputs.directPythonExecute("MGV-1762.png", true, null)).SetFilenames(Tuple.Create(false, false, true), null, false);
new FileProcessing.Filenames("BWC9-A1.png", FileProcessing.WeaponOutputs.directPythonExecute("BWC9-A1.png", true, null)).SetFilenames(Tuple.Create(false, false, true), null, false);
new FileProcessing.Filenames("BWC9-A2.png", FileProcessing.WeaponOutputs.directPythonExecute("BWC9-A2.png", true, null)).SetFilenames(Tuple.Create(false, false, true), null, false);
new FileProcessing.Filenames("FIVE-01.png", FileProcessing.WeaponOutputs.directPythonExecute("FIVE-01.png", true, null)).SetFilenames(Tuple.Create(false, false, true), null, false);
new FileProcessing.Filenames("FIVE-02.png", FileProcessing.WeaponOutputs.directPythonExecute("FIVE-02.png", true, null)).SetFilenames(Tuple.Create(false, false, true), null, false);*/
string nl = Environment.NewLine;
Console.WriteLine("Hello and welcome to FileReader! This program handles building, " + nl +
    "OCR reading, and proofreading. If you would like to build the files needed, first ensure that the directory " + nl +
    @"'..\..\..\..\ImageParser\Weapons"+Global.DBVERSION+@"\' exists. This directory needs to have all the images used for " + nl +
    "reading. If it exists, the files need to have to use the correct format. Would you like to check " + nl +
    "if your files have the correct format? y / n");
string? format = Console.ReadLine();
if(format == "y")
{
    FileProcessing.AllWeaponStrings strings = new(FileProcessing.BuildOptions.All);
    foreach(string r in strings.GetStrings())
    {
        Action<List<string>> check = (weaponstrings) => { 
            if(weaponstrings.Contains(r))
                Console.WriteLine(FileProcessing.Filenames.convertGunNameToImageNames(r).Item2 + "\t\t and \t\t" + FileProcessing.Filenames.convertGunNameToImageNames(r).Item3);
        }; 
        Action<List<string>> check2 = (weaponstrings) => {
            if (weaponstrings.Contains(r))
                Console.WriteLine(FileProcessing.Filenames.convertGunNameToImageNames(r).Item1);
        };

        check(FileProcessing.AllWeaponStrings.AssaultRiflesStrings);
        check(FileProcessing.AllWeaponStrings.PersonalDefenseWeaponsStrings);
        check(FileProcessing.AllWeaponStrings.LightMachineGunsStrings);
        check(FileProcessing.AllWeaponStrings.SniperRiflesStrings);

        check(FileProcessing.AllWeaponStrings.BattleRiflesStrings);
        check(FileProcessing.AllWeaponStrings.CarbineStrings);
        check(FileProcessing.AllWeaponStrings.DesignatedMarksmanRiflesStrings);
        check(FileProcessing.AllWeaponStrings.ShotgunsStrings);

        check(FileProcessing.AllWeaponStrings.PistolsStrings);
        check(FileProcessing.AllWeaponStrings.MachinePistolsStrings);
        check(FileProcessing.AllWeaponStrings.RevolversStrings);
        check(FileProcessing.AllWeaponStrings.OthersStrings);

        check2(FileProcessing.AllWeaponStrings.FragmentationGrenadesStrings);
        check2(FileProcessing.AllWeaponStrings.HighExplosiveGrenadesStrings);
        check2(FileProcessing.AllWeaponStrings.ImpactGrenadesStrings);

        check2(FileProcessing.AllWeaponStrings.OneHandBladeMelees);
        check2(FileProcessing.AllWeaponStrings.OneHandBluntMelees);
        check2(FileProcessing.AllWeaponStrings.TwoHandBladeMelees);
        check2(FileProcessing.AllWeaponStrings.TwoHandBluntMelees);

    }
}

Console.WriteLine("Once you have ensured that the above image files exist, " + nl +
    "you can proceed to build the weapons. If you would like " + nl +
    "to build the text files to be used, please ensure that the " + nl +
    "folder '" + Global.BuildFolder + "' exists. Would you " + nl +
    "like to build? If so, enter in '@'. Ensure that you see" + nl +
    "CAN CANNON or AF2011 at the very end (they're usually "+ nl +
    "the last ones to build). Then, hit any key to continue.");
Console.WriteLine("Note, it may take up to 2 hours for the build to complete.");

var stopwatch = sd.Stopwatch.StartNew();

string? build = Console.ReadLine() ?? "";
if (build == "@")
{
    FileReading file = new(FileReading.BuildOptions.NONE, false, true, true, false);
}

Console.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff"));
Console.WriteLine("Wow! It's been " + stopwatch.ElapsedMilliseconds.ToString() + " milliseconds since the build started." + nl +
    "That means it has been " + ((stopwatch.ElapsedMilliseconds / 1000) / 60) + " minutes since it started.");
Console.WriteLine("Would you like to proofread the values now? (Highly recommended as the OCR is not the best thing " + nl +
    "thing on the planet. y / n");
string? proofread = Console.ReadLine() ?? "";
if(proofread == "y")
{
    FileReading.renameAllFiles(); //for cropped images, no other images are touched :)
    FileReading file = new(FileReading.BuildOptions.NONE, false, true, true, true);
}

Console.WriteLine("Write to SQLite database?");
string? sqlw = Console.ReadLine() ?? "";
if(sqlw == "y")
{
    //FileReading file = new(FileReading.BuildOptions.ARS | FileReading.BuildOptions.PDWS | FileReading.BuildOptions.LMGS | FileReading.BuildOptions.SRS | FileReading.BuildOptions.CAS | FileReading.BuildOptions.DMRS | FileReading.BuildOptions.BRS | FileReading.BuildOptions.SHS | FileReading.BuildOptions.PS | FileReading.BuildOptions.MPS | FileReading.BuildOptions.RES | FileReading.BuildOptions.OTH | FileReading.BuildOptions.FGS | FileReading.BuildOptions.HEGS | FileReading.BuildOptions.IGS | FileReading.BuildOptions.OHBT | FileReading.BuildOptions.OHBE | FileReading.BuildOptions.THBT | FileReading.BuildOptions.THBE, true, true, false, false);
    FileReading file = new(BuildOptions.FGS | BuildOptions.HEGS | BuildOptions.IGS, true, true, false, false);
    SQLConnectionHandling connectionHandling = new();
    Dictionary<FileReading.Classes, Class> pairs = file.classpairs;
    /*
             ohbt: flame of olympia, pacific fm, slay bells, sleigh bells, the countdown
             ohbe: gospell blade, icemourne
             thbt: scl-s3-drastic, warhammer, zircon-slamsickle
             */

    //special thanks to: Kanako#9096, Fork#2067, and シノン△#1231
    //huge shoutout to hackurtoaster#7938
    foreach (FileReading.Classes c in pairs.Keys)
    {
        //connectionHandling.InsertSQLClassRecord(pairs[c]);

    }
    connectionHandling.InsertSQLWeaponRecords(pairs);

}

Console.WriteLine("Read from SQLite Database?");
string? sqlr = Console.ReadLine() ?? "";
if(sqlr == "y")
{
    SQLConnectionHandling a = new();
    //a.GetAllSQLWeaponRecords();
    Class g = a.GetSQLClass(Classes.Assault, Global.DBVERSION);
}

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

    public static class Global
    {

        //version 1.00 = pf version 8.0.0
        //version 1.01 = pf version 8.0.1
        private static readonly string version = "1.00";
        private static string buildFolder = @"all build options v5\";
        private static string readFolder = @"all build options v5\";
        private static readonly string dbversion = "100";


        public static string VERSION { get { return version; } }
        public static string BuildFolder
        {
            get { return buildFolder; }
            set { buildFolder = value; }
        }
        public static string ReadFolder
        {
            get { return readFolder; }
            set { readFolder = value; }
        }

        public static string DBVERSION
        {
            get { return dbversion; }
        }



    }

    public class FileProcessing
    {

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

            private static List<string> personalDefenseWeaponsStrings100 = new() {
                "mp5k","ump45","g36c","mp7","mac10","p90","colt-mars","mp5","colt-smg-633","l2a3","mp5sd","mp10","m3a1",
                "mp510","uzi","aug-a3-para-xs","k7","aks74u","ppsh-41","fal-para-shorty","kriss-vector","pp-19-bizon","mp40",
                "x95-smg","tommy-gun","rama-1130"/*,"bwc9-a","five-0"*/};

            private static List<string> personalDefenseWeaponsStrings101 = new() {
                "mp5k","ump45","g36c","mp7","mac10","p90","colt-mars","mp5","colt-smg-633","l2a3","mp5sd","mp10","m3a1",
                "mp510","uzi","aug-a3-para-xs","k7","aks74u","ppsh-41","fal-para-shorty","kriss-vector","pp-19-bizon","mp40",
                "x95-smg","tommy-gun","rama-1130","bwc9-a","five-0"};

            //rebuild LMGS (m601 and m602 were swapped)
            private static List<string> lightMachineGunsStrings100 = new() {
                "colt-lmg","m60","aug-hbar","mg36","rpk12","l86-lsw","rpk","hk21e","hamr-iar","rpk74","mg3kws"/*,"mgv-176","stoner-96" */};

            private static List<string> lightMachineGunsStrings101 = new() {
                "colt-lmg","m60","aug-hbar","mg36","rpk12","l86-lsw","rpk","hk21e","hamr-iar","rpk74","mg3kws","mgv-176","stoner-96" };

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

            //limited: boxy buster (pistol)

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

            /*
             ohbt: flame of olympia, pacific fm, slay bells, sleigh bells, the countdown
             ohbe: gospell blade, icemourne
             thbt: scl-s3-drastic, warhammer, zircon-slamsickle
             */

            //special thanks to: Kanako#9096, Fork#2067, and シノン△#1231
            //huge shoutout to hackurtoaster#7938

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
            public static List<string> PersonalDefenseWeaponsStrings { get {
                    if (Global.VERSION == "1.01" && Global.DBVERSION == "101")
                    {
                        return personalDefenseWeaponsStrings101;
                    }
                    else if (Global.VERSION == "1.00" && Global.DBVERSION == "100")
                    {
                        return personalDefenseWeaponsStrings100;
                    }
                    else
                    {
                        throw new Exception("mismatched VERSION and DBVERSION, you have a broken copy");
                    }
                }  }

            public static List<string> LightMachineGunsStrings { get {
                    if (Global.VERSION == "1.01" && Global.DBVERSION == "101")
                    {
                        return lightMachineGunsStrings101;
                    }
                    else if(Global.VERSION == "1.00" && Global.DBVERSION == "100")
                    {
                        return lightMachineGunsStrings100;
                    }
                    else
                    {
                        throw new Exception("mismatched VERSION and DBVERSION, you have a broken copy");
                    }
                }  }
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
                            foreach (string s in PersonalDefenseWeaponsStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            break;
                        }
                    case BuildOptions.LightMachineGuns:
                        {
                            foreach (string s in LightMachineGunsStrings)
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
                            foreach (string s in PersonalDefenseWeaponsStrings)
                            {
                                stringBuilding.Add(s);
                            }
                            foreach (string s in LightMachineGunsStrings)
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

            public static async Task<WeaponOutputs> GetStringAsync(string filename, bool? primaryOrSecondary, bool? meleeOrGrenade) {
                Task<string> fileOutputl = executePythonAsync(@"C:\Users\peter\source\repos\Phantom Forces Database\ImageParser\dist\ImageParser.exe", @"..\..\..\..\ImageParser\Weapons\" + filename, @"..\..\..\..\ImageParser\", filename, primaryOrSecondary, meleeOrGrenade);
                return new WeaponOutputs(filename, fileOutputl.Result);
                
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
                return executePython(@"C:\Users\peter\source\repos\Phantom Forces Database\ImageParser\dist\ImageParser.exe", @"..\..\..\..\ImageParser\Weapons"+Global.DBVERSION+@"\" + filename, @"..\..\..\..\ImageParser\",  filename, primaryOrSecondary, meleeOrGrenade);
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
                    if (logfilepath != null && logfilepath != "") File.AppendAllText(Global.BuildFolder + logfilepath, filecontents);

                }
                if (options.Item3)
                {
                    File.AppendAllText(!noExtensions ? (Global.BuildFolder + fileprefix + filename + fileextension2) : Global.BuildFolder + filename, filecontents);
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


                    /*
                    foreach(List<string> category in strings)
                    {
                        foreach(string str in category)
                        {
                            if(gunName == str)
                            {
                                Console.WriteLine(category.ToString() + " needs to be rebuilt. Missing " + gunName);
                            }
                        }
                    }*/

                    for(int j = 0; j < strings.Count; j++)
                    {
                        for(int k = 0; k < strings[j].Count; k++)
                        {
                            if (gunName == strings[j][k])
                            {
                                Console.WriteLine(strings[j] + " needs to be rebuilt. Missing " + gunName);
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

        /*
        public static Bitmap cropAtRect(Bitmap b, Rectangle r)
        {
            using (var nb = new Bitmap(r.Width, r.Height))
            {
                using (Graphics g = Graphics.FromImage(nb))
                {
                    g.DrawImage(b, -r.X, -r.Y);
                    return nb;
                }
            }
        }
        */

        public static string executePython(string cmd, string path, string tessbindata, string name, bool? primaryOrSecondary, bool? meleeOrGrenade)
        {
            string startTime = DateTime.Now.ToString("HH:mm:ss:fff");
            var watch = sd.Stopwatch.StartNew();
            string POS = primaryOrSecondary?.ToString() ?? "k";
            string MOG = meleeOrGrenade?.ToString() ?? "k";
            
            ProcessStartInfo start = new(cmd, string.Format("{0} {1} {2} {3} {4} {5}", path, tessbindata, POS, MOG,name, Global.VERSION));

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
                            string finalOutput = "VERSION:" + Global.VERSION + Environment.NewLine +"Started at " + startTime + Environment.NewLine + result + Environment.NewLine + "Finished at " + finishTime + Environment.NewLine + "Time elapsed: " + watch.ElapsedMilliseconds.ToString() + Environment.NewLine + @"C:\Users\peter\source\repos\Phantom Forces Database\ImageParser\Weapons" + Global.DBVERSION + @"\" + name;
                            Console.WriteLine(name);
                            return finalOutput;
                        }
                    }
                }
            }
            return "Failed. Time = " + DateTime.Now.ToString("HH:mm:ss:fff") + ".";
            //Process.Start(start);
        }

        public static async Task<string> executePythonAsync(string cmd, string path, string tessbindata, string name, bool? primaryOrSecondary, bool? meleeOrGrenade)
        {
            string startTime = DateTime.Now.ToString("HH:mm:ss:fff");
            var watch = sd.Stopwatch.StartNew();
            string POS = primaryOrSecondary?.ToString() ?? "k";
            string MOG = meleeOrGrenade?.ToString() ?? "k";

            ProcessStartInfo start = new(cmd, string.Format("{0} {1} {2} {3} {4} {5}", path, tessbindata, POS, MOG, name, Global.VERSION));

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
                            Task<string> result = reader.ReadToEndAsync();
                            string finishTime = DateTime.Now.ToString("HH:mm:ss:fff");
                            string finalOutput = "VERSION:" + Global.VERSION + "\nStarted at " + startTime + "\n" + result.Result + "\n" + "Finished at " + finishTime + "\nTime elapsed: " + watch.ElapsedMilliseconds.ToString() + "\n" + @"C:\Users\peter\source\repos\Phantom Forces Database\ImageParser\Weapons"+Global.DBVERSION+@"\" + name;
                            Console.WriteLine(name);
                            return finalOutput;
                        }
                    }
                }
            }
            return ("Failed. Time = " + DateTime.Now.ToString("HH:mm:ss:fff") + ".");
            //Process.Start(start);
        }

        //1 ref is redundant
        public static List<string> imagePaths(List<string> strings, bool? meleeOrGrenades)
        {
            List<string> result = new();
            if (meleeOrGrenades == null)
            {
                foreach (string str in strings)
                {
                    result.Add(Filenames.convertGunNameToImageNames(str.ToUpperInvariant()).Item2);
                    result.Add(Filenames.convertGunNameToImageNames(str.ToUpperInvariant()).Item3);
                }
            }
            else
            {
                foreach (string str in strings)
                {
                    result.Add(Filenames.convertGunNameToImageNames(str.ToUpperInvariant()).Item1);
                }
            }
            return result;
        }

        //redundant, called by ctor
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
                    stream1.Add(Filenames.convertGunNameToImageNames(str.ToUpperInvariant()).Item2);
                    stream2.Add(Filenames.convertGunNameToImageNames(str.ToUpperInvariant()).Item3);
                }
            }
            else
            {
                foreach (string str in filenames)
                {
                    stream1.Add(Filenames.convertGunNameToImageNames(str.ToUpperInvariant()).Item1);
                    stream2.Add(Filenames.convertGunNameToImageNames(str.ToUpperInvariant()).Item1);
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
                Task<WeaponOutputs> result = WeaponOutputs.GetStringAsync(threadBuilds[i], primaryOrSecondary, meleeOrGrenade);
                outputs.Add(i, result.Result);
            }
            //calls readfilelist(), which initializes the weaponoutputs objects, each of which have a constructro that calls executepython()
            //string logfileoutput = "";
            foreach (int j in outputs.Keys)
            {
                Filenames filenames = new(outputs[j]);
                filenames.SetFilenames(outputOptions, logfilepath, false);
            }
            FileReading.BuildOptions options = FileReading.BuildOptions.NONE;
            for(int i = 0; i < strings.Count; i++)
            {
                if (AllWeaponStrings.AssaultRiflesStrings.Contains(strings[i]))
                {
                    options |= FileReading.BuildOptions.ARS;
                }
                if (AllWeaponStrings.PersonalDefenseWeaponsStrings.Contains(strings[i]))
                {
                    options |= FileReading.BuildOptions.PDWS;
                }
                if (AllWeaponStrings.LightMachineGunsStrings.Contains(strings[i]))
                {
                    options |= FileReading.BuildOptions.LMGS;
                }
                if (AllWeaponStrings.SniperRiflesStrings.Contains(strings[i]))
                {
                    options |= FileReading.BuildOptions.SRS;
                }

                if (AllWeaponStrings.DesignatedMarksmanRiflesStrings.Contains(strings[i]))
                {
                    options |= FileReading.BuildOptions.DMRS;
                }
                if (AllWeaponStrings.BattleRiflesStrings.Contains(strings[i]))
                {
                    options |= FileReading.BuildOptions.BRS;
                }
                if (AllWeaponStrings.CarbineStrings.Contains(strings[i]))
                {
                    options |= FileReading.BuildOptions.CAS;
                }
                if (AllWeaponStrings.ShotgunsStrings.Contains(strings[i]))
                {
                    options |= FileReading.BuildOptions.SHS;
                }

                if (AllWeaponStrings.PistolsStrings.Contains(strings[i]))
                {
                    options |= FileReading.BuildOptions.PS;
                }
                if (AllWeaponStrings.MachinePistolsStrings.Contains(strings[i]))
                {
                    options |= FileReading.BuildOptions.MPS;
                }
                if (AllWeaponStrings.RevolversStrings.Contains(strings[i]))
                {
                    options |= FileReading.BuildOptions.RES;
                }
                if (AllWeaponStrings.OthersStrings.Contains(strings[i]))
                {
                    options |= FileReading.BuildOptions.OTH;
                }

                if (AllWeaponStrings.FragmentationGrenadesStrings.Contains(strings[i]))
                {
                    options |= FileReading.BuildOptions.FGS;
                }
                if (AllWeaponStrings.ImpactGrenadesStrings.Contains(strings[i]))
                {
                    options |= FileReading.BuildOptions.IGS;
                }
                if (AllWeaponStrings.HighExplosiveGrenadesStrings.Contains(strings[i]))
                {
                    options |= FileReading.BuildOptions.HEGS;
                }

                if (AllWeaponStrings.OneHandBladeMelees.Contains(strings[i]))
                {
                    options |= FileReading.BuildOptions.OHBE;
                }
                if (AllWeaponStrings.OneHandBluntMelees.Contains(strings[i]))
                {
                    options |= FileReading.BuildOptions.OHBT;
                }
                if (AllWeaponStrings.TwoHandBladeMelees.Contains(strings[i]))
                {
                    options |= FileReading.BuildOptions.THBE;
                }
                if (AllWeaponStrings.TwoHandBluntMelees.Contains(strings[i]))
                {
                    options |= FileReading.BuildOptions.THBT;
                }

            }
            //FileReading file = new(options, true, true, false);
            //check strings to determine which category has been just processed
            //call filereading class ctor to read the now built files
            
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
            MaximumDamage,
            TriggerMechanism,
            SpecialEffects,
            StoredCapacity,
            //melees
            BladeLength,
            FrontStabDamage,
            BackStabDamage,
            Walkspeed
        }


        public static List<int> indexFinder(string filetext, string word)
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

        public static List<int> indexFinder(string filetext, char letter)
        {

            List<int> output = new();
            for (; filetext.Contains(letter, StringComparison.CurrentCultureIgnoreCase);)
            {
                if (filetext.LastIndexOf(letter) == -1)
                {
                    break;
                }
                else
                {
                    output.Add(filetext.LastIndexOf(letter));
                }
                try
                {
                    filetext = filetext.Remove(filetext.LastIndexOf(letter));
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
                case SearchTargets.MaximumDamage:
                    {
                        inputWord1 = "maximum";
                        inputWord2 = "damage";
                        break;
                    }


            }
        }

        public static string findStatisticInFile(string filepath, SearchTargets targets, bool consoleWrite)
        {
            if (filepath == null) throw new ArgumentNullException(nameof(filepath));
            if (!File.Exists(filepath)) throw new FileNotFoundException("File not found.", nameof(filepath));
            string filetext = File.ReadAllText(filepath);

            string inputWord1 = "";
            string? inputWord2 = null;

            inputWordsSelection(targets, ref inputWord1, ref inputWord2);

            List<int> inputWord1Locations = new(indexFinder(filetext, inputWord1));
            List<int> inputWord2Locations = new();
            List<int> firstWordFirstCharLocations = new();
            List<int> secondWordFirstCharLocations = new();

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
            else
            {
                firstWordFirstCharLocations = indexFinder(filetext, inputWord1.ToString().ToUpperInvariant()[0]);
                if(inputWord2 != null)secondWordFirstCharLocations = indexFinder(filetext, inputWord2.ToString().ToUpperInvariant()[0]);

                Predicate<int> match = (i) => i > filetext.IndexOf("Time elapsed", StringComparison.CurrentCultureIgnoreCase);
                firstWordFirstCharLocations.RemoveAll(match);
                firstWordFirstCharLocations.TrimExcess();

                secondWordFirstCharLocations.RemoveAll(match);
                secondWordFirstCharLocations.TrimExcess();

                int acceptableSpaces = 3; //margin of error
                int actualSpaces = 0;
                string corruptedWord1 = ""; //to replace
                string corruptedWord2 = ""; //to replace
                int letterMatch = 0;
                StringBuilder tempInputWord1 = new(inputWord1);
                

                foreach(int IndexI in firstWordFirstCharLocations) //through the list of indexes
                {
                    for(int i = IndexI; i < IndexI + tempInputWord1.Length + acceptableSpaces; i++) //for each char of the file at the chars location
                    {
                        for(int j = 0; j < tempInputWord1.Length; j++)
                        {
                            if (filetext[i] == 32 && i + 1 < IndexI + tempInputWord1.Length + acceptableSpaces)
                            {
                                actualSpaces++;
                                i++; //skips if space
                                continue;
                            }
                            string location = filetext.Substring(i, 20); //1423
                            char testi = filetext[i];
                            char testj = tempInputWord1[j];
                            if (filetext[i].ToString().ToLower() == tempInputWord1[j].ToString().ToLower() || (filetext[i].ToString().ToLower() == "i" && tempInputWord1[j].ToString().ToLower() == "l") || (filetext[i].ToString().ToLower() == "l" && tempInputWord1[j].ToString().ToLower() == "i"))
                            {
                                letterMatch++;
                                if(letterMatch <= inputWord1.Length)tempInputWord1[j] = (char)200;
                                break;
                            }
                        }
                    }
                    if(letterMatch == tempInputWord1.Length)
                    {
                        for (int i = IndexI; i < IndexI + tempInputWord1.Length + actualSpaces; i++)
                        {
                            corruptedWord1 += filetext[i];
                        }

                        filetext = filetext.Replace(corruptedWord1, inputWord1+" ", StringComparison.CurrentCultureIgnoreCase);
                        File.WriteAllText(filepath, filetext);
                        Console.WriteLine(corruptedWord1 + " has been corrected to " + inputWord1 + " ");
                        break;
                    }
                    letterMatch = 0; actualSpaces = 0;
                    tempInputWord1 = new(inputWord1);
                }
                letterMatch = 0; actualSpaces = 0;

                if (inputWord2 != null)
                {
                    StringBuilder tempInputWord2 = new(inputWord2);
                    foreach (int IndexJ in secondWordFirstCharLocations) //through the list of indexes
                    {
                        for (int i = IndexJ; i < IndexJ + tempInputWord2.Length + acceptableSpaces; i++) //for each char of the file at the chars location
                        {
                            for (int j = 0; j < tempInputWord2.Length; j++)
                            {
                                if (filetext[i] == 32 && i + 1 < IndexJ + tempInputWord2.Length + acceptableSpaces)
                                {
                                    actualSpaces++;
                                    i++; //skips if space
                                    continue;
                                }
                                string location = filetext.Substring(i, 10); //1423
                                char testi = filetext[i];
                                char testj = tempInputWord2[j];
                                if (filetext[i].ToString().ToLower() == tempInputWord2[j].ToString().ToLower() || (filetext[i].ToString().ToLower() == "i" && tempInputWord2[j].ToString().ToLower() == "l") || (filetext[i].ToString().ToLower() == "l" && tempInputWord2[j].ToString().ToLower() == "i"))
                                {
                                    letterMatch++;
                                    if (letterMatch <= inputWord2.Length) tempInputWord2[j] = (char)200;
                                    break;
                                }
                            }
                        }
                        if (letterMatch == tempInputWord2.Length)
                        {
                            for (int i = IndexJ; i < IndexJ + tempInputWord2.Length + actualSpaces; i++)
                            {
                                corruptedWord2 += filetext[i];
                            }
                            
                            filetext = filetext.Replace(corruptedWord2, inputWord2 + " ", StringComparison.CurrentCultureIgnoreCase);
                            File.WriteAllText(filepath, filetext);
                            Console.WriteLine(corruptedWord2 + " has been corrected to " + inputWord2 + " ");
                            break;
                        }
                        letterMatch = 0; actualSpaces = 0;
                        tempInputWord2 = new(inputWord2);
                    }
                }

                inputWord1Locations = indexFinder(filetext, inputWord1);
                if (inputWord2 != null)
                {
                    inputWord2Locations = indexFinder(filetext, inputWord2);
                }
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
            }
            //Console.WriteLine(inputWord1 + inputWord2 ?? "");
            if(consoleWrite)Console.WriteLine(result);
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

        public static void findStatisticInFileReplace(string filepath, SearchTargets targets, string replacedString, string replacementString, bool missing, bool invalid, bool consoleWrite)
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
                if (filetext.Contains(replacedString) && filetext.IndexOf(replacedString) < 10 + currentPosition && invalid)
                {
                    
                    File.WriteAllText(filepath, filetext.Replace(replacedString, " " + replacementString, StringComparison.CurrentCultureIgnoreCase));
                    
                }
                else if(missing)
                {
                    File.WriteAllText(filepath, filetext.Insert(currentPosition, " " + replacementString));
                }
            }
            if (consoleWrite) Console.WriteLine(replacedString + " is now " + replacementString + " (file location: " + filepath + ")");
        }
    }

    public class FileReading
    {


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

        public enum Classes
        {
            Assault,
            Scout,
            Support,
            Recon,
            Secondary,
            Grenades,
            Melees

        }

        private ConcurrentDictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>> result = new();

        public Dictionary<Classes, Class> classpairs;

        private BuildOptions cpoptions;
        private bool cpread;
        private bool? cpoptimizedBuild;
        private bool? cpfullBuild;
        private bool cpproofread;

        public FileReading(BuildOptions options, bool read, bool? optimizedBuild, bool? fullBuild, bool proofread)
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

            this.cpfullBuild = fullBuild;
            this.cpread = read;
            this.cpproofread = proofread;
            this.cpoptimizedBuild = optimizedBuild;
            this.cpoptions = options;
            if (read || (fullBuild ?? false) || proofread) classpairs = returnFunction().Result;

            //Console.ReadKey();
        }

        public async Task<Dictionary<Classes,Class>> returnFunction()
        {
            return ControlPath(cpoptions, cpread, cpoptimizedBuild, cpfullBuild, cpproofread).Result;
        }
        
        private static Dictionary<int, FileProcessing.WeaponOutputs> GetStrings(List<string> strings, bool twoFiles)
        {
           

            Dictionary<int, FileProcessing.WeaponOutputs> output = new();
            for (int j = 0; j < strings.Count; j++)
            {
                if (twoFiles)
                {
                    output.Add(j*2, new FileProcessing.WeaponOutputs(FileProcessing.Filenames.GetFilenames(Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(strings[j]).Item2)));
                    output.Add(j*2+1, new FileProcessing.WeaponOutputs(FileProcessing.Filenames.GetFilenames(Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(strings[j]).Item3)));
                }
                else
                {
                    output.Add(j, new FileProcessing.WeaponOutputs(FileProcessing.Filenames.GetFilenames(Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(strings[j]).Item1)));
                }
            }
            return output;
        }

        public static Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>> task(Classes opt, Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>> awaiter)
        {
            Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>> result = new();
            Action<BuildOptions, Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>>> adder = (opt, res) => res.Add(opt, awaiter[opt]);

            switch (opt)
            {
                case Classes.Assault:
                    {
                        adder(BuildOptions.ARS, result);
                        adder(BuildOptions.BRS, result);
                        adder(BuildOptions.CAS, result);
                        adder(BuildOptions.SHS, result);
                        break;
                    }
                case Classes.Scout:
                    {
                        adder(BuildOptions.PDWS, result);
                        adder(BuildOptions.DMRS, result);
                        adder(BuildOptions.CAS, result);
                        adder(BuildOptions.SHS, result);
                        break;
                    }
                case Classes.Support:
                    {
                        adder(BuildOptions.LMGS, result);
                        adder(BuildOptions.BRS, result);
                        adder(BuildOptions.CAS, result);
                        adder(BuildOptions.SHS, result);
                        break;
                    }
                case Classes.Recon:
                    {
                        adder(BuildOptions.SRS, result);
                        adder(BuildOptions.DMRS, result);
                        adder(BuildOptions.BRS, result);
                        adder(BuildOptions.CAS, result);
                        break;
                    }
                case Classes.Secondary:
                    {
                        adder(BuildOptions.PS, result);
                        adder(BuildOptions.MPS, result);
                        adder(BuildOptions.RES, result);
                        adder(BuildOptions.OTH, result);
                        break;
                    }
                case Classes.Grenades:
                    {
                        adder(BuildOptions.FGS, result);
                        adder(BuildOptions.HEGS, result);
                        adder(BuildOptions.IGS, result);
                        break;
                    }
                case Classes.Melees:
                    {
                        adder(BuildOptions.OHBE, result);
                        adder(BuildOptions.OHBT, result);
                        adder(BuildOptions.THBE, result);
                        adder(BuildOptions.THBT, result);
                        break;
                    }
            }
            return result;

        }

        public async Task<Dictionary<Classes,Class>> ControlPath(BuildOptions options, bool read, bool? optimizedBuild, bool? fullBuild, bool proofread)
        {
            Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>> awaiter = new();
            
            if (read || proofread)
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

                //awaiter = result;

            }
            else
            {
                throw new ArgumentException("\"read\" cannot be false while \"optimizedBuild\" and \"fullBuild\" are both null");
            }

            //TaskAwaiter<Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>>> taskAwaiter = await awaiter;

            BuildOptions build1 = 
                (fullBuild ?? false) ? BuildOptions.ARS | BuildOptions.PDWS | BuildOptions.LMGS | BuildOptions.SRS | BuildOptions.CAS | BuildOptions.DMRS | BuildOptions.BRS | BuildOptions.SHS | BuildOptions.PS | BuildOptions.MPS | BuildOptions.RES | BuildOptions.OTH | BuildOptions.FGS | BuildOptions.HEGS | BuildOptions.IGS | BuildOptions.OHBT | BuildOptions.OHBE | BuildOptions.THBT | BuildOptions.THBE : options;

            //Thread.BeginCriticalRegion();
            Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>> r = proofread?Proofread(awaiter, build1):awaiter;
            //Thread.EndCriticalRegion();

            

            Class assault = ClassDataBuilder(task(Classes.Assault, awaiter), Classes.Assault);
            Class scout = ClassDataBuilder(task(Classes.Scout, awaiter), Classes.Scout);
            Class support = ClassDataBuilder(task(Classes.Support, awaiter), Classes.Support);
            Class recon = ClassDataBuilder(task(Classes.Recon, awaiter), Classes.Recon);
            Class secondary = ClassDataBuilder(task(Classes.Secondary, awaiter), Classes.Secondary);
            Class grenades = ClassDataBuilder(task(Classes.Grenades, awaiter), Classes.Grenades);
            Class melees = ClassDataBuilder(task(Classes.Melees, awaiter), Classes.Melees);
            Dictionary<Classes,Class> pairs = new()
            {
                { Classes.Assault, assault },
                { Classes.Scout, scout },
                { Classes.Support, support },
                { Classes.Recon, recon },
                { Classes.Secondary, secondary },
                { Classes.Grenades, grenades },
                { Classes.Melees, melees }
            };

            SQLConnectionHandling handler = new();
            //handler.InsertSQLGunRecord();
            //add to sqlite database
            return pairs;

        }

        public static Weapon WeaponDataBuilder(FileProcessing.WeaponOutputs? outputs, BuildOptions category, string weaponName, string? buildfolder)
        {
            Weapon init = new(weaponName, false, 0);
            FileProcessing.AllWeaponStrings strings = new(BuildOptionsConvert(category));
            Console.WriteLine(weaponName);


            if (category == BuildOptions.THBE || category == BuildOptions.THBT || category == BuildOptions.OHBT || category == BuildOptions.OHBE)
            {
                string filepath = "";
                if (buildfolder == null)
                {
                    filepath = Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(weaponName).Item1;

                }
                else
                {
                    filepath = buildfolder + weaponName + ".txt";
                }
                double bladeLength = Convert.ToDouble(FileParsing.findStatisticInFile(filepath, FileParsing.SearchTargets.BladeLength, true).Trim());
                double frontStabDamage = Convert.ToDouble(FileParsing.findStatisticInFile(filepath, FileParsing.SearchTargets.FrontStabDamage, true).Trim());
                double backStabDamage = Convert.ToDouble(FileParsing.findStatisticInFile(filepath, FileParsing.SearchTargets.BackStabDamage, true).Trim());
                double walkspeed = Convert.ToDouble(FileParsing.findStatisticInFile(filepath, FileParsing.SearchTargets.Walkspeed, true).Trim());


                double limbM = Convert.ToDouble(FileParsing.findStatisticInFile(filepath, FileParsing.SearchTargets.LimbMultiplier, true).Trim());
                double headM = Convert.ToDouble(FileParsing.findStatisticInFile(filepath, FileParsing.SearchTargets.HeadMultiplier, true).Trim());
                double torsoM = Convert.ToDouble(FileParsing.findStatisticInFile(filepath, FileParsing.SearchTargets.TorsoMultiplier, true).Trim());

                int rank = 0; bool hasRank = true;
                try
                {
                    rank = Convert.ToInt32(FileParsing.findStatisticInFile(filepath, FileParsing.SearchTargets.Rank, true).Trim());
                }
                catch (FormatException)
                {
                    rank = 262144;
                    hasRank = false;
                }
                init = new Melee(weaponName, hasRank, rank, frontStabDamage, backStabDamage, bladeLength, new Carried(limbM, torsoM, headM, walkspeed));
            }
            else if (category == BuildOptions.FGS || category == BuildOptions.IGS || category == BuildOptions.HEGS)
            {
                string filepath = "";
                if (buildfolder == null)
                {
                    filepath = Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(weaponName).Item1;

                }
                else
                {
                    filepath = buildfolder + weaponName + ".txt";
                }
                string special = FileParsing.findStatisticInFile(filepath, FileParsing.SearchTargets.SpecialEffects, true).Trim();
                string triggerMechanism = FileParsing.findStatisticInFile(filepath, FileParsing.SearchTargets.TriggerMechanism, true).Trim();
                double fuseTime = 0;
                if (triggerMechanism.Contains("fuse", StringComparison.CurrentCultureIgnoreCase))
                {
                    string temp = "";
                    foreach (char t in triggerMechanism)
                    {
                        if (t > 47 && t < 58 || t == 46)
                        {
                            temp += t;
                        }
                    }
                    fuseTime = Convert.ToDouble(temp);
                }

                int storedCapacity = Convert.ToInt32(FileParsing.findStatisticInFile(filepath, FileParsing.SearchTargets.StoredCapacity, true).Trim());
                int maximumDamage = Convert.ToInt32(FileParsing.findStatisticInFile(filepath, FileParsing.SearchTargets.MaximumDamage, true).Trim());
                int killingRadius = Convert.ToInt32(FileParsing.findStatisticInFile(filepath, FileParsing.SearchTargets.KillingRadius, true).Trim());
                int blastRadius = Convert.ToInt32(FileParsing.findStatisticInFile(filepath, FileParsing.SearchTargets.BlastRadius, true).Trim());
                int rank = 0; bool hasRank = true;
                try
                {
                    rank = Convert.ToInt32(FileParsing.findStatisticInFile(Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(weaponName).Item1, FileParsing.SearchTargets.Rank, true).Trim());
                }
                catch (FormatException)
                {
                    rank = 262144;
                    hasRank = false;
                }
                init = new Grenade(weaponName, hasRank, rank,
                    triggerMechanism.Contains("fuse", StringComparison.CurrentCultureIgnoreCase),
                    fuseTime, !special.Contains("none", StringComparison.CurrentCultureIgnoreCase), (!special.Contains("none", StringComparison.CurrentCultureIgnoreCase)) ? special : "", storedCapacity, blastRadius, killingRadius, maximumDamage);
            }
            else
            {
                //double bladeLength = Convert.ToDouble(FileParsing.findStatisticInFile(Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(weaponName).Item1, FileParsing.SearchTargets.BladeLength, true).Trim());
                string filepath1 = ""; string filepath2 = "";
                if (buildfolder == null)
                {
                    filepath1 = Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(weaponName).Item2;
                    filepath2 = Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(weaponName).Item3;

                }
                else
                {
                    filepath1 = buildfolder + weaponName + "1.txt";
                    filepath2 = buildfolder + weaponName + "2.txt";
                }
                string buffer = "    ";

                string damagetemp = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.DamageRange, true) + buffer;
                string damage1str = "";
                string damage2str = "";
                bool damageindexflag = false;
                string t1 = ""; string t2 = "";
                for (int i = 0; i < damagetemp.Length - buffer.Length; i++)
                {
                    char e = damagetemp[i]; char r = damagetemp[i + 1];
                    if (((damagetemp[i] > 47 && damagetemp[i] < 58) || damagetemp[i] == 46) && damageindexflag == false)
                    {
                        t1 += damagetemp[i];
                        if ((damagetemp[i + 1] > 57 || damagetemp[i + 1] < 48) && damagetemp[i + 1] != 46)
                        {
                            damage1str += t1;
                            damageindexflag = true;
                        }
                    }
                    else if (((damagetemp[i] > 47 && damagetemp[i] < 58) || damagetemp[i] == 46) && damageindexflag == true)
                    {
                        t2 += damagetemp[i];
                        if ((damagetemp[i + 1] > 57 || damagetemp[i + 1] < 48 ) && damagetemp[i + 1] != 46)
                        {
                            damage2str += t2;
                            damageindexflag = false;
                        }
                    }
                }
                double damage1 = Convert.ToDouble(damage1str.Trim());
                double damage2 = Convert.ToDouble(damage2str.Trim());


                string ammocapstr = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.AmmoCapacity, true) + buffer;
                string ammomagstr = "";
                string ammoresstr = "";
                bool ammocapindexflag = false;
                t1 = ""; t2 = "";
                for (int i = 0; i < ammocapstr.Length - buffer.Length; i++)
                {
                    if ((ammocapstr[i] > 47 && ammocapstr[i] < 58) && ammocapindexflag == false)
                    {
                        t1 += ammocapstr[i];
                        if (ammocapstr[i + 1] > 57 || ammocapstr[i + 1] < 48)
                        {
                            ammomagstr += t1;
                            ammocapindexflag = true;
                        }
                    }
                    else if ((ammocapstr[i] > 47 && ammocapstr[i] < 58) && ammocapindexflag == true)
                    {
                        t2 += ammocapstr[i];
                        if (ammocapstr[i + 1] > 57 || ammocapstr[i + 1] < 48)
                        {
                            ammoresstr += t2;
                            ammocapindexflag = false;
                        }
                    }
                }
                int magcap = Convert.ToInt32(ammomagstr.Trim());
                int rescap = Convert.ToInt32(ammoresstr.Trim());

                double limbM = Convert.ToDouble(FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.LimbMultiplier, true).Trim());
                double headM = Convert.ToDouble(FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.HeadMultiplier, true).Trim());
                double torsoM = Convert.ToDouble(FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.TorsoMultiplier, true).Trim());

                string mvstr = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.MuzzleVelocity, true) + buffer;
                string mvconvstr = "";
                bool mvdotflag = false;
                t1 = ""; t2 = "";
                for (int i = 0; i < mvstr.Length - buffer.Length; i++)
                {
                    if (((mvstr[i] > 47 && mvstr[i] < 58) || mvstr[i] == 46) && mvdotflag == false)
                    {
                        t1 += mvstr[i];
                        if ((mvstr[i + 1] > 57 || mvstr[i + 1] < 48)&& mvstr[i + 1] != 46)
                        {
                            mvconvstr += t1;
                            mvdotflag = true;
                        }
                    }
                }
                double mv = Convert.ToDouble(mvconvstr.Trim());

                string pdstr = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.PenetrationDepth, true) + buffer;
                string pdconvstr = "";
                bool pddotflag = false;
                t1 = ""; t2 = "";
                for (int i = 0; i < pdstr.Length - buffer.Length; i++)
                {
                    if (((pdstr[i] > 47 && pdstr[i] < 58) || pdstr[i] == 46) && pddotflag == false)
                    {
                        t1 += pdstr[i];
                        if ((pdstr[i + 1] > 57 || pdstr[i + 1] < 48) && pdstr[i + 1] != 46)
                        {
                            pdconvstr += t1;
                            pddotflag = true;
                        }
                    }
                }
                double pd = Convert.ToDouble(pdconvstr.Trim());


                string rtstr = FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.ReloadTime, true) + buffer;
                string rtconvstr = "";
                bool rtdotflag = false;
                t1 = ""; t2 = "";
                for (int i = 0; i < rtstr.Length - buffer.Length; i++)
                {
                    if (((rtstr[i] > 47 && rtstr[i] < 58) || rtstr[i] == 46) && rtdotflag == false)
                    {
                        t1 += rtstr[i];
                        if ((rtstr[i + 1] > 57 || rtstr[i + 1] < 48) && rtstr[i + 1] != 46)
                        {
                            rtconvstr += t1;
                            rtdotflag = true;
                        }
                    }
                }
                double rt = Convert.ToDouble(rtconvstr.Trim());

                string ertstr = FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.EmptyReloadTime, true) + buffer;
                string ertconvstr = "";
                bool ertdotflag = false;
                t1 = ""; t2 = "";
                for (int i = 0; i < ertstr.Length - buffer.Length; i++)
                {
                    if (((ertstr[i] > 47 && ertstr[i] < 58) || ertstr[i] == 46) && ertdotflag == false)
                    {
                        t1 += ertstr[i];
                        if ((ertstr[i + 1] > 57 || ertstr[i + 1] < 48) && ertstr[i + 1] != 46)
                        {
                            ertconvstr += t1;
                            ertdotflag = true;
                        }
                    }
                }
                double ert = Convert.ToDouble(ertconvstr.Trim());

                double aw = Convert.ToDouble(FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.AimingWalkspeed, true));
                double ww = Convert.ToDouble(FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.WeaponWalkspeed, true));

                string ammotype = FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.AmmoType, true);
                


                string dvstr = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.Damage, true) + buffer; //only __1.png files have the correct suppression
                string damageval1 = "";
                string damageval2 = "";
                string pellets1 = "";
                string pellets2 = "";
                bool pelletsflag = false;

                t1 = ""; t2 = "";
                bool dflag = false;
                for (int i = 0; i < dvstr.Length - buffer.Length; i++)
                {
                    if (((dvstr[i] > 47 && dvstr[i] < 58) || dvstr[i] == 46) && dflag == false)
                    {
                        t1 += dvstr[i];
                        if ((dvstr[i + 1] > 57 || dvstr[i + 1] < 48) && dvstr[i + 1] != 46)
                        {
                            damageval1 += t1;
                            dflag = true;
                        }
                        if (dvstr.ToLower()[i + 1] == 120)
                        {
                            pelletsflag = true;
                            pellets1 += dvstr[i + 2];
                            pellets1 += dvstr[i + 3] != 32 ? dvstr[i + 3] : "";
                            i = (dvstr[i + 3] != 32) ? i += 4 : i += 3;
                            dflag = true;
                        }
                    }
                    else if (((dvstr[i] > 47 && dvstr[i] < 58) || dvstr[i] == 46) && dflag == true)
                    {
                        t2 += dvstr[i];
                        if ((dvstr[i + 1] > 57 || dvstr[i + 1] < 48) && dvstr[i + 1] != 46)
                        {
                            damageval2 += t2;
                            dflag = false;
                        }
                        if (dvstr.ToLower()[i + 1] == 120)
                        {
                            pelletsflag = true;
                            pellets2 += dvstr[i + 2];
                            pellets2 += dvstr[i + 3] != 32 ? dvstr[i + 3] : "";
                            i = (dvstr[i + 3] != 32) ? i += 4 : i += 3;
                            dflag = false;
                        }
                    }
                }

                int pel1 = pelletsflag ? Convert.ToInt32(pellets1.Trim()) : 0;
                int pel2 = pelletsflag ? Convert.ToInt32(pellets2.Trim()) : 0;
                if (pel1 != pel2) throw new Exception("pellets do not match");

                double da1 = Convert.ToDouble(damageval1.Trim());
                double da2 = Convert.ToDouble(damageval2.Trim());

                int rank = 0; bool hasRank = true;
                try
                {
                    rank = Convert.ToInt32(FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.Rank, true).Trim());
                }
                catch (FormatException)
                {
                    rank = 262144;
                    hasRank = false;
                }

                string firemodestr = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.FireModes, true) + buffer;
                string lowerfiremodestr = firemodestr.ToLower();
                t1 = ""; t2 = ""; string t3 = "";
                List<string> list = new();
                for(int i = 0; i < firemodestr.Length - buffer.Length; i++)
                {
                    if (lowerfiremodestr[i] > 96 && lowerfiremodestr[i] < 123)
                    {
                        t1 += lowerfiremodestr[i];
                        if (lowerfiremodestr[i + 1] < 97 || lowerfiremodestr[i+1] > 122)
                        {
                            list.Add(t1);
                            t1 = "";
                        }
                    }
                }
                list.TrimExcess();

                double suppression = Convert.ToDouble(FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.Suppression, true).TrimStart() + buffer);

                string fireratestr = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.Firerate, true).TrimStart() + buffer;
                double firerate;
                bool specialFirerate = false;
                t1 = "";
                Dictionary<char, string> dict = new();
                int lettercount = 0;

                for (int i = 0; i < fireratestr.Length - buffer.Length; i++)
                {
                    if (fireratestr.ToLower()[i] > 96 && fireratestr.ToLower()[i] < 123)
                    {
                        lettercount++; //checks the letter count of the firemodes "400B | 500A | 600S" = 3, while "200 SEMI | INSTANT BURST" has >3
                    }
                }
                if (lettercount > 3)
                {
                    specialFirerate = true; //stevens and sawed off, and af2011
                }

                string instantburstflag = "59999"; //the flag that tells if the weapon has instant burst
                
                try
                {
                    firerate = Convert.ToDouble(fireratestr);
                    foreach(string str in list) {
                        if (str.Contains("burst", StringComparison.CurrentCultureIgnoreCase) || str.Contains("ii", StringComparison.CurrentCultureIgnoreCase) || str.Contains("iii", StringComparison.CurrentCultureIgnoreCase) || str.Contains("iiii", StringComparison.CurrentCultureIgnoreCase) || str.Contains("iiiii", StringComparison.CurrentCultureIgnoreCase)) dict.Add('b', fireratestr);
                        if (str.Contains("auto", StringComparison.CurrentCultureIgnoreCase)) dict.Add('a', fireratestr);
                        if (str.Contains("semi", StringComparison.CurrentCultureIgnoreCase)) dict.Add('s', fireratestr);
                    }
                }
                catch (FormatException) //if there are letters or "|"
                {
                    if (!specialFirerate) //regular gun
                    {
                        for (int i = 0; i < fireratestr.Length - buffer.Length; i++)
                        {
                            if (fireratestr[i] > 47 && fireratestr[i] < 58)
                            {
                                t1 += fireratestr[i];
                            }
                            else if (fireratestr.ToLower()[i] > 96 && fireratestr.ToLower()[i] < 123)
                            {
                                dict.Add(fireratestr[i], t1);//adds entry e.g. 'b', "500"
                                t1 = "";
                            }
                            if (i + 1 >= fireratestr.Length - buffer.Length) break;
                        }
                    }
                    else //for stevens, sawed off, af2011
                    {
                        if(fireratestr.Contains("|") && fireratestr.IndexOf("|") > 3)
                        {
                            int separator = fireratestr.IndexOf("|");
                            string g = fireratestr.Substring(0, separator);
                            string h = fireratestr.Substring(separator, fireratestr.Length - buffer.Length- separator);
                            char t = ' '; char u = ' ';
                            if (g.Contains("SEMI", StringComparison.CurrentCultureIgnoreCase))
                            {
                                t = 's';
                            }else if(g.Contains("AUTO", StringComparison.CurrentCultureIgnoreCase)){
                                t = 'a';
                            }else if(g.Contains("BURST", StringComparison.CurrentCultureIgnoreCase))
                            {
                                t = 'b';
                            }
                            if (h.Contains("SEMI", StringComparison.CurrentCultureIgnoreCase))
                            {
                                u = 's';
                            }
                            else if (h.Contains("AUTO", StringComparison.CurrentCultureIgnoreCase))
                            {
                                u = 'a';
                            }
                            else if (h.Contains("BURST", StringComparison.CurrentCultureIgnoreCase))
                            {
                                u  = 'b';
                            }
                            for (int i = 0; i < (g + buffer).Length - buffer.Length; i++)
                            {
                                if (g[i] > 47 && g[i] < 58)
                                {
                                    t1 += g[i];
                                    if (g.ToLower()[i + 1] > 96 && g.ToLower()[i + 1] < 123 || g.ToLower()[i + 1] == 32)
                                    {
                                        dict.Add(t, t1.Trim());
                                    }
                                }
                            }
                            t1 = "";
                            bool tester = false;
                            for (int i = 0; i < (h + buffer).Length - buffer.Length; i++)
                            {
                                if (h[i] > 47 && h[i] < 58)
                                {
                                    t1 += g[i];
                                    if (h.ToLower()[i + 1] > 96 && h.ToLower()[i + 1] < 123 || h.ToLower()[i + 1] == 32)
                                    {
                                        dict.Add(u, t1.Trim());
                                        tester = true;
                                    }
                                }
                            }
                            if (!tester)
                            {
                                dict.Add(u, instantburstflag);
                            }
                        }
                    }
                }
                List<string> firemodeList = new();
                foreach(char k in dict.Keys)
                {
                    string specialFlags = ""; //special flags for firemode constructor
                    if(pel1 != 0)
                    {
                        specialFlags = "sh";
                    }
                    if (k == 'b' || k == 'B')
                    {
                        foreach (string s in list)
                        {
                            if (s.Contains("iiiii", StringComparison.CurrentCultureIgnoreCase))
                            {

                                specialFlags = "bbbbb";
                            }
                            else
                            if (s.Contains("iiii", StringComparison.CurrentCultureIgnoreCase))
                            {

                                specialFlags = "bbbb";
                            }
                            else
                            if (s.Contains("iii", StringComparison.CurrentCultureIgnoreCase))
                            {
                                specialFlags = "bbb";
                                //triple burst
                            }
                            else if (s.Contains("ii", StringComparison.CurrentCultureIgnoreCase))
                            {
                                specialFlags = "bb";
                                //double burst
                                try
                                {
                                    if (dict['b']?.Contains(instantburstflag, StringComparison.CurrentCultureIgnoreCase) == true)
                                    {
                                        specialFlags = "b";
                                        //for stevens db
                                    }
                                }
                                catch
                                {
                                    if (dict['B']?.Contains(instantburstflag, StringComparison.CurrentCultureIgnoreCase) == true)
                                    {
                                        specialFlags = "b";
                                        //for stevens db
                                    }
                                }
                                try
                                {
                                    if (dict['B']?.Contains(instantburstflag, StringComparison.CurrentCultureIgnoreCase) == true)
                                    {
                                        specialFlags = "b";
                                        //for stevens db
                                    }
                                }
                                catch
                                {
                                    if (dict['b']?.Contains(instantburstflag, StringComparison.CurrentCultureIgnoreCase) == true)
                                    {
                                        specialFlags = "b";
                                        //for stevens db
                                    }
                                }
                            }
                        }
                    }
                    firemodeList.Add((k.ToString().ToLower() + dict[k] + (specialFlags!=""?specialFlags:"o") + (pel1 != 0? pel1.ToString():"0")).Trim()) ; 
                }
                

                FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.Firerate, true);

                //put suppression

                init = new Gun(weaponName, hasRank, rank, new Conversion
                    (ammotype, rescap, magcap, firemodeList, new Carried(limbM, torsoM, headM, ww), new Ranged(damage1, damage2, da1, da2), pd, mv, rt, ert, suppression, aw),true, true);
            }
            return init;
        }

        //class is made
        //the class has access to ALL data
        //the class needs to tell the category builder which category it is
        //the category builder needs to tell the weaponbuilder what TYPE of weapon and what CATEGORY
        /*
             ohbt: flame of olympia, pacific fm, slay bells, sleigh bells, the countdown
             ohbe: gospell blade, icemourne
             thbt: scl-s3-drastic, warhammer, zircon-slamsickle
             */

        //special thanks to: Kanako#9096, Fork#2067, and シノン△#1231
        //huge shoutout to hackurtoaster#7938
        public static Category CategoryDataBuilder(Dictionary<int, FileProcessing.WeaponOutputs> valuePairs, BuildOptions categoryOption)
        {
            string categoryName = SQLConnectionHandling.CategoryNames[categoryOption];
            bool melee = categoryOption == BuildOptions.OHBT || categoryOption == BuildOptions.THBT || categoryOption == BuildOptions.THBE || categoryOption == BuildOptions.OHBE;
            Category category = new(null, categoryName);
            FileProcessing.AllWeaponStrings strings = new(BuildOptionsConvert(categoryOption));
            List<string> strings1 = strings.GetStrings();
            int meleeID = 0;
            if (melee)
            {
                foreach (string s in strings1) //iterates through list of guns
                {
                    foreach (int i in valuePairs.Keys) //for each FILE, not gun
                    {
                        if (valuePairs[i].Filename.Contains(s))
                        {
                            //WeaponDataBuilder(valuePairs[i], categoryOption, s);
                            category.addMelee(WeaponDataBuilder(valuePairs[i], categoryOption, s, null), meleeID);
                            break;
                        }
                    }
                    meleeID++;
                }
                
            }
            else
            {
                foreach (string s in strings1) //iterates through list of guns
                {
                    foreach (int i in valuePairs.Keys) //for each FILE, not gun
                    {
                        if (valuePairs[i].Filename.Contains(s))
                        {
                            //WeaponDataBuilder(valuePairs[i], categoryOption, s);
                            category.addWeapon(WeaponDataBuilder(valuePairs[i], categoryOption, s, null));
                            break;
                        }
                    }
                }
            }
            string lim = @"limited\";
            switch (categoryOption)
            {
                case BuildOptions.OHBT:
                    {
                        category.addMelee(WeaponDataBuilder(null,BuildOptions.OHBT,"flame-of-olympia", lim),meleeID);
                        meleeID++;
                        category.addMelee(WeaponDataBuilder(null, BuildOptions.OHBT, "pacific-fm", lim), meleeID);
                        meleeID++; 
                        category.addMelee(WeaponDataBuilder(null, BuildOptions.OHBT, "slay-bells", lim), meleeID);
                        meleeID++;
                        category.addMelee(WeaponDataBuilder(null, BuildOptions.OHBT, "sleigh-bells", lim), meleeID);
                        meleeID++; 
                        category.addMelee(WeaponDataBuilder(null, BuildOptions.OHBT, "the-countdown", lim), meleeID);
                        meleeID++;
                        break;
                    }
                case BuildOptions.OHBE:
                    {
                        category.addMelee(WeaponDataBuilder(null, BuildOptions.OHBE, "gospell-blade", lim), meleeID);
                        meleeID++;
                        category.addMelee(WeaponDataBuilder(null, BuildOptions.OHBE, "icemourne",lim), meleeID);
                        meleeID++;
                        break;
                    }
                case BuildOptions.THBT:
                    {
                        category.addMelee(WeaponDataBuilder(null, BuildOptions.THBT, "scl-s3-drastic", lim), meleeID);
                        meleeID++;
                        category.addMelee(WeaponDataBuilder(null, BuildOptions.THBT, "warhammer", lim), meleeID);
                        meleeID++;
                        category.addMelee(WeaponDataBuilder(null, BuildOptions.THBT, "zircon-slamsickle", lim), meleeID);
                        meleeID++;
                        break;
                    }
                case BuildOptions.PS:
                    {
                        category.addWeapon(WeaponDataBuilder(null, BuildOptions.PS, "boxy-buster", lim));
                        break;
                    }
            }
            return category;
        }

        public static Class ClassDataBuilder(Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>> valuePairs, Classes classOption)
        {
            string className = "";
            switch (classOption)
            {
                case Classes.Assault:   className = "Assault"; break;
                case Classes.Scout:     className = "Scout"; break;
                case Classes.Support:   className = "Support"; break;
                case Classes.Recon:     className = "Recon"; break;
                case Classes.Secondary: className = "Secondary"; break;
                case Classes.Grenades:  className = "Grenades"; break;
                case Classes.Melees:    className = "Melees"; break;
            }
            Class cl = new(null, className);

            foreach (BuildOptions bo in valuePairs.Keys)
            {
                Category h = CategoryDataBuilder(valuePairs[bo], bo);
                cl.addCategory(h);
                //cl = new()
            }
            return cl;
        }


        public Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>> Proofread(Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>> keyValuePairs, BuildOptions options)
        {

            Func<BuildOptions, int, bool> decoder = (en, id) => ((int)en & id) != 0;

            Func<string, Task<string>> strthing = async (input) => input + " hi";

            List<BuildOptions> optionsList = new();

            Action<BuildOptions> adder = (add) => { if (decoder(options, (int)add)) optionsList.Add(add); };

            adder(BuildOptions.ARS);
            adder(BuildOptions.PDWS);
            adder(BuildOptions.DMRS);
            adder(BuildOptions.BRS);
            adder(BuildOptions.LMGS);
            adder(BuildOptions.SRS);
            adder(BuildOptions.CAS);
            adder(BuildOptions.SHS);

            adder(BuildOptions.PS);
            adder(BuildOptions.MPS);
            adder(BuildOptions.RES);
            adder(BuildOptions.OTH);

            adder(BuildOptions.FGS);
            adder(BuildOptions.HEGS);
            adder(BuildOptions.IGS);

            adder(BuildOptions.OHBT);
            adder(BuildOptions.OHBE);
            adder(BuildOptions.THBT);
            adder(BuildOptions.THBE);

            foreach (BuildOptions bo in optionsList)
            {
                foreach (BuildOptions opt in keyValuePairs.Keys) //foreach category
                {
                    switch (opt) //determines options based on category
                    {
                        case BuildOptions.ARS or BuildOptions.PDWS or BuildOptions.LMGS or BuildOptions.SRS or BuildOptions.BRS or BuildOptions.DMRS or BuildOptions.CAS or BuildOptions.SHS
                            or BuildOptions.PS or BuildOptions.MPS or BuildOptions.RES or BuildOptions.OTH:
                            {
                                //keyValuePairs[options]
                                if (opt == bo)
                                {
                                    for (int i = 0; i < keyValuePairs[opt].Count; i+=2) //per each entry (aka weapon) in category
                                    {
                                        FileProcessing.AllWeaponStrings strings = new(BuildOptionsConvert(opt));
                                        string filepath1 = "";
                                        string filepath2 = "";

                                        foreach (string str in strings.GetStrings())
                                        { //compares against allweaponstringsswitch (keyValuePairs[opt][i].Filename)
                                            if (keyValuePairs[opt][i].Filename == (Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(str).Item2)) //output__ak121.png
                                            {
                                                filepath1 = Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(str).Item2;
                                                filepath2 = Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(str).Item3;

                                            }
                                            else if (keyValuePairs[opt][i].Filename == (Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(str).Item3)) //output__ak122.png
                                            { //redundant, but might be necessary
                                                filepath1 = Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(str).Item2;
                                                filepath2 = Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(str).Item3;

                                            }
                                            else if (keyValuePairs[opt][i].Filename == (Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(str).Item1)) //output__semtex.png (impossible in this switch case)
                                            {
                                                Console.WriteLine("impossible error..." + str);
                                            }
                                            else
                                            {
                                                //Console.WriteLine(str);
                                            }
                                        }
                                        Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
                                        Console.WriteLine(keyValuePairs[opt][i].Filename);
                                        bool consoleWrite = false;
                                        string temp;
                                        bool missing = false;
                                        bool invalid = false;
                                        int icount = 0; //invalid counts
                                        int scount = 0; //space counts
                                        int dcount = 0; //dot counts
                                        //<missing, invalid>
                                        Dictionary<FileParsing.SearchTargets, Tuple<bool, bool, string>> issues = new();


                                        FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.Version, consoleWrite);
                                        FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.Version, consoleWrite);

                                        //rank -> must not have any letters, decimals, or special characters
                                        temp = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.Rank, consoleWrite);

                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else {
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && t != 32 && t!= 9)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                            }
                                            if(icount > 0)
                                            {
                                                invalid = true;
                                            } 
                                            if(scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.Rank, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false;


                                        //damage range -> can only have numbers, "-" and ">"
                                        temp = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.DamageRange, consoleWrite);

                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && (t < 97 || t > 122) && t != 32 && t != 9 && t!= 46 && t != 45 && t!= 62)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                                if (t == 46) dcount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                            if(dcount != 2)
                                            {
                                                invalid = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.DamageRange, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false; dcount = 0;


                                        //ammo cap -> can only have numbers and "/"
                                        temp = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.AmmoCapacity, consoleWrite);

                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            int slashindex = temp.IndexOf(@"/");
                                            if (slashindex < 0) invalid = true;
                                            try
                                            {
                                                string ammocap = temp.Substring(slashindex != -1 ? slashindex + 1 : temp.LastIndexOf(' ')).Trim();
                                                string magcap = temp[..^(slashindex != -1 ? slashindex + 1 : temp.IndexOf(' '))].Trim();
                                                if (Convert.ToInt32(magcap.Trim()) < 10) invalid = true; //may flag snipers, but oh well
                                                if (Convert.ToInt32(ammocap.Trim()) < 10) invalid = true;
                                            }
                                            catch
                                            {
                                                invalid = true;
                                            }
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && t != 32 && t != 9 && t!= 47)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.AmmoCapacity, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false;



                                        //multipliers -> must not have any letters or special characters. must be <= 15 (for default) (also must have .)
                                        temp = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.HeadMultiplier, consoleWrite);
                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            try
                                            {
                                                if (Convert.ToDouble(temp.Trim()) > 1.4) invalid = true; //will flag anything above 1.4, just to be sure
                                            }
                                            catch
                                            {
                                                invalid = true;
                                            }
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && t != 32 && t != 9 && t!=46)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                                if (t == 46) dcount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                            if(dcount != 1)
                                            {
                                                invalid = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.HeadMultiplier, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false; dcount = 0;
                                        temp = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.TorsoMultiplier, consoleWrite); 
                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            try
                                            {
                                                if (Convert.ToDouble(temp) > 1.1) invalid = true; //will flag anything above 1.1, just to be sure
                                            }
                                            catch
                                            {
                                                invalid = true;
                                            }
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && t != 32 && t != 9 && t != 46)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                                if (t == 46) dcount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                            if (dcount != 1)
                                            {
                                                invalid = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.TorsoMultiplier, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false; dcount = 0;
                                        temp = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.LimbMultiplier, consoleWrite); 
                                        if(temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            try
                                            {
                                                if (Convert.ToDouble(temp) > 1.0) invalid = true; //will flag anything above 1.0, just to be sure
                                            }
                                            catch
                                            {
                                                invalid = true;
                                            }
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && t != 32 && t != 9 && t != 46)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                                if (t == 46) dcount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                            if (dcount != 1)
                                            {
                                                invalid = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.LimbMultiplier, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false; dcount = 0;
                                        


                                        //will be processed to get rid of "studs/s". must be <= 4000 (for default)
                                        temp = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.MuzzleVelocity, consoleWrite);

                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            Func<string, string> muzzleNumber = (input) => {
                                                string result = "";
                                                string te = "";
                                                for (int i = 0; i < input.Length; i++)
                                                {
                                                    if (temp[i] < 58 && temp[i] > 47 || temp[i] == 46)
                                                    {
                                                        te += temp[i];
                                                        try
                                                        {
                                                            if ((!(temp[i + 1] < 58) || !(temp[i + 1] > 47)) && temp[i + 1] != 46)
                                                            {
                                                                result += te;
                                                            }
                                                        }
                                                        catch
                                                        {
                                                            invalid = true;
                                                        }
                                                    }
                                                }
                                                return result;
                                            };
                                            int sIndex = temp.IndexOf('s');
                                            temp = sIndex != -1 ? temp.Remove(sIndex) : muzzleNumber(temp);
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && (t < 97 || t > 122) && t != 32 && t != 9 && t != 46 && t!=47)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                                if (t == 46) dcount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                            if (dcount != 1)
                                            {
                                                invalid = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.MuzzleVelocity, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false; dcount = 0;


                                        //must only have numbers and decimal. must be <= 50
                                        temp = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.PenetrationDepth, consoleWrite);
                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            Func<string, string> penetrationNumber = (input) => {
                                                string result = "";
                                                string te = "";
                                                for (int i = 0; i < input.Length; i++)
                                                {
                                                    if (temp[i] < 58 && temp[i] > 47 || temp[i] == 46)
                                                    {
                                                        te += temp[i];
                                                        try
                                                        {
                                                            if ((!(temp[i + 1] < 58) || !(temp[i + 1] > 47)) && temp[i + 1] != 46)
                                                            {
                                                                result += te;
                                                            }
                                                        }catch
                                                        {
                                                            invalid = true;
                                                        }
                                                    }
                                                }
                                                return result;
                                            };
                                            try
                                            {
                                                if (Convert.ToDouble(penetrationNumber(temp.Trim())) > 0.5) invalid = true; //will flag anything above 0.5, just to be sure
                                            }
                                            catch
                                            {
                                                invalid = true;
                                            }
                                            try
                                            {
                                                if ((temp.Contains("studs")?temp.Remove(temp.IndexOf("studs")):temp).Trim() == "0.0") invalid = true; //will flag anything above 0.5, just to be sure
                                            }
                                            catch
                                            {
                                                invalid = true;
                                            }
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && (t < 97 || t > 122) && t != 32 && t != 9 && t != 46)
                                                {
                                                    icount++;
                                                }
                                                if (t == 46) dcount++;
                                                if (t == 32) scount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                            if(dcount != 1)
                                            {
                                                invalid = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.PenetrationDepth, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false; dcount = 0;


                                        //must only have numbers and decimal. must be <= 10 (for default)
                                        temp = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.Suppression, consoleWrite);
                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && t != 32 && t != 9 && t != 46)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                                if (t == 46) dcount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (dcount != 1)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.Suppression, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false; dcount = 0;


                                        //must only have numbers and decimal, but "seconds" will be truncated off. must be <= 10
                                        temp = FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.ReloadTime, consoleWrite);
                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && (t < 97 || t > 122) && t != 32 && t != 9 && t != 46)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                                if (t == 46) dcount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                            if (dcount != 1)
                                            {
                                                invalid = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.ReloadTime, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false; dcount = 0;


                                        temp = FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.EmptyReloadTime, consoleWrite);
                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && (t < 97 || t > 122) && t != 32 && t != 9 && t != 46)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                                if (t == 46) dcount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                            if (dcount != 1)
                                            {
                                                invalid = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.EmptyReloadTime, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false; dcount = 0;


                                        //must only have numbers and decimal, but "seconds" will be truncated off. must be <= 10
                                        temp = FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.AimingWalkspeed, consoleWrite);
                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && (t < 97 || t > 122) && t != 32 && t != 9 && t != 46)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                                if (t == 46) dcount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                            if (dcount != 1)
                                            {
                                                invalid = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.AimingWalkspeed, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false; dcount = 0;
                                        temp = FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.WeaponWalkspeed, consoleWrite);
                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && (t < 97 || t > 122) && t != 32 && t != 9 && t != 46)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                                if (t == 46) dcount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                            if (dcount != 1)
                                            {
                                                invalid = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.WeaponWalkspeed, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false; dcount = 0;


                                        //will have to be checked every time. way too unreliable
                                        temp = FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.AmmoType, consoleWrite);
                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && (t < 97 || t > 122) && (t < 65 || t > 90) && t != 32 && t != 9 && t != 46)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                            if (dcount != 1)
                                            {
                                                invalid = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.AmmoType, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false;


                                        //only CAPITAL letters and "|" are allowed
                                        temp = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.FireModes, consoleWrite);
                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            foreach (char t in temp)
                                            {
                                                if ((t < 65 || t > 90) && t != 32 && t != 9 && t != 124)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.FireModes, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false;
                                        temp = FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.FireModes, consoleWrite);


                                        //only numbers, "-",">", and "x" are allowed, must be <= 150
                                        temp = FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.Damage, consoleWrite); //only __1.png files have the correct suppression
                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            Func<string, string> damageNumber = (input) => {
                                                string result = "";
                                                string te = "";
                                                for (int i = 0; i < input.Length; i++)
                                                {
                                                    if (temp[i] < 58 && temp[i] > 47 || temp[i] == 46)
                                                    {
                                                        te += temp[i];
                                                        try
                                                        {
                                                            if ((!(temp[i + 1] < 58) || !(temp[i + 1] > 47)) && temp[i + 1] != 46)
                                                            {
                                                                result += te;
                                                            }
                                                        }catch(IndexOutOfRangeException e)
                                                        {
                                                            invalid = true;
                                                        }
                                                    }
                                                }
                                                return result;
                                            };
                                            string trimmedTemp = temp.Trim();
                                            int sep2 = trimmedTemp.LastIndexOf(' ');
                                            int separatorIndex = trimmedTemp.IndexOf('-') == -1 ?
                                                                    (trimmedTemp.IndexOf('>') == -1 ?
                                                                        (trimmedTemp.IndexOf(' '))
                                                                        : trimmedTemp.IndexOf('>'))
                                                                    : trimmedTemp.IndexOf('-');
                                            double damage1 = 0;
                                            double damage2 = 0;
                                            try
                                            {
                                                damage2 = Convert.ToDouble(
                                                    damageNumber(
                                                        trimmedTemp.Substring(
                                                            sep2 == -1 ?
                                                                (trimmedTemp.IndexOf('>') == -1
                                                                    ? trimmedTemp.IndexOf('-')
                                                                : trimmedTemp.IndexOf('>'))
                                                            : sep2)));
                                            }
                                            catch
                                            {
                                                invalid = true;
                                            }
                                            if (damage2 < 10) invalid = true; //will flag anything above 1.4, just to be sure

                                            try
                                            {
                                                damage1 = Convert.ToDouble(
                                                    damageNumber(trimmedTemp[..^(separatorIndex)]));
                                            }
                                            catch
                                            {
                                                invalid = true;
                                            }
                                            if (damage1 < 10) invalid = true; //will flag anything above 1.4, just to be sure

                                            if (damage1 <= damage2) invalid = true; //will flag anything above 1.4, just to be sure

                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && t != 32 && t != 9 && t != 45 && t!= 62 && t!= 120)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.Damage, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false;


                                        //must only have numbers
                                        temp = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.Firerate, consoleWrite);
                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            string fireratestr = temp.Trim();
                                            double firerateint = 0;
                                            try
                                            {
                                                firerateint = Convert.ToDouble(fireratestr);

                                            }
                                            catch
                                            {
                                                invalid = true;
                                            }
                                            if (firerateint < 500) invalid = true;
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && t != 32 && t != 9)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.Firerate, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false;

                                        string tabs = "\t\t\t ";
                                        Console.WriteLine("Category:" + tabs + "missing invalid");
                                        Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
                                        //Console.Beep();
                                        foreach (FileParsing.SearchTargets st in issues.Keys)
                                        {
                                            if (st == FileParsing.SearchTargets.Damage || st == FileParsing.SearchTargets.Rank) tabs = "\t\t\t\t ";
                                            if(st == FileParsing.SearchTargets.PenetrationDepth) tabs = "\t\t ";
                                            Console.WriteLine(st.ToString() + tabs + (issues[st].Item1?"+++++++":"       ") + " " + (issues[st].Item2 ? "+++++++" : "       ") + " " + (issues[st].Item1 || issues[st].Item2? issues[st].Item3 : "\t\t\t" + issues[st].Item3));

                                            if (issues[st].Item1 || issues[st].Item2 || st==FileParsing.SearchTargets.AmmoType) //proofreading part
                                            {
                                                string proofread = Console.ReadLine() ?? "";
                                                //filepath1 damagerange multis muzzle suppression penetration 
                                                //filepath2 ammotype reloadtime empty reload time aiming walkspeed weapon walkspeed 
                                                //either rank firerate firemodes version ammocap
                                                if (proofread != "")
                                                {
                                                    if (st == FileParsing.SearchTargets.DamageRange || st == FileParsing.SearchTargets.HeadMultiplier || st == FileParsing.SearchTargets.TorsoMultiplier || st == FileParsing.SearchTargets.LimbMultiplier || st == FileParsing.SearchTargets.MuzzleVelocity || st == FileParsing.SearchTargets.Suppression || st == FileParsing.SearchTargets.PenetrationDepth)
                                                    {
                                                        FileParsing.findStatisticInFileReplace(filepath1, st, issues[st].Item3, proofread, issues[st].Item1, issues[st].Item2, true);
                                                    }
                                                    else if (st == FileParsing.SearchTargets.AmmoType || st == FileParsing.SearchTargets.ReloadTime || st == FileParsing.SearchTargets.EmptyReloadTime || st == FileParsing.SearchTargets.AimingWalkspeed || st == FileParsing.SearchTargets.WeaponWalkspeed)
                                                    {
                                                        FileParsing.findStatisticInFileReplace(filepath2, st, issues[st].Item3, proofread, issues[st].Item1, issues[st].Item2 || st==FileParsing.SearchTargets.AmmoType, true);
                                                    }
                                                    else
                                                    {
                                                        FileParsing.findStatisticInFileReplace(filepath1, st, issues[st].Item3, proofread, issues[st].Item1, issues[st].Item2, true);
                                                        FileParsing.findStatisticInFileReplace(filepath2, st, issues[st].Item3, proofread, issues[st].Item1, issues[st].Item2, true);
                                                    }
                                                }else if(proofread == "")
                                                {

                                                }
                                            }
                                            tabs = "\t\t\t ";
                                            continue;
                                        }
                                        if (issues.Count < 1) continue;
                                        //FileParsing.SearchTargets.

                                    }
                                }
                                break;
                            }
                        case BuildOptions.FGS or BuildOptions.HEGS or BuildOptions.IGS:
                            {
                                //keyValuePairs[options]
                                if (opt == bo)
                                {
                                    for (int i = 0; i < keyValuePairs[opt].Count; i++) //per each entry (aka weapon) in category
                                    {
                                        FileProcessing.AllWeaponStrings strings = new(BuildOptionsConvert(opt));
                                        string filepath1 = "";
                                        string filepath2 = "";
                                        
                                        foreach (string str in strings.GetStrings())
                                        { //compares against allweaponstrings
                                            if (keyValuePairs[opt][i].Filename == (Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(str).Item1)) //output__semtex.png
                                            {
                                                filepath1 = Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(str).Item1;
                                                filepath2 = Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(str).Item1;

                                            }
                                            else if (keyValuePairs[opt][i].Filename == (Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(str).Item2)) //output__ak121.png (impossible in this switch case)
                                            {
                                                Console.WriteLine("impossible error..." + str);
                                            }
                                            else if (keyValuePairs[opt][i].Filename == (Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(str).Item3)) //output__ak122.png (impossible in this switch case)
                                            {
                                                Console.WriteLine("impossible error..." + str);
                                            }
                                            else
                                            {
                                                //Console.WriteLine(str);
                                            }
                                        }
                                        Console.WriteLine(keyValuePairs[opt][i].Filename);
                                        bool consoleWrite = false;
                                        string temp;
                                        bool missing = false;
                                        bool invalid = false;
                                        int icount = 0; //invalid counts
                                        int scount = 0; //space counts
                                        int dcount = 0; //dot counts
                                        //<missing, invalid>
                                        Dictionary<FileParsing.SearchTargets, Tuple<bool, bool, string>> issues = new();


                                        FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.Version, consoleWrite);
                                        FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.Version, consoleWrite);

                                        temp = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.BlastRadius, consoleWrite);

                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            try
                                            {
                                                if (Convert.ToDouble(temp.Trim()) < 25) invalid = true;
                                            }
                                            catch
                                            {
                                                invalid = true;
                                            }
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && t != 32 && t != 9)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.BlastRadius, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false;


                                        temp = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.KillingRadius, consoleWrite);

                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            try
                                            {
                                                if (Convert.ToDouble(temp.Trim()) < 25) invalid = true;
                                            }
                                            catch
                                            {
                                                invalid = true;
                                            }
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && t != 32 && t != 9)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.KillingRadius, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false;


                                        temp = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.MaximumDamage, consoleWrite);

                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            try
                                            {
                                                if (Convert.ToDouble(temp.Trim()) < 100) invalid = true;
                                            }
                                            catch
                                            {
                                                invalid = true;
                                            }
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && t != 32 && t != 9 )
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.MaximumDamage, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false;



                                        temp = FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.TriggerMechanism, consoleWrite);
                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && (t < 97 || t > 122) && (t < 65 || t > 90) && t != 32 && t != 9 && t != 46)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                                if (t == 46) dcount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                            if(dcount != 1)
                                            {
                                                invalid = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.TriggerMechanism, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false; dcount = 0;


                                        //only CAPITAL letters are allowed
                                        temp = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.SpecialEffects, consoleWrite);
                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            foreach (char t in temp)
                                            {
                                                if ((t < 65 || t > 90) && t != 32 && t != 9 )
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.SpecialEffects, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false;

                                        //only numbers, "-",">", and "x" are allowed, must be <= 150
                                        temp = FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.StoredCapacity, consoleWrite); //only __1.png files have the correct suppression
                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            try
                                            {
                                                if (Convert.ToDouble(temp.Trim()) > 3) invalid = true;
                                            }
                                            catch
                                            {
                                                invalid = true;
                                            }
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && t != 32 && t != 9)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.StoredCapacity, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false;


                                        string tabs = "\t\t\t ";
                                        Console.WriteLine("Category:" + tabs + "missing invalid");
                                        Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
                                        //Console.Beep();
                                        foreach (FileParsing.SearchTargets st in issues.Keys)
                                        {
                                            if (st == FileParsing.SearchTargets.Damage || st == FileParsing.SearchTargets.Rank) tabs = "\t\t\t\t ";
                                            if (st == FileParsing.SearchTargets.TriggerMechanism) tabs = "\t\t ";
                                            Console.WriteLine(st.ToString() + tabs + (issues[st].Item1 ? "+++++++" : "       ") + " " + (issues[st].Item2 ? "+++++++" : "       ") + " " + (issues[st].Item1 || issues[st].Item2 ? issues[st].Item3 : "\t\t\t" + issues[st].Item3));

                                            if (issues[st].Item1 || issues[st].Item2 || st == FileParsing.SearchTargets.TriggerMechanism) //proofreading part
                                            {
                                                string proofread = Console.ReadLine() ?? "";
                                                //since both filepaths are the same, we dont need any logic separating them
                                                if (proofread != "")
                                                {
                                                    FileParsing.findStatisticInFileReplace(filepath1, st, issues[st].Item3, proofread, issues[st].Item1, issues[st].Item2 || st == FileParsing.SearchTargets.TriggerMechanism, true);
                                                    FileParsing.findStatisticInFileReplace(filepath2, st, issues[st].Item3, proofread, issues[st].Item1, issues[st].Item2 || st == FileParsing.SearchTargets.TriggerMechanism, true);
                                                }
                                            }
                                            tabs = "\t\t\t ";
                                            continue;
                                        }
                                        if (issues.Count < 1) continue;                                        
                                        //FileParsing.SearchTargets.

                                    }
                                }
                                break;
                            }
                        case BuildOptions.OHBT or BuildOptions.OHBE or BuildOptions.THBT or BuildOptions.THBE:
                            {
                                //keyValuePairs[options]
                                if (opt == bo)
                                {
                                    for (int i = 0; i < keyValuePairs[opt].Count; i++) //per each entry (aka weapon) in category
                                    {
                                        FileProcessing.AllWeaponStrings strings = new(BuildOptionsConvert(opt));
                                        string filepath1 = "";
                                        string filepath2 = "";

                                        foreach (string str in strings.GetStrings())
                                        { //compares against allweaponstrings
                                            if (keyValuePairs[opt][i].Filename == (Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(str).Item1)) //output__semtex.png
                                            {
                                                filepath1 = Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(str).Item1;
                                                filepath2 = Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(str).Item1;

                                            }
                                            else if (keyValuePairs[opt][i].Filename == (Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(str).Item2)) //output__ak121.png (impossible in this switch case)
                                            {
                                                Console.WriteLine("impossible error..." + str);
                                            }
                                            else if (keyValuePairs[opt][i].Filename == (Global.ReadFolder + FileProcessing.Filenames.convertGunNameToFileNames(str).Item3)) //output__ak122.png (impossible in this switch case)
                                            {
                                                Console.WriteLine("impossible error..." + str);
                                            }
                                            else
                                            {
                                                //Console.WriteLine(str);
                                            }
                                        }
                                        Console.WriteLine(keyValuePairs[opt][i].Filename);
                                        bool consoleWrite = false;
                                        string temp;
                                        bool missing = false;
                                        bool invalid = false;
                                        int icount = 0; //invalid counts
                                        int scount = 0; //space counts
                                        int dcount = 0; //dot counts
                                        //<missing, invalid>
                                        Dictionary<FileParsing.SearchTargets, Tuple<bool, bool, string>> issues = new();


                                        FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.Version, consoleWrite);
                                        FileParsing.findStatisticInFile(filepath2, FileParsing.SearchTargets.Version, consoleWrite);


                                        //multipliers -> must not have any letters or special characters. must be <= 15 (for default) (also must have .)
                                        temp = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.HeadMultiplier, consoleWrite);
                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            try
                                            {
                                                if (Convert.ToDouble(temp) > 1.0) invalid = true; //will flag anything above 1.4, just to be sure
                                            }
                                            catch
                                            {
                                                invalid = true;
                                            }
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && t != 32 && t != 9 && t != 46)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                                if (t == 46) dcount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                            if (dcount != 1)
                                            {
                                                invalid = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.HeadMultiplier, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false; dcount = 0;
                                        temp = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.TorsoMultiplier, consoleWrite);
                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            try
                                            {
                                                if (Convert.ToDouble(temp) > 1.0) invalid = true; //will flag anything above 1.4, just to be sure
                                            }
                                            catch
                                            {
                                                invalid = true;
                                            }
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && t != 32 && t != 9 && t != 46)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                                if (t == 46) dcount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                            if (dcount != 1)
                                            {
                                                invalid = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.TorsoMultiplier, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false; dcount = 0;
                                        temp = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.LimbMultiplier, consoleWrite);
                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            try
                                            {
                                                if (Convert.ToDouble(temp) > 1.0) invalid = true; //will flag anything above 1.4, just to be sure
                                            }
                                            catch
                                            {
                                                invalid = true;
                                            }
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && t != 32 && t != 9 && t != 46)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                                if (t == 46) dcount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                            if (dcount != 1)
                                            {
                                                invalid = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.LimbMultiplier, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false; dcount = 0;


                                        temp = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.FrontStabDamage, consoleWrite);
                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            try
                                            {
                                                if (Convert.ToDouble(temp) < 60) invalid = true; //will flag anything above 1.4, just to be sure
                                            }
                                            catch
                                            {
                                                invalid = true;
                                            }
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && t != 32 && t != 9 && t != 46)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                                if (t == 46) dcount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                            if (dcount != 1)
                                            {
                                                invalid = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.FrontStabDamage, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false; dcount = 0;


                                        temp = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.BackStabDamage, consoleWrite);
                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            try
                                            {
                                                if (Convert.ToDouble(temp) < 100) invalid = true; //will flag anything above 1.4, just to be sure
                                            }
                                            catch
                                            {
                                                invalid = true;
                                            }
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && t != 32 && t != 9 && t != 46)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                                if (t == 46) dcount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                            if (dcount != 1)
                                            {
                                                invalid = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.BackStabDamage, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false; dcount = 0;


                                        temp = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.Walkspeed, consoleWrite);
                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && t != 32 && t != 9 && t != 46)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                                if (t == 46) dcount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                            if (dcount != 1)
                                            {
                                                invalid = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.Walkspeed, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false; dcount = 0;


                                        temp = FileParsing.findStatisticInFile(filepath1, FileParsing.SearchTargets.BladeLength, consoleWrite);
                                        if (temp == "")
                                        {
                                            missing = true;
                                        }
                                        else
                                        {
                                            foreach (char t in temp)
                                            {
                                                if ((t < 48 || t > 57) && t != 32 && t != 9 && t != 46)
                                                {
                                                    icount++;
                                                }
                                                if (t == 32) scount++;
                                                if (t == 46) dcount++;
                                            }
                                            if (icount > 0)
                                            {
                                                invalid = true;
                                            }
                                            if (scount == temp.Length)
                                            {
                                                missing = true;
                                            }
                                            if (dcount != 1)
                                            {
                                                invalid = true;
                                            }
                                        }
                                        issues.Add(FileParsing.SearchTargets.BladeLength, Tuple.Create(missing, invalid, temp));
                                        icount = 0; scount = 0; missing = false; invalid = false; dcount = 0;


                                        string tabs = "\t\t\t ";
                                        Console.WriteLine("Category:" + tabs + "missing invalid");
                                        Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
                                        //Console.Beep();
                                        foreach (FileParsing.SearchTargets st in issues.Keys)
                                        {
                                            //if (st == FileParsing.SearchTargets.Walkspeed || st == FileParsing.SearchTargets.BladeLength) tabs = "\t\t\t\t ";
                                            //if (st == FileParsing.SearchTargets.FrontStabDamage || st == FileParsing.SearchTargets.BackStabDamage) tabs = "\t\t ";
                                            Console.WriteLine(st.ToString() + tabs + (issues[st].Item1 ? "+++++++" : "       ") + " " + (issues[st].Item2 ? "+++++++" : "       ") + " " + (issues[st].Item1 || issues[st].Item2 ? issues[st].Item3 : "\t\t\t" + issues[st].Item3));

                                            if (issues[st].Item1 || issues[st].Item2) //proofreading part
                                            {
                                                string proofread = Console.ReadLine() ?? "";
                                                //since both filepaths are the same, we dont need any logic separating them
                                                if (proofread != "")
                                                {
                                                    FileParsing.findStatisticInFileReplace(filepath1, st, issues[st].Item3, proofread, issues[st].Item1, issues[st].Item2, true);
                                                    FileParsing.findStatisticInFileReplace(filepath2, st, issues[st].Item3, proofread, issues[st].Item1, issues[st].Item2, true);
                                                }
                                            }
                                            tabs = "\t\t\t ";
                                            continue;
                                        }
                                        if (issues.Count < 1) continue;
                                        //FileParsing.SearchTargets.

                                    }
                                }
                                break;
                            }
                    }
                    continue;
                }
                continue;
            }
            return keyValuePairs;
        }

        public static BuildOptions BuildOptionsConvert (FileProcessing.BuildOptions options)
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

        public static FileProcessing.BuildOptions BuildOptionsConvert(BuildOptions options)
        {
            FileProcessing.BuildOptions result = 0;
            switch (options)
            {

                case BuildOptions.ARS:
                    {
                        result = FileProcessing.BuildOptions.AssaultRifles;
                        break;
                    }
                case BuildOptions.PDWS:
                    {
                        result = FileProcessing.BuildOptions.PersonalDefenseWeapons;
                        break;
                    }
                case BuildOptions.LMGS:
                    {
                        result = FileProcessing.BuildOptions.LightMachineGuns;
                        break;
                    }
                case BuildOptions.SRS:
                    {
                        result = FileProcessing.BuildOptions.SniperRifles;
                        break;
                    }
                case BuildOptions.SHS:
                    {
                        result = FileProcessing.BuildOptions.Shotguns;
                        break;
                    }
                case BuildOptions.CAS:
                    {
                        result = FileProcessing.BuildOptions.Carbines;
                        break;
                    }
                case BuildOptions.DMRS:
                    {
                        result = FileProcessing.BuildOptions.DesignatedMarksmanRifles;
                        break;
                    }
                case BuildOptions.BRS:
                    {
                        result = FileProcessing.BuildOptions.BattleRifles;
                        break;
                    }
                case BuildOptions.PS:
                    {
                        result = FileProcessing.BuildOptions.Pistols;
                        break;
                    }
                case BuildOptions.MPS:
                    {
                        result = FileProcessing.BuildOptions.MachinePistols;
                        break;
                    }
                case BuildOptions.RES:
                    {
                        result = FileProcessing.BuildOptions.Revolvers;
                        break;
                    }
                case BuildOptions.OTH:
                    {
                        result = FileProcessing.BuildOptions.Other;
                        break;
                    }
                case BuildOptions.FGS:
                    {
                        result = FileProcessing.BuildOptions.FragmentationGrenades;
                        break;
                    }
                case BuildOptions.IGS:
                    {
                        result = FileProcessing.BuildOptions.ImpactGrenades;
                        break;
                    }
                case BuildOptions.HEGS:
                    {
                        result = FileProcessing.BuildOptions.HighExplosiveGrenades;
                        break;
                    }
                case BuildOptions.OHBE:
                    {
                        result = FileProcessing.BuildOptions.OneHandBladeMelees;
                        break;
                    }
                case BuildOptions.OHBT:
                    {
                        result = FileProcessing.BuildOptions.OneHandBluntMelees;
                        break;
                    }
                case BuildOptions.THBE:
                    {
                        result = FileProcessing.BuildOptions.TwoHandBladeMelees;
                        break;
                    }
                case BuildOptions.THBT:
                    {
                        result = FileProcessing.BuildOptions.TwoHandBluntMelees;
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

            Action<FileProcessing.BuildOptions, FileProcessing.StreamOptions, bool, bool, bool, string, bool?, bool?, Dictionary<BuildOptions,Dictionary<int, FileProcessing.WeaponOutputs>>> builders = (option, stream, consoleLogging, largeFileLogging, individualFileLogging, largeFileLogName, primaryOrSecondary, meleeOrGrenade, res) => {res.Add(BuildOptionsConvert(option),FileProcessing.multithreadedReadFileListAsync(stream, new FileProcessing.AllWeaponStrings(option).GetStrings(), Tuple.Create(consoleLogging, largeFileLogging, individualFileLogging), largeFileLogName, primaryOrSecondary, meleeOrGrenade, new Dictionary<int, FileProcessing.WeaponOutputs>()).Result); };


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
            ARS.Name = nameof(ARS);
            PDWS.Name = nameof(PDWS);
            LMGS.Name = nameof(LMGS);
            SRS.Name = nameof(SRS);
            CAS.Name = nameof(CAS);
            DMRS.Name = nameof(DMRS);
            BRS.Name = nameof(BRS);
            SHS.Name = nameof(SHS);
            PS.Name = nameof(PS);
            MPS.Name = nameof(MPS);
            RES.Name = nameof(RES);
            OTH.Name = nameof(OTH);

            FGS.Name = nameof(FGS);
            HEGS.Name = nameof(HEGS);
            IGS.Name = nameof(IGS);

            OHBT.Name = nameof(OHBT);
            OHBE.Name = nameof(OHBE);
            THBT.Name = nameof(THBT);
            THBE.Name = nameof(THBE);

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

            this.result = new ConcurrentDictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>> (result);
            return result;

        }

        public async Task<Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>>> FullBuild()
        {
            return Build(BuildOptions.ARS | BuildOptions.PDWS | BuildOptions.LMGS | BuildOptions.SRS | BuildOptions.CAS | BuildOptions.DMRS | BuildOptions.BRS | BuildOptions.SHS | BuildOptions.PS | BuildOptions.MPS | BuildOptions.RES | BuildOptions.OTH | BuildOptions.FGS | BuildOptions.HEGS | BuildOptions.IGS | BuildOptions.OHBT | BuildOptions.OHBE | BuildOptions.THBT | BuildOptions.THBE).Result;
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

            Thread MainThread = Thread.CurrentThread;

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

            //Func<bool, bool, bool, bool, Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>>> func3 = (OHBT, IGS, SRS, RES) => thread3Async(OHBT, IGS, SRS, RES).Result;

            Thread T1 = new(async () => await thread1Async(DMRS, LMGS, OHBE));
            Thread T2 = new(async () => await thread2Async(BRS, SHS, MPS, OTH, FGS, HEGS));
            Thread T3 = new(async () => await thread3Async(OHBT, IGS, SRS, RES));
            Thread T4 = new(async () => await thread4Async(PDWS, CAS));
            Thread T5 = new(async () => await thread5Async(ARS, PS));
            Thread T6 = new(async () => await thread6Async(THBT,THBE));

            //ThreadStart start = new(async () => await thread3Async(OHBT, IGS, SRS, RES));
            //Func<Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>>> t3A = start.Method.CreateDelegate<Func<Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>>>>(thread3Async(OHBT, IGS, SRS, RES).Result);

            T1.Name = nameof(T1);
            T2.Name = nameof(T2);
            T3.Name = nameof(T3);
            T4.Name = nameof(T4);
            T5.Name = nameof(T5);
            T6.Name = nameof(T6);

            List<Thread> threads = new() { T1, T2, T3, T4, T5, T6 };
            foreach(Thread j in threads)
            {
                j.IsBackground = true;
                j.Priority = ThreadPriority.BelowNormal;
            }

            if(DMRS || LMGS || OHBE)T1.Start();
            if(BRS || SHS || MPS || OTH || FGS || HEGS)T2.Start();
            if(OHBT || IGS || SRS || RES)T3.Start();
            if(PDWS || CAS)T4.Start();
            if(ARS || PS)T5.Start();
            if(THBT || THBE)T6.Start();

            Console.WriteLine("damnit");
            
            return new Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>>(result);

            //) thread1Async().Start();

        }

        //31 + 20 = 51 dmrs, lmgs, one hand blade
        public async Task<Dictionary<BuildOptions, Dictionary<int, FileProcessing.WeaponOutputs>>> thread1Async(bool DMRS, bool LMGS, bool OHBE)
        {
            Dictionary<BuildOptions,Dictionary<int, FileProcessing.WeaponOutputs>> result = new();

            Dictionary<int, FileProcessing.WeaponOutputs>? output1 = null; 
            if(DMRS)output1 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.DesignatedMarksmanRiflesStrings, Tuple.Create(false, true, true), "designatedmarksmanrifles.txt", true, null, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.TryAdd(BuildOptions.DMRS, output1 ?? new());


            Dictionary<int, FileProcessing.WeaponOutputs>? output2 = null; 
            if(LMGS)output2 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.LightMachineGunsStrings, Tuple.Create(false, true, true), "lightmachineguns.txt", true, null, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.TryAdd(BuildOptions.LMGS, output2 ?? new());


            Dictionary<int, FileProcessing.WeaponOutputs>? output3 = null;
            if (OHBE) output3 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.OneHandBladeMelees, Tuple.Create(false, true, true), "onehandedblademelees.txt", null, true, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.TryAdd(BuildOptions.OHBE, output3 ?? new());

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
            this.result.TryAdd(BuildOptions.BRS, output1 ?? new());


            Dictionary<int, FileProcessing.WeaponOutputs>? output2 = null;
            if (SHS) output2 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.ShotgunsStrings, Tuple.Create(false, true, true), "shotguns.txt", true, null, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.TryAdd(BuildOptions.SHS, output2 ?? new());


            Dictionary<int, FileProcessing.WeaponOutputs>? output3 = null;
            if (MPS) output3 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.MachinePistolsStrings, Tuple.Create(false, true, true), "machinepistols.txt", false, null, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.TryAdd(BuildOptions.MPS, output3 ?? new());


            Dictionary<int, FileProcessing.WeaponOutputs>? output4 = null;
            if (OTH) output4 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both,FileProcessing.AllWeaponStrings.OthersStrings, Tuple.Create(false, true, true), "others.txt", false, null, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.TryAdd(BuildOptions.OTH, output4 ?? new());


            Dictionary<int, FileProcessing.WeaponOutputs>? output5 = null;
            if (FGS) output5 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.FragmentationGrenadesStrings, Tuple.Create(false, true, true), "fragmentationgrenades.txt", null, false, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.TryAdd(BuildOptions.FGS, output5 ?? new());


            Dictionary<int, FileProcessing.WeaponOutputs>? output6 = null;
            if (HEGS) output6 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.HighExplosiveGrenadesStrings, Tuple.Create(false, true, true), "highexplosivegrenades.txt", null, false, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.TryAdd(BuildOptions.HEGS, output6 ?? new());

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
            this.result.TryAdd(BuildOptions.OHBT, output1 ?? new());


            Dictionary<int, FileProcessing.WeaponOutputs>? output2 = null;
            if (IGS) output2 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.ImpactGrenadesStrings, Tuple.Create(false, true, true), "impactgrenades.txt", null, false, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.TryAdd(BuildOptions.IGS, output2 ?? new());


            Dictionary<int, FileProcessing.WeaponOutputs>? output3 = null;
            if (SRS) output3 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.SniperRiflesStrings, Tuple.Create(false, true, true), "sniperrifles.txt", true, null, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.TryAdd(BuildOptions.SRS, output3 ?? new());

            Dictionary<int, FileProcessing.WeaponOutputs>? output4 = null;
            if (RES) output4 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.RevolversStrings, Tuple.Create(false, true, true), "revolvers.txt", false, null, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.TryAdd(BuildOptions.RES, output4 ?? new());

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
            this.result.TryAdd(BuildOptions.PDWS, output1 ?? new());


            Dictionary<int, FileProcessing.WeaponOutputs>? output2 = null;
            if (CAS) output2 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.CarbineStrings, Tuple.Create(false, true, true), "carbines.txt", true, null, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.TryAdd(BuildOptions.CAS, output2 ?? new());

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
            this.result.TryAdd(BuildOptions.ARS, output1 ?? new());


            Dictionary<int, FileProcessing.WeaponOutputs>? output2 = null;
            if (PS) output2 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both,FileProcessing.AllWeaponStrings.PistolsStrings, Tuple.Create(false, true, true), "carbines.txt", false, null, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.TryAdd(BuildOptions.PS, output2 ?? new());

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
            this.result.TryAdd(BuildOptions.THBT, output1 ?? new());


            Dictionary<int, FileProcessing.WeaponOutputs>? output2 = null;
            if (THBE) output2 = await FileProcessing.multithreadedReadFileListAsync(FileProcessing.StreamOptions.Both, FileProcessing.AllWeaponStrings.TwoHandBladeMelees, Tuple.Create(false, true, true), "twohandedblademelees.txt", null, true, new Dictionary<int, FileProcessing.WeaponOutputs>());
            this.result.TryAdd(BuildOptions.THBE, output2 ?? new());

            result.Add(BuildOptions.THBT, output1 ?? new());
            result.Add(BuildOptions.THBE, output2 ?? new());
            return result;
        }

        public static void renameAllFiles()
        {

            //ars pds lmgs srs brs cas dmrs shs
            //string file1 = ""; string file2 = "";
            //BuildOptions categoryNumber = 0;
            Action<List<string>, int> saveFilesGuns = (strings, savenumber) => {
                for (int s = 0; s < strings.Count; s++)
                {
                    try
                    {
                        File.Move("cropped__1.00__" + strings[s] + "1.png", "cropped__1.00__" + savenumber + "__" + s + "__" + strings[s] + "1.png");
                        File.Move("cropped__1.00__" + strings[s] + "2.png", "cropped__1.00__" + savenumber + "__" + s + "__" + strings[s] + "2.png");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                }
            };
            Action<List<string>, int> saveFilesOther = (strings, savenumber) => {
                for (int s = 0; s < strings.Count; s++)
                {
                    try
                    {
                        File.Move("cropped__1.00__" + strings[s] + ".png", "cropped__1.00__" + savenumber + "__" + s + "__" + strings[s] + ".png");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                }
            };
            //FileProcessing.AllWeaponStrings weaponStrings = new(FileProcessing.BuildOptions.None);
            saveFilesGuns(FileProcessing.AllWeaponStrings.AssaultRiflesStrings, 1);
            saveFilesGuns(FileProcessing.AllWeaponStrings.PersonalDefenseWeaponsStrings, 2);
            saveFilesGuns(FileProcessing.AllWeaponStrings.LightMachineGunsStrings, 3);
            saveFilesGuns(FileProcessing.AllWeaponStrings.SniperRiflesStrings, 4);


            saveFilesGuns(FileProcessing.AllWeaponStrings.BattleRiflesStrings, 5);
            saveFilesGuns(FileProcessing.AllWeaponStrings.CarbineStrings, 6);
            saveFilesGuns(FileProcessing.AllWeaponStrings.DesignatedMarksmanRiflesStrings, 7);
            saveFilesGuns(FileProcessing.AllWeaponStrings.ShotgunsStrings, 8);


            saveFilesGuns(FileProcessing.AllWeaponStrings.PistolsStrings, 9);
            saveFilesGuns(FileProcessing.AllWeaponStrings.MachinePistolsStrings, 10);
            saveFilesGuns(FileProcessing.AllWeaponStrings.RevolversStrings, 11);
            saveFilesGuns(FileProcessing.AllWeaponStrings.OthersStrings, 12);


            saveFilesOther(FileProcessing.AllWeaponStrings.FragmentationGrenadesStrings, 13);
            saveFilesOther(FileProcessing.AllWeaponStrings.HighExplosiveGrenadesStrings, 14);
            saveFilesOther(FileProcessing.AllWeaponStrings.ImpactGrenadesStrings, 15);


            saveFilesOther(FileProcessing.AllWeaponStrings.OneHandBladeMelees, 16);
            saveFilesOther(FileProcessing.AllWeaponStrings.OneHandBluntMelees, 17);
            saveFilesOther(FileProcessing.AllWeaponStrings.TwoHandBladeMelees, 18);
            saveFilesOther(FileProcessing.AllWeaponStrings.TwoHandBluntMelees, 19);
        }

    }

    public class SQLConnectionHandling
    {

        private SQLiteConnectionStringBuilder connectionString;



        //to standardize the SQL categories
        public static Dictionary<FileReading.BuildOptions, string> CategoryNames = new()
        {
            {FileReading.BuildOptions.ARS, "Assault Rifles" },
                    {FileReading.BuildOptions.PDWS, "Personal Defense Weapons" },
                    {FileReading.BuildOptions.LMGS, "Light Machine Guns"},
                    {FileReading.BuildOptions.SRS, "Sniper Rifles" },

                    {FileReading.BuildOptions.BRS, "Battle Rifles" },
                    {FileReading.BuildOptions.CAS, "Carbines" },
                    {FileReading.BuildOptions.DMRS, "Designated Marksman Rifles" },
                    {FileReading.BuildOptions.SHS, "Shotguns" },

                    {FileReading.BuildOptions.PS, "Pistols" },
                    {FileReading.BuildOptions.MPS, "Machine Pistols" },
                    {FileReading.BuildOptions.RES, "Revolvers" },
                    {FileReading.BuildOptions.OTH, "Others" },

                    {FileReading.BuildOptions.FGS, "Fragmentation Grenades" },
                    {FileReading.BuildOptions.HEGS, "High Explosive Grenades" },
                    {FileReading.BuildOptions.IGS, "Impact Grenades" },

                    {FileReading.BuildOptions.OHBE, "One Handed Blade Melees" },

                    {FileReading.BuildOptions.OHBT, "One Handed Blunt Melees" },

                    {FileReading.BuildOptions.THBE, "Two Handed Blade Melees" },

                    {FileReading.BuildOptions.THBT, "Two Handed Blunt Melees" },
            {FileReading.BuildOptions.NONE,"None" }
        };

        public SQLConnectionHandling()
        {
            SQLiteConnectionStringBuilder connectionStringBuilder = new(@"Data Source=.\Nephka.db;Version=3;FailIfMissing=True;");
            connectionString = connectionStringBuilder;
        }

        public void InsertSQLClassRecord(Class insertion)
        {

            using (var conn = new SQLiteConnection(connectionString.ConnectionString))
            {
                using (var command = conn.CreateCommand())
                {
                    List<string> categoryNames = new();
                    foreach(Category s in insertion.getCategoryList())
                    {
                        categoryNames.Add(s.Name);
                    }
                    bool grenades = insertion.Name.Contains("grenade", StringComparison.CurrentCultureIgnoreCase) || insertion.Name.Contains("nade", StringComparison.CurrentCultureIgnoreCase);
                    if (grenades)
                    {
                        command.CommandText = String.Format(
                                                    @"INSERT INTO CategoryData (ClassName, Category1, Category2, Category3)
                                                VALUES ('{0}', '{1}', '{2}', '{3}');",
                                                    insertion.Name, categoryNames[0], categoryNames[1], categoryNames[2]);
                    }
                    else {
                        command.CommandText = String.Format(
                                                    @"INSERT INTO CategoryData (ClassName, Category1, Category2, Category3, Category4)
                                                VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');",
                                                    insertion.Name, categoryNames[0], categoryNames[1], categoryNames[2], categoryNames[3]);
                    }
                    conn.Open();
                    command.ExecuteReader();
                    conn.Close();
                    Console.ReadKey();
                    
                }
            }
        }

        public void InsertSQLWeaponRecords(Dictionary<FileReading.Classes, Class> pairs) 
            
        {
            /*using (var conn = new SQLiteConnection(connectionString.ConnectionString))
            {
                using (var command = conn.CreateCommand())
                {
                    conn.Open();
                    command.CommandText = "SELECT * FROM CategoryData;";
                    using(var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                        }

                    }
                }
            }*/

                    Dictionary<FileReading.BuildOptions, Category> p1 = new();
            foreach(FileReading.Classes r in pairs.Keys) //for each class
            {
                foreach(Category h in pairs[r].getCategoryList()) //for each category in each class
                {
                    foreach(FileReading.BuildOptions k in CategoryNames.Keys)
                    {
                        if(h.Name == CategoryNames[k]){
                            try
                            {
                                p1.Add(k, h);
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                }
            }
            foreach(FileReading.BuildOptions o in p1.Keys)
            {
                if(o == FileReading.BuildOptions.FGS || o == FileReading.BuildOptions.IGS || o == FileReading.BuildOptions.HEGS)
                {
                    foreach(Grenade g in p1[o].getWeapons().Cast<Grenade>())
                    {
                        using (var conn = new SQLiteConnection(connectionString.ConnectionString))
                        {
                            using (var command = conn.CreateCommand())
                            {

                                conn.Open();
                                command.CommandText = String.Format(@"INSERT INTO GrenadeData{10} (Name, HasRank, Rank, FuseTime, SpecialEffects, BlastRadius, KillingRadius, MaximumDamage, Version, Category, StoredCapacity)
                                                VALUES ('{0}', '{1}', {2}, {3}, '{4}', {5}, {6}, {7}, '{8}', '{9}', {11});",
                                                g.Name,g.HasRank?"True":"False",g.Rank, g.FuseTime, 
                                                g.SpecialMode, g.BlastRadius, g.KillRadius, g.MaximumDamage,
                                                Global.VERSION, CategoryNames[o], Global.DBVERSION, g.StoredCapacity);
                                command.ExecuteReader();
                                conn.Close();
                            }
                        }
                    }
                } else if(o == FileReading.BuildOptions.OHBT || o == FileReading.BuildOptions.OHBE || o == FileReading.BuildOptions.THBT  || o == FileReading.BuildOptions.THBE)
                {
                    foreach (Melee m in p1[o].getWeapons().Cast<Melee>())
                    {
                        using (var conn = new SQLiteConnection(connectionString.ConnectionString))
                        {
                            using (var command = conn.CreateCommand())
                            {

                                conn.Open();
                                command.CommandText = String.Format(@"INSERT INTO MeleeData{12} (Name, HasRank, Rank, BladeLength, FrontStabDamage, BackStabDamage, HeadMultiplier, TorsoMultiplier, LimbMultiplier, Walkspeed, Version, Category)
                                                VALUES ('{0}', '{1}', {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, '{10}', '{11}');",
                                                m.Name, m.HasRank ? "True" : "False", m.Rank, m.BladeLength,
                                                m.FrontStabDamage, m.BackStabDamage, m.CarriedAttributes.HeadMultiplier, m.CarriedAttributes.TorsoMultiplier, m.CarriedAttributes.LimbMultiplier, m.CarriedAttributes.WalkSpeed,
                                                Global.VERSION, CategoryNames[o], Global.DBVERSION);
                                command.ExecuteReader();
                                conn.Close();
                            }
                        }
                    }
                } else if(o == FileReading.BuildOptions.NONE)
                {

                }
                else
                {

                    foreach (Gun g in p1[o].getWeapons().Cast<Gun>())
                    {


                        List<string> convForeignHooks = new();
                        List<Conversion> convs = g.Conversions.getConversions();
                        for (int i = 0; i < convs.Count; i++)
                        {
                            using (var conn = new SQLiteConnection(connectionString.ConnectionString))
                            {
                                using (var command = conn.CreateCommand())
                                {
                                    Conversion k = convs[i];

                                    if (i == 0)
                                    {
                                        if (k.ConversionName == null || !k.ConversionName.Contains(g.Name, StringComparison.CurrentCultureIgnoreCase))
                                        {
                                            k.ConversionName = g.Name; //make sure the default conversion has the gun name on it
                                        }
                                    }
                                    if (convForeignHooks.Count > 10)
                                    {
                                        Console.WriteLine("too many conversions"); break;
                                    }
                                    convForeignHooks.Add(k.ConversionName);
                                    

                                    conn.Open();
                                    switch (k.FireModes.getFireModes().Count) {
                                        case 1:
                                            FireMode t = k.FireModes.getFireModes()[0];
                                            command.CommandText = String.Format(@"INSERT INTO ConversionData{23} (ConversionName, Caliber, ReserveAmmoCapacity, MagazineCapacity, HeadMultiplier, TorsoMultiplier, LimbMultiplier, Damage1, Damage2, Damage1Range, Damage2Range, MuzzleVelocity, PenetrationDepth, Suppression, ReloadTime, EmptyReloadTime, AimingWalkspeed, WeaponWalkspeed,Version, Firemode1, Firemode1Firerate, Firemode1Special, Firemode1Pellets)
                                                VALUES ('{0}', '{1}', {2},{3},
                                                        {4},{5},{6},{7},
                                                        {8},{9},{10},{11},
                                                        {12},{13},{14},{15},
                                                        {16},{17},'{18}',
                                                        '{19}',{20},'{21}',{22});",
                                                        k.ConversionName, k.Caliber, k.AmmoCapacity, k.MagazineCapacity,
                                                        k.CarriedAttributes.HeadMultiplier, k.CarriedAttributes.TorsoMultiplier, k.CarriedAttributes.LimbMultiplier, k.RangedAttributes.Range1Damage,
                                                        k.RangedAttributes.Range2Damage, k.RangedAttributes.Range1, k.RangedAttributes.Range2, k.MuzzleVelocity,
                                                        k.Penetration, k.Suppression, k.ReloadSpeed, k.EmptyReloadSpeed,
                                                        k.AimingWalkspeed, k.CarriedAttributes.WalkSpeed, Global.VERSION,
                                                        t.Mode, t.Firerate, t.SpecialMode, t.Pellets, Global.DBVERSION);
                                            break;
                                        case 2:
                                            FireMode w = k.FireModes.getFireModes()[0];
                                            FireMode u = k.FireModes.getFireModes()[1];
                                            command.CommandText = String.Format(@"INSERT INTO ConversionData{27} (ConversionName, Caliber, ReserveAmmoCapacity, MagazineCapacity, HeadMultiplier, TorsoMultiplier, LimbMultiplier, Damage1, Damage2, Damage1Range, Damage2Range, MuzzleVelocity, PenetrationDepth, Suppression, ReloadTime, EmptyReloadTime, AimingWalkspeed, WeaponWalkspeed,Version, Firemode1, Firemode1Firerate, Firemode1Special, Firemode1Pellets, Firemode2, Firemode2Firerate, Firemode2Special, Firemode2Pellets)
                                                VALUES ('{0}', '{1}', {2},{3},
                                                        {4},{5},{6},{7},
                                                        {8},{9},{10},{11},
                                                        {12},{13},{14},{15},
                                                        {16},{17},'{18}',
                                                        '{19}',{20},'{21}',{22},
                                                        '{23}',{24},'{25}',{26});",
                                                        k.ConversionName, k.Caliber, k.AmmoCapacity, k.MagazineCapacity,
                                                        k.CarriedAttributes.HeadMultiplier, k.CarriedAttributes.TorsoMultiplier, k.CarriedAttributes.LimbMultiplier, k.RangedAttributes.Range1Damage,
                                                        k.RangedAttributes.Range2Damage, k.RangedAttributes.Range1, k.RangedAttributes.Range2, k.MuzzleVelocity,
                                                        k.Penetration, k.Suppression, k.ReloadSpeed, k.EmptyReloadSpeed,
                                                        k.AimingWalkspeed, k.CarriedAttributes.WalkSpeed, Global.VERSION,
                                                        w.Mode, w.Firerate,w.SpecialMode, w.Pellets,
                                                        u.Mode, u.Firerate, u.SpecialMode, u.Pellets, Global.DBVERSION);
                                            break;
                                        case 3:
                                            FireMode x = k.FireModes.getFireModes()[0];
                                            FireMode y = k.FireModes.getFireModes()[1];
                                            FireMode z = k.FireModes.getFireModes()[2];
                                            command.CommandText = String.Format(@"INSERT INTO ConversionData{31} (ConversionName, Caliber, ReserveAmmoCapacity, MagazineCapacity, HeadMultiplier, TorsoMultiplier, LimbMultiplier, Damage1, Damage2, Damage1Range, Damage2Range, MuzzleVelocity, PenetrationDepth, Suppression, ReloadTime, EmptyReloadTime, AimingWalkspeed, WeaponWalkspeed,Version, Firemode1, Firemode1Firerate, Firemode1Special, Firemode1Pellets, Firemode2, Firemode2Firerate, Firemode2Special, Firemode2Pellets, Firemode3, Firemode3Firerate, Firemode3Special, Firemode3Pellets)
                                                VALUES ('{0}', '{1}', {2},{3},
                                                        {4},{5},{6},{7},
                                                        {8},{9},{10},{11},
                                                        {12},{13},{14},{15},
                                                        {16},{17},'{18}',
                                                        '{19}',{20},'{21}',{22},
                                                        '{23}',{24},'{25}',{26},
                                                        '{27}',{28},'{29}',{30});",
                                                        k.ConversionName, k.Caliber, k.AmmoCapacity, k.MagazineCapacity,
                                                        k.CarriedAttributes.HeadMultiplier, k.CarriedAttributes.TorsoMultiplier, k.CarriedAttributes.LimbMultiplier, k.RangedAttributes.Range1Damage,
                                                        k.RangedAttributes.Range2Damage, k.RangedAttributes.Range1, k.RangedAttributes.Range2, k.MuzzleVelocity,
                                                        k.Penetration, k.Suppression, k.ReloadSpeed, k.EmptyReloadSpeed,
                                                        k.AimingWalkspeed, k.CarriedAttributes.WalkSpeed, Global.VERSION,
                                                        x.Mode,x.Firerate,x.SpecialMode, x.Pellets,
                                                        y.Mode,y.Firerate, y.SpecialMode, y.Pellets,
                                                        z.Mode,z.Firerate, z.SpecialMode, z.Pellets, Global.DBVERSION);
                                            break;
                                    }
                                    command.ExecuteReader();
                                    conn.Close();

                                }
                            }
                        }
                        using (var conn = new SQLiteConnection(connectionString.ConnectionString))
                        {
                            using (var command = conn.CreateCommand())
                            {



                                conn.Open();

                                string commandStringLine1p1 = "INSERT INTO GunData"+Global.DBVERSION+"(DefaultName,HasRank,Rank,Version,Category";
                                string commandStringLine2p1 = String.Format(")VALUES ('{0}','{1}',{2},'{3}','{4}'",
                                    g.Name, g.HasRank ? "True" : "False", g.Rank, Global.VERSION, CategoryNames[o]);
                                string commandStringLine1p2 = "";
                                string commandStringLine2p2 = ""; 
                                for (int i = 1; i < convForeignHooks.Count + 1; i++)
                                {
                                    commandStringLine1p2 += (",Conversion" + i.ToString());
                                    commandStringLine2p2 += (",'" + convForeignHooks[i - 1]+"'");
                                }

                                command.CommandText = commandStringLine1p1 + commandStringLine1p2 + commandStringLine2p1 + commandStringLine2p2 + ");";
                                command.ExecuteReader();
                                conn.Close();

                            }
                        }

                    }
                }
            }

        }

        /*
        public void GetAllSQLWeaponRecords()
        {
            List<string> tableNames = new();
            using (var conn = new SQLiteConnection(connectionString.ConnectionString))
            {
                using (var command = conn.CreateCommand())
                {
                    conn.Open();
                    command.CommandText = "SELECT * FROM GunData100 FULL JOIN ConversionData100 ON ConversionData100.ConversionName = GunData100.Conversion1;";
                    using(var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string line = "";
                            //iterate through columns (31) 
                            for(int i = 0; i < 31 + 14; i++)
                            {
                                line += (reader.GetValue(i).ToString() + "\t");
                            }
                            Console.WriteLine(line);
                        }
                    }
                }
            }
        }*/

        public enum FiremodeSelect
        {
            Firemode1,
            Firemode2,
            Firemode3
        }

        public enum ConversionsSelect
        {
            C1 = 1,C2,C3,C4,C5,C6,C7,C8,C9,C10
        }

        public Class GetSQLClass(FileReading.Classes classOption, string VERSIONSTRING)
        {
            string className = "";
            switch (classOption)
            {
                case FileReading.Classes.Assault: className = "Assault"; break;
                case FileReading.Classes.Scout: className = "Scout"; break;
                case FileReading.Classes.Support: className = "Support"; break;
                case FileReading.Classes.Recon: className = "Recon"; break;
                case FileReading.Classes.Secondary: className = "Secondary"; break;
                case FileReading.Classes.Grenades: className = "Grenades"; break;
                case FileReading.Classes.Melees: className = "Melees"; break;
            }
            Class cl = new(null, className);
            string v = VERSIONSTRING;
            
            switch (classOption)
            {
                case Classes.Assault:
                    {

                        cl.addCategory(GetSQLGunCategoryRecords(BuildOptions.ARS, v));
                        cl.addCategory(GetSQLGunCategoryRecords(BuildOptions.BRS, v));
                        cl.addCategory(GetSQLGunCategoryRecords(BuildOptions.CAS, v));
                        cl.addCategory(GetSQLGunCategoryRecords(BuildOptions.SHS, v));
                        break;
                    }
                case Classes.Scout:
                    {

                        cl.addCategory(GetSQLGunCategoryRecords(BuildOptions.PDWS, v));
                        cl.addCategory(GetSQLGunCategoryRecords(BuildOptions.DMRS, v));
                        cl.addCategory(GetSQLGunCategoryRecords(BuildOptions.CAS, v));
                        cl.addCategory(GetSQLGunCategoryRecords(BuildOptions.SHS, v));
                        break;
                    }
                case Classes.Support:
                    {
                        cl.addCategory(GetSQLGunCategoryRecords(BuildOptions.LMGS, v));
                        cl.addCategory(GetSQLGunCategoryRecords(BuildOptions.BRS, v));
                        cl.addCategory(GetSQLGunCategoryRecords(BuildOptions.CAS, v));
                        cl.addCategory(GetSQLGunCategoryRecords(BuildOptions.SHS, v));
                        break;
                    }
                case Classes.Recon:
                    {

                        cl.addCategory(GetSQLGunCategoryRecords(BuildOptions.SRS, v));
                        cl.addCategory(GetSQLGunCategoryRecords(BuildOptions.DMRS, v));
                        cl.addCategory(GetSQLGunCategoryRecords(BuildOptions.BRS, v));
                        cl.addCategory(GetSQLGunCategoryRecords(BuildOptions.CAS, v));
                        break;
                    }
                case Classes.Secondary:
                    {

                        cl.addCategory(GetSQLGunCategoryRecords(BuildOptions.PS, v));
                        cl.addCategory(GetSQLGunCategoryRecords(BuildOptions.MPS, v));
                        cl.addCategory(GetSQLGunCategoryRecords(BuildOptions.RES, v));
                        cl.addCategory(GetSQLGunCategoryRecords(BuildOptions.OTH, v));
                        break;
                    }
                case Classes.Melees:
                    {

                        cl.addCategory(GetSQLMeleeCategoryRecords(BuildOptions.OHBE, v));
                        cl.addCategory(GetSQLMeleeCategoryRecords(BuildOptions.OHBT, v));
                        cl.addCategory(GetSQLMeleeCategoryRecords(BuildOptions.THBE, v));
                        cl.addCategory(GetSQLMeleeCategoryRecords(BuildOptions.THBT, v));
                        break;
                    }
                case Classes.Grenades:
                    {
                        cl.addCategory(GetSQLGrenadeCategoryRecords(BuildOptions.FGS, v));
                        cl.addCategory(GetSQLGrenadeCategoryRecords(BuildOptions.HEGS, v));
                        cl.addCategory(GetSQLGrenadeCategoryRecords(BuildOptions.IGS, v));
                        break;
                    }
            }
                        
            return cl;
        }
        
        //match the 0
        public Category GetSQLGunCategoryRecords(FileReading.BuildOptions option, string VERSIONSTRING)
        {
            if (option != BuildOptions.OHBT && option != BuildOptions.OHBE && option != BuildOptions.THBT && option != BuildOptions.THBE && option != BuildOptions.FGS && option != BuildOptions.HEGS && option != BuildOptions.IGS)
            {
                string v = VERSIONSTRING;
                Category result = new(null, CategoryNames[option]);
                using (var conn = new SQLiteConnection(connectionString.ConnectionString))
                {
                    using (var command = conn.CreateCommand())
                    {
                        //DefaultName	HasRank	Rank	Version	Category	Conversion1	Conversion2	Conversion3	Conversion4	Conversion5	Conversion6	Conversion7	Conversion8	Conversion9	Conversion10	ConversionName	Caliber	ReserveAmmoCapacity	MagazineCapacity	HeadMultiplier	TorsoMultiplier	LimbMultiplier	Damage1	Damage2	Damage1Range	Damage2Range	MuzzleVelocity	PenetrationDepth	Suppression	ReloadTime	EmptyReloadTime	AimingWalkspeed	WeaponWalkspeed	Version1	Firemode1	Firemode1Firerate	Firemode1Special	Firemode1Pellets	Firemode2	Firemode2Firerate	Firemode2Special	Firemode2Pellets	Firemode3	Firemode3Firerate	Firemode3Special	Firemode3Pellets
                        //1dn 2hr 3r 4v 5c 6c1 7c2 8c3 9c4 10c5 11c6 12c7 13c8 14c9 15c10
                        //16cn 17c 18rac 19mc 20hm 21tm 22lm 23d1 24d2 25d1r 26d2r 27mv
                        //28pd 29s 30rt 31ert 32aw 33ww 34v1
                        //35f1 36f1f 37f1s 38f1p
                        //39f1 40f1f 41f1s 42f1p
                        //43f1 44f1f 45f1s 46f1p


                        conn.Open();
                        command.CommandText = "SELECT * FROM GunData" + v + " WHERE GunData" + v + ".Category = '" + CategoryNames[option] + "';"; //+ " LEFT JOIN ConversionData" + v + " ON ConversionData" + v + ".ConversionName = GunData" + v + ".Conversion"+cs+";";
                                                                                                                                                   //remember that there are two ways: search by category and search by name listed in weaponstrings, laptop will die soon

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name = reader.GetString(0);
                                List<string> conversions = new();
                                Action<int> adder = (i) => { if (!reader.IsDBNull(i)) conversions.Add(reader.GetString(i)); };
                                adder(5);
                                adder(6);
                                adder(7);
                                adder(8);
                                adder(9);
                                adder(10);
                                adder(11);
                                adder(12);
                                adder(13);
                                adder(14);
                                result.addWeapon(new Gun(name, reader.GetString(1).Contains("True", StringComparison.CurrentCultureIgnoreCase), reader.GetInt32(2), GetSQLConversionRecord(name, conversions, v)));
                            }
                        }
                    }
                }
                return result;
            }
            else
            {

                throw new ArgumentException("option cannot be a non-gun type");
            }
        }

        public Category GetSQLMeleeCategoryRecords(FileReading.BuildOptions option, string VERSIONSTRING)
        {
            if (option != BuildOptions.OHBT && option != BuildOptions.OHBE && option != BuildOptions.THBT && option != BuildOptions.THBE)
            {
                throw new ArgumentException("option cannot be a non-melee type");
            }
            Category result = new(null, CategoryNames[option]);
            string v = VERSIONSTRING;
            int meleeID = 0;
            using (var conn = new SQLiteConnection(connectionString.ConnectionString))
            {
                using (var command = conn.CreateCommand())
                {
                    conn.Open();
                    command.CommandText = "SELECT * FROM MeleeData" + v + " WHERE MeleeData" + v + ".Category = '" + CategoryNames[option] + "';";
                    using(var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader.GetString(0);
                            List<string> conversions = new();

                            result.addMelee(new Melee(name, reader.GetString(1).Contains("True", StringComparison.CurrentCultureIgnoreCase), reader.GetInt32(2),reader.GetDouble(4),reader.GetDouble(5),reader.GetDouble(3),new(reader.GetDouble(8), reader.GetDouble(7), reader.GetDouble(6), reader.GetDouble(9))),meleeID);
                            meleeID++;
                        }
                    }

                }

            }
            return result;
        }

        public Category GetSQLGrenadeCategoryRecords(FileReading.BuildOptions option, string VERSIONSTRING)
        {
            if(option != BuildOptions.FGS && option != BuildOptions.HEGS && option != BuildOptions.IGS)
            {
                throw new ArgumentException("option cannot be a non-grenade type");
            }
            Category result = new(null, CategoryNames[option]);
            string v = VERSIONSTRING;
            int meleeID = 0;
            using (var conn = new SQLiteConnection(connectionString.ConnectionString))
            {
                using (var command = conn.CreateCommand())
                {
                    conn.Open();
                    command.CommandText = "SELECT * FROM GrenadeData" + v + " WHERE GrenadeData" + v + ".Category = '" + CategoryNames[option] + "';";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader.GetString(0);
                            List<string> conversions = new();

                            result.addWeapon(new Grenade(name, reader.GetString(1).Contains("True", StringComparison.CurrentCultureIgnoreCase), reader.GetInt32(2), reader.GetDouble(3) != 0, reader.GetDouble(3),reader.GetString(4) != "", reader.GetString(4), reader.GetInt32(10),reader.GetDouble(5), reader.GetDouble(6),reader.GetDouble(7)));
                            meleeID++;
                        }
                    }

                }

            }
            return result;
        }

        public ConversionList GetSQLConversionRecord(string GunName,List<string> ConversionNames, string VERSIONSTRING)
        {
            string v = VERSIONSTRING;
            ConversionList list = new(new(false, false, new("blank", false, 1, new Conversion("blank", 10, 10, new() { "a100sh8"}, new(1,1,1,1), new(1,1,1,1),1, 100, 2,2,2,2),false, false))); //to init, will get overwritten
            foreach (string s in ConversionNames)
            {
                using (var conn = new SQLiteConnection(connectionString.ConnectionString))
                {
                    //1cn 2c 3rac 4mc 5hm 6tm 7lm 8d1 9d2 10d1r 11d2r 12mv
                    //13pd 14s 15rt 16ert 17aw 18ww 19v1
                    //20f1 21f1f 22f1s 23f1px
                    //24f1 25f1f 26f1s 27f1p
                    //28f1 29f1f 30f1s 31f1p
                    using (var command = conn.CreateCommand())
                    {
                        conn.Open();
                        command.CommandText = $"SELECT * FROM ConversionData" + v + " WHERE ConversionName LIKE '"+s+"';";
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                if (reader.GetString(0).Contains(GunName) && GunName.Contains(reader.GetString(0)))
                                {
                                    List<FireMode> fconv = new();
                                    if (!reader.IsDBNull(19)){
                                        fconv.Add(new(reader.GetDouble(20), reader.GetString(19), (reader.GetString(21).Contains("burst", StringComparison.CurrentCultureIgnoreCase)), reader.GetString(21), (!reader.IsDBNull(21) && reader.GetString(21) != ""), reader.GetString(21), reader.GetInt32(22)));
                                    }
                                    if (!reader.IsDBNull(23))
                                    {
                                        fconv.Add(new FireMode(reader.GetDouble(24), reader.GetString(23), (reader.GetString(25).Contains("burst", StringComparison.CurrentCultureIgnoreCase)), reader.GetString(25), (!reader.IsDBNull(25) && reader.GetString(25) != ""), reader.GetString(25), reader.GetInt32(26)));
                                    }
                                    if (!reader.IsDBNull(27))
                                    {
                                        fconv.Add(new FireMode(reader.GetDouble(28), reader.GetString(27), (reader.GetString(29).Contains("burst", StringComparison.CurrentCultureIgnoreCase)), reader.GetString(29), (!reader.IsDBNull(29) && reader.GetString(29) != ""), reader.GetString(29), reader.GetInt32(30)));
                                    }
                                    list.DefaultConversion = 
                                        new(GunName, "", reader.GetString(0), null, reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), new FireModeList(fconv), new Carried(reader.GetDouble(4), reader.GetDouble(5), reader.GetDouble(6), reader.GetDouble(17)), new Ranged(reader.GetDouble(9), reader.GetDouble(10), reader.GetDouble(7), reader.GetDouble(8)), reader.GetDouble(12), reader.GetDouble(11), reader.GetDouble(14), reader.GetDouble(15), reader.GetDouble(13), reader.GetDouble(16));

                                    
                                }
                                else
                                {
                                    List<FireMode> fconv = new();
                                    if (!reader.IsDBNull(19))
                                    {
                                        fconv.Add(new(reader.GetDouble(20), reader.GetString(19), (reader.GetString(21).Contains("burst", StringComparison.CurrentCultureIgnoreCase)), reader.GetString(21), (!reader.IsDBNull(21) && reader.GetString(21) != ""), reader.GetString(21), reader.GetInt32(22)));
                                    }
                                    if (!reader.IsDBNull(23))
                                    {
                                        fconv.Add(new FireMode(reader.GetDouble(24), reader.GetString(23), (reader.GetString(25).Contains("burst", StringComparison.CurrentCultureIgnoreCase)), reader.GetString(25), (!reader.IsDBNull(25) && reader.GetString(25) != ""), reader.GetString(25), reader.GetInt32(26)));
                                    }
                                    if (!reader.IsDBNull(27))
                                    {
                                        fconv.Add(new FireMode(reader.GetDouble(28), reader.GetString(27), (reader.GetString(29).Contains("burst", StringComparison.CurrentCultureIgnoreCase)), reader.GetString(29), (!reader.IsDBNull(29) && reader.GetString(29) != ""), reader.GetString(29), reader.GetInt32(30)));
                                    }
                                    list.addConversion(
                                        new(GunName, "", reader.GetString(0), null, reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), new FireModeList(fconv), new Carried(reader.GetDouble(4), reader.GetDouble(5), reader.GetDouble(6), reader.GetDouble(17)), new Ranged(reader.GetDouble(9), reader.GetDouble(10), reader.GetDouble(7), reader.GetDouble(8)), reader.GetDouble(12), reader.GetDouble(11), reader.GetDouble(14), reader.GetDouble(15), reader.GetDouble(13), reader.GetDouble(16)));
                                }
                            }
                        }
                        conn.Close();
                    }
                }
            }
            return list;
        }
    }

}
