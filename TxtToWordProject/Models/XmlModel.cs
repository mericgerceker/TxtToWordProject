using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TxtToWordProject.Models
{
    public class XmlModel
    {
        public string Text { get; set; }
        public int Count { get; set; }
    }

    public class MainModel
    {
        public List<XmlModel> lstXml { get; set; }
    }
}