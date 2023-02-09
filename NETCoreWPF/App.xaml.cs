using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NETCoreWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
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
