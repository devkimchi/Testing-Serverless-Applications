using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Practices.ServiceLocation;

using Sample.Extensions;
using Sample.Functions.ServiceLocators;
using Sample.Services;

namespace Sample.FunctionApp
{
    /// <summary>
    /// This represents the HTTP trigger entity for the GetArmTemplateDirectories event with service locator.
    /// </summary>
    public static class GetArmTemplateDirectoriesHttpTriggerWithServiceLocator
    {
        /// <summary>
        /// Gets or sets the <see cref="IServiceLocator"/> instance.
        /// </summary>
        public static IServiceLocator ServiceLocator { get; set; } = new ServiceLocatorBuilder()
                                                                         .RegisterModule<AppModule>()
                                                                         .Build();

        /// <summary>
        /// Invokes the HTTP trigger.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Returns the <see cref="HttpResponseMessage"/> instance.</returns>
        public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, ILogger log)
        {
            var service = ServiceLocator.GetInstance<IGitHubService>();

            var query = GetQuery(req);
            var models = service.GetArmTemplateDirectoriesAsync(query);

            return req.CreateResponse(HttpStatusCode.OK, models);
        }

        private static string GetQuery(HttpRequestMessage req)
        {
            var query = req.GetQueryNameValuePairs().SingleOrDefault(p => p.Key.IsEquivalentTo("q")).Value;

            return query;
        }
    }
}