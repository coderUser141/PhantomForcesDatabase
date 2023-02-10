using System;
using System.Collections.Generic;
using System.Text;

namespace NETCoreWPF
{

    class Weapon
    {
        protected string name;
        protected int rank;
    }

    class Carried
    {
        protected double limbMultipler,
                            torsoMultiplier,
                            headMultiplier,
                            walkspeed;
    }

    class Ranged
    {
        protected double range1,
                        range2,
                        damageRange1,
                        damageRange2;
    }

    class Gun : Weapon, Carried, Ranged
    {
        public string caliber;
        public double firerate;
        public int ammoCapacity;
        public int magazineCapacity;
        public string[] firemodes = new string[5];
        public double penetration;
        public double muzzleVelocity;
        public double aimingWalkspeed;
        public double reloadSpeed;
        public double emptyReloadSpeed;

        public class Conversion
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
