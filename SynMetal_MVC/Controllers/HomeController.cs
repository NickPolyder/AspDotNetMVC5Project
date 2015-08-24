using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SynMetal_MVC.Models;
using SynMetal_MVC.Models.ViewMod;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SynMetal_MVC.Controllers
{
    public class HomeController : Controller
    {
        SynMetalEntities db = new SynMetalEntities();

        public ActionResult Index()
        {
            
            if(!db.PdfCategories.Any() && !db.ProdCategories.Any())
            {
                DataHelpers.CreateCategories();
            }
            
            HomeIndexView view = new HomeIndexView();
            var last = db.Products.OrderByDescending(u => u.ProductId).First();
            
            view.lastProduct.Description = last.Description;
            view.lastProduct.id = last.ProductId;
            view.lastProduct.Name = last.Name;
            last = null;
            var allCategories = db.PdfCategories.ToList() ;
            foreach(PdfCategory pd in allCategories)
            {
                var LastPdf1 = (from dd in db.PdfFiles.OrderByDescending(u =>u.PdfId)
                              where dd.Category.CategoryId == pd.CategoryId
                              select  dd).ToList();
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
           
           

            return View(view);
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
    }
}