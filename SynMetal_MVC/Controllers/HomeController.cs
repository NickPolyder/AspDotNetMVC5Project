using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SynMetal_MVC.Models;
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
     
        public ActionResult Index()
        {
         //   DataHelpers.CreateCategories();
            return View();
        }
        public ActionResult AboutUsPartial()
        {

            return PartialView("Partial/_ForUs");
        }

        public ActionResult About()
        {
           
            ViewBag.Message = "Your application description page. " ;
            SynMetalEntities db = new SynMetalEntities();


            return View(db.PdfCategories.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}