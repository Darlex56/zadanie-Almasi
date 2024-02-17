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
                    ///Vypisanie celeho listu
                    foreach (var values in textValues)
                    {
                        Console.WriteLine("ID: " + values.Key);
                        Console.WriteLine("Author:" + values.Value[1]);
                        Console.WriteLine("Zaznam:" + values.Value[0]);
                    }
                }
                if (option.Equals("2"))
                {
                    string deleteID = menu.askOptions("Zadaj ID ktore si zelas odstranit: ");
                    try
                    {
                        ///Vyhodi Exception ak sa kluc v dict nechadza kedze nemozem vymazat nieco co neexistuje
                        if (!textValues.ContainsKey(Convert.ToInt32(deleteID))) {
                            throw new Exception();                        
                        }
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
                    string changeID = menu.askOptions("Zadaj ID ktore si zelas zmenit: ");

                    string[] add = menu.versatilMenu("Zadaj text ktory si zelas pridat: ", "Zadaj autora: ");
                    try
                    {
                        ///Vyhodi Exception ak sa kluc v dict nechadza kedze nemozem zmenit nieco co neexistuje
                        if (!textValues.ContainsKey(Convert.ToInt32(changeID))) {
                            throw new Exception();
                        }
                        ///Zmena stringu
                        storage.updateString(textValues, add, Convert.ToInt32(changeID));
                        Console.WriteLine("Zaznam s ID " + changeID + " sa podarilo zmenit");

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

                    string[] add = menu.versatilMenu("Zadaj text ktory si zelas pridat: ", "Zadaj autora: ");

                    try
                    {
                        ///Vyhodi Exception ak sa kluc v dict nachadza aby nedoslo k prepisaniu
                        if (textValues.ContainsKey(Convert.ToInt32(addID))) {
                            throw new Exception();
                        }

                        ///Zmena stringu
                        storage.updateString(textValues, add, Convert.ToInt32(addID));
                        Console.WriteLine("Zaznam s ID " + addID + " sa podarilo pridat");
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

        public string askOptions(string text) {
            Console.WriteLine(text);
            return Console.ReadLine();
        }

        public string menuAdd()
        {
            return Console.ReadLine();
        }
        public string[] versatilMenu(string text, string author) {

            Console.WriteLine(text);
            string addText = Console.ReadLine();

            Console.WriteLine(author);
            string addAuthor = Console.ReadLine();
            return new string[] { addText, addAuthor };
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
        public void updateString(Dictionary<int, string[]> values, string[] updated, int ID) {
            ///Zabalenie stringov do updatedString ktore sa nasledne aktualizuje/prida
            string[] updatedString = new string[] { updated[0], updated[1] };
            values[ID] = updatedString;
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
