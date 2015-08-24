using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynMetal_MVC.Models.ViewMod
{
  public  class HomeIndexView
    {
        public ProductView lastProduct { get; set; }

        public List<PdfView> LastPdf { get; set; }

        public HomeIndexView()
        {
            lastProduct = new ProductView();
            LastPdf = new List<PdfView>();
        }

        public class ProductView
        {
            public int id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }

        }

        public class PdfView
        {
            public int id { get; set; }
            public string FilePath { get; set; }
            public PdfCategory Category { get; set; }
            public string FileName { get; set; }
            public string Description { get; set; }
        }
    }
}
