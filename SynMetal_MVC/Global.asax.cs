using SynMetal_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SynMetal_MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
         
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            SynMetalEntities db = new SynMetalEntities();
            var prodPhotoId = db.Products.Select(x => x.Photo.PhotoId).ToArray();
            var Images = db.Photos.Where(x => !prodPhotoId.Contains(x.PhotoId));
            bool db_changed = false;
            foreach(PhotoFile ph in Images)
            {
                var file = Server.MapPath(ph.FilePath);
              
                DateTime date = File.GetCreationTime(file);
                if(date.AddHours(1) < DateTime.Now )
                {
                    File.Delete(file);
                   
                   db.Photos.Remove(db.Photos.Find(ph.PhotoId));
                    db_changed = true;
                }

            }
            if(db_changed)
            {
                db.SaveChanges();
            }
            

        }

        private void Application_Error(object sender, EventArgs e)
        {
            if (GlobalHelper.IsMaxRequestExceededException(this.Server.GetLastError()))
            {
                this.Server.ClearError();
              
                Session.Add("ToBigFile", "The size of the file must be below 6mb");
                Response.Redirect("UploadPhoto");
                // this.Server.Transfer("Products/UploadPhoto",false);
            }
            if (GlobalHelper.IsHttpRequestValidationException(this.Server.GetLastError()))
            {
                this.Server.ClearError();

                Session.Add("HttpValid", "Hey Yaaaa next time try without the XSS!");
                Response.Redirect("Error");
                // this.Server.Transfer("Products/UploadPhoto",false);
            }
        }
        static class GlobalHelper
        {
            const int TimedOutExceptionCode = -2147467259;
            public static bool IsMaxRequestExceededException(Exception e)
            {
                // unhandled errors = caught at global.ascx level
                // http exception = caught at page level

                Exception main;
                var unhandled = e as HttpUnhandledException;

                if (unhandled != null && unhandled.ErrorCode == TimedOutExceptionCode)
                {
                    main = unhandled.InnerException;
                }
                else
                {
                    main = e;
                }


                var http = main as HttpException;

                if (http != null && http.ErrorCode == TimedOutExceptionCode)
                {
                    // hack: no real method of identifying if the error is max request exceeded as 
                    // it is treated as a timeout exception
                    if (http.StackTrace.Contains("GetEntireRawContent"))
                    {
                        // MAX REQUEST HAS BEEN EXCEEDED
                        return true;
                    }
                }

                return false;
            }


            public static bool IsHttpRequestValidationException(Exception e)
            {
                // unhandled errors = caught at global.ascx level
                // http exception = caught at page level

                Exception main;
                var unhandled = e as HttpRequestValidationException;

                if (unhandled != null && unhandled.ErrorCode == TimedOutExceptionCode)
                {
                    //   main = unhandled.InnerException;
                    main = unhandled;
                }
                else
                {
                    main = e;
                }


                var http = main as HttpException;

                if (http != null && http.ErrorCode == TimedOutExceptionCode)
                {
                    // hack: no real method of identifying if the error is max request exceeded as 
                    // it is treated as a timeout exception
                    if (http.StackTrace.Contains("System.Web.HttpRequest.ValidateString"))
                    {
                        // HttpRequest valid Has Delivered
                        return true;
                    }
                }

                return false;
            }

        }
    }
}
