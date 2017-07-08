using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;

using FluentAssertions;

using Microsoft.Extensions.Logging;
using Microsoft.Practices.ServiceLocation;

using Moq;

using Sample.Models.GitHub;
using Sample.Services;

using Xunit;

namespace Sample.FunctionApp.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="GetArmTemplateDirectoriesHttpTriggerWithServiceLocator"/> class.
    /// </summary>
    public class GetArmTemplateDirectoriesHttpTriggerWithServiceLocatorTests
    {
        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> value.</param>
        [Theory]
        [InlineData(HttpStatusCode.OK)]
        public async void Given_Instance_Run_ShouldReturn_Response(HttpStatusCode statusCode)
        {
            var models = new[] { new ContentModel() }.ToList();

            var service = new Mock<IGitHubService>();
            service.Setup(p => p.GetArmTemplateDirectoriesAsync(It.IsAny<string>())).ReturnsAsync(models);

            var locator = new Mock<IServiceLocator>();
            locator.Setup(p => p.GetInstance<IGitHubService>()).Returns(service.Object);

            GetArmTemplateDirectoriesHttpTriggerWithServiceLocator.ServiceLocator = locator.Object;

            var config = new HttpConfiguration() { Formatters = { new JsonMediaTypeFormatter() } };
            var context = new HttpRequestContext() { Configuration = config };
            var req = new HttpRequestMessage() { Properties = { { HttpPropertyKeys.RequestContextKey, context } } };
            var log = new Mock<ILogger>();

            var res = await GetArmTemplateDirectoriesHttpTriggerWithServiceLocator.Run(req, log.Object).ConfigureAwait(false);

            res.StatusCode.Should().Be(statusCode);
        }
    }
}
