using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace zadanie_Almasi
{
    internal class Program
    {
        static Dictionary<int, string[]> textValues = new Dictionary<int, string[]>();

        static void Main(string[] args)
        {
            webDownloader webDownloader = new webDownloader();
            Menu menu = new Menu();
            Storage storage = new Storage();
            storage.splitString(textValues, webDownloader.download("http://mvi.mechatronika.cool/sites/default/files/berces.html"));

            while (true)
            {
                string option = menu.menu();

                if (option.Equals("1"))
                {
                    foreach (var values in textValues)
                    {
                        Console.WriteLine("ID: " + values.Key);
                        Console.WriteLine("Author:" + values.Value[1]);
                        Console.WriteLine("Zaznam:" + values.Value[0]);
                    }
                }
                if (option.Equals("2"))
                {
                    string deleteID = menu.menuDelete();
                    try
                    {
                        textValues.Remove(Convert.ToInt32(deleteID));
                        Console.WriteLine("Zaznam s ID " + deleteID + " bolo uspesne vymazane.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Zaznam s ID " + deleteID + " sa nepodarilo vymazat.");
                    }
                }
                if (option.Equals("3"))
                {
                    string changeID = menu.menuChange();

                    Console.WriteLine("TEXT: ");
                    string updateText = Console.ReadLine();

                    Console.WriteLine("Autor: ");
                    string updateAuthor = Console.ReadLine();

                    try
                    {
                        ///Vyhodi Exception ak sa kluc v dict nechadza kedze nemozem zmenit nieco co neexistuje
                        if (!textValues.ContainsKey(Convert.ToInt32(changeID))) {
                            throw new Exception();
                        }
                            ///Zmena stringu
                            string[] updatedString = new string[] { updateText, updateAuthor };
                        textValues[Convert.ToInt32(changeID)] = updatedString;
                        Console.WriteLine("Zaznam s ID" + changeID + " sa podarilo zmenit");

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Zaznam sa nepodarilo zmenit");
                    }
                }
                if (option.Equals("4"))
                {
                    Console.WriteLine("Zadaj ID zaznamu ktory si zelas pridat");
                    string addID = Console.ReadLine();

                    Console.WriteLine("Zadaj text ktory si zelas pridat: ");
                    string addText = Console.ReadLine();

                    Console.WriteLine("Zadaj autora: ");
                    string addAuthor = Console.ReadLine();

                    try
                    {
                        ///Vyhodi Exception ak sa kluc v dict nachadza aby nedoslo k prepisaniu
                        if (textValues.ContainsKey(Convert.ToInt32(addID))) {
                            throw new Exception();
                        }

                        ///Zmena stringu
                        string[] updatedString = new string[] { addText, addAuthor };
                        textValues[Convert.ToInt32(textValues.Count)] = updatedString;
                        Console.WriteLine("Zaznam s ID" + addID + " sa podarilo pridat");

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Zaznam sa nepodarilo pridat");
                    }
                }
            }
        }
    }
    class Menu {
        ///Vsetky funkcie ktore pouzivam na vypisovanie roznych menu casti
        public string menu() {
            Console.WriteLine("------------------------\n1->Vypis vsetky zaznamy\n2->Vymaz zaznam\n3->Zmen vetu v zazname a nasledne autora\n4->Pridaj zaznam\n5->Koniec\n------------------------");
            return Console.ReadLine();
        }

        public string menuDelete() {
            Console.WriteLine("Zadaj ID ktore si zelas odstranit: ");
            return Console.ReadLine();
        }
        public string menuChange()
        {
            Console.WriteLine("Zadaj ID ktore si zelas zmenit: ");
            return Console.ReadLine();
        }
        public string menuAdd()
        {
            
            return Console.ReadLine();
        }
        }
    class Storage {
        string[] newString;

        
        public void splitString(Dictionary<int, string[]> textDictionary,  string text) {
            //Splitnutie stringu pri znaku . a ulozenie do array
            newString = text.Split('.');

            //Pridanie array do dictionary s jeho klucom
            for (int i = 0; i < newString.Length; i++) {
                string[] newArray = new string[] { newString[i] + '.', "unknown" };
                textDictionary.Add(i, newArray);
            }
            
        }

    }
    class webDownloader { 
        ///Stahovanie a vratenie stringu z web stranky
        public string download(string url)
        {
            WebClient client = new WebClient();
            return client.DownloadString(url);
        }


    }
}
