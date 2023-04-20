using System;
using System.Windows.Controls;
using System.Windows.Documents;
using WeaponClasses;

namespace PFDB.User_Controls
{
    /// <summary>
    /// Interaction logic for Stats.xaml
    /// </summary>
    public partial class Stats : UserControl
    {
        public Stats()
        {
            InitializeComponent();
        }

        

        public void passText(Gun gun)
        {
            clearText();
            Name.Text = gun.Name;
            c1r1.Text = "Gun Name:";

            Rank.Text = "Rank " + gun.Rank.ToString();
            c1r2.Text = "Gun Rank:";

            Firemodes.Text = GUIWeaponClass.firemodelistJoiner(GUIWeaponClass.firemodeGUI(gun.Conversions.DefaultConversion.FireModes));
            c1r3.Text = "Firemodes:";

            limbMultiplier.Text = gun.DefaultCarriedAttributes.LimbMultiplier.ToString() + "x";
            c1r4.Text = "Limb Multiplier:";

            headMultiplier.Text = gun.DefaultCarriedAttributes.HeadMultiplier.ToString() + "x";
            c1r6.Text = "Head Multiplier:";

            torsoMultiplier.Text = gun.DefaultCarriedAttributes.TorsoMultiplier.ToString() + "x";
            c1r5.Text = "Torso Multiplier:";

            walkspeed.Text = gun.DefaultCarriedAttributes.WalkSpeed.ToString();
            c1r7.Text = "Weapon Walkspeed:";
            Range1.Text = gun.DefaultRangedAttributes.Range1.ToString() + " studs";
            c1r8.Text = "Range 1:";
            Range2.Text = gun.DefaultRangedAttributes.Range2.ToString() + " studs";
            c1r9.Text = "Range 2:";
            Range1Damage.Text = gun.DefaultRangedAttributes.Range1Damage.ToString();
            c1r10.Text = "Range 1 Damage:";
            Range2Damage.Text = gun.DefaultRangedAttributes.Range2Damage.ToString();
            c1r11.Text = "Range 2 Damage:";

            Penetration.Text = gun.DefaultPenetration == 1 ? gun.DefaultPenetration.ToString() + " stud" : gun.DefaultPenetration.ToString() + " studs";
            c2r4.Text = "Penetration Depth:";
            MuzzleVelocity.Text = gun.DefaultMuzzleVelocity.ToString() + " studs/s";
            c2r5.Text = "Muzzle Velocity:";
            ReloadSpeed.Text = gun.DefaultReloadSpeed.ToString() + " seconds";
            c2r6.Text = "Reload Time:";
            EmptyReloadSpeed.Text = gun.DefaultEmptyReloadSpeed.ToString() + " seconds";
            c2r7.Text = "Empty Reload Time:";
            Suppression.Text = gun.DefaultSuppression.ToString();
            c2r8.Text = "Suppression:";
            AimingWalkspeeed.Text = gun.DefaultAimingWalkspeed.ToString();
            c2r9.Text = "Aiming Walkspeed:";
            Caliber.Text = gun.DefaultCaliber.ToString();
            c2r10.Text = "Caliber:";
            TotalAmmo.Text = gun.DefaultAmmoCapacity.ToString();
            c2r11.Text = "Reserve Ammo Capacity:";
            MagazineCapacity.Text = gun.DefaultMagazineCapacity.ToString();
            c2r12.Text = "Magazine Capacity:";
            

        }
        public void passText(Grenade grenade)
        {
            clearText();

            Name.Text = grenade.Name;
            c1r1.Text = "Grenade Name:"; 

            Rank.Text = "Rank " + grenade.Rank.ToString();
            c1r2.Text = "Grenade Rank:";

            Firemodes.Text = grenade.Special?grenade.SpecialMode:"None";
            c1r3.Text = "Trigger Mechanism:";

            limbMultiplier.Text = grenade.Fuse?grenade.FuseTime.ToString():"No fuse";
            c1r4.Text = "Fuse:";
            
            torsoMultiplier.Text = grenade.StoredCapacity.ToString();
            c1r5.Text = "Stored Capacity:";

            headMultiplier.Text = grenade.MaximumDamage.ToString();
            c1r6.Text = "Maximum Damage:";

            walkspeed.Text = grenade.BlastRadius.ToString();
            c1r7.Text = "Blast Radius:";

            Range1.Text = grenade.KillRadius.ToString();
            c1r8.Text = "Killing Radius:";


        }
        public void clearText()
        {
            Name.Text = "";
            Rank.Text = "";
            Firemodes.Text = "";
            limbMultiplier.Text = "";
            headMultiplier.Text = "";
            torsoMultiplier.Text = "";
            walkspeed.Text = "";
            Range1.Text = "";
            Range2.Text = "";
            Range1Damage.Text = "";
            Range2Damage.Text = "";

            Penetration.Text = "";
            MuzzleVelocity.Text = "";
            ReloadSpeed.Text = "";
            EmptyReloadSpeed.Text = "";
            Suppression.Text = "";
            AimingWalkspeeed.Text = "";
            Caliber.Text = "";
            TotalAmmo.Text = "";
            MagazineCapacity.Text = "";

            c1r1.Text = "";
            c1r2.Text = "";
            c1r3.Text = "";
            c1r4.Text = "";
            c1r5.Text = "";
            c1r6.Text = "";
            c1r7.Text = "";
            c1r8.Text = "";
            c1r9.Text = "";
            c1r10.Text = "";
            c1r11.Text = "";

            c2r4.Text = "";
            c2r5.Text = "";
            c2r6.Text = "";
            c2r7.Text = "";
            c2r8.Text = "";
            c2r9.Text = "";
            c2r10.Text = "";
            c2r11.Text = "";
            c2r12.Text = "";
        }

        public void passText(Melee melee)
        {
            clearText();
            Name.Text = melee.Name;
            c1r1.Text = "Gun Name:";

            Rank.Text = "Rank " + melee.Rank.ToString();
            c1r2.Text = "Gun Rank:";

            Firemodes.Text = melee.BladeLength.ToString();
            c1r3.Text = "Blade Length:";

            limbMultiplier.Text = melee.CarriedAttributes.LimbMultiplier.ToString();
            c1r4.Text = "Limb Multiplier:";

            headMultiplier.Text = melee.CarriedAttributes.HeadMultiplier.ToString();
            c1r6.Text = "Head Multiplier:";

            torsoMultiplier.Text = melee.CarriedAttributes.TorsoMultiplier.ToString();
            c1r5.Text = "Torso Multiplier:";

            walkspeed.Text = melee.CarriedAttributes.WalkSpeed.ToString();
            c1r7.Text = "Weapon Walkspeed:";


            Range1.Text = melee.FrontStabDamage.ToString();
            c1r8.Text = "Front Stab Damage:";

            Range2.Text = melee.BackStabDamage.ToString();
            c1r9.Text = "Back Stab Damage:";
        }
    }
}
/*<TextBlock Name="c1r1" VerticalAlignment="Top" Height="18" Margin="0,0,0,0">Weapon:</TextBlock>
        <TextBlock Name ="c1r2" VerticalAlignment="Top" Height="18" Margin="0,30,0,0">Rank:</TextBlock>
        <TextBlock Name="c1r3" VerticalAlignment="Top" Height="18" Margin="0,60,0,0">FireModes:</TextBlock>
        <TextBlock Name="c1r4" VerticalAlignment="Top" Height="18" Margin="0,90,0,0">Limb Multiplier:</TextBlock>
        <TextBlock Name="c1r5" VerticalAlignment="Top" Height="18" Margin="0,120,0,0">Torso Multiplier:</TextBlock>
        <TextBlock Name="c1r6" VerticalAlignment="Top" Height="18" Margin="0,150,0,0">Head Multiplier:</TextBlock>
        <TextBlock Name="c1r7" VerticalAlignment="Top" Height="18" Margin="0,180,0,0">Walkspeed:</TextBlock>
        <TextBlock Name="c1r8" VerticalAlignment="Top" Height="18" Margin="0,210,0,0">Range 1:</TextBlock>
        <TextBlock Name="c1r9" VerticalAlignment="Top" Height="18" Margin="0,240,0,0">Range 2:</TextBlock>
        <TextBlock Name="c1r10" VerticalAlignment="Top" Height="18" Margin="0,270,0,0">Range 1 Damage:</TextBlock>
        <TextBlock Name="c1r11" VerticalAlignment="Top" Height="18" Margin="0,300,0,0">Range 2 Damage:</TextBlock>
        <TextBlock VerticalAlignment="Top" Name="Name" Height="18" Margin="120,0,0,0">Example Gun</TextBlock>
        <TextBlock VerticalAlignment="Top" Name="Rank" Height="18" Margin="120,30,0,0">Example Rank</TextBlock>
        <TextBlock VerticalAlignment="Top" Name="Firemodes" Height="18" Margin="120,60,0,0" TextWrapping="Wrap">Example Firemode</TextBlock>
        <TextBlock VerticalAlignment="Top" Name="limbMultiplier" Height="18" Margin="120,90,0,0">Example lM</TextBlock>
        <TextBlock VerticalAlignment="Top" Name="torsoMultiplier" Height="18" Margin="120,120,0,0">Example tM</TextBlock>
        <TextBlock VerticalAlignment="Top" Name="headMultiplier" Height="18" Margin="120,150,0,0">Example hM</TextBlock>
        <TextBlock VerticalAlignment="Top" Name="walkspeed" Height="18" Margin="120,180,0,0">Example ws</TextBlock>
        <TextBlock VerticalAlignment="Top" Name="Range1" Height="18" Margin="120,210,0,0">Example range1</TextBlock>
        <TextBlock VerticalAlignment="Top" Name="Range2" Height="18" Margin="120,240,0,0">Example range2</TextBlock>
        <TextBlock VerticalAlignment="Top" Name="Range1Damage" Height="18" Margin="120,270,0,0">Example range1damage</TextBlock>
        <TextBlock VerticalAlignment="Top" Name="Range2Damage" Height="18" Margin="120,300,0,0">Example range2damage</TextBlock>

        <TextBlock Name="c2r4" VerticalAlignment="Top" Height="18" Margin="300,90,0,0">Penetration:</TextBlock>
        <TextBlock Name="c2r5" VerticalAlignment="Top" Height="18" Margin="300,120,0,0">Muzzle Velocity:</TextBlock>
        <TextBlock Name="c2r6" VerticalAlignment="Top" Height="18" Margin="300,150,0,0">Reload speed:</TextBlock>
        <TextBlock Name="c2r7" VerticalAlignment="Top" Height="18" Margin="300,180,0,0">Empty reload speed:</TextBlock>
        <TextBlock Name="c2r8" VerticalAlignment="Top" Height="18" Margin="300,210,0,0">Suppression:</TextBlock>
        <TextBlock Name="c2r9" VerticalAlignment="Top" Height="18" Margin="300,240,0,0">Aiming Walkspeed:</TextBlock>
        <TextBlock Name="c2r10" VerticalAlignment="Top" Height="18" Margin="300,270,0,0">Caliber:</TextBlock>
        <TextBlock Name="c2r11" VerticalAlignment="Top" Height="18" Margin="300,300,0,0">Total Ammo:</TextBlock>
        <TextBlock Name="c2r12" VerticalAlignment="Top" Height="18" Margin="300,330,0,0">Magazine Capacity:</TextBlock>*/