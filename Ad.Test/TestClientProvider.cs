using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;

//https://www.youtube.com/watch?v=p5l7x_pFjmI
//https://www.meziantou.net/2017/08/21/testing-an-asp-net-core-application-using-testserver
namespace Ad.Test
{
    public class TestServerFixture : IDisposable
    {
        private readonly TestServer _testServer;
        public HttpClient Client { get; }

        public TestServerFixture()
        {
            var builder = new WebHostBuilder()
                .ConfigureAppConfiguration(
                        (builderContext, config) => { config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); }
                    )
            //.UseEnvironment("Development")
            .UseStartup<Startup>();

            _testServer = new TestServer(builder);
            Client = _testServer.CreateClient();
        }

        public void Dispose()
        {
            Client.Dispose();
            _testServer.Dispose();
        }
    }
}
