using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Sample.Extensions;
using Sample.Models.Settings;
using Sample.Services;

namespace Sample.FunctionApp
{
    /// <summary>
    /// This represents the HTTP trigger entity for the GetArmTemplateDirectories event without using dependency injection.
    /// </summary>
    public static class GetArmTemplateDirectoriesHttpTriggerWithoutDi
    {
        /// <summary>
        /// Invokes the HTTP trigger.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Returns the <see cref="HttpResponseMessage"/> instance.</returns>
        public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, ILogger log)
        {
            var settings = new FunctionAppSettings();
            var client = new HttpClient();
            var service = new GitHubService(settings, client);

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