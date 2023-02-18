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
        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            List<Class> classes = new();
            
            //DDataAccess BDA = new DDataAccess("Server=LAPTOP-FGMJ15RD;Database=Nephtalima;Trusted_Connection=True;MultipleActiveResultSets=true");
            //BaseDataAccess.GetConnection();
            //Weapon gun = new Gun("FAL Para Shorty", true, 98, "7.62x45mm", 140, 20, new string[] {"a600","s600"}, 1.8, 1800, 15, 13, 14);
            FireModeList blah = new(new string[] { "automatic400agtcfc666cygcyft787656", "burst600b0", "s856la0", "s200sh8", "a 340" });
            blah.addFireMode(FireModeList.ParseFireModeString("a500" ));
            Gun AN94 = new("AN94", true, 10, new Conversion("5.45x39mm", 150, 30, new string[] { "a600", "b1800bb0", "s600" }, new Carried(1.00, 1.00, 1.40, 14.00), new Ranged(50, 120, 30, 23), 1.00, 2500.00, 2.5, 3.2, 0.5), 8.4, true, true);
            AN94.Conversions.addConversion(new Conversion(AN94.Name,"7.62x39mm", "AN94 7.62x39mm", true, "7.62x39mm", 150, 30, AN94.FireModes, AN94.CarriedAttributes, new Ranged(50, 150, 38, 21), 1.50, 2000.00, 2.5, 3.2, 0.6));
            //AN94.Conversions.addConversion(new Conversion(true, false, AN94.CarriedAttributes, AN94.RangedAttributes, AN94.Penetration, AN94.Suppression));
            //AN94.Conversions.addConversion(new Conversion(true, false, AN94));
            //AN94.Conversions.addConversion(new Conversion(false, true, AN94));
            Gun G36 = new("G36", true, 25, new Conversion("5.56x45mm", 150, 30, new string[] { "a750", "b750bb0", "s750" }, new Carried(1.00, 1.00, 1.40, 14.00), new Ranged(80, 160, 31, 23), 1.30, 2700.00, 2.6, 3.4, 0.35), 8.4, true, true);
            G36.Conversions.addConversion(new Conversion(G36.Name,".300 BLK", "T36", true, "300 AAC Blackout", 150, 30, new string[] { "a750", "s750" }, G36.CarriedAttributes, new Ranged(45, 160, 31, 20.13), 1.30, 2000.00, 2.6, 3.4, 1.00));
            G36.Conversions.DefaultConversion.CarriedAttributes.LimbMultiplier = 1;
            //Gun.lethalityCalculator(AN94.Conversions.DefaultConversion.RangedAttributes, true, false, false);
            double gg = Gun.lethalityCalculatorGeneral(G36.RangedAttributes, G36.Conversions.DefaultConversion.CarriedAttributes.LimbMultiplier, 101);
            int regt = G36.Rank;
            G36.Conversions.DefaultConversion.FireModes.addNewDefaultFiremode(FireModeList.ParseFireModeString("a399"));
            Category AssaultRifles = new Category(G36);
            AssaultRifles.addWeapon(AN94);
            Gun AK103 = new("AK103", true, 103, new Conversion("7.62x39mm", 150, 30, new string[] { "a600", "b600bbb", "s600" }, new Carried(1.00, 1.00, 1.40, 14.00), new Ranged(60, 170, 37, 23), 1.40, 2000.00, 2.5, 3.3, 0.5),8.4,true,true);
            AssaultRifles.addWeapon(AK103);
            Gun KRISS_VECTOR = new("Kriss Vector", true, 100, new Conversion("9x19mm", 150, 25, new string[] { "a1200", "s1200", "b1200bb" }, new Carried(1.00, 1.00, 1.40, 14.00), new Ranged(30, 100, 34, 16), 0.50, 1700.00, 2.3, 3.2, 0.25), 10.5, true, true);
            Category PDWs = new(KRISS_VECTOR);
            AssaultRifles.addWeapon(KRISS_VECTOR);
            double an94damage = Ranged.damageFunction(AN94.RangedAttributes, 101);
            AssaultRifles.removeWeaponByRank(100);
            int ak103id = AssaultRifles.IDLookup(AK103);
            Class Assault = new("Assault");
            Assault.addCategory(AssaultRifles);

            
            
            //classes = SQLiteDataAccess.loadClass();

            //SQLiteDataAccess.saveClass(Assault);
        }
    }
}
