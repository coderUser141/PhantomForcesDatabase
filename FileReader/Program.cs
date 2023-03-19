// See https://aka.ms/new-console-template for more information
using WeaponClasses;
using System.Diagnostics;


Console.WriteLine("Hello, World!");
Console.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff"));
//string? filepath = weaponName.ToUpper() + "1.png";
//string? filepath2 = weaponName.ToUpper() + "2.png";
//FileReadList fileReadList = new(new List<string> { filepath });
int choice = (Convert.ToInt32(Console.ReadLine()) < 13 && Convert.ToInt32(Console.ReadLine()) > -1 )? Convert.ToInt32(Console.ReadLine()) : 0;
FileReadList n = new()
Console.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff"));



//string walkSpeed;

/*
string headMultipler, torsoMultiplier, limbMultiplier, walkSpeed, muzzleVelocity, penetration, aimingWalkspeed;
List<string> suppression, ammoType , damage, damageRange, ammoCapacity, firemodes, firerate;
try
{
    ammoType = new List<string>(bruh.findStatisticInFile(filepath, bruh.statisticInFile.AmmoType));
}catch(ArgumentException)
{
    ammoType = new List<string>(bruh.findStatisticInFile(filepath2, bruh.statisticInFile.AmmoType));

}

try
{
    damage = new List<string>(bruh.findStatisticInFile(filepath, bruh.statisticInFile.Damage));
}
catch (ArgumentException)
{
    damage = new List<string>(bruh.findStatisticInFile(filepath2, bruh.statisticInFile.Damage));

}

try
{
    ammoCapacity = new List<string>(bruh.findStatisticInFile(filepath, bruh.statisticInFile.AmmoMagCapacity));
}
catch (ArgumentException)
{

    ammoCapacity = new List<string>(bruh.findStatisticInFile(filepath2, bruh.statisticInFile.AmmoMagCapacity));
}

try
{
    damageRange = new List<string>(bruh.findStatisticInFile(filepath, bruh.statisticInFile.DamageRange));
}
catch (ArgumentException)
{
    damageRange = new List<string>(bruh.findStatisticInFile(filepath2, bruh.statisticInFile.DamageRange));
}

try
{
    firemodes = new List<string>(bruh.findStatisticInFile(filepath, bruh.statisticInFile.Firemodes));
}
catch (ArgumentException)
{
    firemodes = new List<string>(bruh.findStatisticInFile(filepath2, bruh.statisticInFile.Firemodes));
}

try
{
    firerate = new List<string>(bruh.findStatisticInFile(filepath, bruh.statisticInFile.Firerate));
}
catch (ArgumentException)
{
    firerate = new List<string>(bruh.findStatisticInFile(filepath2, bruh.statisticInFile.Firerate));
}

try
{
    headMultipler = bruh.findStatisticInFile(filepath, "head", "multiplier");
}
catch (ArgumentException)
{
    headMultipler = bruh.findStatisticInFile(filepath2, "head", "multiplier");
}

try
{
    torsoMultiplier = bruh.findStatisticInFile(filepath, "torso", "multiplier");
}
catch (ArgumentException)
{
    torsoMultiplier = bruh.findStatisticInFile(filepath2, "torso", "multiplier");
}

try
{
    limbMultiplier = bruh.findStatisticInFile(filepath, "limb", "multiplier");
}
catch (ArgumentException)
{
    limbMultiplier = bruh.findStatisticInFile(filepath2, "limb", "multiplier");
}

try
{
    walkSpeed = bruh.findStatisticInFile(filepath, "weapon", "walkspeed");
}
catch (ArgumentException)
{
    walkSpeed = bruh.findStatisticInFile(filepath2, "weapon", "walkspeed");
}

try
{
    muzzleVelocity = bruh.findStatisticInFile(filepath, "muzzle", "velocity");
}
catch (ArgumentException)
{
    muzzleVelocity = bruh.findStatisticInFile(filepath2, "muzzle", "velocity");
}

try
{
    penetration = bruh.findStatisticInFile(filepath, "penetration", "depth");
}
catch (ArgumentException)
{
    penetration = bruh.findStatisticInFile(filepath2, "penetration", "depth");
}

try
{
    suppression = new List<string>(bruh.findStatisticInFile(filepath, "suppression"));
}
catch (ArgumentException)
{
    suppression = new List<string>(bruh.findStatisticInFile(filepath2, "suppression"));
}


try
{
    aimingWalkspeed = bruh.findStatisticInFile(filepath, "aiming", "walkspeed");
}
catch (ArgumentException)
{
    aimingWalkspeed = bruh.findStatisticInFile(filepath2, "aiming", "walkspeed");
}


//suppression.Union


Console.WriteLine("head multi " + headMultipler);
Console.WriteLine("torso multi " + torsoMultiplier);
Console.WriteLine("limb multi " + limbMultiplier);
Console.WriteLine("walkspeed " + walkSpeed);
Console.WriteLine("mv " + muzzleVelocity);
Console.WriteLine("pen " + penetration);
Console.WriteLine("aim walkspeed " + aimingWalkspeed);
foreach (string h in suppression)
{
    Console.WriteLine("suppression " + h);
}
foreach (string i in ammoType)
{
    Console.WriteLine("ammo " + i);
}
foreach (string j in ammoCapacity)
{
    Console.WriteLine("ammo/mag " + j);
}
foreach (string k in damageRange)
{
    Console.WriteLine("dmg range " + k);
}
foreach (string l in damage)
{
    Console.WriteLine("dmg " + l);
}
foreach (string m in firemodes)
{
    Console.WriteLine("firemodes " + m);
}
foreach (string n in firerate)
{
    Console.WriteLine("firerate " + n);
}*/

public class FilePathsList
{
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
        All
    }

    private List<string> weaponNames;

    public FilePathsList(BuildOptions buildOptions = BuildOptions.None) {
        weaponNames = new();
        switch (buildOptions) {
            case BuildOptions.None:
                {

                    break;
                }
                case BuildOptions.AssaultRifles:
                {
                    addWeapon("ak12");
                    addWeapon("an94");
                    addWeapon("as-val");
                    addWeapon("scar-l");
                    addWeapon("aug-a1");
                    addWeapon("m16a4");
                    addWeapon("g36");
                    addWeapon("m16a1");
                    addWeapon("m16a3");
                    addWeapon("type-20");
                    addWeapon("aug-a2");
                    addWeapon("k2");
                    addWeapon("famas-f1");
                    addWeapon("ak47");
                    addWeapon("aug-a3");
                    addWeapon("l85a2");
                    addWeapon("hk416");
                    addWeapon("ak74");
                    addWeapon("akm");
                    addWeapon("ak103");
                    addWeapon("tar-21");
                    addWeapon("type-88");
                    addWeapon("m231");
                    addWeapon("c7a2");
                    addWeapon("stg-44");
                    addWeapon("g11k2");
                    break;
                }
            case BuildOptions.PersonalDefenseWeapons:
                {
                    addWeapon("mp5k");
                    addWeapon("ump45");
                    addWeapon("g36c");
                    addWeapon("mp7");
                    addWeapon("mac10");
                    addWeapon("p90");
                    addWeapon("colt-mars");
                    addWeapon("mp5");
                    addWeapon("colt-smg-633");
                    addWeapon("l2a3");
                    addWeapon("mp5sd");
                    addWeapon("mp10");
                    addWeapon("m3a1");
                    addWeapon("mp510");
                    addWeapon("uzi");
                    addWeapon("aug-a3-para-xs");
                    addWeapon("k7");
                    addWeapon("ak74u");
                    addWeapon("ppsh-41");
                    addWeapon("fal-para-shorty");
                    addWeapon("kriss-vector");
                    addWeapon("pp-19-bizon");
                    addWeapon("mp40");
                    addWeapon("x95-smg");
                    addWeapon("tommy-gun");
                    addWeapon("rama-1130");
                    break;
                }
            case BuildOptions.LightMachineGuns:
                {
                    addWeapon("colt-lmg");
                    addWeapon("m60");
                    addWeapon("aug-hbar");
                    addWeapon("mg36");
                    addWeapon("rpk12");
                    addWeapon("l86-lsw");
                    addWeapon("rpk");
                    addWeapon("hk21e");
                    addWeapon("hamr-iar");
                    addWeapon("rpk74");
                    addWeapon("mg3kws");
                    break;
                }
            case BuildOptions.SniperRifles:
                {
                    addWeapon("intervention");
                    addWeapon("model-700");
                    addWeapon("dragunov-svu");
                    addWeapon("aws");
                    addWeapon("bfg-50");
                    addWeapon("awm");
                    addWeapon("trg-42");
                    addWeapon("mosin-nagant");
                    addWeapon("dragunov-svds");
                    addWeapon("m1903");
                    addWeapon("k14");
                    addWeapon("hecate-ii");
                    addWeapon("ft300");
                    addWeapon("m107");
                    addWeapon("steyr-scout");
                    addWeapon("wa2000");
                    addWeapon("ntw-20");
                    break;
                }
            case BuildOptions.DesignatedMarksmanRifles:
                {
                    addWeapon("mk11");
                    addWeapon("sks");
                    addWeapon("sl-8");
                    addWeapon("vss-vintorez");
                    addWeapon("msg90");
                    addWeapon("m21");
                    addWeapon("beowulf-tcr");
                    addWeapon("sa58-spr");
                    addWeapon("scar-ssr");
                    break;
                }
            case BuildOptions.Carbines:
                {
                    addWeapon("m4a1");
                    addWeapon("g36k");
                    addWeapon("m4");
                    addWeapon("l22");
                    addWeapon("scar-pdw");
                    addWeapon("aku12");
                    addWeapon("groza-1");
                    addWeapon("ots-126");
                    addWeapon("ak12c");
                    addWeapon("honey-badger");
                    addWeapon("k1a");
                    addWeapon("sr-3m");
                    addWeapon("groza-4");
                    addWeapon("mc51");
                    addWeapon("fal-5063-para");
                    addWeapon("1858-carbine");
                    addWeapon("ak105");
                    addWeapon("jury");
                    addWeapon("kac-srr");
                    addWeapon("gyrojet-carbine");
                    addWeapon("c8a2");
                    addWeapon("x95r");
                    addWeapon("hk51b");
                    addWeapon("can-cannon");
                    break;
                }
            case BuildOptions.BattleRifles:
                {
                    addWeapon("m14");
                    addWeapon("beowulf-ecr");
                    addWeapon("scar-h");
                    addWeapon("ak12br");
                    addWeapon("g3a3");
                    addWeapon("ag-3");
                    addWeapon("hk417");
                    addWeapon("henry-45-70");
                    addWeapon("fal-5000");
                    break;
                }
            case BuildOptions.Shotguns:
                {
                    addWeapon("ksg-12");
                    addWeapon("model-870");
                    addWeapon("dbv12");
                    addWeapon("ks-23m");
                    addWeapon("saiga-12");
                    addWeapon("stevens-db");
                    addWeapon("e-gun");
                    addWeapon("aa-12");
                    addWeapon("spas-12");
                    addWeapon("dt11");
                    addWeapon("usas-12");
                    break;
                }
            case BuildOptions.Pistols:
                {
                    addWeapon("g17");
                    addWeapon("m9");
                    addWeapon("m1911a1");
                    addWeapon("desert-eagle-l5");
                    addWeapon("g21");
                    addWeapon("g23");
                    addWeapon("m45a1");
                    addWeapon("g40");
                    addWeapon("kg-99");
                    addWeapon("g50");
                    addWeapon("five-seven");
                    addWeapon("zip-22");
                    addWeapon("gi-m1");
                    addWeapon("hardballer");
                    addWeapon("izhevsk-pb");
                    addWeapon("makarov-pm");
                    addWeapon("gb-22");
                    addWeapon("desert-eagle-xix");
                    addWeapon("automag-iii");
                    addWeapon("gyrojet-mark-i");
                    addWeapon("gsp");
                    addWeapon("grizzly");
                    addWeapon("m2011");
                    addWeapon("alien");
                    addWeapon("af2011-a1");
                    break;
                }
            case BuildOptions.MachinePistols:
                {
                    addWeapon("g18c");
                    addWeapon("93r");
                    addWeapon("pp-2000");
                    addWeapon("tec-9");
                    addWeapon("micro-uzi");
                    addWeapon("skorpion-vz61");
                    addWeapon("asmi");
                    addWeapon("mp1911");
                    addWeapon("arm-pistol");
                    break;
                }
            case BuildOptions.Revolvers:
                {
                    addWeapon("mp412-rex");
                    addWeapon("mateba-6");
                    addWeapon("1858-new-army");
                    addWeapon("redhawk-44");
                    addWeapon("judge");
                    addWeapon("executioner");
                    break;
                }
            case BuildOptions.Other:
                {
                    addWeapon("super-shorty");
                    addWeapon("sfg-50");
                    addWeapon("m79-thumper");
                    addWeapon("advanced-coilgun");
                    addWeapon("sawed-off");
                    addWeapon("saiga-12u");
                    addWeapon("obrez");
                    addWeapon("sass-308");
                    break;
                }
            case BuildOptions.All:
                {
                    addWeapon("ak12");
                    addWeapon("an94");
                    addWeapon("as-val");
                    addWeapon("scar-l");
                    addWeapon("aug-a1");
                    addWeapon("m16a4");
                    addWeapon("g36");
                    addWeapon("m16a1");
                    addWeapon("m16a3");
                    addWeapon("type-20");
                    addWeapon("aug-a2");
                    addWeapon("k2");
                    addWeapon("famas-f1");
                    addWeapon("ak47");
                    addWeapon("aug-a3");
                    addWeapon("l85a2");
                    addWeapon("hk416");
                    addWeapon("ak74");
                    addWeapon("akm");
                    addWeapon("ak103");
                    addWeapon("tar-21");
                    addWeapon("type-88");
                    addWeapon("m231");
                    addWeapon("c7a2");
                    addWeapon("stg-44");
                    addWeapon("g11k2");
                    addWeapon("mp5k");
                    addWeapon("ump45");
                    addWeapon("g36c");
                    addWeapon("mp7");
                    addWeapon("mac10");
                    addWeapon("p90");
                    addWeapon("colt-mars");
                    addWeapon("mp5");
                    addWeapon("colt-smg-633");
                    addWeapon("l2a3");
                    addWeapon("mp5sd");
                    addWeapon("mp10");
                    addWeapon("m3a1");
                    addWeapon("mp510");
                    addWeapon("uzi");
                    addWeapon("aug-a3-para-xs");
                    addWeapon("k7");
                    addWeapon("ak74u");
                    addWeapon("ppsh-41");
                    addWeapon("fal-para-shorty");
                    addWeapon("kriss-vector");
                    addWeapon("pp-19-bizon");
                    addWeapon("mp40");
                    addWeapon("x95-smg");
                    addWeapon("tommy-gun");
                    addWeapon("rama-1130");
                    addWeapon("colt-lmg");
                    addWeapon("m60");
                    addWeapon("aug-hbar");
                    addWeapon("mg36");
                    addWeapon("rpk12");
                    addWeapon("l86-lsw");
                    addWeapon("rpk");
                    addWeapon("hk21e");
                    addWeapon("hamr-iar");
                    addWeapon("rpk74");
                    addWeapon("mg3kws");
                    addWeapon("intervention");
                    addWeapon("model-700");
                    addWeapon("dragunov-svu");
                    addWeapon("aws");
                    addWeapon("bfg-50");
                    addWeapon("awm");
                    addWeapon("trg-42");
                    addWeapon("mosin-nagant");
                    addWeapon("dragunov-svds");
                    addWeapon("m1903");
                    addWeapon("k14");
                    addWeapon("hecate-ii");
                    addWeapon("ft300");
                    addWeapon("m107");
                    addWeapon("steyr-scout");
                    addWeapon("wa2000");
                    addWeapon("ntw-20");
                    addWeapon("mk11");
                    addWeapon("sks");
                    addWeapon("sl-8");
                    addWeapon("vss-vintorez");
                    addWeapon("msg90");
                    addWeapon("m21");
                    addWeapon("beowulf-tcr");
                    addWeapon("sa58-spr");
                    addWeapon("scar-ssr");
                    addWeapon("m4a1");
                    addWeapon("g36k");
                    addWeapon("m4");
                    addWeapon("l22");
                    addWeapon("scar-pdw");
                    addWeapon("aku12");
                    addWeapon("groza-1");
                    addWeapon("ots-126");
                    addWeapon("ak12c");
                    addWeapon("honey-badger");
                    addWeapon("k1a");
                    addWeapon("sr-3m");
                    addWeapon("groza-4");
                    addWeapon("mc51");
                    addWeapon("fal-5063-para");
                    addWeapon("1858-carbine");
                    addWeapon("ak105");
                    addWeapon("jury");
                    addWeapon("kac-srr");
                    addWeapon("gyrojet-carbine");
                    addWeapon("c8a2");
                    addWeapon("x95r");
                    addWeapon("hk51b");
                    addWeapon("can-cannon");
                    addWeapon("m14");
                    addWeapon("beowulf-ecr");
                    addWeapon("scar-h");
                    addWeapon("ak12br");
                    addWeapon("g3a3");
                    addWeapon("ag-3");
                    addWeapon("hk417");
                    addWeapon("henry-45-70");
                    addWeapon("fal-5000");
                    addWeapon("ksg-12");
                    addWeapon("model-870");
                    addWeapon("dbv12");
                    addWeapon("ks-23m");
                    addWeapon("saiga-12");
                    addWeapon("stevens-db");
                    addWeapon("e-gun");
                    addWeapon("aa-12");
                    addWeapon("spas-12");
                    addWeapon("dt11");
                    addWeapon("usas-12");
                    addWeapon("g17");
                    addWeapon("m9");
                    addWeapon("m1911a1");
                    addWeapon("desert-eagle-l5");
                    addWeapon("g21");
                    addWeapon("g23");
                    addWeapon("m45a1");
                    addWeapon("g40");
                    addWeapon("kg-99");
                    addWeapon("g50");
                    addWeapon("five-seven");
                    addWeapon("zip-22");
                    addWeapon("gi-m1");
                    addWeapon("hardballer");
                    addWeapon("izhevsk-pb");
                    addWeapon("makarov-pm");
                    addWeapon("gb-22");
                    addWeapon("desert-eagle-xix");
                    addWeapon("automag-iii");
                    addWeapon("gyrojet-mark-i");
                    addWeapon("gsp");
                    addWeapon("grizzly");
                    addWeapon("m2011");
                    addWeapon("alien");
                    addWeapon("af2011-a1");
                    addWeapon("g18c");
                    addWeapon("93r");
                    addWeapon("pp-2000");
                    addWeapon("tec-9");
                    addWeapon("micro-uzi");
                    addWeapon("skorpion-vz61");
                    addWeapon("asmi");
                    addWeapon("mp1911");
                    addWeapon("arm-pistol");
                    addWeapon("mp412-rex");
                    addWeapon("mateba-6");
                    addWeapon("1858-new-army");
                    addWeapon("redhawk-44");
                    addWeapon("judge");
                    addWeapon("executioner");
                    addWeapon("super-shorty");
                    addWeapon("sfg-50");
                    addWeapon("m79-thumper");
                    addWeapon("advanced-coilgun");
                    addWeapon("sawed-off");
                    addWeapon("saiga-12u");
                    addWeapon("obrez");
                    addWeapon("sass-308");
                    break;
                }
    }
        filepaths();
    }

    public void filepaths()
    {
        List<string> br = new();
        foreach(string str in weaponNames)
        {
            br.Add(str + "1.png");
            br.Add(str + "2.png");
        }
        FileReadList list = new(br);
    }

    public void addWeapon(string weaponName)
    {
        weaponNames.Add(weaponName.ToUpper());
    }

}

public class FileRead
{
    //constructor
    public FileRead(string filepath)
    {
        this.filepath = filepath;
        this.fileOutput = callPythonScript(@"C:\Users\peter\source\repos\Phantom Forces Database\ImageParser\dist\ImageParser.exe", /*"""C:\Users\peter\source\repos\Phantom Forces Database\ImageParser\"""+*/@"..\..\..\..\ImageParser\Weapons\"+filepath, @"..\..\..\..\ImageParser\");
    }

    private string filepath;
    private string fileOutput;

    //reads
    public string callPythonScript(string cmd, string path, string tessbindata)
    {

        Console.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff") + " started");
        ProcessStartInfo start = new(cmd, string.Format("{0} {1}", path, tessbindata));
        //start.FileName = cmd;
        //start.Arguments = string.Format("{ 0}", path);

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
                        Console.Write(result);
                        File.AppendAllText("output.txt", result);
                        File.AppendAllText("output.txt",DateTime.Now.ToString("HH:mm:ss:fff") + " finished");
                        return result;
                    }
                }
            }
        }
        return "failed";
        //Process.Start(start);
    }

    public string Filepath
    {
        get { return filepath; }
        set { filepath = value; }
    }


    public string FileOutput
    {
        get { return fileOutput; }
        set { fileOutput = value; }
    }


}

//maybe simplify?
public class FileReadList
{

    private Dictionary<int, FileRead> files;


    public FileReadList(List<string> filepaths)
    {
        files = new Dictionary<int, FileRead>();
        for(int i = 0; i < filepaths.Count; i++)
        {
            files.Add(i, new FileRead(filepaths[i]));
            File.AppendAllText("output__" + filepaths[i] + ".txt", files[i].FileOutput);
        }
    }

    public Dictionary<int, FileRead> Files
    {
        get { return files; }
        set { files = value; }
    }

}

public class FileParsing
{
    public enum statisticInFile
    {
        DamageRange,
        AmmoMagCapacity,
        Damage,
        AmmoType,
        Firerate,
        Firemodes
    }


    private static List<int> indexFinder(string filetext, string word)
    {

        List<int> output = new();
        for(; filetext.Contains(word, StringComparison.CurrentCultureIgnoreCase); )
        {
            output.Add(filetext.IndexOf(word, StringComparison.CurrentCultureIgnoreCase));
            try
            {
                filetext = filetext.Remove(filetext.IndexOf(word,StringComparison.CurrentCultureIgnoreCase), word.Length);
            }
            catch (ArgumentOutOfRangeException) { break; }
        }
        return output;
    }

    public static List<string> findStatisticInFile(string filepath, statisticInFile statisticInFile)
    {
        if (filepath == null) throw new ArgumentNullException(nameof(filepath));
        if (!File.Exists(filepath)) throw new FileNotFoundException("File not found.", nameof(filepath));
        string filetext = File.ReadAllText(filepath);

        //Console.WriteLine(filetext); //remove

        string inputWord1 = "";
        string inputWord2 = "";

        //checks the mode of what to find
        switch (statisticInFile)
        {
            case statisticInFile.DamageRange:
                {
                    inputWord1 = "damage";
                    inputWord2 = "range";
                    break;
                }
            case statisticInFile.AmmoMagCapacity:
                {
                    inputWord1 = "ammo";
                    inputWord2 = "capacity";
                    break;
                }
            case statisticInFile.Damage:
                {
                    inputWord1 = "damage";
                    inputWord2 = "range";
                    break;
                }
            case statisticInFile.AmmoType:
                {
                    inputWord1 = "ammo";
                    inputWord2 = "type";
                    break;
                }
            case statisticInFile.Firemodes:
                {
                    inputWord1 = "fire";
                    inputWord2 = "modes";
                    break;
                }
            case statisticInFile.Firerate:
                {
                    inputWord1 = "fire";
                    inputWord2 = "rate";
                    break;
                }
        }
        List<string> strings = new();

        //detects if the words are actually in the file
        if (filetext.Contains(inputWord1, StringComparison.CurrentCultureIgnoreCase) && filetext.Contains(inputWord2, StringComparison.CurrentCultureIgnoreCase))
        {

            int currentWordPosition = 0; //current text position
            string outputTempString = "";
            bool found = false;


            List<int> listOfIndexes = indexFinder(filetext, inputWord1);

            foreach (int index in listOfIndexes)
            {
                //Console.WriteLine(index);
                for (int i = 0; i < 20 + inputWord1.Length + inputWord2.Length; i++) //iterates looking for word2
                {
                    switch (statisticInFile)
                    {
                        case statisticInFile.DamageRange: //normal behaviour
                            {
                                if (filetext.Substring(index + i, inputWord2.Length + 4).Contains(inputWord2, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    found = true;
                                    currentWordPosition = index + i;
                                }
                                break;
                            }
                        case statisticInFile.AmmoMagCapacity: //again normal behaviour
                            {
                                if (filetext.Substring(index + i, inputWord2.Length + 4).Contains(inputWord2, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    currentWordPosition = index + i;
                                    found = true;
                                }
                                //filetext.Substring(currentWordPosition + i, inputWord2.Length + 4);
                                break;

                            }
                        case statisticInFile.AmmoType:
                            {
                                if (filetext.Substring(index + i, inputWord2.Length + 4).Contains(inputWord2, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    currentWordPosition = index + i + inputWord1.Length + inputWord2.Length;
                                    found = true;
                                }
                                break;
                            }
                        case statisticInFile.Damage:
                            {
                                if (filetext.Substring(index + i, inputWord2.Length + 4).Contains(inputWord2, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    //Console.WriteLine("hello i did not found it (dmgonly)");
                                }
                                else
                                {
                                    found = true;
                                    currentWordPosition = index + i;
                                    break;
                                }
                                break;
                            }
                        case statisticInFile.Firemodes:
                            {
                                if (filetext.Substring(index + i, inputWord2.Length + 4).Contains(inputWord2, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    found = true;
                                    currentWordPosition = index + i + inputWord1.Length + inputWord2.Length;
                                }
                                break;
                            }
                        case statisticInFile.Firerate:
                            {
                                if (filetext.Substring(index + i, inputWord2.Length + 4).Contains(inputWord2, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    found = true;
                                    currentWordPosition = index + i + inputWord1.Length + inputWord2.Length;
                                }
                                break;
                            }
                    }
                    if (found) break;
                    //Console.WriteLine(i); //get rid

                }
            }

            if (found)
            {
                for (int j = currentWordPosition; j < currentWordPosition + 100; j++)
                {
                    if (filetext[j] != ' ')
                    { 
                        if(statisticInFile == statisticInFile.AmmoType )
                        {
                            if ((filetext[j] > 47 && filetext[j] < 58) || (filetext[j] > 64 && filetext[j] < 91) || (filetext[j] > 96 && filetext[j] < 123) || filetext[j] == 46 || filetext[j] == 45) //for numbers, letters, dash and dot
                            {
                                outputTempString += filetext[j];
                                //Console.WriteLine(filetext[j] + " " + outputTempString + " " + j); //remove
                                if ((filetext[j + 1] > 57 || filetext[j + 1] < 48) && filetext[j + 1] != 46 && filetext[j + 1] != 45 && (filetext[j + 1] < 97 || filetext[j + 1] > 122) && (filetext[j + 1] < 65 || filetext[j + 1] > 90) || filetext[j + 1] < 32)
                                {
                                    strings.Add(outputTempString);
                                    outputTempString = "";
                                }


                            }
                        } 
                        else if(statisticInFile == statisticInFile.Firemodes || statisticInFile == statisticInFile.Firerate)
                        {
                            if ((filetext[j] > 47 && filetext[j] < 58) || (filetext[j] > 64 && filetext[j] < 91) || (filetext[j] > 96 && filetext[j] < 123) || filetext[j] == 46 || filetext[j] == 45 || filetext[j] == 124) //for numbers, letters, dash and dot
                            {
                                outputTempString += filetext[j];

                                char curr = filetext[j];
                                char next = filetext[j + 1];
                                //Console.WriteLine(filetext[j] + " " + outputTempString + " " + j); //remove
                                if ((filetext[j + 1] > 57 || filetext[j + 1] < 48) && filetext[j + 1] != 46 && filetext[j + 1] != 45 && (filetext[j + 1] < 97 || filetext[j + 1] > 122) && (filetext[j + 1] < 65 || filetext[j + 1] > 90) && filetext[j+1] != 124 || filetext[j + 1] < 32)
                                {
                                    strings.Add(outputTempString);
                                    outputTempString = "";
                                }
                            }
                        }
                        else
                        {

                            if ((filetext[j] > 47 && filetext[j] < 58) || filetext[j] == 46) //for numbers, and dot
                            {
                                outputTempString += filetext[j];
                                //Console.WriteLine(filetext[j] + " " + outputTempString + " " + j); //remove
                                if ((filetext[j + 1] > 57 || filetext[j + 1] < 48) && filetext[j + 1] != 46 || filetext[j + 1] < 32)
                                {
                                    strings.Add(outputTempString);
                                    outputTempString = "";
                                }


                            }

                        }

                    }
                }
            }

            //Console.WriteLine(outputTempString); //remove
            return strings;
        }
        else if (filetext.Contains(inputWord1, StringComparison.CurrentCultureIgnoreCase) && !filetext.Contains(inputWord2, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new ArgumentException(inputWord2 + " was not found inside the file provided.", nameof(inputWord2));
        }
        else
        {
            throw new ArgumentException(inputWord1 + " and " + inputWord2 + " were not found inside the file provided.", nameof(inputWord1));

        }

    }

    
    public static string findStatisticInFile(string filepath, string inputWord1, string inputWord2)
    {
        if(filepath == null)throw new ArgumentNullException(nameof(filepath));
        if(inputWord1 == null)throw new ArgumentNullException(nameof(inputWord1));
        if(inputWord2 == null)throw new ArgumentNullException(nameof(inputWord2));
        if(!File.Exists(filepath))throw new FileNotFoundException("File not found.", nameof(filepath));
        string filetext = File.ReadAllText(filepath);
        //Console.WriteLine(filetext); //remove
        //Console.WriteLine(inputWord1 + " " + inputWord2);


        if (filetext.Contains(inputWord1, StringComparison.CurrentCultureIgnoreCase) && filetext.Contains(inputWord2, StringComparison.CurrentCultureIgnoreCase))
        {

            int currentWordPosition = 0; //current text position
            string outputTempString = "";
            bool found = false;

            List<int> indexes = indexFinder(filetext, inputWord1);
            foreach (int index in indexes)
            {

                //Console.WriteLine(index);
                //currentWordPosition = filetext.IndexOf(inputWord1, currentWordPosition, StringComparison.CurrentCultureIgnoreCase);
                for (int i = 0; i < 20 + inputWord1.Length + inputWord2.Length; i++)
                {

                    if (filetext.Substring(index + i, inputWord2.Length).Contains(inputWord2, StringComparison.CurrentCultureIgnoreCase))
                    {
                        //Console.WriteLine("hello i found it"); //remove
                        currentWordPosition = index + i + inputWord1.Length + inputWord2.Length;
                        found = true;
                        break;
                    }
                    //Console.WriteLine(i); //remove
                    //Console.WriteLine(filetext.Substring(index + i, inputWord2.Length));

                }
            }

            if (found)
            {
                for (int j = currentWordPosition; j < currentWordPosition + 70; j++)
                {
                    if (filetext[j] != ' ')
                    {
                        if ((filetext[j] > 47 && filetext[j] < 58) || filetext[j] == 46)
                        {
                            outputTempString += filetext[j];
                            //Console.WriteLine(filetext[j]+ " " + outputTempString + " " + j); //remove
                        }


                    }
                }
            }

            //Console.WriteLine(outputTempString); //remove
            return outputTempString;
        }
        else if (filetext.Contains(inputWord1, StringComparison.CurrentCultureIgnoreCase) && !filetext.Contains(inputWord2, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new ArgumentException(inputWord2 + " was not found inside the file provided.", nameof(inputWord2));
        }
        else
        {
            throw new ArgumentException(inputWord1 + " and " + inputWord2 + " were not found inside the file provided.", nameof(inputWord1));

        }
    }


    public static List<string> findStatisticInFile(string filepath, string inputWord1)
    {
        if (filepath == null) throw new ArgumentNullException(nameof(filepath));
        if(inputWord1 == null)throw new ArgumentNullException(nameof(inputWord1));
        string filetext = File.ReadAllText(filepath);
        List<string> output = new();


        if (filetext.Contains(inputWord1, StringComparison.CurrentCultureIgnoreCase))
        {

            string outputTempString = "";



            List<int> indexes = indexFinder(filetext, inputWord1);
            foreach (int index in indexes)
            {
                for (int j = index; j < index + 100; j++)
                {
                    if (filetext[j] != ' ')
                    {
                        if ((filetext[j] > 47 && filetext[j] < 58) || filetext[j] == 46)
                        {
                            outputTempString += filetext[j];
                            if ((filetext[j + 1] < 48 && filetext[j + 1] > 57) && filetext[j + 1] != 46 || filetext[j+1] <= 32)
                            {
                                output.Add(outputTempString);
                                outputTempString = "";
                            }
                        }
                    }
                }

            }

            return output;
        }
        else
        {
            throw new ArgumentException(inputWord1 + " was not found in the file specified.", nameof(inputWord1));

        }
    }
}