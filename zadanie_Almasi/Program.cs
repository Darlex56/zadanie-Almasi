using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace zadanie_Almasi
{
    internal class Program
    {
        static Dictionary<int , string> textValues = new Dictionary<int , string>();
        static Dictionary<int, string> authorVaules = new Dictionary<int , string>();

        static void Main(string[] args)
        {
            webDownloader webDownloader = new webDownloader();
            Storage storage = new Storage();
            storage.splitString(textValues, authorVaules, webDownloader.download("http://mvi.mechatronika.cool/sites/default/files/berces.html"));

            for (int i = 0; i < textValues.Count; i++) {
                Console.WriteLine(textValues[i] + ' ' + authorVaules[i]);
            }
            Console.ReadLine();
        }
    }

    class Storage {
        string[] newString;

        public void splitString(Dictionary<int, string> textDictionary, Dictionary<int, string> authorDictionary, string text) {
            newString = text.Split('.');

            for (int i = 0; i < newString.Length; i++) {
                textDictionary.Add(i, newString[i] + '.');
                authorDictionary.Add(i, "UNKNOWN");
            }
            
        }

    }
    class webDownloader { 
        public string download(string url)
        {
            WebClient client = new WebClient();

            return client.DownloadString(url);
        }
    }
}
