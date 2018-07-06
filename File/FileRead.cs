using System;
namespace File
{
    public class FileRead : IFileRead
    {
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
    }

    public interface IFileRead
    {
        string FillContent(string content, dynamic anonymousDataObject);
    }
}
