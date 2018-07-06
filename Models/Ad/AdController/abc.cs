using Models.Ad.Dtos;
using System;

namespace Models.Ad.AdController
{
    public class abc
    {
        public string FileName { get; set; }
        public DateTime CacheExpiryDateTime { get; set; }
        public string ObjectNameWithExt { get; set; }
        public string GoogleStorageBucketName { get; set; }
        public object AnonymousDataObject { get; set; }
        public string ContentType { get; set; }
        public string CACHE_KEY { get; set; }
        public string HtmlFileTemplateFullPathWithExt { get; set; }
    }
}
