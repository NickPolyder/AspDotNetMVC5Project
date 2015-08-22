using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SynMetal_MVC.Controllers
{
    public class TryController : Controller
    {
        // GET: Try
        public ActionResult Index()
        {
            return View();
        }
        [Route("Try/Upload")]
        [HttpGet]
        public ActionResult AddPhoto()
        {
            return View();
        }
        [Route("Try/Upload")]
        [HttpPost]
        public ActionResult AddPhotos()
        {
            foreach(string upload in Request.Files) { 
            if (Request.Files[upload].ContentLength > 0)
            {
                string pathToSave = Server.MapPath("~/Photos/");
                string filename = Path.GetFileName(Request.Files[upload].FileName);
                Request.Files[upload].SaveAs(Path.Combine(pathToSave, filename));
                return Content(upload+" |Done| "+filename);
            }
            }
            return Content("Fail");
            

        }

    }
}