using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SPU123_Shop_MVC.Interfaces;

namespace SPU123_Shop_MVC.Services
{
    public class AzureFileService : IFileService
    {
        private const string containerName = "product-images";
        private readonly IConfiguration configuration;

        public AzureFileService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Task DeleteImage(string path)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            BlobContainerClient client = new(configuration.GetConnectionString("AzureStorage"), containerName);

            await client.CreateIfNotExistsAsync();
            await client.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

            string name = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(file.FileName);
            string blobName = name + extension;

            BlobClient blob = client.GetBlobClient(blobName);
            await blob.UploadAsync(file.OpenReadStream());
            
            return blob.Uri.ToString();
        }
    }
}
