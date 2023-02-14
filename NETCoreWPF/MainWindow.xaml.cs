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
            Gun AN94 = new("AN94", true, 10, new Conversion("5.45x39mm", 150, 30, new string[] { "a600", "b1800bb0", "s600" }, new Carried(1.00, 1.00, 1.40, 14.00), new Ranged(50, 120, 30, 23), 1.00, 2500.00, 2.5, 3.2, 0.5), 8.4, true, true);
            AN94.Conversions.addConversion(new Conversion("7.62x39mm", "AN94 7.62x39mm", true, "7.62x39mm", 150, 30, AN94.FireModes, AN94.CarriedAttributes, new Ranged(50, 150, 38, 21), 1.50, 2000.00, 2.5, 3.2, 0.6));
            //AN94.Conversions.addConversion(new Conversion(true, false, AN94.CarriedAttributes, AN94.RangedAttributes, AN94.Penetration, AN94.Suppression));
            //AN94.Conversions.addConversion(new Conversion(true, false, AN94));
            //AN94.Conversions.addConversion(new Conversion(false, true, AN94));
            Gun G36 = new("G36", true, 25, new Conversion("5.56x45mm", 150, 30, new string[] { "a750", "b750bb0", "s750" }, new Carried(1.00, 1.00, 1.40, 14.00), new Ranged(80, 160, 31, 23), 1.30, 2700.00, 2.6, 3.4, 0.35), 8.4, true, true);
            G36.Conversions.addConversion(new Conversion())

        }
    }
}
