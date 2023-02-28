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
using System.Data.SQLite;
//using System.Data.SqlClient;
//using FirebirdSql.Data.FirebirdClient;
//using FirebirdSql.Data.Isql;
//using SQLite;
//using SQLitePCL;
using Microsoft.Data.Sqlite;
using System.IO;
using System.Collections.ObjectModel;
using NETCoreWPF.User_Controls;

namespace NETCoreWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Gun G36, M16A4, AS_VAL, SCAR_L, AUG_A1;

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainDisplay.passText(GunObjects[GunList.SelectedIndex]);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            Carried defaultAR = new(1.00, 1.00, 1.40, 14.00);
            G36 = new("G36", true, 25, new Conversion("5.56x45mm", 150, 30, new string[] { "a750", "b750bb0", "s750" }, new Carried(1.00, 1.00, 1.40, 14.00), new Ranged(80, 160, 31, 23), 1.30, 2700.00, 2.6, 3.4, 0.35, 8.4), true, true);
            G36.Conversions.addConversion(new Conversion(G36.Name, ".300 BLK", "T36", true, "300 AAC Blackout", 150, 30, new string[] { "a750", "s750" }, G36.DefaultCarriedAttributes, new Ranged(45, 160, 31, 20.13), 1.30, 2000.00, 2.6, 3.4, 1.00, G36.DefaultAimingWalkspeed));
            M16A4 = new("M16A4", true, 20, new Conversion("5.56x45mm NATO", 150, 30, new string[] { "a680", "s680" }, new Carried(1.00, 1.00, 1.50, 14.00), new Ranged(40, 165, 35, 22), 1.20, 2800.00, 2.0, 2.7, 0.50, 9.8), true, true);
            AS_VAL = new("AS VAL", true, 15, new Conversion("9x39mm SP", 140, 20, new string[] { "a800", "s800" }, new Carried(1.00, 1.00, 1.40, 14.00), new Ranged(60, 95, 34, 20), 1.00, 1500.00, 2.3, 3.0, 0.40, 8.4), true, true);

            Category AssaultRifles = new(G36, "Assault Rifles");




            DataContext = this;


            //declare GUI lists
            //carrieds = new ObservableCollection<string>();
            //rangeds = new ObservableCollection<string>();
            //conversions = new ObservableCollection<string>();
            guns = new ObservableCollection<string>();
            gunObjects = new ObservableCollection<Gun>();


            InitializeComponent();


        }

        private void addGun(Gun gun)
        {
            Guns.Add(gun.Name);
            GunObjects.Add(gun);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            addGun(G36);
            addGun(M16A4);
            addGun(AS_VAL);
            //addGun(SCAR_L);
            //addGun(AUG_A1);

            /*
            Carrieds.Add(GUIWeaponClass.carriedGUI(G36.Conversions.DefaultConversion.CarriedAttributes));
            Carrieds.Add(GUIWeaponClass.carriedGUI(G36.Conversions[1].CarriedAttributes));
            Carrieds.Add(GUIWeaponClass.carriedGUI(G36.Conversions[2].CarriedAttributes));


            Rangeds.Add(GUIWeaponClass.rangedGUI(G36.Conversions.DefaultConversion.RangedAttributes));
            Rangeds.Add(GUIWeaponClass.rangedGUI(G36.Conversions[1].RangedAttributes));
            Rangeds.Add(GUIWeaponClass.rangedGUI(G36.Conversions[2].RangedAttributes));

            Conversions.Add(GUIWeaponClass.conversionGUI(G36.Conversions.DefaultConversion, true));
            Conversions.Add(GUIWeaponClass.conversionGUI(G36.Conversions[1], false));
            Conversions.Add(GUIWeaponClass.conversionGUI(G36.Conversions[2], false));
            */
        }
    }
}
