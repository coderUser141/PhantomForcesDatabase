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
        }
    }
}
