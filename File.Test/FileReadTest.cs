using Microsoft.Extensions.Caching.Memory;
using System;
using Xunit;
using FluentAssertions;
using System.IO;
using File;

namespace File.Test
{
    public class FileReadTest
    {
        [Fact]
        public void Test_FileAsString()
        {
            int days = Helper.GetCacheExpireDays();
            string fileName = Helper.GetAdFileName();
            string firstTimeContent = Helper.GetAdTemplateFileContent();
            Action act = () => firstTimeContent.Should().NotBeEmpty();
            act.Should().NotThrow();
            string secondTimeContent = Helper.GetAdTemplateFileContent();
            if (firstTimeContent == secondTimeContent)
            {
            }
            Action act1 = () => secondTimeContent.Should().BeSameAs(firstTimeContent);
            //act1.Should().NotThrow();
        }

        [Fact]
        public void Test_FillContent()
        {
            var anonymousData = new
            {
                Name = "Riya",
                Occupation = "Kavin Brother."
            };

            string content = Helper.GetAdTemplateFileContent();
            IFileRead read = new FileRead();
            content = read.FillContent(content, anonymousData);
            Action act = () => content.Should().Contain(anonymousData.Name);
            act.Should().NotThrow();
            Action act1 = () => content.Should().Contain(anonymousData.Occupation);
            act1.Should().NotThrow();
        }
    }
}
