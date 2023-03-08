using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeaponClasses;

namespace PFDB.User_Controls
{
    /// <summary>
    /// Interaction logic for MainStatDisplay.xaml
    /// </summary>
    public partial class MainStatDisplay : UserControl
    {
        public MainStatDisplay()
        {
            InitializeComponent();
        }


        public void passText(Gun gun)
        {
            Name.Text = gun.Name;
            Rank.Text = "Rank " + gun.Rank.ToString();
            Firemodes.Text = GUIWeaponClass.firemodelistJoiner(GUIWeaponClass.firemodeGUI(gun.Conversions.DefaultConversion.FireModes));
            limbMultiplier.Text = gun.DefaultCarriedAttributes.LimbMultiplier.ToString() + "x";
            headMultiplier.Text = gun.DefaultCarriedAttributes.HeadMultiplier.ToString() + "x";
            torsoMultiplier.Text = gun.DefaultCarriedAttributes.TorsoMultiplier.ToString() + "x";
            walkspeed.Text = gun.DefaultCarriedAttributes.WalkSpeed.ToString();
            Range1.Text = gun.DefaultRangedAttributes.Range1.ToString() + " studs";
            Range2.Text = gun.DefaultRangedAttributes.Range2.ToString() + " studs";
            Range1Damage.Text = gun.DefaultRangedAttributes.Range1Damage.ToString();
            Range2Damage.Text = gun.DefaultRangedAttributes.Range2Damage.ToString();

            Penetration.Text = gun.DefaultPenetration == 1 ? gun.DefaultPenetration.ToString() + " stud" : gun.DefaultPenetration.ToString() + " studs";
            MuzzleVelocity.Text = gun.DefaultMuzzleVelocity.ToString() + " studs/s";
            ReloadSpeed.Text = gun.DefaultReloadSpeed.ToString() + " seconds";
            EmptyReloadSpeed.Text = gun.DefaultEmptyReloadSpeed.ToString() + " seconds";
            Suppression.Text = gun.DefaultSuppression.ToString();
            AimingWalkspeeed.Text = gun.DefaultAimingWalkspeed.ToString();
            Caliber.Text = gun.DefaultCaliber.ToString();
            TotalAmmo.Text = gun.DefaultAmmoCapacity.ToString();
            MagazineCapacity.Text = gun.DefaultMagazineCapacity.ToString();

        }

    }
}
