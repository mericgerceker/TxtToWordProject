using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using TxtToWordProject.Models;

namespace TxtToWordProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            XElement xelement;
            try
            {
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "wordXml.xml");

                xelement = XElement.Load(filePath);
            }
            catch (IOException ex)
            {
                throw ex;
            }
            // \AppData\Roaming altındaki dosyadan okunur.
            
            // En çok geçen 10 kelimeyi alır.
            var wordList = xelement.Descendants("word")
                                .ToDictionary(x => (string)x.Attribute("text"), x => (int)x.Attribute("count"))
                                .OrderByDescending(y => y.Value)
                                .Take(10).ToList();

            MainModel model = new MainModel();
            model.lstXml = new List<XmlModel>();
            foreach (var item in wordList)
            {
                model.lstXml.Add(new XmlModel { Text = item.Key, Count = item.Value });
            }
           
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}