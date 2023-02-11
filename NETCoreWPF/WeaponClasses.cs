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
        protected List<string> modes = new List<string>();
        

        public FireModeList(string[] firemodes)
        {
            for(int j = 0; j < firemodes.Length; j++) {
                string str = firemodes[j];

                char firstCharacter = str[0];


                List<string> resultInt = new List<string>();
                List<string> resultString = new List<string>();
                int count = 0; //count for numbers
                int count1 = 0; //count for letters
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

                //foreach (string ite in resultInt) Console.WriteLine(ite);

                //foreach (string itee in resultString) Console.WriteLine(itee);

                if ((str.Contains("automatic") || str.Contains("auto") || str.Contains("a")) && firstCharacter == 'a')
                {
                    //FireMode Automatic = new FireMode()
                } else if(str.Contains("semiautomatic") || str.Contains("semi") || str.Contains("s"))
                {

                } 
            }
        }

        public List<string> Mode { get { return modes; } }
    }

    class FireMode{

        private double firerate;
        private string mode;
        private bool burst;
        private int burstMode;
        private bool special;
        private string specialMode;

        public FireMode(double firerate, string mode, bool burst, string burstMode, bool special, string specialMode, int pellets)
        {
            this.firerate = firerate;
            this.mode = mode;
            if (burst)
            {
                
            }


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

}
