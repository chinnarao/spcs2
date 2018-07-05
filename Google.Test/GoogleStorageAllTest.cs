using Google;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Xunit;

//https://console.cloud.google.com/storage/browser?project=oceanic-cacao-203021&folder&organizationId
namespace Google.Test
{
    public class GoogleStorageAllTest
    {
        [Fact]
        public void Test_UploadFile()
        {
            IConfiguration _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string bucketName = _configuration["AdBucketNameInGoogleCloudStorage"];
            string fileName = "1.jpg";
            string objectName = DateTime.Now.ToString("yyyy-MM-dd-") + new Random().Next(0,Byte.MaxValue) + Path.GetExtension(fileName);
            GoogleStorageAll storage = new GoogleStorageAll();
            storage.UploadFile(bucketName, fileName, objectName);
            Assert.True(1 < 2);
        }

        [Fact]
        public void Test_LoadAnyJsonLocalFile()
        {
            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("spgoogle-fe1635053afe.json", optional: false, reloadOnChange: true).Build();
            Assert.True(1 < 2);
        }
    }
}
