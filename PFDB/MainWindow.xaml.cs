using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using WeaponClasses;
using FileProcessingParsingReading;
using PFDB.User_Controls;
using System;
using System.Reflection;
using System.Diagnostics.Metrics;
using System.Windows.Documents;
using System.Threading;
//using FileReader;

namespace PFDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private double version = 1.00;
        private string dbversion = "100";

        public Dictionary<FileReading.Classes, Class> classpairs;

        private FileReading.BuildOptions gunlistsend = FileReading.BuildOptions.NONE;

        private ObservableCollection<string> categories;
        private ObservableCollection<Category> categoryObjects;
        private ObservableCollection<string> weapons;
        private ObservableCollection<Weapon> weaponObjects;

        private Class classflag = new(null,"");

        /// <summary>
        /// <c>Weapon</c> objects that are passed to the main display panel
        /// </summary>
        public ObservableCollection<Weapon> WeaponObjects { get { return weaponObjects; } set { weaponObjects = value; } }
        

        /// <summary>
        /// Weapon names displayed in list
        /// </summary>
        public ObservableCollection<string> Weapons { get { return weapons; } set { weapons = value; } }

        /// <summary>
        /// <c>Category</c> objects that are passed to the main display panel
        /// </summary>
        public ObservableCollection<Category> CategoryObjects{ get { return categoryObjects; } set{ categoryObjects = value; } }

        /// <summary>
        /// Category names displayed in a list
        /// </summary>
        public ObservableCollection<string> Categories { get { return categories; } set { categories = value; } }
        

        private void GunList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (GunList.SelectedIndex < 0)
            {
                //do nothing
            }
            else
            {
                switch (gunlistsend)
                {
                    case FileReading.BuildOptions.FGS or FileReading.BuildOptions.IGS or FileReading.BuildOptions.HEGS:
                        {
                            DefaultConversionButton.IsEnabled = false;
                            APConversionButton.IsEnabled = false;
                            HPConversionButton.IsEnabled = false;
                            StatsDisplay.passText((Grenade)weaponObjects[GunList.SelectedIndex]);
                            break;
                        }
                    case FileReading.BuildOptions.OHBT or FileReading.BuildOptions.OHBE or FileReading.BuildOptions.THBE or FileReading.BuildOptions.THBT:
                        {
                            DefaultConversionButton.IsEnabled = false;
                            APConversionButton.IsEnabled = false;
                            HPConversionButton.IsEnabled = false;
                            StatsDisplay.passText((Melee)weaponObjects[GunList.SelectedIndex]);
                            break;
                        }
                    case FileReading.BuildOptions.NONE:
                        {
                            break;
                        }
                    default: //guns
                        {
                            DefaultConversionButton.IsEnabled = true;
                            APConversionButton.IsEnabled = true;
                            HPConversionButton.IsEnabled = true;

                            Gun gun = (Gun)weaponObjects[GunList.SelectedIndex];
                            if (DefaultConversionButton.IsChecked == true)
                            {
                                StatsDisplay.passText(gun, gun.Conversions.DefaultConversion);
                            }
                            else if (APConversionButton.IsChecked == true)
                            {
                                StatsDisplay.passText(gun, new(true, false, gun));
                            }
                            else if (HPConversionButton.IsChecked == true)
                            {
                                StatsDisplay.passText(gun, new(false, true, gun));
                            }
                            break;
                        }
                }
            }
        }

        private void ConversionButton_Click(object sender, RoutedEventArgs e)
        {
            if (GunList.SelectedIndex < 0)
            {
                //do nothing
            }
            else
            {
                Gun gun = (Gun)weaponObjects[GunList.SelectedIndex];
                if (DefaultConversionButton.IsChecked == true)
                {
                    StatsDisplay.passText(gun, gun.Conversions.DefaultConversion);
                }
                else if (APConversionButton.IsChecked == true)
                {
                    StatsDisplay.passText(gun, new(true, false, gun));
                }
                else if (HPConversionButton.IsChecked == true)
                {
                    StatsDisplay.passText(gun, new(false, true, gun));
                }
            }
        }

        private void CategoryList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (CategoryList.SelectedIndex < 0)
            {
                //do nothing
            }
            else
            {
                int index = CategoryList.SelectedIndex; //gets the selected index
                FileReading.BuildOptions build = CategoryFinder(index);
                gunList(CategoryObjects[CategoryList.SelectedIndex], build);
            }
        }

        private void Assault_Click(object sender, RoutedEventArgs e)
        {
            listUpdater(FileReading.Classes.Assault);
        }

        private void Scout_Click(object sender, RoutedEventArgs e)
        {

            listUpdater(FileReading.Classes.Scout);
        }

        private void Support_Click(object sender, RoutedEventArgs e)
        {

            listUpdater(FileReading.Classes.Support);
        }

        private void Recon_Click(object sender, RoutedEventArgs e)
        {

            listUpdater(FileReading.Classes.Recon);
        }

        private void Melees_Click(object sender, RoutedEventArgs e)
        {

            listUpdater(FileReading.Classes.Melees);
        }

        private void Grenades_Click(object sender, RoutedEventArgs e)
        {

            listUpdater(FileReading.Classes.Grenades);
        }

        private void Secondary_Click(object sender, RoutedEventArgs e)
        {
            listUpdater(FileReading.Classes.Secondary);

        }

        private void Version_Click(object sender, RoutedEventArgs e)
        {
            VersionUpdater();
            if(Version100Button?.IsChecked == true)
            {
                version = 1.00;
            }else if(Version101Button?.IsChecked==true)
            {
                version = 1.01;
            }

            weapons.Clear();
            weaponObjects.Clear();
            categories.Clear();
            categoryObjects.Clear();
            //StatsDisplay.clearText();
            VersionUpdater();
            Dictionary<FileReading.Classes, Class> cl = new();
            SQLConnectionHandling connectionHandling = new();
            Action<FileReading.Classes> action = (a) => { cl.Add(a, connectionHandling.GetSQLClass(a, dbversion)); };
            action(FileReading.Classes.Assault);
            action(FileReading.Classes.Scout);
            action(FileReading.Classes.Support);
            action(FileReading.Classes.Recon);
            action(FileReading.Classes.Secondary);
            action(FileReading.Classes.Grenades);
            action(FileReading.Classes.Melees);
            classpairs = cl;
            VersionDisplay.Text = version.ToString();
        }

        private static void ShowMessage()
        {
            MessageBox.Show("Please wait while the database loads the data...");
        }

        private void VersionUpdater()
        {
            if(version == 1.00)
            {
                dbversion = "100";
            }else if(version == 1.01)
            {
                dbversion = "101";
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow() 
        {
            Thread T1 = new(() => ShowMessage());
            T1.Name = "T1";
            T1.Start();
            Dictionary<FileReading.Classes, Class> cl = new();
            SQLConnectionHandling connectionHandling = new();
            VersionUpdater();
            Action<FileReading.Classes> action = (a) => { cl.Add(a, connectionHandling.GetSQLClass(a, dbversion)); };
            action(FileReading.Classes.Assault);
            action(FileReading.Classes.Scout);
            action(FileReading.Classes.Support);
            action(FileReading.Classes.Recon);
            action(FileReading.Classes.Secondary);
            action(FileReading.Classes.Grenades);
            action(FileReading.Classes.Melees);
            classpairs = cl;

            DataContext = this;


            //declare GUI lists

            //carrieds = new ObservableCollection<string>();
            //rangeds = new ObservableCollection<string>();
            //conversions = new ObservableCollection<string>();
            weapons = new();
            weaponObjects = new();
            categoryObjects = new();
            categories = new();


            InitializeComponent();

            VersionDisplay.Text = version.ToString();

        }


        


        //adds the guns in the category to the gunlist
        private void gunList(Category category, FileReading.BuildOptions buildOptions)
        {
            Weapons.Clear();
            WeaponObjects.Clear();

            switch (buildOptions)
            {
                case FileReading.BuildOptions.NONE:
                    gunlistsend = FileReading.BuildOptions.NONE;
                    break;
                case FileReading.BuildOptions.ARS:
                    gunlistsend = FileReading.BuildOptions.ARS;
                    break;
                case FileReading.BuildOptions.PDWS:
                    gunlistsend = FileReading.BuildOptions.PDWS;
                    break;
                case FileReading.BuildOptions.LMGS:
                    gunlistsend = FileReading.BuildOptions.LMGS;
                    break;
                case FileReading.BuildOptions.SRS:
                    gunlistsend = FileReading.BuildOptions.SRS;
                    break;
                case FileReading.BuildOptions.BRS:
                    gunlistsend = FileReading.BuildOptions.BRS;
                    break;
                case FileReading.BuildOptions.CAS:
                    gunlistsend = FileReading.BuildOptions.CAS;
                    break;
                case FileReading.BuildOptions.DMRS:
                    gunlistsend = FileReading.BuildOptions.DMRS;
                    break;
                case FileReading.BuildOptions.SHS:
                    gunlistsend = FileReading.BuildOptions.SHS;
                    break;
                case FileReading.BuildOptions.PS:
                    gunlistsend = FileReading.BuildOptions.PS;
                    break;
                case FileReading.BuildOptions.MPS:
                    gunlistsend = FileReading.BuildOptions.MPS;
                    break;
                case FileReading.BuildOptions.RES:
                    gunlistsend = FileReading.BuildOptions.RES;
                    break;
                case FileReading.BuildOptions.OTH:
                    gunlistsend = FileReading.BuildOptions.OTH;
                    break;
                case FileReading.BuildOptions.FGS:
                    gunlistsend = FileReading.BuildOptions.FGS;
                    break;
                case FileReading.BuildOptions.IGS:
                    gunlistsend = FileReading.BuildOptions.IGS;
                    break;
                case FileReading.BuildOptions.HEGS:
                    gunlistsend = FileReading.BuildOptions.HEGS;
                    break;
                case FileReading.BuildOptions.OHBT:
                    gunlistsend = FileReading.BuildOptions.OHBT;
                    break;
                case FileReading.BuildOptions.OHBE:
                    gunlistsend = FileReading.BuildOptions.OHBE;
                    break;
                case FileReading.BuildOptions.THBE:
                    gunlistsend = FileReading.BuildOptions.THBE;
                    break;
                case FileReading.BuildOptions.THBT:
                    gunlistsend = FileReading.BuildOptions.THBT;
                    break;
            }
            foreach(Weapon weapon in category.getWeapons())
            {
                addWeapon(weapon);
            }
            
            
        }

        //adds to the observable collection
        private void addWeapon(Weapon gun)
        {
            Weapons.Add(gun.Name);
            WeaponObjects.Add(gun);
        }

        private void categoryList(Class classes) { 
            Categories.Clear(); //clears the category names
            CategoryObjects.Clear(); //clears the category objects
            foreach (Category category in classes.getCategoryList())
            {
                addCategory(category); //adds a category according to the class
            }
        }


        private void addCategory(Category category)
        {
            
            Categories.Add(category.Name);
            CategoryObjects.Add(category);
        }

        
        public FileReading.BuildOptions CategoryFinder(int index)
        {
            FileReading.BuildOptions options = FileReading.BuildOptions.NONE;
            foreach (FileReading.BuildOptions b in SQLConnectionHandling.CategoryNames.Keys) //goes through the list of categories and their buildoptions
            {
                if (categories[index].Contains(SQLConnectionHandling.CategoryNames[b])) //if the selected category matches one of the categories in the list, assign options to the key
                {
                    options = b; break;
                }
            }
            return options;
        }
        private void listUpdater(FileReading.Classes classes) //called when a category button is clicked
        {
            switch (classes)
            {
                case FileReading.Classes.Assault:
                    {
                        categoryList(classpairs[FileReading.Classes.Assault]);
                        break;
                    }
                case FileReading.Classes.Scout:
                    {
                        categoryList(classpairs[FileReading.Classes.Scout]);
                        break;
                    }
                case FileReading.Classes.Support:
                    {
                        categoryList(classpairs[FileReading.Classes.Support]);
                        break;
                    }
                case FileReading.Classes.Recon:
                    {
                        categoryList(classpairs[FileReading.Classes.Recon]);
                        break;
                    }
                case FileReading.Classes.Melees:
                    {
                        categoryList(classpairs[FileReading.Classes.Melees]);
                        break;
                    }
                case FileReading.Classes.Grenades:
                    {
                        categoryList(classpairs[FileReading.Classes.Grenades]);
                        break;
                    }
                case FileReading.Classes.Secondary:
                    {
                        categoryList(classpairs[FileReading.Classes.Secondary]);
                        break;
                    }
            }
        }

        
    }
}

//button gets clicked
//handler adds the categories listed inside the Class list
//function iterates through the categories listed
//categories listed then add their weapon list along with the type of weapons