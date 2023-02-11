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

    


    public class FireModeList
    {
        protected List<string> modes = new List<string>();
        

        public FireModeList(string[] firemodes)
        {
            foreach (string str in firemodes)
            {
                if (str == "automatic" || str == "auto" || str == "a")
                {
                    //FireMode Automatic = new FireMode()
                } else if(str == "semiautomatic" || str == "semi" || str == "s")
                {

                } else if(str == "b"  || str == "i" || str == "oneburst")
                {

                } else if(str == "bb" || str == "ii" || str=="twoburst")
                {

                } else if(str == "bbb" || str == "iii" || str == "threeburst")
                {

                }
            }
        }
    }

    class FireMode{

        private double firerate;
        private string mode;
        private bool burst;
        private int burstMode;
        private bool special;
        private string specialMode;

        public FireMode(double firerate, string mode, bool burst, int burstMode, bool special, string specialMode)
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
