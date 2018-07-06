using System;
namespace Models.Ad.Dtos
{
    public class GoogleStorageFileDto
    {
        public DateTime CacheExpiryDateTimeForHtmlTemplate { get; set; } = DateTime.Now.AddDays(30);
        public string GoogleStorageObjectNameWithExt { get; set; }
        public string GoogleStorageBucketName { get; set; }
        public dynamic AnonymousDataObjectForHtmlTemplate { get; set; }
        public string ContentType { get; set; } = "text/html";
        public string CACHE_KEY { get; set; }
        public string HtmlFileTemplateFullPathWithExt { get; set; }
    }
}
