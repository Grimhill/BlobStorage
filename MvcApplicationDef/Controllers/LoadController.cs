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
    public class LoadController : Controller
    {
        //
        // GET: /Load/

        public ActionResult FileUpload()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FileUpload(HttpPostedFileBase uploadFile)
        {
            if (uploadFile.ContentLength > 0)
            {
                //Upload file to server and get file path to load into the storage
                string filePath = System.IO.Path.Combine(HttpContext.Server.MapPath("../Uploads"),
                                               System.IO.Path.GetFileName(uploadFile.FileName));
                MemoryStream target = new MemoryStream();

                //копируем файл в поток
                uploadFile.InputStream.CopyTo(target);
                target.Position = 0;

                //Задаем ключ для шифровки, преобразуем в поток байт и шифруем
                var byteStream = new ByteStream();
                byte[] test = byteStream.ReadFully(target);
                
                byte[] bytekey = ASCIIEncoding.ASCII.GetBytes("Key");             

                var encodeObject = new RC4(bytekey);
                byte[] result = encodeObject.Encode(test, test.Length);

                //обратное преобразование уже зашифрованного файла в поток
                target = new MemoryStream(result);

                //Создание нового объекта для работы с blob - хранилищем 
                DAL.BlobService blobService = new DAL.BlobService();

                //Сохранение в хранилище зашифрованого файла с потока
                blobService.SaveFile(target, System.IO.Path.GetFileName(filePath), System.Web.HttpContext.Current.User.Identity.Name);
            }
            return View();
        }
    }
}
