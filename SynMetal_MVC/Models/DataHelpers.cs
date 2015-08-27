using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace SynMetal_MVC.Models
{
    public enum PhotoEdit { Edit, EditProduct, Create, def };
    static class Variables
    {
        public const string RegexForNames = @"[A-Za-z0-9'_'\u0391-\u03A9" + "\x5C\x3B\x3A\x2E\x2C\x28\x29\x20" +
             "\u03B1-\u03C9\x2D\u0386-\u038F\u03AA-\u03AF\u03CA-\u03CF\u0390\u03B0]+";
        public const string RegexForNumbers = @"[0-9]+";
        public const string pathToSavePhotos = "~/Photos/";
        public const string pathToSavePDFs = "~/PDF/";


    }
    static class Search
    {
        static SynMetalEntities db = new SynMetalEntities();
        public static List<Product> ByProdCategory(int? id)
        {
            List<Product> products;
            if (id != null && Regex.IsMatch(id.ToString(), Variables.RegexForNumbers))
            {
                var cat = db.ProdCategories.Find(id);
                var prod = from prd in db.Products
                           where prd.Category.CategoryId == cat.CategoryId
                           select prd;
                products = prod.ToList();
                //for (int i = 0; i < products.Count; i++)
                //{
                //    products[i].Category = cat;
                //}

            }
            else
            {
                var prod = db.Products;
                products = prod.ToList();
                //for (int i = 0; i < prod.Count(); i++)
                //{
                //    products[i].Category = db.ProdCategories.Find(products[i].Category.CategoryId);
                //}


            }
            return products;
        }

        public static List<PdfFile> ByPdfCategory(int? id)
        {
            List<PdfFile> PdfFiles;
            if (id != null && Regex.IsMatch(id.ToString(), Variables.RegexForNumbers))
            {
                var cat = db.PdfCategories.Find(id);
                var prod = from prd in db.PdfFiles
                           where prd.Category.CategoryId == cat.CategoryId
                           select prd;
                PdfFiles = prod.ToList();
                //for (int i = 0; i < PdfFiles.Count; i++)
                //{
                //    PdfFiles[i].Category = cat;
                //}

            }
            else
            {
                var prod = db.PdfFiles;
                PdfFiles = prod.ToList();
                //for (int i = 0; i < prod.Count(); i++)
                //{
                //    PdfFiles[i].Category = db.PdfCategories.Find(PdfFiles[i].Category.CategoryId);
                //}


            }
            return PdfFiles;

        }
    }


    public class SaveObject
    {
        public string FilePath { get; set; }
        public string Name { get; set; }
        public List<string> Error { get; set; }
        public bool isValid { get; set; }

        public SaveObject()
        {
            Error = new List<string>();
        }


    }

    static class PdfHelper
    {
        static SynMetalEntities DB = new SynMetalEntities();
        public static SaveObject SavePDF(HttpRequestBase Request, HttpServerUtilityBase Server, string prefix, string htmlattr)
        {
            SaveObject obj = new SaveObject();
            try
            {
                var file = Request.Files[htmlattr];
                string pathToSave = Server.MapPath(SynMetal_MVC.Models.Variables.pathToSavePDFs);
                string extension = Path.GetExtension(file.FileName);
                if (extension == ".pdf" || extension == ".PDF")
                {
                    if (file.ContentLength > 0 && file.ContentLength <= 6000000)
                    {


                        string filename = prefix + "_pdf_"
                            + Guid.NewGuid().ToString().Substring(0, 8) + extension;
                        string url = Path.Combine(pathToSave, filename);
                        file.SaveAs(url);
                        obj.Name = filename;
                        obj.FilePath = Path.Combine(Variables.pathToSavePDFs, filename);
                        obj.isValid = true;

                    }
                    else
                    {
                        obj.isValid = false;
                        obj.Error.Add("The size of the file must be below 6mb");
                    }
                }
                else
                {
                    obj.isValid = false;
                    obj.Error.Add("Not valid extension!! Use .pdf");

                }
            }
            catch (HttpException)
            {
                obj.isValid = false;
                obj.Error.Add("The size of the file must be below 6mb");
                // return obj;
            }

            return obj;
        }
        public static SaveObject EditPDF(int id, PdfFile editfile, HttpRequestBase Request, HttpServerUtilityBase Server, string prefix, string htmlattr)
        {
            SaveObject obj = new SaveObject();
            obj = DeletePDF(id, Server, true);
            if (obj.isValid)
            {
                obj = SavePDF(Request, Server, prefix, htmlattr);
               

            }
            return obj;
        }
        public static SaveObject DeletePDF(int id, HttpServerUtilityBase Server, bool IsEditable)
        {
            SaveObject obj = new SaveObject();
            PdfFile file = DB.PdfFiles.Find(id);

            if (file == null)
            {
                obj.isValid = false;
                obj.Error.Add("The File you trying to delete doesnt exist!");
                return obj;
            }
            try
            {
                File.Delete(Server.MapPath(file.FilePath));
                if (!IsEditable)
                {
                   

                    DB.PdfFiles.Remove(file);
                    DB.SaveChanges();
                }
                obj.Name = file.FileName;
                obj.isValid = true;


            }
            catch (FileNotFoundException)
            {
                obj.isValid = false;
                obj.Error.Add("The File:" + file.FileName + " could not be deleted");
                return obj;
            }

            return obj;
        }


    }

    static class PhotoHelper
    {
        static SynMetalEntities StoreDB = new SynMetalEntities();

        public static SaveObject SavePhoto(HttpRequestBase Request, HttpServerUtilityBase Server, string prefix, string htmlattr)
        {
            SaveObject obj = new SaveObject();
            try
            {
                var file = Request.Files[htmlattr];
                string pathToSave = Server.MapPath(SynMetal_MVC.Models.Variables.pathToSavePhotos);
                string extension = Path.GetExtension(file.FileName);
                if (extension == ".jpg" || extension == ".png" || extension == ".JPG" || extension == ".PNG")
                {
                    if (file.ContentLength > 0 && file.ContentLength <= 6000000)
                    {


                        string filename = prefix + "_photo_"
                            + Guid.NewGuid().ToString().Substring(0, 8) + extension;
                        string url = Path.Combine(pathToSave, filename);
                        file.SaveAs(url);
                        obj.Name = filename;
                        obj.FilePath = Path.Combine(Variables.pathToSavePhotos, filename);
                        obj.isValid = true;

                    }
                    else
                    {
                        obj.isValid = false;
                        obj.Error.Add("The size of the file must be below 6mb");
                    }
                }
                else
                {
                    obj.isValid = false;
                    obj.Error.Add("Not valid extension!! Use .jpg or .png");

                }
            }
            catch (HttpException)
            {
                obj.isValid = false;
                obj.Error.Add("The size of the file must be below 6mb");
                // return obj;
            }

            return obj;
        }

        public static SaveObject EditPhoto(int id, PhotoFile editfile, HttpRequestBase Request, HttpServerUtilityBase Server, string prefix, string htmlattr)
        {
            SaveObject obj = new SaveObject();
            obj = DeletePhoto(id, Server, true);
            if (obj.isValid)
            {
                obj = SavePhoto(Request, Server, prefix, htmlattr);
                if (obj.isValid)
                {
                    PhotoFile file = StoreDB.Photos.Find(id);
                    file.Description = editfile.Description;
                    file.Name = editfile.Name;
                    file.FilePath = obj.FilePath;
                    editfile = null;
                    StoreDB.Photos.AddOrUpdate(file);
                    StoreDB.SaveChanges();
                }

            }
            return obj;
        }

        public static SaveObject DeletePhoto(int id, HttpServerUtilityBase Server, bool IsEditable)
        {

            SaveObject obj = new SaveObject();
            PhotoFile file = StoreDB.Photos.Find(id);

            if (file == null)
            {
                obj.isValid = false;
                obj.Error.Add("The File you trying to delete doesnt exist!");
                return obj;
            }
            try
            {
                File.Delete(Server.MapPath(file.FilePath));
                if (!IsEditable)
                {
                    var trry = from prod in StoreDB.Products
                               where prod.Photo.PhotoId == file.PhotoId
                               select prod;
                    foreach(var tr in trry)
                    {
                        tr.Photo = null;
                    }
                    
                    StoreDB.Photos.Remove(file);
                    StoreDB.SaveChanges();
                }
                obj.Name = file.Name;
                obj.isValid = true;


            }
            catch (FileNotFoundException)
            {
                obj.isValid = false;
                obj.Error.Add("The File:" + file.Name + " could not be deleted");
                return obj;
            }

            return obj;
        }


    }

    static class DataHelpers
    {
        static SynMetalEntities StoreDB = new SynMetalEntities();

        public static void CreateCategories()
        {

            #region Categories


            StoreDB.Database.Connection.Open();

            string[] PdfCats = { "Ισολογισμός", "Τ.Γ.Συνέλευση", "Έκθεση ΔΣ", "Έκθεση Ελεγκτών" };
            foreach (string cat in PdfCats)
            {
                StoreDB.PdfCategories.AddOrUpdate(new PdfCategory { Name = cat });

            }
            string[] ProductCats = {"Αλουμίνια","Εξαρτήματα","Σίδερα","Λοιπά","Λαμαρίνες"
                    ,"Σταντζαριστά","Πλέγματα","Panel - Τσίγκοι"};
            foreach (string cat in ProductCats)
            {
                StoreDB.ProdCategories.AddOrUpdate(new ProductCategory { Name = cat });
            }
            StoreDB.SaveChanges();
            StoreDB.Database.Connection.Close();
            #endregion
        }

        public static void CreateNews()
        {
            for (int i = 0; i < 100; i++)
            {
                StoreDB.News.Add(new NewsModel { Name = "New" + i, Description = "mplamplamplamplampla " + i + " mplamplampla" });
            }
            StoreDB.SaveChanges();
        }
    }
}
