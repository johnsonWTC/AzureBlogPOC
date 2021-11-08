using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Azure.Storage.Blobs;
using System.Threading.Tasks;



namespace AzureBlogPOC
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
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
                var connectionString = "<Enter the connection string here>";
                string containerName = "blobcontainer";
                var serviceClient = new BlobServiceClient(connectionString);
                var containerClient = serviceClient.GetBlobContainerClient(containerName);
                const string JsonDoc = "./Docs/";
                string folderName = JsonDoc;
                var candidateID = "testfile";
                string textFileLocation = folderName + candidateID + ".txt";



                var fileName = "testfile.txt";
             //   var localFile = Path.Combine(path, fileName);
                await File.WriteAllTextAsync(textFileLocation, "This is a test message");
                var blobClient = containerClient.GetBlobClient(fileName);
                Console.WriteLine("Uploading to Blob storage");
                using FileStream uploadFileStream = File.OpenRead(textFileLocation);
                await blobClient.UploadAsync(uploadFileStream, true);
                uploadFileStream.Close();
            }
        }
}
