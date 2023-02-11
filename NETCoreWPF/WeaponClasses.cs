using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace NETCoreWPF
{
    
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
                this.hasRank = true;
                this.rank = rank;
            } else
            {
                this.hasRank = false;
                this.rank = 0;
            }
        }

        protected string Name { get; set; }
        protected int Rank { get; set; }
        protected bool HasRank { get; set; }

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

        protected double LimbMultiplier { get; set; }
        protected double TorsoMultiplier { get; set; }
        protected double HeadMultiplier { get; set; }
        protected double WalkSpeed { get; set; }
    }

    /*class CarriedAux
    {
        //Gun GunAux();
    }*/

    class Ranged
    {
        private double range1,
                        range2,
                        damageRange1,
                        damageRange2;
    }
    /*
     formatting for firemodes:
        a600 -> automatic 600rpm
        s750 -> semiautomatic 750rpm
        s40la -> semiautomatic 40rpm, lever action
        s300sh8 -> semiautomatic 300 rpm, shotgun with 8 pellets
        
     
    char firstCharacter = str[0];
    try{
        
    }

     
     
     */
    


    public class FireModeList
    {
        private List<FireMode> modes = new List<FireMode>();
        
        public FireMode this[int index]
        {
            get { return modes[index]; }
            set { modes[index] = value; }
        }

        public FireModeList(string[] firemodes)
        {

            for(int j = 0; j < firemodes.Length; j++) { //iterates through the firemodes
                
                string str = firemodes[j]; 

                string mode;
                int firerate;
                string specialFlags;
                int pellets;

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

                firerate = Convert.ToInt32(resultInt[0]);
                mode = resultString[0];
                specialFlags = resultString[1];
                pellets = Convert.ToInt32(resultInt[1]);

                char firstCharacterMode = mode[0];

                
                if ((mode.Contains("automatic") || str.Contains("auto")) && firstCharacterMode == 'a')
                {
                    FireMode Automatic = new FireMode(firerate, "Automatic", false, "", (specialFlags.Length != 0), specialFlags, 0);
                } else if((mode.Contains("semiautomatic") || mode.Contains("semi")) && firstCharacterMode == 's')
                {
                    if (specialFlags.Contains("boltaction") || specialFlags.Contains("bolt") || specialFlags.Contains("ba")) {
                        FireMode BoltAction = new FireMode(firerate, "SemiAutomatic", false, "", true, "BoltAction", 0);
                    } else if(specialFlags.Contains("leveraction") || specialFlags.Contains("lever") || specialFlags.Contains("la"))
                    {
                        FireMode LeverAction = new FireMode(firerate, "SemiAutomatic", false, "", true, "LeverAction", 0);
                    } else if(specialFlags.Contains("pumpshotgun") || specialFlags.Contains("pump") || specialFlags.Contains("ps"))
                    {
                        FireMode PumpShotgun = new FireMode(firerate, "SemiAutomatic", false, "", true, "PumpShotgun", pellets);
                    }

                    FireMode SemiAutomatic = new FireMode(firerate, "SemiAutomatic", false, "", (specialFlags.Length != 0), specialFlags, 0);

                } 
            }
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

    }


    class Gun : Weapon
    {
        

        private FireModeList fireModes;
        private string caliber;
        private int ammoCapacity;
        private int magazineCapacity;
        private double penetration;
        private double muzzleVelocity;
        private double aimingWalkspeed;
        private double reloadSpeed;
        private double emptyReloadSpeed;

        public Gun(string name, bool hasRank, int rank, string caliber, int ammoCapacity, int magazineCapacity, string[] firemodes, double penetration, double muzzleVelocity, double aimingWalkspeed, double reloadSpeed, double emptyReloadSpeed) : base(name, hasRank, rank)
        {
            this.caliber = caliber;
            this.ammoCapacity = ammoCapacity;
            this.emptyReloadSpeed = emptyReloadSpeed;
            this.magazineCapacity = magazineCapacity;
            this.aimingWalkspeed = aimingWalkspeed;
            this.reloadSpeed = reloadSpeed;
            this.muzzleVelocity = muzzleVelocity;
            this.penetration = penetration;
            this.fireModes = new FireModeList(firemodes);
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
