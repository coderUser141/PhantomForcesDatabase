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

namespace NETCoreWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Gun G36;
        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            G36 = new("G36", true, 25, new Conversion("5.56x45mm", 150, 30, new string[] { "a750", "b750bb0", "s750" }, new Carried(1.00, 1.00, 1.40, 14.00), new Ranged(80, 160, 31, 23), 1.30, 2700.00, 2.6, 3.4, 0.35, 8.4), true, true);
            G36.Conversions.addConversion(new Conversion(G36.Name, ".300 BLK", "T36", true, "300 AAC Blackout", 150, 30, new string[] { "a750", "s750" }, G36.DefaultCarriedAttributes, new Ranged(45, 160, 31, 20.13), 1.30, 2000.00, 2.6, 3.4, 1.00, G36.DefaultAimingWalkspeed));

            DataContext = this;


            //declare GUI lists
            carrieds = new ObservableCollection<string>();
            rangeds = new ObservableCollection<string>();
            conversions = new ObservableCollection<string>();




            InitializeComponent();


        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Carrieds.Add(GUIWeaponClass.carriedGUI(G36.Conversions.DefaultConversion.CarriedAttributes));
            Carrieds.Add(GUIWeaponClass.carriedGUI(G36.Conversions[1].CarriedAttributes));
            Carrieds.Add(GUIWeaponClass.carriedGUI(G36.Conversions[2].CarriedAttributes));


            Rangeds.Add(GUIWeaponClass.rangedGUI(G36.Conversions.DefaultConversion.RangedAttributes));
            Rangeds.Add(GUIWeaponClass.rangedGUI(G36.Conversions[1].RangedAttributes));
            Rangeds.Add(GUIWeaponClass.rangedGUI(G36.Conversions[2].RangedAttributes));

            Conversions.Add(GUIWeaponClass.conversionGUI(G36.Conversions.DefaultConversion));
            Conversions.Add(GUIWeaponClass.conversionGUI(G36.Conversions[1]));
            Conversions.Add(GUIWeaponClass.conversionGUI(G36.Conversions[2]));
        }
    }
}
