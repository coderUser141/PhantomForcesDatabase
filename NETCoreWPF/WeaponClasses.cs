using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using System.Xml.Linq;

namespace NETCoreWPF
{
    
    /// <summary>
    /// Defines a weapon object.
    /// </summary>
    public class Weapon
    {

        private string name;
        private int rank;
        private bool hasRank;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Name of the weapon.</param>
        /// <param name="hasRank">If the weapon has a rank.</param>
        /// <param name="rank">If the previous parameter is true, the rank of the weapon.</param>
        public Weapon(string name, bool hasRank, int rank)
        {
            this.name = name;
            if (hasRank)
            {
                this.rank = rank;
            } else
            {
                this.rank = 0;
            }
            this.hasRank = hasRank;
        }

        /// <summary>
        /// Name getter/setter.
        /// </summary>
        protected string Name { get { return name; } set { name = value; } }
        /// <summary>
        /// Rank getter/setter.
        /// </summary>
        protected int Rank { get { return rank; } set { rank = value; } }
        /// <summary>
        /// HasRank getter/setter.
        /// </summary>
        protected bool HasRank { get { return hasRank; } set { hasRank = value; } }

    }

    /// <summary>
    /// Creates objects for defining carried weapons' attributes (melees, guns).
    /// </summary>
    public class Carried
    {

        private double limbMultipler,
                            torsoMultiplier,
                            headMultiplier,
                            walkspeed;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="limbMultipler">Limb multiplier of the weapon.</param>
        /// <param name="torsoMultiplier">Torso multiplier of the weapon.</param>
        /// <param name="headMultiplier">Head multiplier of the weapon.</param>
        /// <param name="walkspeed">Walkspeed of the weapon.</param>
        public Carried(double limbMultipler, double torsoMultiplier, double headMultiplier, double walkspeed)
        {
            this.limbMultipler = limbMultipler;
            this.torsoMultiplier = torsoMultiplier;
            this.headMultiplier = headMultiplier;
            this.walkspeed = walkspeed;
        }

        /// <summary>
        /// LimbMultipler getter/setter.
        /// </summary>
        protected double LimbMultiplier { get { return limbMultipler; } set { limbMultipler = value; } }
        /// <summary>
        /// TorsoMultiplier getter/setter.
        /// </summary>
        protected double TorsoMultiplier { get { return torsoMultiplier; } set { torsoMultiplier = value; } }
        /// <summary>
        /// HeadMultiplier getter/setter.
        /// </summary>
        protected double HeadMultiplier { get { return headMultiplier; } set { headMultiplier = value; } }
        /// <summary>
        /// WalkSpeed getter/setter.
        /// </summary>
        protected double WalkSpeed { get { return walkspeed; } set { walkspeed = value; } }
    }

    /// <summary>
    /// Creates objects for defining ranged weapons' attributes (grenades, guns).
    /// </summary>
    public class Ranged
    {
        private double range1,
                        range2,
                        damageRange1,
                        damageRange2;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="range1">Close range in studs.</param>
        /// <param name="range2">Far range in studs.</param>
        /// <param name="damageRange1">Range 1's damage.</param>
        /// <param name="damageRange2">Range 2's damage.</param>
        public Ranged(double range1, double range2, double damageRange1, double damageRange2)
        {
            this.range1 = range1;
            this.range2 = range2;
            this.damageRange1 = damageRange1;
            this.damageRange2 = damageRange2;
        }
        /// <summary>
        /// Range1 getter/setter.
        /// </summary>
        protected double Range1 { get { return range1; } set { range1 = value; } }
        /// <summary>
        /// Range2 getter/setter.
        /// </summary>
        protected double Range2 { get { return range2; } set { range2 = value; } }
        /// <summary>
        /// DamageRange1 getter/setter.
        /// </summary>
        protected double DamageRange1 { get { return damageRange1; } set { damageRange1 = value; } }
        /// <summary>
        /// DamageRange2 getter/setter.
        /// </summary>
        protected double DamageRange2 { get { return damageRange2; } set { damageRange2 = value; } }

    }

    /// <summary>
    /// Class defining a list of <c>FireMode</c> objects.
    /// </summary>
    public class FireModeList
    {
        private List<FireMode> modes = new List<FireMode>();

        /// <summary>
        /// <c>FireModeList</c> indexer
        /// </summary>
        /// <param name="index">Index of the <c>FireModeList</c>.</param>
        /// <returns></returns>
        public FireMode this[int index]
        {
            get { return modes[index]; }
            set { modes[index] = value; }
        }

        /// <summary>
        /// Parses a string into a <c>FireMode</c> object. Case-insensitive. Does not accept any special characters or spaces.
        /// <example>
        /// Example of formatting:
        /// <code>
        /// a600 -> automatic 600rpm
        /// s750 -> semiautomatic 750rpm
        /// s40la0 -> semiautomatic 40rpm, lever action
        /// s300sh8 -> semiautomatic 300 rpm, shotgun with 8 pellets
        /// b400bb0 -> burst (double) 400rpm
        /// </code>
        /// Code formatting: mode,firerate(rpm),special modes,pellets(for shotguns)
        /// <code>
        /// mode = (a|auto|automatic), (s|semi|semiautomatic), (b|burst)
        /// firerate = any integer
        /// special modes = for burst: (b = InstantBurst,bb = DoubleBurst,bbb = TripleBurst); 
        ///                 for semiautomatic: (la|lever|leveraction = LeverAction, 
        ///                                     ps|pump|pumpshotgun = PumpShotgun,
        ///                                     sh|shotgun = Shotgun)
        /// pellets = any integer, but required to include and can be set to 0
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="firemodes">A string specified in the format above. Case-insensitive. Does not accept any special characters or spaces.</param>
        /// <returns><c>FireMode</c> object referring to Automatic, Semiautomatic</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public FireMode ParseFireModeString(string firemodes)
        {
            

            string str = firemodes;

            string mode = "";
            int firerate = 0;
            string specialFlags = "";
            int pellets = 0;

            List<string> resultInt = new List<string>();
            List<string> resultString = new List<string>();
            int count = 0; //count for numbers
            int count1 = 0; //count for letters
            string output = " ";
            try
            {
                //iterates through the individual string and parses
                for (int i = 0; i < str.Length; i++)
                {
                    output = (count+", "+count1+", "+i+" ");
                    File.AppendAllText("test.log", output);

                    if (Convert.ToInt32(str[i]) > 64 && Convert.ToInt32(str[i]) < 91)
                    { //if uppercase, turns into lowercase
                        str = str.Replace(str[i], (char)(Convert.ToInt32(str[i]) + 32));
                    }

                    if (Convert.ToInt32(str[i]) > 47 && Convert.ToInt32(str[i]) < 58)
                    { //detects number, increments count

                        count++;
                        if (i + 1 == str.Length)
                        { //detects if the end of the string has been reached and adds previous whole number
                            resultInt.Add(str.Substring(i - count + 1, count));
                            output = (str.Substring(i - count + 1, count) + " ");
                            File.AppendAllText("test.log", output);
                        }

                        if (i > 0)
                        { //avoids outofboundexception
                            if (Convert.ToInt32(str[i - 1]) > 96 && Convert.ToInt32(str[i - 1]) < 123)
                            { //detects if previous character is a letter and adds the whole word before into the list
                                resultString.Add(str.Substring(i - count1, count1));
                                output = (str.Substring(i - count, count) + " ");

                                File.AppendAllText("test.log", output);
                            }
                        }

                        count1 = 0; //reset letter counter
                    }
                    else
                    {

                        if (i > 0)
                        { //avoids outofboundexception
                            if (Convert.ToInt32(str[i - 1]) > 47 && Convert.ToInt32(str[i - 1]) < 58)
                            { //detects if previous character is a number and adds the whole number before into the list
                                resultInt.Add(str.Substring(i - count, count));
                                output = (str.Substring(i - count, count) + " ");

                                File.AppendAllText("test.log", output);
                            }
                        }

                        if (Convert.ToInt32(str[i]) > 96 && Convert.ToInt32(str[i]) < 123)
                        { //detects letter, increments count
                            count1++;
                            if (i + 1 == str.Length)
                            { //detects if the end of the string has been reached and adds previous whole word
                                resultString.Add(str.Substring(i - count + 1, count));
                                output = (str.Substring(i - count + 1, count) + " ");
                                File.AppendAllText("test.log", output);
                            }
                        }

                        count = 0; //reset number counter
                    }
                }

                firerate = (resultInt[0].Length != 0) ? Convert.ToInt32(resultInt[0]) : 0;
                mode = (resultString[0].Length != 0) ? resultString[0] : "";
            }
            catch (NullReferenceException)
            {
                return new FireMode(-1, "NullReferenceException", false, "Null", false, "String cannot be null", 0);
            }
            catch (ArgumentOutOfRangeException)
            {
                return new FireMode(-2, "ArgumentOutOfRangeException", false, "Null", false, "String must have both numbers and letters", 0);
            }


            try
            {
                specialFlags = (resultString[1].Length != 0) ? resultString[1] : "";
            }
            catch (ArgumentOutOfRangeException)
            {

                specialFlags = "";
            }

            try
            {
                pellets = (resultInt[1].Length != 0) ? Convert.ToInt32(resultInt[1]) : 0;
            }
            catch (ArgumentOutOfRangeException)
            {
                pellets = 0;
            }

            char firstCharacterMode = mode[0];


            if ((mode.Contains("automatic") || str.Contains("auto")) || firstCharacterMode == 'a')
            {
                return new FireMode(firerate, "Automatic", false, "", (specialFlags.Length != 0), specialFlags, 0);
                //modes.Add(Automatic);

            }
            else if ((mode.Contains("semiautomatic") || mode.Contains("semi") || mode.Contains("semi-automatic")) || firstCharacterMode == 's')
            {
                if (specialFlags.Contains("boltaction") || specialFlags.Contains("bolt") || specialFlags.Contains("ba"))
                {
                    return new FireMode(firerate, "SemiAutomatic", false, "", true, "BoltAction", 0);
                    //modes.Add(BoltAction);
                }
                else if (specialFlags.Contains("leveraction") || specialFlags.Contains("lever") || specialFlags.Contains("la"))
                {
                    return new FireMode(firerate, "SemiAutomatic", false, "", true, "LeverAction", 0);
                    //modes.Add(LeverAction);
                }
                else if (specialFlags.Contains("pumpshotgun") || specialFlags.Contains("pump") || specialFlags.Contains("ps"))
                {
                    return new FireMode(firerate, "SemiAutomatic", false, "", true, "PumpShotgun", pellets);
                    //modes.Add(PumpShotgun);
                }
                else if ((specialFlags.Contains("shotgun") || specialFlags.Contains("sh")) && !(specialFlags.Contains("pumpshotgun")))
                {
                    return new FireMode(firerate, "SemiAutomatic", false, "", true, "Shotgun", pellets);
                    //modes.Add(Shotgun);
                }


                return new FireMode(firerate, "SemiAutomatic", false, "", (specialFlags.Length != 0), specialFlags, 0);
                //modes.Add(SemiAutomatic);

            }
            else if ((mode.Contains("burst") || firstCharacterMode == 'b'))
            {
                
                if (specialFlags.Contains("bbb"))
                {
                    return new FireMode(firerate, "Burst", true, "III", true, "TripleBurst", 0);
                    //modes.Add(TripleBurst);
                }else if (specialFlags.Contains("bb"))
                {

                    return new FireMode(firerate, "Burst", true, "II", true, "DoubleBurst", 0);
                    //modes.Add(DoubleBurst);
                }
                else if (specialFlags.Contains("b"))
                {
                    return new FireMode(firerate, "Burst", true, "I", true, "InstantBurst", pellets);
                    //modes.Add(InstantBurst);
                }



            }

            return new FireMode(5, "", false, resultString[1], false, "", 0);
        }

        /// <summary>
        /// Iterates through the string array of firemodes.
        /// </summary>
        /// <param name="firemodes">An array of strings as specified in the <c>ParseFireModeString</c> method as <see cref="ParseFireModeString(string)">seen here</see>.</param>
        public void ParseFireModeStringIterator(string[] firemodes)
        {
            try {
                foreach(string str in firemodes)
                {
                    modes.Add(ParseFireModeString(str));

                }
            }
            catch (NullReferenceException)
            {
                modes.Add(new FireMode(-3, "NullReferenceException", false, "Null", false, "Cannot pass null as string array", 0));
            }
        }


        /// <summary>
        /// Searches the list for a specific <c>FireMode</c>.
        /// </summary>
        /// <param name="item">The <c>FireMode</c> to search with.</param>
        /// <returns>True if the list contains the FireMode, false otherwise.</returns>
        public bool searchList(FireMode item)
        {
            if (modes.Contains(item))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// Searches the list for a specific <c>FireMode</c>'s index.
        /// </summary>
        /// <param name="item">The <c>FireMode</c> to search with.</param>
        /// <returns>Index of item if it is found, otherwise -1.</returns>
        public int indexOfItem(FireMode item)
        {
            if (modes.Contains(item))
            {
                return modes.IndexOf(item);
            }
            else
            {
                return -1;
            }
        }


        /// <summary>
        /// Adds element to the end of the <c>FireModeList</c> list
        /// </summary>
        /// <param name="firemode">The <c>FireMode</c> to add.</param>
        /// <returns></returns>
        public bool addElement(FireMode firemode)
        {
            modes.Add(firemode);
            return searchList(firemode);
        }


        /// <summary>
        /// Adds new default firemode.
        /// </summary>
        /// <param name="firemode">The <c>FireMode</c> to insert.</param>
        public void addNewDefaultFiremode(FireMode firemode)
        {
            modes.Insert(0, firemode);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="firemodes"><see cref="ParseFireModeStringIterator(string[])">See parameters.</see></param>
        public FireModeList(string[] firemodes)
        {
            ParseFireModeStringIterator(firemodes);
            int r = 5;
        }

    }


    /// <summary>
    /// Class that defines firemodes for guns.
    /// </summary>
    public class FireMode{

        
        private double firerate;
        private string mode;
        private bool burst;
        private string burstMode;
        private bool special;
        private string specialMode;
        private int pellets;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="firerate">Firerate of the gun in rounds per minute (RPM).</param>
        /// <param name="mode">Firing mode name, e.g. <c>(Automatic|Semiautomatic|Burst)</c>.</param>
        /// <param name="burst">Specifies if the firemode has burst capabilities.</param>
        /// <param name="burstMode">If the previous value is true, specifies if the firemode has InstantBurst("I"), DoubleBurst("II"), or TripleBurst("III").</param>
        /// <param name="special">Specifies if the firemode has special modes.</param>
        /// <param name="specialMode">If the previous value is true, specifies if the firemode has any special modes.</param>
        /// <param name="pellets">If the firemode is of a shotgun, specifies the number of pellets.</param>
        public FireMode(double firerate, string mode, bool burst, string burstMode, bool special, string specialMode, int pellets)
        {
            this.firerate = firerate;
            this.mode = mode;
            if (burst)
            {
                this.burstMode = burstMode;
            } else
            {
                this.burstMode = "";
            }

            if (special)
            {
                this.specialMode = specialMode;
            } else
            {
                this.specialMode = "";
            }

            this.burst = burst;
            this.special = special;
            this.pellets = pellets;
        }
        /// <summary>
        /// Firerate getter/setter.
        /// </summary>
        public double Firerate { get { return firerate; } set { firerate = value; } }
        /// <summary>
        /// Mode getter/setter.
        /// </summary>
        public string Mode { get { return mode; } set{ mode = value; } }
        /// <summary>
        /// Burst getter/setter.
        /// </summary>
        public bool Burst { get { return burst; } set { burst = value; } }
        /// <summary>
        /// BurstMode getter/setter.
        /// </summary>
        public string BurstMode { get { return burstMode;} set { burstMode = value; } }
        /// <summary>
        /// Special getter/setter.
        /// </summary>
        public bool Special { get { return special; } set { special = value; } }
        /// <summary>
        /// SpecialMode getter/setter.
        /// </summary>
        public string SpecialMode { get { return specialMode;} set { specialMode = value; } }
        /// <summary>
        /// Pellets getter/setter.
        /// </summary>
        public int Pellets { get { return pellets; } set { pellets = value; } }


    }


    class Gun : Weapon
    {
        

        private FireModeList fireModes;
        private Carried carriedAttributes;
        private Ranged rangedAttributes;
        private string caliber;
        private int ammoCapacity;
        private int magazineCapacity;
        private double penetration;
        private double muzzleVelocity;
        private double aimingWalkspeed;
        private double reloadSpeed;
        private double emptyReloadSpeed;

        public Gun(string name, bool hasRank, int rank, string caliber, int ammoCapacity, int magazineCapacity, string[] firemodes, Carried carriedAttributes, Ranged rangedAttributes, double penetration, double muzzleVelocity, double aimingWalkspeed, double reloadSpeed, double emptyReloadSpeed) : base(name, hasRank, rank)
        {
            this.caliber = caliber;
            this.ammoCapacity = ammoCapacity;
            this.emptyReloadSpeed = emptyReloadSpeed;
            this.magazineCapacity = magazineCapacity;
            this.aimingWalkspeed = aimingWalkspeed;
            this.reloadSpeed = reloadSpeed;
            this.muzzleVelocity = muzzleVelocity;
            this.penetration = penetration;
            fireModes = new FireModeList(firemodes);
            this.carriedAttributes = carriedAttributes;
            this.rangedAttributes = rangedAttributes;
            
        }

        public class Conversion//abstract class 
        {
            public string attachment;
            public string conversionName;

            public bool ammoConversion;
            public string ammoType; //depends on above bool
        }
    }

    
    class Melee : Weapon
    {
        public double frontStabDamage;
        public double backStabDamage;
        public double bladeLength;

        public Melee(string name, bool hasRank, int rank, double frontStabDamage, double backStabDamage, double bladeLength) : base(name, hasRank, rank)
        {
            this.frontStabDamage = frontStabDamage;
            this.backStabDamage = backStabDamage;
            this.bladeLength = bladeLength;

        }
    }

    class Grenade : Weapon
    {
        public bool fuse;
        public double fuseTime; //depends on above bool

        public int storedCapacity;

        public Grenade(string name, bool hasRank, int rank) : base(name, hasRank, rank)
        {
        }
    }
    
}



/*
weapons:
    gun
    grenade
    melee

class Weapon{
    protected string name;
    protected int rank;
}

class Carried{
    protected double limbMultipler, 
                        torsoMultiplier, 
                        headMultiplier, 
                        walkspeed;
}

class Ranged{
    protected double range1, 
                    range2,
                    damageRange1,
                    damageRange2;
}

class Gun : Weapon, Carried, Ranged {
    public string caliber;
    public double firerate;
    public int ammoCapacity;
    public int magazineCapacity;
    public string[ ] firemodes = new string[5];
    public double penetration;
    public double muzzleVelocity;
    public double aimingWalkspeed;
    public double reloadSpeed;
    public double emptyReloadSpeed;

    public class Conversion{
        public string attachment;
        public string conversionName;

        public bool ammoConversion; 
        public string ammoType; //depends on above bool
    }
}

class Melee : Weapon, Carried {
    public double frontStabDamage;
    public double backStabDamage;
    public double bladeLength;


}

class Grenade : Weapon, Ranged {
    public bool fuse;
    public double fuseTime; //depends on above bool
    
    public int storedCapacity;

}

carried class (for gun, melee):
    limb multiplier
    torso multiplier
    head multiplier
    walkspeed
    
ranged weapon (for gun, grenade):
    range 1
    range 2
    damage range 1
    damage range 2

weapon general properties
    name
    rank

gun properties
    caliber
    firerate
    ammo capacity
    mag capacity
    fire modes
    penetration
    muzzle velocity
    aiming walkspeed
    reload time
    empty reload time

    special attachments
    special conversions 
    conversion names

melee properties
    front stab damage
    back stab damage
    blade length
 
grenade
    fuse? (true/false)
    fuse time
    stored capacity
    

 */
