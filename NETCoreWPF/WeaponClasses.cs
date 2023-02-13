using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using System.Xml.Linq;

namespace NETCoreWPF
{
    
    /// <summary>
    /// Defines a weapon 
    /// </summary>
    /// <param name="name">Name of weapon</param>
    /// <param name="hasRank"></param>
    class Weapon
    {

        private string name;
        private int rank;
        private bool hasRank;

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

        protected string Name { get { return name; } set { name = value; } }
        protected int Rank { get { return rank; } set { rank = value; } }
        protected bool HasRank { get { return hasRank; } set { hasRank = value; } }

    }

    class Carried
    {

        private double limbMultipler,
                            torsoMultiplier,
                            headMultiplier,
                            walkspeed;

        public Carried(double lM, double tM, double hM, double ws)
        {
            this.limbMultipler = lM;
            this.torsoMultiplier = tM;
            this.headMultiplier = hM;
            this.walkspeed = ws;
        }

        protected double LimbMultiplier { get { return limbMultipler; } set { limbMultipler = value; } }
        protected double TorsoMultiplier { get { return torsoMultiplier; } set { torsoMultiplier = value; } }
        protected double HeadMultiplier { get { return headMultiplier; } set { headMultiplier = value; } }
        protected double WalkSpeed { get { return walkspeed; } set { walkspeed = value; } }
    }

    class Ranged
    {
        private double range1,
                        range2,
                        damageRange1,
                        damageRange2;

        public Ranged(double range1, double range2, double damageRange1, double damageRange2)
        {
            this.range1 = range1;
            this.range2 = range2;
            this.damageRange1 = damageRange1;
            this.damageRange2 = damageRange2;
        }

        protected double Range1 { get { return range1; } set { range1 = value; } }
        protected double Range2 { get { return range2; } set { range2 = value; } }
        protected double DamageRange1 { get { return damageRange1; } set { damageRange1 = value; } }
        protected double DamageRange2 { get { return damageRange2; } set { damageRange2 = value; } }

    }

    /*
     formatting for firemodes:
        a600 -> automatic 600rpm
        s750 -> semiautomatic 750rpm
        s40la -> semiautomatic 40rpm, lever action
        s300sh8 -> semiautomatic 300 rpm, shotgun with 8 pellets
     */

    public class FireModeList
    {
        private List<FireMode> modes = new List<FireMode>();

        public FireMode this[int index]
        {
            get { return modes[index]; }
            set { modes[index] = value; }
        }

        public FireMode ParseFireModeString(string[] firemodes)
        {
            for (int j = 0; j < firemodes.Length; j++)
            { //iterates through the firemodes

                string str = firemodes[j];

                string mode = "";
                int firerate = 0;
                string specialFlags = "";
                int pellets = 0;

                List<string> resultInt = new List<string>();
                List<string> resultString = new List<string>();
                int count = 0; //count for numbers
                int count1 = 0; //count for letters

                //iterates through the individual string and parses
                for (int i = 0; i < str.Length; i++)
                {

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
                        }

                        if (i > 0)
                        { //avoids outofboundexception
                            if (Convert.ToInt32(str[i - 1]) > 96 && Convert.ToInt32(str[i - 1]) < 123)
                            { //detects if previous character is a letter and adds the whole word before into the list
                                resultString.Add(str.Substring(i - count1, count1));
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
                            }
                        }

                        if (Convert.ToInt32(str[i]) > 96 && Convert.ToInt32(str[i]) < 123)
                        { //detects letter, increments count
                            count1++;
                            if (i + 1 == str.Length)
                            { //detects if the end of the string has been reached and adds previous whole word
                                resultString.Add(str.Substring(i - count + 1, count));
                            }
                        }

                        count = 0; //reset number counter
                    }
                }

                firerate = (resultInt[0].Length != 0) ? Convert.ToInt32(resultInt[0]) : 0;
                mode = (resultString[0].Length != 0) ? resultString[0] : "";
                try
                {
                    specialFlags = (resultString[1].Length != 0) ? resultString[1] : "";
                }
                catch (Exception ex)
                {
                    specialFlags = "";
                }
                try
                {
                    pellets = (resultInt[1].Length != 0) ? Convert.ToInt32(resultInt[1]) : 0;
                }
                catch (Exception ex)
                {
                    pellets = 0;
                }

                char firstCharacterMode = mode[0];


                if ((mode.Contains("automatic") || str.Contains("auto")) || firstCharacterMode == 'a')
                {
                    return new FireMode(firerate, "Automatic", false, "", (specialFlags.Length != 0), specialFlags, 0);
                    //modes.Add(Automatic);

                }
                else if ((mode.Contains("semiautomatic") || mode.Contains("semi")) || firstCharacterMode == 's')
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
                    if (specialFlags.Contains("b") && !(specialFlags.Contains("bb")) && !(specialFlags.Contains("bbb")))
                    {
                        return new FireMode(firerate, "Burst", true, "I", true, "InstantBurst", 0);
                        //modes.Add(InstantBurst);
                    }
                    else if (specialFlags.Contains("bb") && !(specialFlags.Contains("bbb")))
                    {

                        return new FireMode(firerate, "Burst", true, "II", true, "DoubleBurst", 0);
                        //modes.Add(DoubleBurst);
                    }
                    else if (specialFlags.Contains("bbb"))
                    {
                        return new FireMode(firerate, "Burst", true, "III", true, "TripleBurst", 0);
                        //modes.Add(TripleBurst);
                    } else
                    {
                        break;
                    }

                    
                } else
                {
                    break;
                }
            }

            return new FireMode(0, "", false, "", false, "", 0);
        }


        /// <summary>
        /// Searches the list for a specific FireMode
        /// </summary>
        /// <param name="item">The FireMode to search with</param>
        /// <returns>True if the list contains the FireMode, false otherwise</returns>
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
        /// Searches the list for a specific FireMode's index.
        /// </summary>
        /// <param name="item">The FireMode to search with</param>
        /// <returns>Index of item if it is found, otherwise -1</returns>
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

        public bool addElement(FireMode firemode)
        {
            modes.Add(firemode);
            return searchList(firemode);
        }

        //public void


        public FireModeList(string[] firemodes)
        {
            modes.Add(ParseFireModeString(firemodes));
            
        }

    }

    public class FireMode{

        
        private double firerate;
        private string mode;
        private bool burst;
        private string burstMode;
        private bool special;
        private string specialMode;
        private int pellets;

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

        public double Firerate { get { return firerate; } set { firerate = value; } }
        public string Mode { get { return mode; } set{ mode = value; } }
        public bool Burst { get { return burst; } set { burst = value; } }
        public string BurstMode { get { return burstMode;} set { burstMode = value; } }
        public bool Special { get { return special; } set { special = value; } }
        public string SpecialMode { get { return specialMode;} set { specialMode = value; } }
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

    /*
    class Melee : Weapon, Carried
    {
        public double frontStabDamage;
        public double backStabDamage;
        public double bladeLength;

        
    }

    class Grenade : Weapon, Ranged
    {
        public bool fuse;
        public double fuseTime; //depends on above bool

        public int storedCapacity;

    }
    */
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
