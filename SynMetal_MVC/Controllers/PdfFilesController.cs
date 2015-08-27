using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SynMetal_MVC.Models;
using System.Text.RegularExpressions;
using System.Data.Entity.Migrations;

namespace SynMetal_MVC.Controllers
{
    public class PdfFilesController : Controller
    {
        private SynMetalEntities db = new SynMetalEntities();

        // GET: PdfFiles
        public ActionResult Index(int? id)
        {

            List<PdfFile> result;
            if(id == null)
            {
                result = db.PdfFiles.ToList();
                ViewBag.PdfCategories = new SelectList(db.PdfCategories, "CategoryId", "Name");

            }
            else
            {
                result = Search.ByPdfCategory(id);
                if(Regex.IsMatch(id.ToString(),Variables.RegexForNumbers))
                { 
                ViewBag.PdfCategories = new SelectList(db.PdfCategories, "CategoryId", "Name",db.PdfCategories.Find(id).CategoryId);
                }
                else
                {
                    ViewBag.PdfCategories = new SelectList(db.PdfCategories, "CategoryId", "Name");
                }
            }
            
            return View(result);
        }
        
        public ActionResult getPdfsBy(int? id)
        {
            System.Threading.Thread.Sleep(5000);

            List<PdfFile> PdfFile;
            PdfFile = Search.ByPdfCategory(id);

            return PartialView("Partial/_PdfPerCategory", PdfFile);
        }

        // GET: PdfFiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PdfFile pdfFile = db.PdfFiles.Find(id);
            if (pdfFile == null)
            {
                return HttpNotFound();
            }
            return View(pdfFile);
        }
        #region pdffile
        public ActionResult CreateUploadForm()
        {
            return PartialView("Partial/_PdfUpEdDel", null);
        }





        [HttpGet]
        public ActionResult DeletePDF(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!Regex.IsMatch(id.ToString(), Variables.RegexForNumbers))
            {
                return Content("Problem with your Id tag!");
            }
            SaveObject Deleted = PdfHelper.DeletePDF((int)id, Server, false);
            if (Deleted.isValid)
            {
                return Content("The File with Name: " + Deleted.Name + " has been Deleted Successfully!!");
            }

            return PartialView("Partial/_DeletedPDF", Deleted);
        }
        [HttpGet]
        public ActionResult EditPDF(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PdfFile file = db.PdfFiles.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }
        [HttpPost]
        public ActionResult EditPDF([Bind(Include = "FileName,Description")] PdfFile editfile, int? id)
        {
            string htmlattr = "FileUpload";
            SaveObject Edit = new SaveObject();

            if (!Regex.IsMatch(id.ToString(), Variables.RegexForNumbers))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Request.Files[htmlattr].ContentLength > 0)
            {
                Edit = PdfHelper.EditPDF((int)id, editfile, Request, Server, "Product", htmlattr);
            }
            else
            {
                PdfFile file = db.PdfFiles.Find(id);
                if (file == null)
                {
                    return HttpNotFound();
                }
                file.Description = editfile.Description;
                file.FileName = editfile.FileName;
                try
                {
                    db.PdfFiles.AddOrUpdate(file);
                    db.SaveChanges();
                    Edit.isValid = true;
                }
                catch (Exception)
                {
                    Edit.Error.Add("Something happened!");
                    Edit.isValid = false;
                }
            }
            if (Edit.isValid)
            {
                string path = "";
                PhotoEdit check = PhotoEdit.def;
                if (Session["EditUrl"] != null)
                {
                    check = (PhotoEdit)Enum.Parse(typeof(PhotoEdit), Session["EditUrl"].ToString());
                    Session.Remove("EditUrl");
                }
                int EditId = 0;
                if (Session["EditID"] != null)
                {
                    EditId = Regex.IsMatch(Session["EditID"].ToString(), Variables.RegexForNumbers) ?
                        Int32.Parse(Session["EditID"].ToString()) : -1;
                    Session.Remove("EditID");
                    if (EditId == -1)
                    {
                        Edit.Error.Add("Edit ID Problem or Something!");
                        ViewBag.Edit = Edit;

                        return View();
                    }
                }
                string prod = null;
                dynamic model = null;
                switch (check)
                {
                    case PhotoEdit.Create:
                        ViewBag.FileName = db.PdfFiles.Find(id);

                        ViewBag.PDFCategories = new SelectList(db.PdfCategories, "CategoryId", "Name");
                        path = "Create";
                        break;
                    case PhotoEdit.EditProduct:
                        TempData["FileName"] = db.PdfFiles.Find(id);
                        // model = db.Products.Find();
                        prod = "PdfFiles";
                        path = "Edit";
                        break;
                    case PhotoEdit.Edit:

                        model = db.PdfFiles.Find(id);
                        path = "EditPDF";
                        break;
                    default:
                        return RedirectToAction("Index");

                }
                if (Session["FromEditProduct"] != null)
                {
                    bool ep = bool.Parse(Session["FromEditPDF"].ToString());
                    Session.Remove("FromEditPDF");
                    if (ep)
                    {
                        path = "Edit";
                        prod = "Products";
                        TempData["FileName"] = db.PdfFiles.Find(id);
                    }
                }
                System.Web.Routing.RouteValueDictionary routeValues = new System.Web.Routing.RouteValueDictionary();
                routeValues.Add("id", EditId);

                return RedirectToAction(path, prod, routeValues);

                // return RedirectToAction(path, model);

            }
            ViewBag.Edit = Edit;
            return View();

        }

        #endregion
        // GET: PdfFiles/Create
        public ActionResult Create()
        {
            int? id;
            if (Session["FileID"] != null && Regex.IsMatch(Session["FileID"].ToString(), Variables.RegexForNumbers))
            {
                id = Int32.Parse(Session["FileID"].ToString());
                Session.Remove("FileID");

            }
            else
            {
                id = null;
            }

            ViewBag.FileName = db.PdfFiles.Find(id);
            ViewBag.PDFCategories = new SelectList(db.PdfCategories, "CategoryId", "Name");
            return View();
        }

        // POST: PdfFiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FileName,Description")] PdfFile pdfFile)
        {

            int? PdfCatId = null;
            var req = Request["PDFCategories"];
            if (req != null)
            {
                if (req == "")
                {
                    req = "0";
                }

                if (Regex.IsMatch(req, Variables.RegexForNumbers))
                {
                    PdfCatId = Int32.Parse(req);
                    if (PdfCatId == 0 || PdfCatId == null)
                    {
                        ViewBag.PDFCategories = new SelectList(db.PdfCategories, "CategoryId", "Name");
                        ViewBag.PdfCatError = "Please Select a Product Category!";

                    }
                }
            }
            SaveObject obj;
            try
            {
                obj = PdfHelper.SavePDF(Request, Server, "PDF", "FileUpload");
                if (obj.isValid)
                {
                    pdfFile.Category = db.PdfCategories.Find(PdfCatId);
                    ModelState.Clear();
                    if (TryValidateModel(pdfFile))
                    {

                        pdfFile.FilePath = obj.FilePath;

                        db.PdfFiles.AddOrUpdate(pdfFile);
                        db.SaveChanges();
                        Session.Add("FileID", pdfFile.PdfId);
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        return View(pdfFile);
                    }
                    //  ViewBag.ProdCategories = new SelectList(db.ProdCategories, "CategoryId", "Name");




                }
                else
                {
                    ViewBag.Errors = obj.Error;
                    return View();
                }
            }
            catch (HttpException)
            {
                obj = new SaveObject();
                obj.isValid = false;
                obj.Error.Add("The size of the file must be below 6mb");
                return View();
            }

        }

        // GET: PdfFiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PdfFile pdfFile = db.PdfFiles.Find(id);
            if (pdfFile == null)
            {
                return HttpNotFound();
            }
            ViewBag.PDFCategories = new SelectList(db.PdfCategories, "CategoryId", "Name", pdfFile.Category.CategoryId);
            return View(pdfFile);
        }

        // POST: PdfFiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PdfId,FileName,Description")] PdfFile pdfFile)
        {

            int? ProdCatId = null;
            var req = Request["PDFCategories"];
            if (req != null)
            {
                if (req == "")
                {
                    req = "0";
                }

                if (Regex.IsMatch(req, Variables.RegexForNumbers))
                {
                    ProdCatId = Int32.Parse(req);
                    if (ProdCatId == 0 || ProdCatId == null)
                    {
                        ViewBag.PDFCategories = new SelectList(db.PdfCategories, "CategoryId", "Name");
                        ViewBag.PdfCatError = "Please Select a Pdf  Category!";

                    }
                }
            }
            PdfFile editprod = db.PdfFiles.Find(pdfFile.PdfId); // Creates the Edit Pdf to populate for update 
            // in the database
            pdfFile.Category = db.PdfCategories.Find(editprod.Category.CategoryId); // Gets the old Category
            PdfCategory NewCategory = db.PdfCategories.Find(ProdCatId); //Gets the Category
            // from the form 
            if (NewCategory != null && pdfFile.Category.CategoryId != NewCategory.CategoryId) // if the new category
                                                                                              //is not null and is different from the Category of old Category!
            {
                editprod.Category = db.PdfCategories.Find(ProdCatId);
            }
            else
            {
                ViewBag.PDFCategories = new SelectList(db.PdfCategories, "CategoryId", "Name");
                ViewBag.PdfCatError = "Please Select a Valid Pdf Category!";
            }


            ModelState.Clear();
            ValidateModel(pdfFile);
            if (ModelState.IsValid)
            {
                SaveObject obj;
                string htmlattr = "FileUpload";
                if (Request.Files[htmlattr].ContentLength > 0)
                {
                    obj = PdfHelper.EditPDF(editprod.PdfId, editprod, Request, Server, "PDF", htmlattr);
                    if (obj.isValid)
                    {
                        editprod.FilePath = obj.FilePath;
                    }
                    else
                    {
                        ViewBag.Errors = obj.Error;
                        return View();
                    }
                }



                if (pdfFile.FileName != null && editprod.FileName != pdfFile.FileName)
                {
                    editprod.FileName = pdfFile.FileName;
                }
                if (pdfFile.Description != null && editprod.Description != pdfFile.Description)
                {
                    editprod.Description = pdfFile.Description;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
           


            // db.Entry(product).State = EntityState.Modified;


        


        ViewBag.PDFCategories = new SelectList(db.PdfCategories, "CategoryId", "Name");
            return View(pdfFile);
    }

    // GET: PdfFiles/Delete/5
    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        PdfFile pdfFile = db.PdfFiles.Find(id);
        if (pdfFile == null)
        {
            return HttpNotFound();
        }
        return View(pdfFile);
    }

    // POST: PdfFiles/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        PdfFile pdfFile = db.PdfFiles.Find(id);
        SaveObject obj = PdfHelper.DeletePDF(id, Server, false);
        db.PdfFiles.Remove(pdfFile);
        db.SaveChanges();
        return RedirectToAction("Index");
    }


        public ActionResult getPDFbyCategory(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            if(Regex.IsMatch(id.ToString(),Variables.RegexForNumbers))
            {
                var edit = db.PdfCategories.Find(id);
                var prod = from pdf in db.PdfFiles
                           where pdf.Category.CategoryId == edit.CategoryId
                           select pdf;
                return PartialView("Partial/_PdfPerCategory",prod);
            }
            else
            {
                return Content("your id isnt right!");
            }
           
        }


    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            db.Dispose();
        }
        base.Dispose(disposing);
    }
}
}
