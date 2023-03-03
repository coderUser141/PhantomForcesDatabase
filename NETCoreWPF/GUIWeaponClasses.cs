using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NETCoreWPF
{
    
    public partial class MainWindow
    {
        /*
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
        */
        private ObservableCollection<string> categories;
        private ObservableCollection<Category> categoryObjects;
        private ObservableCollection<string> guns;
        private ObservableCollection<Gun> gunObjects;

        /// <summary>
        /// <c>Gun</c> objects that are passed to the main display panel
        /// </summary>
        public ObservableCollection<Gun> GunObjects
        {
            get { return gunObjects; }
            set { gunObjects = value; }
        }

        /// <summary>
        /// Gun names displayed in list
        /// </summary>
        public ObservableCollection<string> Guns
        {
            get { return guns; }
            set { guns = value; }
        }

        public ObservableCollection<Category> CategoryObjects
        {
            get { return categoryObjects; }
            set { categoryObjects = value; }
        }

        public ObservableCollection<string> Categories
        {
            get { return categories; }
            set { categories = value; }
        }



    }

    /// <summary>
    /// Defines a class for useful GUI methods to display from objects to strings 
    /// </summary>
    public class GUIWeaponClass
    {

        /// <summary>
        /// Converts a <c>Carried</c> object into a string. 
        /// </summary>
        /// <param name="carried">The <c>Carried</c> object to be passed.</param>
        /// <returns>String in format: <c>(limbMultiplier, torsoMultiplier, headMultiplier, walkspeed)</c> </returns>
        public static string carriedGUI(Carried carried)
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append(carried.LimbMultiplier);
            stringBuilder.Append(", ");
            stringBuilder.Append(carried.TorsoMultiplier);
            stringBuilder.Append(", ");
            stringBuilder.Append(carried.HeadMultiplier);
            stringBuilder.Append(", ");
            stringBuilder.Append(carried.WalkSpeed);
            return stringBuilder.ToString();

        }

        /// <summary>
        /// Converts a <c>Ranged</c> object into a string. 
        /// </summary>
        /// <param name="ranged">The <c>Ranged</c> object to be passed.</param>
        /// <returns>String in format: <c>(Range1, Range2, Range1Damage, Range2Damage)</c> </returns>
        public static string rangedGUI(Ranged ranged)
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append(ranged.Range1);
            stringBuilder.Append(", ");
            stringBuilder.Append(ranged.Range2);
            stringBuilder.Append(", ");
            stringBuilder.Append(ranged.Range1Damage);
            stringBuilder.Append(", ");
            stringBuilder.Append(ranged.Range2Damage);
            return stringBuilder.ToString();

        }

        /// <summary>
        /// Converts a <c>FireModeList</c> object into a list of strings. 
        /// </summary>
        /// <param name="firemodes">The <c>FireModeList</c> object to be passed.</param>
        /// <returns> List of strings containing firemode data. See <seealso cref="firemodelistJoiner(List{string})"/> </returns>
        public static List<string> firemodeGUI(FireModeList firemodes)
        {
            List<string> stringList = new();
            foreach(FireMode fireMode in firemodes.getFireModes()) {
                StringBuilder stringBuilder = new();
                stringBuilder.Append(fireMode.Mode);
                stringBuilder.Append(", ");
                stringBuilder.Append(fireMode.Firerate);
                stringBuilder.Append(", ");
                stringBuilder.Append(fireMode.SpecialMode);
                stringBuilder.Append(", ");
                stringBuilder.Append(fireMode.BurstMode);
                stringBuilder.Append(", ");
                stringBuilder.Append(fireMode.Pellets);
                stringList.Add(stringBuilder.ToString());
            }
            return stringList;
        }

        /// <summary>
        /// Joins a list of strings from <see cref="firemodeGUI(FireModeList)"/> into a single string.
        /// </summary>
        /// <param name="strings">The list of strings.</param>
        /// <returns>A string with all firemodes.</returns>
        public static string firemodelistJoiner(List<string> strings)
        {
            StringBuilder stringBuilder = new();
            foreach (string firemode in strings)
            {
                stringBuilder.Append(firemode);
                stringBuilder.Append("/ ");
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Converts a <c>Conversion</c> object into a string.
        /// </summary>
        /// <param name="conversion">The <c>Conversion</c> object to be passed.</param>
        /// <param name="defaultConversion">If the preceding value is a default conversion.</param>
        /// <returns>A string with all of the <c>Conversion</c> object's data.</returns>
        public static string conversionGUI(Conversion conversion, bool defaultConversion)
        {
            StringBuilder stringBuilder = new();
            bool addLabels = true;
            stringBuilder.Append(addLabels ? "FireModes: " : " ");
            stringBuilder.Append(firemodelistJoiner(firemodeGUI(conversion.FireModes)));
            stringBuilder.Append("; ");

            stringBuilder.Append(addLabels ? "Carried: " : " ");
            stringBuilder.Append(carriedGUI(conversion.CarriedAttributes));
            stringBuilder.Append("; ");

            stringBuilder.Append(addLabels ? "Ranged: " : " ");
            stringBuilder.Append(rangedGUI(conversion.RangedAttributes));
            stringBuilder.Append("; ");

            stringBuilder.Append(addLabels ? "Caliber: " : " ");
            stringBuilder.Append(conversion.Caliber);
            stringBuilder.Append("; ");

            stringBuilder.Append(addLabels ? "Ammo Capacity: " : " ");
            stringBuilder.Append(conversion.AmmoCapacity);
            stringBuilder.Append("; ");

            stringBuilder.Append(addLabels ? "Magazine Capacity: " : " ");
            stringBuilder.Append(conversion.MagazineCapacity);
            stringBuilder.Append("; ");

            stringBuilder.Append(addLabels ? "Penetration: " : " ");
            stringBuilder.Append(conversion.Penetration);
            stringBuilder.Append("; ");

            stringBuilder.Append(addLabels ? "Aiming Walkspeed: " : " ");
            stringBuilder.Append(conversion.AimingWalkspeed);
            stringBuilder.Append("; ");

            stringBuilder.Append(addLabels ? "Reload Speed: " : " ");
            stringBuilder.Append(conversion.ReloadSpeed);
            stringBuilder.Append("; ");

            stringBuilder.Append(addLabels ? "Empty Reload Speed: " : " ");
            stringBuilder.Append(conversion.EmptyReloadSpeed);
            stringBuilder.Append("; ");

            stringBuilder.Append(addLabels ? "Suppression: " : " ");
            stringBuilder.Append(conversion.Suppression);
            stringBuilder.Append("; ");

            stringBuilder.Append(addLabels ? "Attachment: " : " ");
            stringBuilder.Append(defaultConversion ? " " : conversion.Attachment);
            stringBuilder.Append("; ");

            stringBuilder.Append(addLabels ? "Conversion Name: " : " ");
            stringBuilder.Append(defaultConversion ? " " : conversion.ConversionName);
            stringBuilder.Append("; ");

            stringBuilder.Append(addLabels ? "Ammo Conversion: " : " ");
            stringBuilder.Append(defaultConversion ? " " : conversion.AmmoConversion);
            stringBuilder.Append("; ");

            //stringBuilder.Append()
            return stringBuilder.ToString();
        }


    }



}
