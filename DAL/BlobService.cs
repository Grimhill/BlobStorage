using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;

using System.IO;

namespace DAL
{
    public class BlobService
    {
        //private CloudBlockBlob snapshot;
        //переменные клиента и аккаунта
        private CloudBlobClient Client;
        private CloudStorageAccount StorageAccount;

        public BlobService() 
        {
            //get connection options
            string connstring = ConfigurationManager.AppSettings["AzureStorageConn"];
            //Create blob storage
            StorageAccount    = CloudStorageAccount.Parse(connstring);
            //blob client
            Client            = StorageAccount.CreateCloudBlobClient();
        }

        public CloudBlobContainer GetContainer(string containerName) 
        {
            //create containter
            CloudBlobContainer container = Client.GetContainerReference(containerName.ToLower());
            //контейнер делается открытым
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            return container;
        }

        //выбор файла для загрузки в хранилище. Имя контейнера по умолчанию "default"
        //загрузка файла в конейнер
        public void SaveFile(Stream stream, string fileName, string containerName = "default")
        {
            var container       = GetContainer(containerName);
            // Retrieve reference to a blob
            CloudBlockBlob blob = container.GetBlockBlobReference(System.IO.Path.GetFileName(fileName));
            
            blob.UploadFromStream(stream);                        
        }

        //скачивание файла
        public void DownloadFile (Stream stream, string filename, string containerName = "default")
        {
            var container = GetContainer(containerName);
 
            CloudBlockBlob blockblob = container.GetBlockBlobReference(filename);
 
            blockblob.DownloadToStream(stream);
        }

        //удаление файла
        public void DelFile (string filename, string containerName = "default")
        {
            var container = GetContainer(containerName);

            CloudBlockBlob delblob = container.GetBlockBlobReference(filename);
            delblob.Delete();
        }

        //Перечисление blob-объектов в контейнере
        public List<Data> GetFilesList(string containerName = "default")
        {
            var result = new List<Data>();
            var container = GetContainer(containerName);
            // Loop over items
            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    //новый объект класса, где принимаем интересующие параметры
                    var it = new Data();

                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    //Имя файла
                    it.FileName = blob.Name.ToString();
                    //размер файла в байтах
                    it.Size = blob.Properties.Length;

                    //дата и время загрузки    
                    CloudBlockBlob snapshot = blob.CreateSnapshot();

                    if (blob != null)
                    {
                        blob.FetchAttributes();
                        DateTime lastModifiedUtc = blob.Properties.LastModified.Value.DateTime.ToLocalTime();
                        it.Time = lastModifiedUtc;
                    }                    

                    //ссылка на файл в хранилище
                    it.Url = blob.Uri.ToString();
                    //Имя владельца файла
                    it.UserName = containerName;
                    
                    result.Add(it);                    
                }
            }
            return result;
        }
    }
}
