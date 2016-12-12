using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Configuration;
using System.IO;
using System.Text;
using Cryptografy;
using MvcApplicationDef.Models;

namespace MvcApplicationDef.Controllers
{
    public class DeleteController : Controller
    {
        //
        // GET: /Delete/

        public ActionResult fileDel()
        {
            return View();
        }

        public ActionResult FileDelete(string fileName)
        {
            DAL.BlobService blobService = new DAL.BlobService();

            blobService.DelFile(fileName, System.Web.HttpContext.Current.User.Identity.Name);
   
            return RedirectToAction("StorView", "View");
        }
    }
}
