using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SynMetal_MVC.Models;
using SynMetal_MVC.Models.ViewMod;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace SynMetal_MVC.Controllers
{
    public class HomeController : Controller
    {
        SynMetalEntities db = new SynMetalEntities();

        public ActionResult changes()
        {
            Session.Add("NN", 22);
            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {

            #region Creation of dummy data
            //if(!db.News.Any())
            //{ 
            //DataHelpers.CreateNews();
            //}
            //if (!db.PdfCategories.Any() && !db.ProdCategories.Any())
            //{
            //    DataHelpers.CreateCategories();
            //}
            #endregion

            #region loadLastPDFAndProductAndNews 
            try
            {
                HomeIndexView view = new HomeIndexView();
                if (db.Products.Any())
                {
                    var last = db.Products.OrderByDescending(u => u.ProductId).First();

                    view.lastProduct.Description = last.Description;
                    view.lastProduct.id = last.ProductId;
                    view.lastProduct.Name = last.Name;

                    last = null;
                }
                else
                {
                    view.lastProduct = null;
                }

                var allCategories = db.PdfCategories.ToList();
                foreach (PdfCategory pd in allCategories)
                {
                    var LastPdf1 = (from dd in db.PdfFiles.OrderByDescending(u => u.PdfId)
                                    where dd.Category.CategoryId == pd.CategoryId
                                    select dd).ToList();
                    HomeIndexView.PdfView pdf = new HomeIndexView.PdfView();

                    if (LastPdf1.Count > 0)
                    {
                        pdf.Description = LastPdf1.First().Description;
                        pdf.FileName = LastPdf1.First().FileName;
                        pdf.FilePath = LastPdf1.First().FilePath;
                        pdf.Category = LastPdf1.First().Category;
                        pdf.id = LastPdf1.First().PdfId;
                        view.LastPdf.Add(pdf);
                    }
                    

                    pdf = null;
                    LastPdf1 = null;
                }
                if(view.LastPdf.Count == 0 )
                {
                    view.LastPdf = null;
                }
                if(db.News.Any())
                { 
                view.LastNews = db.News.OrderByDescending(k => k.NewsId).Take(db.News.Count() > 5 ? 5 : 2).ToList();
                }
                else
                {
                    view.LastNews = null;
                }
                return View(view);
            }
            catch (InvalidOperationException)
            {
                return View();
            }
            #endregion



        }


        public ActionResult getPdfCategoriesPartial()
        {
            return PartialView("Partial/_getpdfCategories",db.PdfCategories.ToList());
        }



        public ActionResult AboutUsPartial()
        {

            return PartialView("Partial/_ForUs");
        }

        public ActionResult About()
        {
           
            ViewBag.Message = "Your application description page. " ;
            


            return View(db.PdfCategories.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Search(string SearchText,int? page)
        {

            string searchstring;
            try { 
            searchstring = Request.QueryString["SearchText"] != null ? Request.QueryString["SearchText"] : "";
            }catch(HttpRequestValidationException)
            {
                searchstring = "";
            }
            List<SearchView> search = new List<SearchView>();
            if(Regex.IsMatch(searchstring.ToString(),Variables.RegexForNames))
            {
                
                var news = db.News.Where(S => S.Name.Contains(searchstring)
                || S.Description.Contains(searchstring)).ToList();
                var prods = db.Products.Where(S => S.Name.Contains(searchstring) 
                || S.Description.Contains(searchstring)).ToList();
                var files = db.PdfFiles.Where(S => S.FileName.Contains(searchstring)
                || S.Description.Contains(searchstring)).ToList();
                int CountNews = news.Count;
                int CountProds = prods.Count;
                int CountFiles = files.Count;
                int i = 0;
                int j = 0;
                int k = 0;
                while(CountNews > 0 || CountProds>0 || CountFiles>0)
                {
                    SearchView res = new SearchView();
                    if(CountNews>0)
                    {
                        res.News = news[i++];
                        CountNews--;
                    }
                    if(CountProds >0)
                    {
                        res.Products = prods[j++];
                        CountProds--;
                    }
                    if(CountFiles >0)
                    {
                        res.Files = files[k++];
                        CountFiles--;
                    }
                    search.Add(res);

                }
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                ViewBag.Search = searchstring;
                ViewBag.CountRes = search.Count;
                return View(search.ToPagedList(pageNumber,pageSize));

            }

            return View();
        }


        public ActionResult Error()
        {
            return View();
        }
    }
}