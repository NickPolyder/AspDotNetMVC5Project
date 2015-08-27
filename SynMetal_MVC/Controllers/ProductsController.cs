using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SynMetal_MVC.Models;
using System.Data.Entity.Migrations;
using System.Text.RegularExpressions;


namespace SynMetal_MVC.Controllers
{
    public class ProductsController : Controller
    {
        private SynMetalEntities db = new SynMetalEntities();

        // GET: Products
        public ActionResult Index()
        {
            var prod = db.Products;
            var result = prod.ToList();
           
            ViewBag.Categories = new SelectList(db.ProdCategories, "CategoryId", "Name");
            return View(result);
        }

        public ActionResult getProductsBy(int? id)
        {
            System.Threading.Thread.Sleep(5000);

            List<Product> products;
            products = Search.ByProdCategory(id);
            
            return PartialView("Partial/_ProductView",products);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        #region photo_actions
        public ActionResult CreateUploadForm()
        {
            return PartialView("Partial/_PhotoUpEdDel",null);
        }


        public ActionResult UploadPhoto()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadPhoto([Bind(Include = "Name,Description")]  PhotoFile file)
        {
            SaveObject obj;
            try
            {
                obj = PhotoHelper.SavePhoto(Request, Server, "Product", "FileUpload");
                if (obj.isValid)
                {
                    if (ModelState.IsValid)
                    {
                        file.FilePath = obj.FilePath;


                        db.Photos.AddOrUpdate(file);
                        db.SaveChanges();
                        Session.Add("FileID", file.PhotoId);

                    }
                    else
                    {
                        return View(file);
                    }
                    //  ViewBag.ProdCategories = new SelectList(db.ProdCategories, "CategoryId", "Name");

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

                            obj.Error.Add("Edit ID Problem or Something!");
                            ViewBag.Errors = obj;

                            return View();
                        }
                    }
                    string prod = null;
                    dynamic model = null;
                    switch (check)
                    {
                        case PhotoEdit.Create:
                            ViewBag.FileName = db.Photos.Find(file.PhotoId);

                            ViewBag.ProdCategories = new SelectList(db.ProdCategories, "CategoryId", "Name");
                            path = "Create";
                            break;
                        case PhotoEdit.EditProduct:
                            TempData["FileName"] = db.Photos.Find(file.PhotoId);
                            // model = db.Products.Find();
                            prod = "Products";
                            path = "Edit";
                            break;
                        case PhotoEdit.Edit:

                            model = db.Photos.Find(file.PhotoId);
                            path = "EditPhoto";
                            break;
                        default:
                            return RedirectToAction("Index");

                    }
                    System.Web.Routing.RouteValueDictionary routeValues = new System.Web.Routing.RouteValueDictionary();
                    routeValues.Add("id",EditId);
                    
                    return RedirectToAction(path,prod,routeValues);


                    // return RedirectToAction("Create");

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

        [HttpGet]
        public ActionResult DeletePhoto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!Regex.IsMatch(id.ToString(), Variables.RegexForNumbers))
            {
                return Content("Problem with your Id tag!");
            }
            SaveObject Deleted = PhotoHelper.DeletePhoto((int)id, Server, false);
            if (Deleted.isValid)
            {
                return Content("The File with Name: " + Deleted.Name + " has been Deleted Successfully!!");
            }

            return PartialView("Partial/_DeletedPhoto", Deleted);
        }
        [HttpGet]
        public ActionResult EditPhoto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhotoFile file = db.Photos.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }
        [HttpPost]
        public ActionResult EditPhoto([Bind(Include = "Name,Description")] PhotoFile editfile, int? id)
        {
            string htmlattr = "FileUpload";
            SaveObject Edit = new SaveObject();

            if (!Regex.IsMatch(id.ToString(), Variables.RegexForNumbers))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Request.Files[htmlattr].ContentLength > 0)
            {
                Edit = PhotoHelper.EditPhoto((int)id, editfile, Request, Server, "Product", htmlattr);
            }
            else
            {
                PhotoFile file = db.Photos.Find(id);
                if (file == null)
                {
                    return HttpNotFound();
                }
                file.Description = editfile.Description;
                file.Name = editfile.Name;
                try
                {
                    db.Photos.AddOrUpdate(file);
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
                if(Session["EditID"] != null)
                {
                    EditId = Regex.IsMatch(Session["EditID"].ToString(), Variables.RegexForNumbers) ?
                        Int32.Parse(Session["EditID"].ToString()) : -1;
                    Session.Remove("EditID");
                    if(EditId == -1)
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
                        ViewBag.FileName = db.Photos.Find(id);

                        ViewBag.ProdCategories = new SelectList(db.ProdCategories, "CategoryId", "Name");
                        path = "Create";
                        break;
                    case PhotoEdit.EditProduct:
                        TempData["FileName"] = db.Photos.Find(id);
                        // model = db.Products.Find();
                        prod = "Products";
                        path = "Edit";
                        break;
                    case PhotoEdit.Edit:

                        model = db.Photos.Find(id);
                        path = "EditPhoto";
                        break;
                    default:
                        return RedirectToAction("Index");

                }
                if(Session["FromEditProduct"] !=null)
                {
                    bool ep = bool.Parse(Session["FromEditProduct"].ToString());
                    Session.Remove("FromEditProduct");
                    if(ep)
                    {
                        path = "Edit";
                        prod = "Products";
                        TempData["FileName"] = db.Photos.Find(id);
                    }
                }
                System.Web.Routing.RouteValueDictionary routeValues = new System.Web.Routing.RouteValueDictionary();
                routeValues.Add("id", EditId);

                return RedirectToAction(path, prod, routeValues);

            }
            ViewBag.Edit = Edit;
            return View();

        }

        #endregion
        // GET: Products/Create
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

            ViewBag.FileName = db.Photos.Find(id);
            ViewBag.ProdCategories = new SelectList(db.ProdCategories, "CategoryId", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from over posting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description")] Product product)
        {
            int? ProdCatId = null;
            var req = Request["ProdCategories"];
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
                        ViewBag.ProdCategories = new SelectList(db.ProdCategories, "CategoryId", "Name");
                        ViewBag.ProdCatError = "Please Select a Product Category!";

                    }
                }
            }
            product.Category = db.ProdCategories.Find(ProdCatId);
            ModelState.Clear();
            

            if (TryValidateModel(product))
            {
                int? id;
                if (Request["PhotoId"] != null && Regex.IsMatch(Request["PhotoId"].ToString(), Variables.RegexForNumbers))
                {
                    id = Int32.Parse(Request["PhotoId"].ToString());


                }
                else
                {
                    id = null;
                }

                product.Photo = db.Photos.Find(id);
                
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProdCategories = new SelectList(db.ProdCategories, "CategoryId", "Name",product.Category.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,Name,Description")] Product product)
        {
            int? ProdCatId = null;
            var req = Request["ProdCategories"];
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
                        ViewBag.ProdCategories = new SelectList(db.ProdCategories, "CategoryId", "Name");
                        ViewBag.ProdCatError = "Please Select a Product Category!";

                    }
                }
            }
            Product editprod = db.Products.Find(product.ProductId); // Creates the Edit Product to populate for update 
            // in the database
            product.Category = db.ProdCategories.Find(editprod.Category.CategoryId); // Gets the old Category
            ProductCategory NewCategory = db.ProdCategories.Find(ProdCatId); //Gets the Category
            // from the form 
            if (NewCategory != null && product.Category.CategoryId != NewCategory.CategoryId) // if the new category
                //is not null and is different from the Category of old Category!
            {
                editprod.Category = db.ProdCategories.Find(ProdCatId);
            }
            else
            {
                ViewBag.ProdCategories = new SelectList(db.ProdCategories, "CategoryId", "Name");
                ViewBag.ProdCatError = "Please Select a Valid Product Category!";
            }
            
        
            ModelState.Clear();
            ValidateModel(product);
            if (ModelState.IsValid)
            {
                int? id;
                if (Request["PhotoId"] != null && Regex.IsMatch(Request["PhotoId"].ToString(), Variables.RegexForNumbers))
                {
                    id = Int32.Parse(Request["PhotoId"].ToString());


                }
                else
                {
                    id = null;
                }

                editprod.Photo = db.Photos.Find(id);

                // db.Entry(product).State = EntityState.Modified;
              
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProdCategories = new SelectList(db.ProdCategories, "CategoryId", "Name");
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            if(product.Photo !=null)
            {
                PhotoHelper.DeletePhoto(product.Photo.PhotoId, Server, false);
            }
            db.Products.Remove(product);
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
