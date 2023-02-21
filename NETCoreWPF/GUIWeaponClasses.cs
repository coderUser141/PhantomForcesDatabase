using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NETCoreWPF
{
    
    public partial class MainWindow
    {

        private ObservableCollection<string> rangeds;

        public ObservableCollection<string> Rangeds
        {
            get { return rangeds; }
            set { rangeds = value; }
        }

        private ObservableCollection<string> conversions;

        public ObservableCollection<string> Conversions
        {
            get { return conversions; }
            set { conversions = value; }
        }


        private ObservableCollection<string> carrieds;

        public ObservableCollection<string> Carrieds
        {
            get { return carrieds; }
            set { carrieds = value; }
        }


    }


    public class GUIWeaponClass
    {
        public static string carriedGUI(Carried carried)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(carried.LimbMultiplier.ToString());
            stringBuilder.Append(", ");
            stringBuilder.Append(carried.TorsoMultiplier.ToString());
            stringBuilder.Append(", ");
            stringBuilder.Append(carried.HeadMultiplier.ToString());
            stringBuilder.Append(", ");
            stringBuilder.Append(carried.WalkSpeed.ToString());
            return stringBuilder.ToString();

        }

        public static string rangedGUI(Ranged ranged)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(ranged.Range1.ToString());
            stringBuilder.Append(", ");
            stringBuilder.Append(ranged.Range2.ToString());
            stringBuilder.Append(", ");
            stringBuilder.Append(ranged.Range1Damage.ToString());
            stringBuilder.Append(", ");
            stringBuilder.Append(ranged.Range2Damage.ToString());
            return stringBuilder.ToString();

        }

        public static string conversionGUI(Conversion conversion)
        {
            StringBuilder stringBuilder = new StringBuilder();
            //stringBuilder.Append()
            return stringBuilder.ToString();
        }


    }



}
