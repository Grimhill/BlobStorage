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
    public class DownLoadController : Controller
    {
        //
        // GET: /DownLoad/

        public ActionResult Fileload()
        {
            return View();
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FileDownload(string fileName)
        {
                DAL.BlobService blobService = new DAL.BlobService();
                //копируем взятый с хранилища объект в поток
                MemoryStream target = new MemoryStream();
                blobService.DownloadFile(target, fileName, System.Web.HttpContext.Current.User.Identity.Name);  
                target.Position = 0;

                //Задаем ключ для шифровки, преобразуем поток и шифруем
                var byteStream = new ByteStream();
                byte[] test = byteStream.ReadFully(target);
                
                byte[] bytekey = ASCIIEncoding.ASCII.GetBytes("Key");
         
                var encodeObject = new RC4(bytekey);
                byte[] result = encodeObject.Encode(test, test.Length);
                return File(result, "application/force-download", fileName);
        }
    }
}
