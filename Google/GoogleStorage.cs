using Google.Cloud.Storage.V1;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Google
{
    public class GoogleStorage : IGoogleStorage
    {
        public void UploadObject(string bucketName, Stream stream, string objectName, string contentType)
        {
            #region validation
            if (string.IsNullOrEmpty(bucketName))       throw new ArgumentException(nameof(bucketName));
            if (stream == null || stream.Length <= 0)   throw new ArgumentException(nameof(stream));
            if (string.IsNullOrEmpty(objectName))       throw new ArgumentException(nameof(objectName));
            if (string.IsNullOrEmpty(contentType))      throw new ArgumentException(nameof(contentType));
            #endregion

            var storage = StorageClient.Create();
            storage.UploadObject(bucketName, objectName, contentType, stream);
        }

        public async Task UploadObjectAsync(string bucketName, Stream stream, string objectName, string contentType)
        {
            #region MyRegion
            if (string.IsNullOrEmpty(bucketName))       throw new ArgumentException(nameof(bucketName));
            if (stream == null || stream.Length <= 0)   throw new ArgumentException(nameof(stream));
            if (string.IsNullOrEmpty(objectName))       throw new ArgumentException(nameof(objectName));
            if (string.IsNullOrEmpty(contentType))      throw new ArgumentException(nameof(contentType));
            #endregion

            var storage = StorageClient.Create();
            await storage.UploadObjectAsync(bucketName, objectName, contentType, stream);
        }
    }

    public interface IGoogleStorage
    {
        void UploadObject(string bucketName, Stream stream, string objectName, string contentType);
        Task UploadObjectAsync(string bucketName, Stream stream, string objectName, string contentType);
    }
}
