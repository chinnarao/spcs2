using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Ad.Test
{
    public class AdControllerTest
    {
        [Fact]
        public async Task WhenGetMethodIsInvokedWithoutAValidToken_GetShouldAnswerUnAuthorized()
        {
            using (TestServerFixture fixture = new TestServerFixture())
            {
                // Act
                var response = await fixture.Client.GetAsync("/Api/Ad/GetAllAds");

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }
    }
}
