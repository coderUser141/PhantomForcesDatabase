// See https://aka.ms/new-console-template for more information
using System.IO;
using System;
using System.Diagnostics;

Console.WriteLine("Hello, World!");
string? filepath = "AWS1.txt";
//Console.WriteLine(findStatisticInFile(filepath,"reload","time"));
//findStatisticInFile(filepath, "suppression");
List<string> files = new List<string>(bruh.findStatisticInFile(filepath, bruh.statisticInFile.AmmoType));
foreach(string file in files)
{
    Console.WriteLine(file);
}


class bruh
{
    public enum statisticInFile
    {
        DamageRange,
        AmmoMagCapacity,
        Damage,
        AmmoType
    }


    private static List<int> indexFinder(string filetext, string word)
    {

        List<int> output = new();
        for(; filetext.Contains(word, StringComparison.CurrentCultureIgnoreCase); )
        {
            output.Add(filetext.IndexOf(word, StringComparison.CurrentCultureIgnoreCase));
            try
            {
                filetext = filetext.Remove(filetext.IndexOf(word,StringComparison.CurrentCultureIgnoreCase), word.Length);
            }
            catch (ArgumentOutOfRangeException) { break; }
        }
        return output;
    }

    public static List<string> findStatisticInFile(string filepath, statisticInFile statisticInFile)
    {
        if (filepath == null) throw new ArgumentNullException(nameof(filepath));
        if (!File.Exists(filepath)) throw new FileNotFoundException("File not found.", nameof(filepath));
        string filetext = File.ReadAllText(filepath);

        Console.WriteLine(filetext); //remove

        string inputWord1 = "";
        string inputWord2 = "";

        //checks the mode of what to find
        switch (statisticInFile)
        {
            case statisticInFile.DamageRange:
                {
                    inputWord1 = "damage";
                    inputWord2 = "range";
                    break;
                }
            case statisticInFile.AmmoMagCapacity:
                {
                    inputWord1 = "ammo";
                    inputWord2 = "capacity";
                    break;
                }
            case statisticInFile.Damage:
                {
                    inputWord1 = "damage";
                    inputWord2 = "range";
                    break;
                }
            case statisticInFile.AmmoType:
                {
                    inputWord1 = "ammo";
                    inputWord2 = "type";
                    break;
                }
        }
        List<string> strings = new();

        //detects if the words are actually in the file
        if (filetext.Contains(inputWord1, StringComparison.CurrentCultureIgnoreCase) && filetext.Contains(inputWord2, StringComparison.CurrentCultureIgnoreCase))
        {

            int currentWordPosition = 0; //current text position
            string outputTempString = "";
            bool found = false;


            List<int> listOfIndexes = indexFinder(filetext, inputWord1);

            foreach (int index in listOfIndexes)
            {
                for (int i = 0; i < 20 + inputWord2.Length; i++) //iterates looking for word2
                {
                    switch (statisticInFile)
                    {
                        case statisticInFile.DamageRange: //normal behaviour
                            {

                                if (filetext.Substring(index + i, inputWord2.Length + 4).Contains(inputWord2, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    Console.WriteLine("hello i found it (damage range)"); //get rid
                                    //currentWordPosition += i;
                                    found = true;
                                    currentWordPosition = index + i;
                                }
                                break;

                            }
                        case statisticInFile.AmmoMagCapacity: //again normal behaviour
                            {

                                if (filetext.Substring(index + i, inputWord2.Length + 4).Contains(inputWord2, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    Console.WriteLine("hello i found it (ammomag)"); //get rid
                                    Console.WriteLine(filetext.Substring(currentWordPosition + i, inputWord2.Length + 4));
                                    //currentWordPosition += i;
                                    currentWordPosition = index + i;
                                    found = true;
                                }
                                //filetext.Substring(currentWordPosition + i, inputWord2.Length + 4);
                                break;

                            }
                        case statisticInFile.AmmoType:
                            {
                                if (filetext.Substring(index + i, inputWord2.Length + 4).Contains(inputWord2, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    Console.WriteLine("hello i found it (ammotype)"); //get rid
                                    Console.WriteLine(filetext.Substring(currentWordPosition + i, inputWord2.Length + 4));
                                    //currentWordPosition += i;
                                    currentWordPosition = index + i + inputWord1.Length + inputWord2.Length;
                                    found = true;
                                }
                                break;
                            }
                        case statisticInFile.Damage:
                            {

                                if (filetext.Substring(index + i, inputWord2.Length + 4).Contains(inputWord2, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    Console.WriteLine("hello i did not found it (dmgonly)"); //get rid
                                                                               
                                    
                                    //currentWordPosition += i;
                                    
                                    //found = true;
                                }
                                else
                                {
                                    Console.WriteLine("dingus");
                                    found = true;
                                    currentWordPosition = index + i;
                                    break;
                                }
                                //Console.WriteLine(filetext.Substring(inputWord1.LastIndexOf(inputWord1),inputWord1.Length) + "hii");
                                break;
                            }
                    }
                    if (found) break;
                    Console.WriteLine(i); //get rid

                }
            }

            if (found)
            {
                for (int j = currentWordPosition; j < currentWordPosition + 100; j++)
                {
                    if (filetext[j] != ' ')
                    {
                        if(statisticInFile == statisticInFile.AmmoType)
                        {
                            if ((filetext[j] > 47 && filetext[j] < 58) || (filetext[j] > 64 && filetext[j] < 91) || (filetext[j] > 96 && filetext[j] < 123) || filetext[j] == 46 || filetext[j] == 45) //for numbers, letters, dash and dot
                            {
                                outputTempString += filetext[j];
                                Console.WriteLine(filetext[j] + " " + outputTempString + " " + j); //remove
                                if ((filetext[j + 1] > 57 || filetext[j + 1] < 48) && filetext[j + 1] != 46 && filetext[j + 1] != 45 && (filetext[j + 1] < 97 || filetext[j + 1] > 122) && (filetext[j + 1] < 65 || filetext[j + 1] > 90))
                                {
                                    strings.Add(outputTempString);
                                    outputTempString = "";
                                }


                            }
                        } else
                        {

                            if ((filetext[j] > 47 && filetext[j] < 58) || filetext[j] == 46) //for numbers, and dot
                            {
                                outputTempString += filetext[j];
                                Console.WriteLine(filetext[j] + " " + outputTempString + " " + j); //remove
                                if ((filetext[j + 1] > 57 || filetext[j + 1] < 48) && filetext[j + 1] != 46)
                                {
                                    strings.Add(outputTempString);
                                    outputTempString = "";
                                }


                            }

                        }

                    }
                }
            }

            Console.WriteLine(outputTempString); //remove
            return strings;
        }
        else if (filetext.Contains(inputWord1, StringComparison.CurrentCultureIgnoreCase) && !filetext.Contains(inputWord2, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new ArgumentException(inputWord2 + " was not found inside the file provided.", nameof(inputWord2));
        }
        else
        {
            throw new ArgumentException(inputWord1 + " and " + inputWord2 + " were not found inside the file provided.", nameof(inputWord1));

        }

    }

    
    public static string findStatisticInFile(string filepath, string inputWord1, string inputWord2)
    {
        if(filepath == null)throw new ArgumentNullException(nameof(filepath));
        if(inputWord1 == null)throw new ArgumentNullException(nameof(inputWord1));
        if(inputWord2 == null)throw new ArgumentNullException(nameof(inputWord2));
        if(!File.Exists(filepath))throw new FileNotFoundException("File not found.", nameof(filepath));
        string filetext = File.ReadAllText(filepath);
        Console.WriteLine(filetext); //remove


        if (filetext.Contains(inputWord1, StringComparison.CurrentCultureIgnoreCase) && filetext.Contains(inputWord2, StringComparison.CurrentCultureIgnoreCase))
        {

            int currentWordPosition = 0; //current text position
            string outputTempString = "";
            bool found = false;


            currentWordPosition = filetext.IndexOf(inputWord1, currentWordPosition, StringComparison.CurrentCultureIgnoreCase);
            for (int i = 0; i < 10 + inputWord2.Length; i++)
            {

                if (filetext.Substring(currentWordPosition + i, inputWord2.Length).Contains(inputWord2, StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("hello i found it"); //remove
                    currentWordPosition += i;
                    found = true;
                    break;
                }
                Console.WriteLine(i); //remove

            }

            if (found)
            {
                for (int j = currentWordPosition; j < currentWordPosition + 50; j++)
                {
                    if (filetext[j] != ' ')
                    {
                        if ((filetext[j] > 47 && filetext[j] < 58) || filetext[j] == 46)
                        {
                            outputTempString += filetext[j];
                            Console.WriteLine(filetext[j]+ " " + outputTempString + " " + j); //remove
                        }


                    }
                }
            }

            Console.WriteLine(outputTempString); //remove
            return outputTempString;
        }
        else if (filetext.Contains(inputWord1, StringComparison.CurrentCultureIgnoreCase) && !filetext.Contains(inputWord2, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new ArgumentException(inputWord2 + " was not found inside the file provided.", nameof(inputWord2));
        }
        else
        {
            throw new ArgumentException(inputWord1 + " and " + inputWord2 + " were not found inside the file provided.", nameof(inputWord1));

        }
    }


    public static string findStatisticInFile(string filepath, string inputWord1)
    {
        if (filepath == null) throw new ArgumentNullException(nameof(filepath));
        if(inputWord1 == null)throw new ArgumentNullException(nameof(inputWord1));
        string filetext = File.ReadAllText(filepath);
        Console.WriteLine(filetext); //remove



        if (filetext.Contains(inputWord1, StringComparison.CurrentCultureIgnoreCase))
        {

            int currentWordPosition = 0; //current text position
            string outputTempString = "";


            currentWordPosition = filetext.IndexOf(inputWord1, currentWordPosition, StringComparison.CurrentCultureIgnoreCase);


            for (int j = currentWordPosition; j < currentWordPosition + 50; j++)
            {
                if (filetext[j] != ' ')
                {
                    if ((filetext[j] > 47 && filetext[j] < 58) || filetext[j] == 46)
                    {
                        outputTempString += filetext[j];
                        Console.WriteLine(filetext[j] + " " + outputTempString + " " + j); //remove
                    }
                }
            }


            Console.WriteLine(outputTempString); //remove
            return outputTempString;
        }
        else
        {
            throw new ArgumentException(inputWord1 + " was not found in the file specified.", nameof(inputWord1));

        }
    }
}