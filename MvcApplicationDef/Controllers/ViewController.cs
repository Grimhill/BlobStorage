using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MvcApplicationDef.Models;

namespace MvcApplicationDef.Controllers
{
    public class ViewController : Controller
    {
        //
        // GET: /View/

        public ActionResult StorView()
        {
            DAL.BlobService service = new DAL.BlobService();
            
            ViewModel model = new ViewModel()
            {
                Files = service.GetFilesList(System.Web.HttpContext.Current.User.Identity.Name), //контейнер с именем пользователя
                Extensions = new List<string>() { "jpg", "png", "jpeg" },
                //data = service.
            };
            return View(model);
        }

    }
}
