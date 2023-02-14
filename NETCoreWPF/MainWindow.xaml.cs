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



namespace NETCoreWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //DDataAccess BDA = new DDataAccess("Server=LAPTOP-FGMJ15RD;Database=Nephtalima;Trusted_Connection=True;MultipleActiveResultSets=true");
            //BaseDataAccess.GetConnection();
            //Weapon gun = new Gun("FAL Para Shorty", true, 98, "7.62x45mm", 140, 20, new string[] {"a600","s600"}, 1.8, 1800, 15, 13, 14);
            FireModeList blah = new FireModeList(new string[] {"automatic400agtcfc666cygcyft787656", "burst600b0", "s856la0", "s200sh8", "a 340"});
            Weapon AN94 = new Gun("AN94", true, 10, "5.45x39mm", 150, 30, new string[] { "a600", "b1800bb", "s600" }, new Carried(1.00, 1.00, 1.40, 14.00), new Ranged(50, 120, 30, 23), 1.00, 2500.00, 8.4, 2.5, 3.2);
            
        }
    }
}
