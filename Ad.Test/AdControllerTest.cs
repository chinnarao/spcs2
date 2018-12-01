﻿using FluentAssertions;
using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Ad.Test
{
    public class AdControllerTest
    {
        //[Fact]
        //public async Task Test_PostAd()
        //{
        //    using (var httpClient = new TestClientProvider().HttpClient)
        //    {
        //        HttpResponseMessage response = await httpClient.GetAsync("/Ad/PostAd");
        //        response.EnsureSuccessStatusCode();
        //        response.StatusCode.Should().Be(HttpStatusCode.OK); // or old school: Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //    }
        //}

        [Fact]
        public async Task WhenGetMethodIsInvokedWithoutAValidToken_GetShouldAnswerUnAuthorized()
        {
            using (TestServerFixture fixture = new TestServerFixture())
            {
                // Act
                var response = await fixture.Client.GetAsync("/Ad/GetAllAds");

                // Assert
                response
                .StatusCode
                .Should()
                .Be(HttpStatusCode.Unauthorized);
            }
        }
    }
}
