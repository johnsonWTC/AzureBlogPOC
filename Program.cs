using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Azure.Storage.Blobs;
using System.Threading.Tasks;



namespace AzureBlogPOC
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            await AzureBlobClient.UploadBlob();
        }
        static IConfigurationRoot GetConfigurationRoot()
        => new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
            .AddJsonFile("appsettings.json")
            .Build();

        public class AzureBlobClient
        {
            public static async Task UploadBlob()
            {
               var connectionString = "";
                string containerName = "videos";
                var serviceClient = new BlobServiceClient(connectionString);
                var containerClient = serviceClient.GetBlobContainerClient(containerName);
                const string JsonDoc = "./Docs/";
                string folderName = JsonDoc;
                var candidateID = "stumza.txt";
                string textFileLocation = folderName + candidateID + ".txt";
                var fileName = "stumza.txt";
                await File.WriteAllTextAsync(textFileLocation, "This is a test message");
                var blobClient = containerClient.GetBlobClient(fileName);
                Console.WriteLine("Uploading to Blob storage");
                using FileStream uploadFileStream = File.OpenRead(textFileLocation);
                await blobClient.UploadAsync(uploadFileStream, true);
                uploadFileStream.Close();
            }
        }
    }
}
