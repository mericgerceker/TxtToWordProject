using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookToXml
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("TXT dosya adresini giriniz: ");
            string txt = Console.ReadLine();
            string txtFile = txtDownload(txt);

            List<string> wordArrayList = txtToWord(txtFile);

            xmlCreate(wordArrayList);
        }

        // Kullanıcıdan alınan text url'sini indirir.
        public static string txtDownload(string txtURI)
        {
            var webClient = new WebClient();
            string readHtml = webClient.DownloadString(txtURI);
            readHtml = readHtml.ToLower(new System.Globalization.CultureInfo("en-EN"));

            return readHtml;
        }

        // İndirilen text dosyasını split eder.
        public static List<string> txtToWord(string txtString)
        {
            var punctuation = txtString.Where(Char.IsPunctuation).Distinct().ToArray();

            var words = txtString.Split().Select(x => x.Trim(punctuation));
            List<string> wordList = words.ToList();
            wordList.RemoveAll(item => item == "");

            return wordList;
        }

        // XML oluşturulur.
        public static void xmlCreate(List<String> wordArrayList)
        {
            Dictionary<string, int> wordCount = new Dictionary<string, int>();
            foreach (string word in wordArrayList)
            {
                if (wordCount.ContainsKey(word))
                    wordCount[word] = wordCount[word] + 1;
                else
                    wordCount.Add(word, 1);
            }

            XElement xelement = new XElement(
                    "words",
                    wordCount.Select(x => new XElement("word", new XAttribute("text", x.Key), new XAttribute("count", x.Value)))
                 );
            var xml = xelement.ToString();
            // \AppData\Roaming altına dosya kaydedilir.
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "wordXml.xml");
            xelement.Save(path);

        }
    }
}
