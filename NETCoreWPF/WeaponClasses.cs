using Azure.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Transactions;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Xml.Linq;
using System.Data.SqlClient;
//using System.Data.SQLite;
//using System.Data.SQLite;


namespace NETCoreWPF
{
    


    /// <summary>
    /// Defines a weapon object.
    /// </summary>
    public class Weapon
    {
        /// <summary>
        /// Name of the weapon.
        /// </summary>
        protected string name;
        /// <summary>
        /// Rank of the weapon.
        /// </summary>
        protected int rank;
        /// <summary>
        /// If the weapon has a rank.
        /// </summary>
        protected bool hasRank;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Name of the weapon.</param>
        /// <param name="hasRank">If the weapon has a rank.</param>
        /// <param name="rank">If the previous parameter is true, the rank of the weapon.</param>
        public Weapon(string name, bool hasRank, int rank)
        {
            this.name = name;
            if (hasRank && rank > 0)
            {
                this.rank = rank;
            } else
            {
                this.rank = 0;
            }
            this.hasRank = hasRank;
        }

        /// <summary>
        /// Name getter/setter.
        /// </summary>
        public string Name { get { return name; } set { name = value; } }
        /// <summary>
        /// Rank getter/setter.
        /// </summary>
        public int Rank { get { return rank; } set { rank = value; } }
        /// <summary>
        /// HasRank getter/setter.
        /// </summary>
        public bool HasRank { get { return hasRank; } set { hasRank = value; } }

        /// <summary>
        /// General calculator for how many hits it takes to kill an enemy.
        /// </summary>
        /// <param name="rangedAttributes">The <see cref="Ranged.Ranged(double, double, double, double)">rangedAttributes</see> of the weapon.</param>
        /// <param name="multiplier">The multiplier associated with the weapon.</param>
        /// <param name="range">The distance to the target.</param>
        /// <returns></returns>
        public static int lethalityCalculatorGeneral(Ranged rangedAttributes, double multiplier, double range)
        {
            if (rangedAttributes.Range2Damage > 0 && range >= 0)
            {
                for (int i = 0; i < 50; i++)
                {
                    double outt = Ranged.damageFunction(rangedAttributes, range) * multiplier * i;
                    if (outt >= 100)
                    {
                        return i;
                    }

                    if (i > 25) return -2;

                }
            }

            return -1;

        }

        /// <summary>
        /// Gun calculator for how many shots it takes to kill an enemy.
        /// </summary>
        /// <param name="conversion">The <c>Conversion</c> object to be used for the calculations.</param>
        /// <param name="range">The distance to the target.</param>
        /// <returns></returns>
        public static Tuple<int, int, int> lethalityCalculator(Conversion conversion, double range)
        {
            int limb = lethalityCalculatorGeneral(conversion.RangedAttributes, conversion.CarriedAttributes.LimbMultiplier, range);
            int torso = lethalityCalculatorGeneral(conversion.RangedAttributes, conversion.CarriedAttributes.TorsoMultiplier, range);
            int head = lethalityCalculatorGeneral(conversion.RangedAttributes, conversion.CarriedAttributes.HeadMultiplier, range);
            return Tuple.Create(limb, torso, head);
        }

    }

    /// <summary>
    /// Creates objects for defining carried weapons' attributes (melees, guns).
    /// </summary>
    public class Carried
    {

        private double limbMultipler,
                            torsoMultiplier,
                            headMultiplier,
                            walkspeed;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="limbMultipler">Limb multiplier of the weapon.</param>
        /// <param name="torsoMultiplier">Torso multiplier of the weapon.</param>
        /// <param name="headMultiplier">Head multiplier of the weapon.</param>
        /// <param name="walkspeed">Walkspeed of the weapon.</param>
        public Carried(double limbMultipler, double torsoMultiplier, double headMultiplier, double walkspeed)
        {
            this.limbMultipler = limbMultipler;
            this.torsoMultiplier = torsoMultiplier;
            this.headMultiplier = headMultiplier;
            this.walkspeed = walkspeed;
        }

        /// <summary>
        /// LimbMultipler getter/setter.
        /// </summary>
        public double LimbMultiplier { get { return limbMultipler; } set { limbMultipler = value; } }
        /// <summary>
        /// TorsoMultiplier getter/setter.
        /// </summary>
        public double TorsoMultiplier { get { return torsoMultiplier; } set { torsoMultiplier = value; } }
        /// <summary>
        /// HeadMultiplier getter/setter.
        /// </summary>
        public double HeadMultiplier { get { return headMultiplier; } set { headMultiplier = value; } }
        /// <summary>
        /// WalkSpeed getter/setter.
        /// </summary>
        public double WalkSpeed { get { return walkspeed; } set { walkspeed = value; } }
    }

    /// <summary>
    /// Creates objects for defining ranged weapons' attributes (grenades, guns).
    /// </summary>
    public class Ranged 
    {
        private double range1,
                        range2,
                        range1Damage,
                        range2Damage;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="range1">Close range in studs.</param>
        /// <param name="range2">Far range in studs.</param>
        /// <param name="range1Damage">Range 1's damage.</param>
        /// <param name="range2Damage">Range 2's damage.</param>
        public Ranged(double range1, double range2, double range1Damage, double range2Damage)
        {
            this.range1 = range1;
            this.range2 = range2;
            this.range1Damage = range1Damage;
            this.range2Damage = range2Damage;
        }

        /// <summary>
        /// Damage for a given weapon and range.
        /// </summary>
        /// <param name="weapon">The weapon to be used for calculations.</param>
        /// <param name="range">The distance to the target.</param>
        /// <returns></returns>
        public static double damageFunction(Ranged weapon, double range)
        {
            double range1 = weapon.Range1;
            double range2 = weapon.Range2;
            double range1damage = weapon.Range1Damage;
            double range2damage = weapon.Range2Damage;

            if (range <= range1)
            {
                return range1damage;
            }
            else if (range >= range2)
            {
                return range2damage;
            }
            else if (range <= range2 && range >= range1)
            {
                double slope = (range2damage - range1damage) / (range2 - range1);
                double yintercept = range2damage - (range2 * slope);
                return range * slope + yintercept;
            }
            else
            {
                return -1;
            }
            //gun.RangedAttributes.Range1Damage
        }

        /// <summary>
        /// Range1 getter/setter.
        /// </summary>
        public double Range1 { get { return range1; } set { range1 = value; } }
        /// <summary>
        /// Range2 getter/setter.
        /// </summary>
        public double Range2 { get { return range2; } set { range2 = value; } }
        /// <summary>
        /// DamageRange1 getter/setter.
        /// </summary>
        public double Range1Damage { get { return range1Damage; } set { range1Damage = value; } }
        /// <summary>
        /// DamageRange2 getter/setter.
        /// </summary>
        public double Range2Damage { get { return range2Damage; } set { range2Damage = value; } }

    }

    /// <summary>
    /// Class defining a list of <c>FireMode</c> objects.
    /// </summary>
    public class FireModeList
    {
        private List<FireMode> modes = new List<FireMode>();

        /// <summary>
        /// <c>FireModeList</c> indexer
        /// </summary>
        /// <param name="index">Index of the <c>FireModeList</c>.</param>
        /// <returns></returns>
        public FireMode this[int index]
        {
            get { return modes[index]; }
            set { modes[index] = value; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="firemodes"><see cref="ParseFireModeStringIterator(string[])">See parameters.</see></param>
        public FireModeList(string[] firemodes)
        {
            ParseFireModeStringIterator(firemodes);
        }

        /// <summary>
        /// Parses a string into a <c>FireMode</c> object. Case-insensitive. Does not accept any special characters or spaces.
        /// <example>
        /// Example of formatting:
        /// <code>
        /// a600 -> automatic 600rpm
        /// s750 -> semiautomatic 750rpm
        /// s40la0 -> semiautomatic 40rpm, lever action
        /// s300sh8 -> semiautomatic 300 rpm, shotgun with 8 pellets
        /// b400bb0 -> burst (double) 400rpm
        /// </code>
        /// Code formatting: mode,firerate(rpm),special modes,pellets(for shotguns)
        /// <code>
        /// mode = (a|auto|automatic), (s|semi|semiautomatic), (b|burst)
        /// firerate = any integer
        /// special modes = for burst: (b = InstantBurst,bb = DoubleBurst,bbb = TripleBurst); 
        ///                 for semiautomatic: (la|lever|leveraction = LeverAction, 
        ///                                     ps|pump|pumpshotgun = PumpShotgun,
        ///                                     sh|shotgun = Shotgun)
        /// pellets = any integer, but required to include and can be set to 0
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="firemodes">A string specified in the format above. Case-insensitive. Does not accept any special characters or spaces.</param>
        /// <returns><c>FireMode</c> object referring to Automatic, Semiautomatic</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public static FireMode ParseFireModeString(string firemodes)
        {
            

            string str = firemodes;

            string mode = "";
            int firerate = 0;
            string specialFlags = "";
            int pellets = 0;

            List<string> resultInt = new();
            List<string> resultString = new();
            int count = 0; //count for numbers
            int count1 = 0; //count for letters
            string output = " ";
            try
            {
                //iterates through the individual string and parses
                for (int i = 0; i < str.Length; i++)
                {
                    output = (count+", "+count1+", "+i+" ");
                    File.AppendAllText("test.log", output);

                    if (Convert.ToInt32(str[i]) > 64 && Convert.ToInt32(str[i]) < 91)
                    { //if uppercase, turns into lowercase
                        str = str.Replace(str[i], (char)(Convert.ToInt32(str[i]) + 32));
                    }

                    if (Convert.ToInt32(str[i]) > 47 && Convert.ToInt32(str[i]) < 58)
                    { //detects number, increments count

                        count++;
                        if (i + 1 == str.Length)
                        { //detects if the end of the string has been reached and adds previous whole number
                            resultInt.Add(str.Substring(i - count + 1, count));
                            output = string.Concat(str.AsSpan(i - count + 1, count), " ");
                            File.AppendAllText("test.log", output);
                        }

                        if (i > 0)
                        { //avoids outofboundexception
                            if (Convert.ToInt32(str[i - 1]) > 96 && Convert.ToInt32(str[i - 1]) < 123)
                            { //detects if previous character is a letter and adds the whole word before into the list
                                resultString.Add(str.Substring(i - count1, count1));
                                output = string.Concat(str.AsSpan(i - count, count), " ");

                                File.AppendAllText("test.log", output);
                            }
                        }

                        count1 = 0; //reset letter counter
                    }
                    else
                    {

                        if (i > 0)
                        { //avoids outofboundexception
                            if (Convert.ToInt32(str[i - 1]) > 47 && Convert.ToInt32(str[i - 1]) < 58)
                            { //detects if previous character is a number and adds the whole number before into the list
                                resultInt.Add(str.Substring(i - count, count));
                                output = string.Concat(str.AsSpan(i - count, count), " ");

                                File.AppendAllText("test.log", output);
                            }
                        }

                        if (Convert.ToInt32(str[i]) > 96 && Convert.ToInt32(str[i]) < 123)
                        { //detects letter, increments count
                            count1++;
                            if (i + 1 == str.Length)
                            { //detects if the end of the string has been reached and adds previous whole word
                                resultString.Add(str.Substring(i - count + 1, count));
                                output = string.Concat(str.AsSpan(i - count + 1, count), " ");
                                File.AppendAllText("test.log", output);
                            }
                        }

                        count = 0; //reset number counter
                    }
                }

                firerate = (resultInt[0].Length != 0) ? Convert.ToInt32(resultInt[0]) : 0;
                mode = (resultString[0].Length != 0) ? resultString[0] : "";
            }
            catch (NullReferenceException)
            {
                return new FireMode(-1, "NullReferenceException", false, "Null", false, "String cannot be null", 0);
            }
            catch (ArgumentOutOfRangeException)
            {
                return new FireMode(-2, "ArgumentOutOfRangeException", false, "Null", false, "String must have both numbers and letters", 0);
            }


            try
            {
                specialFlags = (resultString[1].Length != 0) ? resultString[1] : "";
            }
            catch (ArgumentOutOfRangeException)
            {

                specialFlags = "";
            }

            try
            {
                pellets = (resultInt[1].Length != 0) ? Convert.ToInt32(resultInt[1]) : 0;
            }
            catch (ArgumentOutOfRangeException)
            {
                pellets = 0;
            }

            char firstCharacterMode = mode[0];


            if ((mode.Contains("automatic") || str.Contains("auto")) || firstCharacterMode == 'a')
            {
                return new FireMode(firerate, "Automatic", false, "", (specialFlags.Length != 0), specialFlags, 0);
                //modes.Add(Automatic); //Automatic SemiAutomatic Burst | BoltAction LeverAction PumpShotgun Shotgun TripleBurst DoubleBurst InstantBurst
            }
            else if ((mode.Contains("semiautomatic") || mode.Contains("semi") || mode.Contains("semi-automatic")) || firstCharacterMode == 's')
            {
                if (specialFlags.Contains("boltaction") || specialFlags.Contains("bolt") || specialFlags.Contains("ba"))
                {
                    return new FireMode(firerate, "SemiAutomatic", false, "", true, "BoltAction", 0);
                    //modes.Add(BoltAction);
                }
                else if (specialFlags.Contains("leveraction") || specialFlags.Contains("lever") || specialFlags.Contains("la"))
                {
                    return new FireMode(firerate, "SemiAutomatic", false, "", true, "LeverAction", 0);
                    //modes.Add(LeverAction);
                }
                else if (specialFlags.Contains("pumpshotgun") || specialFlags.Contains("pump") || specialFlags.Contains("ps"))
                {
                    return new FireMode(firerate, "SemiAutomatic", false, "", true, "PumpShotgun", pellets);
                    //modes.Add(PumpShotgun);
                }
                else if ((specialFlags.Contains("shotgun") || specialFlags.Contains("sh")) && !(specialFlags.Contains("pumpshotgun")))
                {
                    return new FireMode(firerate, "SemiAutomatic", false, "", true, "Shotgun", pellets);
                    //modes.Add(Shotgun);
                }


                return new FireMode(firerate, "SemiAutomatic", false, "", (specialFlags.Length != 0), specialFlags, 0);
                //modes.Add(SemiAutomatic);

            }
            else if ((mode.Contains("burst") || firstCharacterMode == 'b'))
            {
                
                if (specialFlags.Contains("bbb"))
                {
                    return new FireMode(firerate, "Burst", true, "III", true, "TripleBurst", 0);
                    //modes.Add(TripleBurst);
                }else if (specialFlags.Contains("bb"))
                {

                    return new FireMode(firerate, "Burst", true, "II", true, "DoubleBurst", 0);
                    //modes.Add(DoubleBurst);
                }
                else if (specialFlags.Contains('b'))
                {
                    return new FireMode(firerate, "Burst", true, "I", true, "InstantBurst", pellets);
                    //modes.Add(InstantBurst);
                }



            }

            return new FireMode(5, "", false, resultString[1], false, "", 0);
        }

        /// <summary>
        /// Iterates through the string array of firemodes.
        /// </summary>
        /// <param name="firemodes">An array of strings as specified in the <c>ParseFireModeString</c> method as <see cref="ParseFireModeString(string)">seen here</see>.</param>
        public void ParseFireModeStringIterator(string[] firemodes)
        {
            try {
                foreach(string str in firemodes)
                {
                    modes.Add(ParseFireModeString(str));

                }
            }
            catch (NullReferenceException)
            {
                modes.Add(new FireMode(-3, "NullReferenceException", false, "Null", false, "Cannot pass null as string array", 0));
            }
        }


        /// <summary>
        /// Searches the list for a specific <c>FireMode</c>.
        /// </summary>
        /// <param name="firemode">The <c>FireMode</c> to search with.</param>
        /// <returns>True if the list contains the FireMode, false otherwise.</returns>
        public bool searchList(FireMode firemode)
        {
            if (modes.Contains(firemode))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// Searches the list for a specific <c>FireMode</c>'s index.
        /// </summary>
        /// <param name="firemode">The <c>FireMode</c> to search with.</param>
        /// <returns>Index of item if it is found, otherwise -1.</returns>
        public int indexOfFireMode(FireMode firemode)
        {
            if (modes.Contains(firemode))
            {
                return modes.IndexOf(firemode);
            }
            else
            {
                return -1;
            }
        }


        /// <summary>
        /// Adds element to the end of the <c>FireModeList</c> list
        /// </summary>
        /// <param name="firemode">The <c>FireMode</c> to add.</param>
        /// <returns></returns>
        public bool addFireMode(FireMode firemode)
        {
            modes.Add(firemode);
            return searchList(firemode);
        }


        /// <summary>
        /// Adds new default firemode.
        /// </summary>
        /// <param name="firemode">The <c>FireMode</c> to insert.</param>
        public void addNewDefaultFiremode(FireMode firemode)
        {
            modes.Insert(0, firemode);
        }

        /// <summary>
        /// Searches through the list of <c>FireMode</c> objects for a specific property.
        /// </summary>
        /// <param name="mode">String of the mode desired. Modes include: <code>Automatic SemiAutomatic Burst | BoltAction LeverAction PumpShotgun Shotgun TripleBurst DoubleBurst InstantBurst</code></param>
        /// <param name="firerate">Firerate of the mode desired.</param>
        /// <param name="pellets">Pellets of the mode desired.</param>
        /// <returns></returns>
        public bool searchListProperties(string mode, int firerate, int pellets)
        {
            //Automatic SemiAutomatic Burst | BoltAction LeverAction PumpShotgun Shotgun TripleBurst DoubleBurst InstantBurst
            foreach(FireMode firemode in modes)
            {
                if (mode.ToLower() == firemode.Mode.ToLower() || (mode.ToLower().Contains("semi") && firemode.Mode.ToLower() == "semiautomatic") || firemode.Pellets == pellets || firemode.Firerate == firerate)
                {
                    return true;
                } else
                {
                    if(mode.ToLower() == firemode.SpecialMode.ToLower() && firemode.Special)
                    {
                        return true;
                    } else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Deletes a <c>FireMode</c> at a specified index.
        /// </summary>
        /// <param name="index">Index must be not equal to 0, and must also be greater than 0.</param>
        public void deleteFireMode(int index)
        {
            if(index != 0 && index > 0)
            {
                modes.RemoveAt(index);
            } 
        }

        /// <summary>
        /// Deletes a range of <c>FireMode</c> objects.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        public void deleteFireModeRange(int index, int count)
        {
            if (index != 0 && index > 0 && count != 0 && count > 0)
            {
                modes.RemoveRange(index, count);

            }
        }

        

    }

    /// <summary>
    /// Class that defines firemodes for guns.
    /// </summary>
    public class FireMode{

        
        private double firerate;
        private string mode;
        private bool burst;
        private string burstMode;
        private bool special;
        private string specialMode;
        private int pellets;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="firerate">Firerate of the gun in rounds per minute (RPM).</param>
        /// <param name="mode">Firing mode name, e.g. <c>(Automatic|Semiautomatic|Burst)</c>.</param>
        /// <param name="burst">Specifies if the firemode has burst capabilities.</param>
        /// <param name="burstMode">If the previous value is true, specifies if the firemode has InstantBurst("I"), DoubleBurst("II"), or TripleBurst("III").</param>
        /// <param name="special">Specifies if the firemode has special modes.</param>
        /// <param name="specialMode">If the previous value is true, specifies if the firemode has any special modes.</param>
        /// <param name="pellets">If the firemode is of a shotgun, specifies the number of pellets.</param>
        public FireMode(double firerate, string mode, bool burst, string burstMode, bool special, string specialMode, int pellets)
        {
            this.firerate = firerate;
            this.mode = mode;
            if (burst)
            {
                this.burstMode = burstMode;
            } else
            {
                this.burstMode = "";
            }

            if (special)
            {
                this.specialMode = specialMode;
            } else
            {
                this.specialMode = "";
            }

            this.burst = burst;
            this.special = special;
            this.pellets = pellets;
        }
        /// <summary>
        /// Firerate getter/setter.
        /// </summary>
        public double Firerate { get { return firerate; } set { firerate = value; } }
        /// <summary>
        /// Mode getter/setter.
        /// </summary>
        public string Mode { get { return mode; } set{ mode = value; } }
        /// <summary>
        /// Burst getter/setter.
        /// </summary>
        public bool Burst { get { return burst; } set { burst = value; } }
        /// <summary>
        /// BurstMode getter/setter.
        /// </summary>
        public string BurstMode { get { return burstMode;} set { burstMode = value; } }
        /// <summary>
        /// Special getter/setter.
        /// </summary>
        public bool Special { get { return special; } set { special = value; } }
        /// <summary>
        /// SpecialMode getter/setter.
        /// </summary>
        public string SpecialMode { get { return specialMode;} set { specialMode = value; } }
        /// <summary>
        /// Pellets getter/setter.
        /// </summary>
        public int Pellets { get { return pellets; } set { pellets = value; } }


    }

    /// <summary>
    /// Defines a list of <c>Conversion</c> objects.
    /// </summary>
    public class ConversionList
    {

        private List<Conversion> conversions = new List<Conversion>();

        /// <summary>
        /// Indexer for the list.
        /// </summary>
        /// <param name="index">Index of the list to be accessed.</param>
        /// <returns>Index of the list to be accessed.</returns>
        public Conversion this[int index]
        {
            get
            {
                return conversions[index];
            }                           
            set
            {
                conversions[index] = value;
            }
        }

        /// <summary>
        /// Adds a new <c>Conversion</c> object to the end of the list.
        /// </summary>
        /// <param name="conversion">The <c>Conversion</c> object to add.</param>
        public void addConversion(Conversion conversion)
        {
            conversions.Add(conversion);
        }

        /// <summary>
        /// Default <c>Conversion</c> object getter/setter.
        /// </summary>
        public Conversion DefaultConversion { get { return conversions[0]; } set { conversions[0] = value; } }

        /// <summary>
        /// Removes the <c>Conversion</c> object at the specified index of the list.
        /// </summary>
        /// <param name="index">The index of the list to be removed.</param>
        public void removeConversion(int index)
        {
            conversions.RemoveAt(index);
        }
        
        /// <summary>
        /// Inserts a new <c>Conversion</c> object.
        /// </summary>
        /// <param name="index">Index of the intended <c>Conversion</c> object insertion.</param>
        /// <param name="conversion">The <c>Conversion</c> object to be inserted.</param>
        public void insertConversion( Conversion conversion, int index)
        {
            conversions.Insert(index, conversion);
        }

        /// <summary>
        /// Gets the length of the list.
        /// </summary>
        /// <returns>The count of elements in the list.</returns>
        public int getLength()
        {
            return conversions.Count;
            
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="defaultConversion">The default <c>Conversion</c> object of the gun. See <see cref="Conversion.Conversion(string, int, int, string[], Carried, Ranged, double, double, double, double, double)"/></param>
        public ConversionList(Conversion defaultConversion)
        {
            addConversion(defaultConversion);

        }
    }

    /// <summary>
    /// Defines a conversion for a gun.
    /// </summary>
    public class Conversion
    {
        private string attachment;
        private string conversionName;
        private bool ammoConversion;


        private FireModeList fireModes;
        private Carried carriedAttributes;
        private Ranged rangedAttributes;
        private string caliber;
        private int ammoCapacity;
        private int magazineCapacity;
        private double penetration;
        private double muzzleVelocity;
        private double aimingWalkspeed;
        private double reloadSpeed;
        private double emptyReloadSpeed;
        private double suppression;

        /// <summary>
        /// DO NOT USE THIS OVERLOAD
        /// </summary>
        /// <param name="armourPiercing"></param>
        /// <param name="hollowPoint"></param>
        /// <param name="carriedAttributes"></param>
        /// <param name="rangedAttributes"></param>
        /// <param name="penetration"></param>
        /// <param name="suppression"></param>
        public Conversion(bool armourPiercing, bool hollowPoint, Carried carriedAttributes, Ranged rangedAttributes, double penetration, double suppression)
        {
            if (hollowPoint) {
                this.carriedAttributes = new Carried(carriedAttributes.LimbMultiplier, carriedAttributes.TorsoMultiplier / carriedAttributes.TorsoMultiplier, carriedAttributes.HeadMultiplier * (1 / 1.2), carriedAttributes.WalkSpeed);
                this.rangedAttributes = new Ranged(10.5529 * Math.Log(rangedAttributes.Range1, 2.61), rangedAttributes.Range2 * 0.9, rangedAttributes.Range1Damage * 1.2, rangedAttributes.Range2Damage / 1.2);
                this.penetration = penetration * 0.5;
                this.suppression = suppression * 1.1;
            } else if (armourPiercing)
            {
                this.carriedAttributes = new Carried(carriedAttributes.LimbMultiplier, carriedAttributes.TorsoMultiplier, carriedAttributes.HeadMultiplier, carriedAttributes.WalkSpeed);
                this.rangedAttributes = new Ranged(rangedAttributes.Range1 * 0.5, rangedAttributes.Range2, rangedAttributes.Range1Damage, rangedAttributes.Range2Damage);
                this.penetration = penetration * 1.5;
                this.suppression = suppression * 0.5;
            }
        }

        /// <summary>
        /// Constructor for armour piercing / hollow point rounds.
        /// </summary>
        /// <param name="armourPiercing">If the gun has armour piercing rounds. Mutually exclusive with <c>hollowPoint</c>.</param>
        /// <param name="hollowPoint">If the gun has hollow point rounds. Mutually exclusive with <c>armourPiercing</c>.</param>
        /// <param name="gun"><c>Gun</c> object to pass through.</param>
        public Conversion(bool armourPiercing, bool hollowPoint, Gun gun)
        {
            if (hollowPoint)
            {
                this.carriedAttributes = new Carried(gun.CarriedAttributes.LimbMultiplier, gun.CarriedAttributes.TorsoMultiplier / gun.CarriedAttributes.TorsoMultiplier, gun.CarriedAttributes.HeadMultiplier * (1 / 1.2), gun.CarriedAttributes.WalkSpeed);
                this.rangedAttributes = new Ranged(10.5529 * Math.Log(gun.RangedAttributes.Range1, 2.61), gun.RangedAttributes.Range2 * 0.9, gun.RangedAttributes.Range1Damage * 1.2, gun.RangedAttributes.Range2Damage / 1.2);
                penetration = gun.Penetration * 0.5;
                suppression = gun.Suppression * 1.1;

                //pass through params
                fireModes = gun.FireModes;
                caliber = gun.Caliber;
                aimingWalkspeed = gun.AimingWalkspeed;
                attachment = "Hollow Point";
                conversionName = gun.Name + " Hollow Point";
                emptyReloadSpeed = gun.EmptyReloadSpeed;
                reloadSpeed = gun.ReloadSpeed;
                muzzleVelocity = gun.MuzzleVelocity;
                ammoCapacity = gun.AmmoCapacity;
                magazineCapacity = gun.MagazineCapacity;
            }
            else if (armourPiercing)
            {
                this.carriedAttributes = new Carried(gun.CarriedAttributes.LimbMultiplier, gun.CarriedAttributes.TorsoMultiplier, gun.CarriedAttributes.HeadMultiplier, gun.CarriedAttributes.WalkSpeed);
                this.rangedAttributes = new Ranged(gun.RangedAttributes.Range1 * 0.5, gun.RangedAttributes.Range2, gun.RangedAttributes.Range1Damage, gun.RangedAttributes.Range2Damage);
                penetration = gun.Penetration * 1.5;
                suppression = gun.Suppression * 0.5;


                //pass through params
                fireModes = gun.FireModes;
                caliber = gun.Caliber;
                aimingWalkspeed = gun.AimingWalkspeed;
                attachment = "Armour Piercing";
                conversionName = gun.Name + " Armour Piercing";
                emptyReloadSpeed = gun.EmptyReloadSpeed;
                reloadSpeed = gun.ReloadSpeed;
                muzzleVelocity = gun.MuzzleVelocity;
                ammoCapacity = gun.AmmoCapacity;
                magazineCapacity = gun.MagazineCapacity;
            }
        }

        /// <summary>
        /// For adding a new conversion with <b>different firemodes</b> from the original gun. 
        /// </summary>
        /// <param name="name">Name of the gun which has the conversion. (You can pass <c>[Gun].Name</c>)</param>
        /// <param name="attachment">Attachment required for conversion.</param>
        /// <param name="conversionName">The name of the gun if the conversion affects the gun's name. If left blank, this will be <c>name + " " + attachment</c>.</param>
        /// <param name="ammoConversion">If the conversion is an ammo conversion.</param>
        /// <param name="caliber">The caliber of the gun with the conversion.</param>
        /// <param name="ammoCapacity">The ammo capacity of the gun with the conversion.</param>
        /// <param name="magazineCapacity">The magazine capacity of the gun with the conversion.</param>
        /// <param name="firemodes">The <see cref="FireModeList.ParseFireModeStringIterator(string[])">firemodes</see> of the gun with the conversion.</param>
        /// <param name="carriedAttributes">The <see cref="Carried">carriedAttributes</see> of the gun with the conversion.</param>
        /// <param name="rangedAttributes">The <see cref="Ranged">rangedAttributes</see> of the gun with the conversion.</param>
        /// <param name="penetration">The penetration depth of the gun with the conversion.</param>
        /// <param name="muzzleVelocity">The muzzle velocity of the gun with the conversion.</param>
        /// <param name="reloadSpeed">The reload speed of the gun with the conversion.</param>
        /// <param name="emptyReloadSpeed">The empty reload speed of the gun with the conversion.</param>
        /// <param name="suppression">The suppression of the gun with the conversion.</param>
        public Conversion(string name,string attachment, string conversionName, bool ammoConversion, string caliber, int ammoCapacity, int magazineCapacity, string[] firemodes, Carried carriedAttributes, Ranged rangedAttributes, double penetration, double muzzleVelocity, double reloadSpeed, double emptyReloadSpeed, double suppression)
        { //new conversions
            this.attachment = attachment;
            this.ammoConversion = ammoConversion;
            if(conversionName == "")
            {
                this.conversionName = name + " " + attachment;
            } else
            {
                this.conversionName = conversionName;
            }


            this.caliber = caliber;
            this.ammoCapacity = ammoCapacity;
            this.magazineCapacity = magazineCapacity;
            fireModes = new FireModeList(firemodes);
            this.carriedAttributes = carriedAttributes;
            this.rangedAttributes = rangedAttributes;
            this.penetration = penetration;
            this.muzzleVelocity = muzzleVelocity;
            this.reloadSpeed = reloadSpeed;
            this.emptyReloadSpeed = emptyReloadSpeed;
            this.suppression = suppression;

        }

        /// <summary>
        /// For adding a new conversion with the <b>same firemodes</b> as the original gun. 
        /// </summary>
        /// <param name="name">Name of the gun which has the conversion. (You can pass <c>[Gun].Name</c>)</param>
        /// <param name="attachment">Attachment required for conversion.</param>
        /// <param name="conversionName">The name of the gun if the conversion affects the gun's name. If left blank, this will be <c>name + " " + attachment</c>.</param>
        /// <param name="ammoConversion">If the conversion is an ammo conversion.</param>
        /// <param name="caliber">The caliber of the gun with the conversion.</param>
        /// <param name="ammoCapacity">The ammo capacity of the gun with the conversion.</param>
        /// <param name="magazineCapacity">The magazine capacity of the gun with the conversion.</param>
        /// <param name="firemodes">The firemodes of the gun with the conversion.</param>
        /// <param name="carriedAttributes">The <see cref="Carried">carriedAttributes</see> of the gun with the conversion.</param>
        /// <param name="rangedAttributes">The <see cref="Ranged">rangedAttributes</see> of the gun with the conversion.</param>
        /// <param name="penetration">The penetration depth of the gun with the conversion.</param>
        /// <param name="muzzleVelocity">The muzzle velocity of the gun with the conversion.</param>
        /// <param name="reloadSpeed">The reload speed of the gun with the conversion.</param>
        /// <param name="emptyReloadSpeed">The empty reload speed of the gun with the conversion.</param>
        /// <param name="suppression">The suppression of the gun with the conversion.</param>
        public Conversion(string name, string attachment, string conversionName, bool ammoConversion, string caliber, int ammoCapacity, int magazineCapacity, FireModeList firemodes, Carried carriedAttributes, Ranged rangedAttributes, double penetration, double muzzleVelocity, double reloadSpeed, double emptyReloadSpeed, double suppression)
        { //new conversions (with same firemodes)
            this.attachment = attachment;
            this.ammoConversion = ammoConversion;
            if (conversionName == "")
            {
                this.conversionName = name + " " + attachment;
            }
            else
            {
                this.conversionName = conversionName;
            }

            this.caliber = caliber;
            this.ammoCapacity = ammoCapacity;
            this.magazineCapacity = magazineCapacity;
            fireModes = firemodes;
            this.carriedAttributes = carriedAttributes;
            this.rangedAttributes = rangedAttributes;
            this.penetration = penetration;
            this.muzzleVelocity = muzzleVelocity;
            this.reloadSpeed = reloadSpeed;
            this.emptyReloadSpeed = emptyReloadSpeed;
            this.suppression = suppression;
        }

        /// <summary>
        /// For adding the default conversion for a gun. 
        /// </summary>
        /// <param name="caliber">The caliber of the gun with the conversion.</param>
        /// <param name="ammoCapacity">The ammo capacity of the gun with the conversion.</param>
        /// <param name="magazineCapacity">The magazine capacity of the gun with the conversion.</param>
        /// <param name="firemodes">The firemodes of the gun with the conversion.</param>
        /// <param name="carriedAttributes">The <see cref="Carried">carriedAttributes</see> of the gun with the conversion.</param>
        /// <param name="rangedAttributes">The <see cref="Ranged">rangedAttributes</see> of the gun with the conversion.</param>
        /// <param name="penetration">The penetration depth of the gun with the conversion.</param>
        /// <param name="muzzleVelocity">The muzzle velocity of the gun with the conversion.</param>
        /// <param name="reloadSpeed">The reload speed of the gun with the conversion.</param>
        /// <param name="emptyReloadSpeed">The empty reload speed of the gun with the conversion.</param>
        /// <param name="suppression">The suppression of the gun with the conversion.</param>
        public Conversion(string caliber, int ammoCapacity, int magazineCapacity, string[] firemodes, Carried carriedAttributes, Ranged rangedAttributes, double penetration, double muzzleVelocity, double reloadSpeed, double emptyReloadSpeed, double suppression)
        { //default

            this.caliber = caliber;
            this.ammoCapacity = ammoCapacity;
            this.magazineCapacity = magazineCapacity;
            fireModes = new FireModeList(firemodes);
            this.carriedAttributes = carriedAttributes;
            this.rangedAttributes = rangedAttributes;
            this.penetration = penetration;
            this.muzzleVelocity = muzzleVelocity;
            this.reloadSpeed = reloadSpeed;
            this.emptyReloadSpeed = emptyReloadSpeed;
            this.suppression = suppression;

        }

        /// <summary>
        /// Attachment getter/setter. See <see cref="Conversion.Conversion(string, string, string, bool, string, int, int, FireModeList, Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public string Attachment { get { return attachment; } set { attachment = value; } }

        /// <summary>
        /// ConversionName getter/setter. See <see cref="Conversion.Conversion(string, string, string, bool, string, int, int, FireModeList, Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public string ConversionName { get { return conversionName; } set { conversionName = value; } }

        /// <summary>
        /// AmmoConversion getter/setter.  See <see cref="Conversion.Conversion(string, string, string, bool, string, int, int, FireModeList, Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public bool AmmoConversion { get { return ammoConversion; } set { ammoConversion = value; } }


        /// <summary>
        /// FireModes getter/setter. See <see cref="Conversion.Conversion(string, string, string, bool, string, int, int, FireModeList, Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public FireModeList FireModes { get { return fireModes; } set { fireModes = value; } }

        /// <summary>
        /// CarriedAttributes getter/setter. See <see cref="Conversion.Conversion(string, string, string, bool, string, int, int, FireModeList, Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public Carried CarriedAttributes { get { return carriedAttributes; } set { carriedAttributes = value; } }

        /// <summary>
        /// RangedAttributes getter/setter. See <see cref="Conversion.Conversion(string, string, string, bool, string, int, int, FireModeList, Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public Ranged RangedAttributes { get { return rangedAttributes; } set { rangedAttributes = value; } }

        /// <summary>
        /// Caliber getter/setter. See <see cref="Conversion.Conversion(string, string, string, bool, string, int, int, FireModeList, Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public string Caliber { get { return caliber; } set { caliber = value; } }

        /// <summary>
        /// AmmoCapacity getter/setter. See<see cref="Conversion.Conversion(string, string, string, bool, string, int, int, FireModeList, Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public int AmmoCapacity { get { return ammoCapacity; } set { ammoCapacity = value; } }

        /// <summary>
        /// MagazineCapacity getter/setter. See <see cref="Conversion.Conversion(string, string, string, bool, string, int, int, FireModeList, Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public int MagazineCapacity { get { return magazineCapacity; } set { magazineCapacity = value; } }

        /// <summary>
        /// Penetration getter/setter. See <see cref="Conversion.Conversion(string, string, string, bool, string, int, int, FireModeList, Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public double Penetration { get { return penetration; } set { penetration = value; } }

        /// <summary>
        /// MuzzleVelocity getter/setter. See <see cref="Conversion.Conversion(string, string, string, bool, string, int, int, FireModeList, Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public double MuzzleVelocity { get { return muzzleVelocity; } set { muzzleVelocity = value; } }

        /// <summary>
        /// AimingWalkspeed getter/setter. See <see cref="Conversion.Conversion(string, string, string, bool, string, int, int, FireModeList, Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public double AimingWalkspeed { get { return aimingWalkspeed; } set { aimingWalkspeed = value; } }

        /// <summary>
        /// ReloadSpeed getter/setter. See <see cref="Conversion.Conversion(string, string, string, bool, string, int, int, FireModeList, Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public double ReloadSpeed { get { return reloadSpeed; } set { reloadSpeed = value; } }

        /// <summary>
        /// EmptyReloadSpeed getter/setter. See <see cref="Conversion.Conversion(string, string, string, bool, string, int, int, FireModeList, Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public double EmptyReloadSpeed { get { return emptyReloadSpeed; } set { emptyReloadSpeed = value; } }

        /// <summary>
        /// Suppression getter/setter. See <see cref="Conversion.Conversion(string, string, string, bool, string, int, int, FireModeList, Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public double Suppression { get { return suppression; } set { suppression = value; } }
    }

    /// <summary>
    /// Defines a gun.
    /// </summary>
    public class Gun : Weapon
    {

        private ConversionList conversions;

        private FireModeList fireModes;
        private Carried carriedAttributes;
        private Ranged rangedAttributes;
        private string caliber;
        private int ammoCapacity;
        private int magazineCapacity;
        private double penetration;
        private double muzzleVelocity;
        private double aimingWalkspeed;
        private double reloadSpeed;
        private double emptyReloadSpeed;
        private double suppression;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">The name of the gun.</param>
        /// <param name="hasRank">If the gun has a rank.</param>
        /// <param name="rank">The rank of the gun.</param>
        /// <param name="defaultGun">The <c>Conversion</c> object defining the default gun. <see cref="Conversion.Conversion(string, int, int, string[], Carried, Ranged, double, double, double, double, double)"></see></param>
        /// <param name="aimingWalkspeed">The aiming walkspeed of the gun.</param>
        /// <param name="hasArmourPiercing">If the gun supports armour piercing rounds.</param>
        /// <param name="hasHollowPoint">If the gun supports hollow point rounds</param>
        public Gun(string name, bool hasRank, int rank, Conversion defaultGun, double aimingWalkspeed, bool hasArmourPiercing, bool hasHollowPoint) : base(name, hasRank, rank)
        {

            caliber = defaultGun.Caliber;
            ammoCapacity = defaultGun.AmmoCapacity;
            magazineCapacity = defaultGun.MagazineCapacity;
            fireModes = defaultGun.FireModes;
            carriedAttributes = defaultGun.CarriedAttributes;
            rangedAttributes = defaultGun.RangedAttributes;
            penetration = defaultGun.Penetration;
            muzzleVelocity = defaultGun.MuzzleVelocity;
            reloadSpeed = defaultGun.ReloadSpeed;
            emptyReloadSpeed = defaultGun.EmptyReloadSpeed;
            suppression = defaultGun.Suppression;
            this.aimingWalkspeed = aimingWalkspeed;

            conversions = addNewConversionList(defaultGun);
            if (hasArmourPiercing)
            {
                conversions.addConversion(new Conversion(true, false, this));
            }
            if (hasHollowPoint)
            {
                conversions.addConversion(new Conversion(false, true, this));
            }



        }

        private static ConversionList addNewConversionList(Conversion defaultConversion)
        {
            return new ConversionList(defaultConversion);
        }

        

        
        //public 
        /// <summary>
        /// Conversions getter/setter.
        /// </summary>
        public ConversionList Conversions { get { return conversions; } set { conversions = value; } }


        /// <summary>
        /// FireModes getter/setter. See <see cref="Gun.Gun(string, bool, int, Conversion, double, bool, bool)"/> and <see cref="Conversion.Conversion(string, int, int, string[], Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public FireModeList FireModes { get { return fireModes; } set { fireModes = value; } }

        /// <summary>
        /// CarriedAttributes getter/setter. See <see cref="Gun.Gun(string, bool, int, Conversion, double, bool, bool)"/> and <see cref="Conversion.Conversion(string, int, int, string[], Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public Carried CarriedAttributes { get { return carriedAttributes; } set { carriedAttributes = value; } }

        /// <summary>
        /// RangedAttributes getter/setter. See <see cref="Gun.Gun(string, bool, int, Conversion, double, bool, bool)"/> and <see cref="Conversion.Conversion(string, int, int, string[], Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public Ranged RangedAttributes { get { return rangedAttributes; } set { rangedAttributes = value; } }

        /// <summary>
        /// Caliber getter/setter. See <see cref="Gun.Gun(string, bool, int, Conversion, double, bool, bool)"/> and <see cref="Conversion.Conversion(string, int, int, string[], Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public string Caliber { get { return caliber; } set { caliber = value; } }

        /// <summary>
        /// AmmoCapacity getter/setter. See <see cref="Gun.Gun(string, bool, int, Conversion, double, bool, bool)"/> and <see cref="Conversion.Conversion(string, int, int, string[], Carried, Ranged, double, double, double, double, double)"/> 
        /// </summary>
        public int AmmoCapacity { get { return ammoCapacity; } set { ammoCapacity = value; } }

        /// <summary>
        /// MagazineCapacity getter/setter. See <see cref="Gun.Gun(string, bool, int, Conversion, double, bool, bool)"/> and <see cref="Conversion.Conversion(string, int, int, string[], Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public int MagazineCapacity { get { return magazineCapacity; } set { magazineCapacity = value; } }

        /// <summary>
        /// Penetration getter/setter. See <see cref="Gun.Gun(string, bool, int, Conversion, double, bool, bool)"/> and <see cref="Conversion.Conversion(string, int, int, string[], Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public double Penetration { get { return penetration; } set { penetration = value; } }

        /// <summary>
        /// MuzzleVelocity getter/setter. See <see cref="Gun.Gun(string, bool, int, Conversion, double, bool, bool)"/> and <see cref="Conversion.Conversion(string, int, int, string[], Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public double MuzzleVelocity { get { return muzzleVelocity; } set { muzzleVelocity = value; } }

        /// <summary>
        /// AimingWalkspeed getter/setter. See <see cref="Gun.Gun(string, bool, int, Conversion, double, bool, bool)"/> and <see cref="Conversion.Conversion(string, int, int, string[], Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public double AimingWalkspeed { get { return aimingWalkspeed; } set { aimingWalkspeed = value; } }

        /// <summary>
        /// ReloadSpeed getter/setter. See <see cref="Gun.Gun(string, bool, int, Conversion, double, bool, bool)"/> and <see cref="Conversion.Conversion(string, int, int, string[], Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public double ReloadSpeed { get { return reloadSpeed; } set { reloadSpeed = value; } }

        /// <summary>
        /// EmptyReloadSpeed getter/setter. See <see cref="Gun.Gun(string, bool, int, Conversion, double, bool, bool)"/> and <see cref="Conversion.Conversion(string, int, int, string[], Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public double EmptyReloadSpeed { get { return emptyReloadSpeed; } set { emptyReloadSpeed = value; } }

        /// <summary>
        /// Suppression getter/setter. See <see cref="Gun.Gun(string, bool, int, Conversion, double, bool, bool)"/> and <see cref="Conversion.Conversion(string, int, int, string[], Carried, Ranged, double, double, double, double, double)"/>
        /// </summary>
        public double Suppression { get { return suppression; } set { suppression = value; } }


        

    }

    /// <summary>
    /// Defines a melee weapon.
    /// </summary>
    public class Melee : Weapon
    {
        private double frontStabDamage;
        private double backStabDamage;
        private double bladeLength;
        private Carried carriedAttributes;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">The name of the melee.</param>
        /// <param name="hasRank">If the melee has a rank.</param>
        /// <param name="rank">The rank of the melee.</param>
        /// <param name="frontStabDamage">The front stab damage of the melee.</param>
        /// <param name="backStabDamage">The back stab damage of the melee.</param>
        /// <param name="bladeLength">The blade length of the melee.</param>
        /// <param name="carriedAttributes">The <see cref="Carried.Carried(double, double, double, double)">carriedAttributes</see> of the melee.</param>
        public Melee(string name, bool hasRank, int rank, double frontStabDamage, double backStabDamage, double bladeLength, Carried carriedAttributes) : base(name, hasRank, rank)
        {
            this.frontStabDamage = frontStabDamage;
            this.backStabDamage = backStabDamage;
            this.bladeLength = bladeLength;
            this.carriedAttributes = carriedAttributes;
        }

        /// <summary>
        /// FrontStabDamage getter/setter. See <see cref="Melee.Melee(string, bool, int, double, double, double, Carried)"/>
        /// </summary>
        public double FrontStabDamage { get { return frontStabDamage; } set { frontStabDamage = value; } }

        /// <summary>
        /// BackStabDamage getter/setter. See <see cref="Melee.Melee(string, bool, int, double, double, double, Carried)"/>
        /// </summary>
        public double BackStabDamage { get { return backStabDamage; } set { backStabDamage = value; } }

        /// <summary>
        /// BladeLength getter/setter. See <see cref="Melee.Melee(string, bool, int, double, double, double, Carried)"/>
        /// </summary>
        public double BladeLength { get { return bladeLength; } set { bladeLength = value; } }

        /// <summary>
        /// CarriedAttributes getter/setter. See <see cref="Melee.Melee(string, bool, int, double, double, double, Carried)"/>
        /// </summary>
        public Carried CarriedAttributes { get { return carriedAttributes; } set { carriedAttributes = value; } } 


    }

    /// <summary>
    /// Defines a grenade.
    /// </summary>
    public class Grenade : Weapon
    {
        private bool fuse;
        private double fuseTime; //depends on above bool
        private bool special;
        private string specialMode; //depends on above bool
        private int storedCapacity;
        private double blastRadius;
        private double killRadius;
        private Ranged rangedAttributes;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">The name of the grenade.</param>
        /// <param name="hasRank">If the grenade has a rank.</param>
        /// <param name="rank">The rank of the grenade.</param>
        /// <param name="fuse">If the grenade has a fuse.</param>
        /// <param name="fuseTime">The fuse time of the grenade.</param>
        /// <param name="special">If the grenade has a special property.</param>
        /// <param name="specialMode">The special property of the grenade.</param>
        /// <param name="storedCapacity">The stored capacity of the grenades.</param>
        /// <param name="blastRadius">The radius that is guaranteed to deal minimum damage.</param>
        /// <param name="killRadius">The radius that is guaranteed to deal lethal (100) damage.</param>
        /// <param name="rangedAttributes">The <see cref="Ranged.Ranged(double, double, double, double)">rangedAttributes</see> of the grenade.</param>
        public Grenade(string name, bool hasRank, int rank, bool fuse, double fuseTime, bool special, string specialMode, int storedCapacity, double blastRadius, double killRadius, Ranged rangedAttributes) : base(name, hasRank, rank)
        {
            this.fuse = fuse;
            this.fuseTime = fuseTime;
            this.special = special;
            this.specialMode = specialMode;
            this.storedCapacity = storedCapacity;
            this.blastRadius = blastRadius; 
            this.killRadius = killRadius;
            this.rangedAttributes = rangedAttributes;
        }

        /// <summary>
        /// Fuse getter/setter. See <see cref="Grenade.Grenade(string, bool, int, bool, double, bool, string, int, double, double, Ranged)"></see>
        /// </summary>
        public bool Fuse { get { return fuse; } set { fuse = value;} }

        /// <summary>
        /// FuseTime getter/setter. See <see cref="Grenade.Grenade(string, bool, int, bool, double, bool, string, int, double, double, Ranged)"></see>
        /// </summary>
        public double FuseTime { get { return fuseTime; } set { fuseTime = value; } }

        /// <summary>
        /// Special getter/setter. See <see cref="Grenade.Grenade(string, bool, int, bool, double, bool, string, int, double, double, Ranged)"></see>
        /// </summary>
        public bool Special { get { return special; } set { special = value;} }

        /// <summary>
        /// SpecialMode getter/setter. See <see cref="Grenade.Grenade(string, bool, int, bool, double, bool, string, int, double, double, Ranged)"></see>
        /// </summary>
        public string SpecialMode { get { return specialMode; } set { specialMode = value;} }

        /// <summary>
        /// StoredCapacity getter/setter. See <see cref="Grenade.Grenade(string, bool, int, bool, double, bool, string, int, double, double, Ranged)"></see>
        /// </summary>
        public int StoredCapacity { get { return storedCapacity; } set { storedCapacity = value; } }

        /// <summary>
        /// BlastRadius getter/setter. See <see cref="Grenade.Grenade(string, bool, int, bool, double, bool, string, int, double, double, Ranged)"></see>
        /// </summary>
        public double BlastRadius {  get { return blastRadius; } set { blastRadius = value; } }

        /// <summary>
        /// KillRadius getter/setter. See <see cref="Grenade.Grenade(string, bool, int, bool, double, bool, string, int, double, double, Ranged)"></see>
        /// </summary>
        public double KillRadius { get { return killRadius; } set { killRadius = value; } }

        /// <summary>
        /// RangedAttributes getter/setter. See <see cref="Grenade.Grenade(string, bool, int, bool, double, bool, string, int, double, double, Ranged)"></see>
        /// </summary>
        public Ranged RangedAttributes { get { return rangedAttributes; } set { rangedAttributes = value; } }

    }

    /*
    public interface ICategory
    {
        ICategory LCategory { get; set; }
    }
    */

    public class Category//<Weapon> //: ICategory
    {
        private Dictionary<int,Weapon> weaponList = new();
        /*
        public Category(ICategory lCategory = null)
        {
            LCategory = lCategory; 
        }
        public ICategory LCategory { get; set; }
        */

        public Weapon this[int index]
        {
            get
            {
                return weaponList[index];
            }
            set
            {
                weaponList[index] = value;
            }
        }

        public Category(Weapon weapon)
        {
            if (weapon != null) addWeapon(weapon);
        }

        //generates id from weapon rank
        private static int IDGenerator(Weapon weapon)
        {
            if(weapon.Rank > 10000)
            {
                return -1;
            }

            return weapon.Rank * 10;
        }

        //matches a key and value pair (give a key, get a value; give a value, get a key)
        public bool matchKeyValue(ref int key, ref Weapon value)
        {
            if (weaponList.ContainsKey(key))
            {
                value = weaponList[key];
                return true;
            }
            else if (weaponList.ContainsValue(value))
            {
                for(int t = 0; t < weaponList.Count; t++){
                    if(weaponList[t] == value)
                    {
                        value = weaponList[t];
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }

        }

        //if the list has a key
        public bool hasKey(int key)
        {
            foreach(int r in weaponList.Keys)
            {
                if (r == key) return true;
            }
            return false;
        }

        //if the list has a weapon
        public bool hasWeapon(Weapon weapon)
        {
            foreach(Weapon weapon1 in weaponList.Values)
            {
                if (weapon1 == weapon) return true;
            }
            return false;
        }

        //give a weapon, get its id
        public int IDLookup(Weapon weapon)
        {
            foreach(int id in weaponList.Keys)
            {
                if (weaponList[id] == weapon)
                {
                    return id;
                }
            }
            return -1;
        }

        //get a weapon by looking up its rank 
        public Weapon weaponLookupByRank(int rank)
        {
            foreach(Weapon weapon in weaponList.Values)
            {
                if (weapon.Rank == rank)
                {
                    return weapon;
                }
            }
            return new Weapon("Catch wLBR", false, 0);
        }

        //get a weapon by looking up its id
        public Weapon weaponLookupByID(int id)
        {
            return weaponList[id];
        }
        
        //adds a weapon to the list
        public bool addWeapon(Weapon weapon) {
            int er = 2;
            Func<Weapon, int> deleg = x => IDGenerator(x) != -1 ? IDGenerator(weapon) : er;
            if (deleg(weapon) == er) return false;
            try
            {
                weaponList.Add(deleg(weapon), weapon);
            }
            catch (ArgumentException)
            {
                weaponList.Add(deleg(weapon) + 1, weapon);
            }
            return true;
        }

        public bool removeWeaponByRank(int rank)
        {
            return weaponList.Remove(IDLookup(weaponLookupByRank(rank)));
        }

        public bool removeWeaponByID(int id)
        {
            return weaponList.Remove(id);
        }

        public int weaponCount()
        {
            return weaponList.Count;
        }

        


    }

    


    public class Class
    {
        private List<Category> categoryList = new();
        private string classname;

        public Category this[int index]
        {
            get
            {
                return categoryList[index];
            }
            set
            {
                categoryList[index] = value;
            }
        }

        public Class(string name)
        {
            this.classname = name;
        }

        public void addCategory(Category category)
        {
            categoryList.Add(category);
        }

        public void removeCategory(Category category)
        {
            categoryList.Remove(category);
        }

        public bool hasCategory(Category category)
        {
            return categoryList.Contains(category);
        }

        public int categoryLength(Category category)
        {
            return categoryList.Count;
        }

    }




}



/*
weapons:
    gun
    grenade
    melee

class Weapon{
    protected string name;
    protected int rank;
}

class Carried{
    protected double limbMultipler, 
                        torsoMultiplier, 
                        headMultiplier, 
                        walkspeed;
}

class Ranged{
    protected double range1, 
                    range2,
                    damageRange1,
                    damageRange2;
}

class Gun : Weapon, Carried, Ranged {
    public string caliber;
    public double firerate;
    public int ammoCapacity;
    public int magazineCapacity;
    public string[ ] firemodes = new string[5];
    public double penetration;
    public double muzzleVelocity;
    public double aimingWalkspeed;
    public double reloadSpeed;
    public double emptyReloadSpeed;

    public class Conversion{
        public string attachment;
        public string conversionName;

        public bool ammoConversion; 
        public string ammoType; //depends on above bool
    }
}

class Melee : Weapon, Carried {
    public double frontStabDamage;
    public double backStabDamage;
    public double bladeLength;


}

class Grenade : Weapon, Ranged {
    public bool fuse;
    public double fuseTime; //depends on above bool
    
    public int storedCapacity;

}

carried class (for gun, melee):
    limb multiplier
    torso multiplier
    head multiplier
    walkspeed
    
ranged weapon (for gun, grenade):
    range 1
    range 2
    damage range 1
    damage range 2

weapon general properties
    name
    rank

gun properties
    caliber
    firerate
    ammo capacity
    mag capacity
    fire modes
    penetration
    muzzle velocity
    aiming walkspeed
    reload time
    empty reload time

    special attachments
    special conversions 
    conversion names

melee properties
    front stab damage
    back stab damage
    blade length
 
grenade
    fuse? (true/false)
    fuse time
    stored capacity
    

 */
