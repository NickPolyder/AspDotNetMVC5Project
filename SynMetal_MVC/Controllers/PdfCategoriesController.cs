using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SynMetal_MVC.Models;

namespace SynMetal_MVC.Controllers
{
    public class PdfCategoriesController : Controller
    {
        private SynMetalEntities db = new SynMetalEntities();

        // GET: PdfCategories
        public ActionResult Index()
        {
            return View(db.PdfCategories.ToList());
        }

        // GET: PdfCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PdfCategory pdfCategory = db.PdfCategories.Find(id);
            if (pdfCategory == null)
            {
                return HttpNotFound();
            }
            return View(pdfCategory);
        }

        // GET: PdfCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PdfCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,Name")] PdfCategory pdfCategory)
        {
            if (ModelState.IsValid)
            {
                db.PdfCategories.Add(pdfCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pdfCategory);
        }

        // GET: PdfCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PdfCategory pdfCategory = db.PdfCategories.Find(id);
            if (pdfCategory == null)
            {
                return HttpNotFound();
            }
            return View(pdfCategory);
        }

        // POST: PdfCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,Name")] PdfCategory pdfCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pdfCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pdfCategory);
        }

        // GET: PdfCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PdfCategory pdfCategory = db.PdfCategories.Find(id);
            if (pdfCategory == null)
            {
                return HttpNotFound();
            }
            return View(pdfCategory);
        }

        // POST: PdfCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PdfCategory pdfCategory = db.PdfCategories.Find(id);
            db.PdfCategories.Remove(pdfCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
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
