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

namespace NETCoreWPF.User_Controls
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
            Name.Text = gun.Name;
            Rank.Text = gun.Rank.ToString();
            Firemodes.Text = GUIWeaponClass.firemodelistJoiner(GUIWeaponClass.firemodeGUI(gun.Conversions.DefaultConversion.FireModes));
            limbMultiplier.Text = gun.DefaultCarriedAttributes.LimbMultiplier.ToString();
            headMultiplier.Text = gun.DefaultCarriedAttributes.HeadMultiplier.ToString();
            torsoMultiplier.Text = gun.DefaultCarriedAttributes.TorsoMultiplier.ToString();
            walkspeed.Text = gun.DefaultCarriedAttributes.WalkSpeed.ToString();
            Range1.Text = gun.DefaultRangedAttributes.Range1.ToString();
            Range2.Text = gun.DefaultRangedAttributes.Range2.ToString();
            Range1Damage.Text = gun.DefaultRangedAttributes.Range1Damage.ToString();
            Range2Damage.Text = gun.DefaultRangedAttributes.Range2Damage.ToString();
            


        }
    }
}
