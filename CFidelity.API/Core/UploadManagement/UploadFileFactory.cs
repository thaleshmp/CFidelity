using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace CFidelity.API.Core.UploadManagement
{
    public class UploadFileFactory : IUploadFileFactory
    {
        IConfiguration _configuration;

        public UploadFileFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateUploadFile(UploadFile fileUpload)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_configuration.GetConnectionString("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(fileUpload.Container);

            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileUpload.PrefixFolderNameFullName);

            byte[] bytes = System.Convert.FromBase64String(fileUpload.File);

            Task.Run(() => blockBlob.UploadFromByteArrayAsync(bytes, 0, bytes.Length)).Wait();

            return blockBlob.Uri.OriginalString;
        }
    }
}
