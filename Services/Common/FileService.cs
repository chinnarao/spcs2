using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Share.Utilities;

namespace Services.Commmon
{
    public class FileRead : IFileRead
    {
        private readonly IConfiguration _configuration;
        private readonly ICacheService _cacheService;

        public FileRead(IConfiguration configuration, ICacheService cacheService)
        {
            _configuration = configuration;
            _cacheService = cacheService;
        }

        public string FillContent(string content, dynamic anonymousDataObject)
        {
            var template = Scriban.Template.Parse(content);
            if (template.HasErrors)
            {
                throw new Exception(string.Join<Scriban.Parsing.LogMessage>(',', template.Messages.ToArray()));
            }
            string result = template.Render(anonymousDataObject);
            return result;
        }

        public string ReadJsonFile(string fileNameWithExtension)
        {
            if (string.IsNullOrWhiteSpace(fileNameWithExtension))
                throw new Exception(nameof(fileNameWithExtension));

            string json = _cacheService.Get<string>(fileNameWithExtension);
            if (string.IsNullOrWhiteSpace(json))
            {
                var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (string.IsNullOrWhiteSpace(dir))
                    throw new Exception(nameof(dir));

                var filePath = Path.Combine(dir, fileNameWithExtension);
                if (!File.Exists(filePath))
                    throw new Exception(nameof(filePath));

                json = File.ReadAllText(filePath);
                if (string.IsNullOrWhiteSpace(json))
                    throw new Exception(nameof(json));

                json = _cacheService.GetOrAdd<string>(fileNameWithExtension, () => json, Utility.GetCacheExpireDateTime(_configuration["CacheExpireDays"]));
                if (string.IsNullOrEmpty(json)) throw new Exception(nameof(json));
            }

            return json;
        }
    }

    public interface IFileRead
    {
        string FillContent(string content, dynamic anonymousDataObject);
        string ReadJsonFile(string fileNameWithExtension);
    }
}
