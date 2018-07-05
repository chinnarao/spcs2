using Microsoft.Extensions.Caching.Memory;
using System;
using System.IO;
using System.Text;

namespace File
{
    public class FileRead : IFileRead
    {
        private readonly IMemoryCache _memoryCache;
        public FileRead(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Stream FileAsStream(string content)
        {
            MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            return memoryStream;
        }

        /// <summary>
        /// file shoud be build to copy always in file properties
        /// </summary>
        /// <param name="fileName">ad.template.html</param>
        /// <param name="inMemoryCacheExpireDays"></param>
        /// <returns></returns>
        public string FileAsString(string fileName, int inMemoryCacheExpireDays, string CACHE_KEY)
        {
            string _htmlFileContent = string.Empty;
            if (!_memoryCache.TryGetValue(CACHE_KEY, out _htmlFileContent))
            {
                using (StreamReader sr = new StreamReader(fileName)) _htmlFileContent = sr.ReadToEnd();
                var opts = new MemoryCacheEntryOptions { SlidingExpiration = TimeSpan.FromDays(inMemoryCacheExpireDays) };
                _memoryCache.Set(CACHE_KEY, _htmlFileContent, opts);
            }

            if (string.IsNullOrEmpty(_htmlFileContent))
                throw new Exception("Ad Html File Template content is empty, unexpected.");

            return _htmlFileContent;
        }

        public string FillContent(string content, object anonymousDataObject)
        {
            var template = Scriban.Template.Parse(content);
            if (template.HasErrors)
            {
                throw new Exception(string.Join<Scriban.Parsing.LogMessage>(',', template.Messages.ToArray()));
            }
            string result = template.Render(anonymousDataObject);
            return result;
        }
    }

    public interface IFileRead
    {
        string FileAsString(string fileName, int inMemoryCachyExpireDays, string CACHE_KEY);
        string FillContent(string content, object anonymousDataObject);
        Stream FileAsStream(string content);
    }
}
