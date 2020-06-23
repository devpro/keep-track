using FluentAssertions;
using OpenQA.Selenium;
using Withywoods.Selenium;
using Withywoods.WebTesting.TestHost;
using Xunit;

namespace KeepTrack.Api.UnitTests.Controllers
{
    [Trait("Category", "UnitTests")]
    public class SwaggerControllerTest : SeleniumTestBase, IClassFixture<LocalServerFactory<Startup>>
    {
        private const string ResourceEndpoint = "swagger";

        private readonly LocalServerFactory<Startup> _server;

        public SwaggerControllerTest(LocalServerFactory<Startup> server)
            : base(new WebDriverOptions { IsHeadless = true })
        {
            _server = server;
            _ = server.CreateClient(); // needed to change server state
        }

        [Fact]
        public void AspNetCoreApiSampleSwaggerResourceGet_ReturnsHttpOk()
        {
            _server.RootUri.Should().Be("https://localhost:5001");

            try
            {
                // Arrange & Act
                WebDriver.Navigate().GoToUrl($"{_server.RootUri}/{ResourceEndpoint}");

                // Assert
                WebDriver.FindElement(By.ClassName("title"), 60);
                WebDriver.Title.Should().Be("Swagger UI");
                WebDriver.FindElementByClassName("title").Text.Should().Contain("Keep Track API");
            }
            catch
            {
                TakeScreenShot(nameof(AspNetCoreApiSampleSwaggerResourceGet_ReturnsHttpOk));
                throw;
            }
        }
    }
}
