﻿using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System;
using Xunit;

namespace Ad.Test
{
    public class ConfigurationTest
    {
        [Fact]
        public void Test_Configuration()
        {
            IConfiguration _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            int days = Convert.ToInt32(_configuration["CacheExpireDays"]);
            string fileName = _configuration["AdHtmlTemplateFileNameWithExt"];
            Action act1 = () => days.Should().BePositive();
            act1.Should().NotThrow();
            Action act2 = () => fileName.Should().EndWith(".html");
            act2.Should().NotThrow();
        }
    }
}
