using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace Sample.FunctionApp
{
    /// <summary>
    /// This represents the HTTP trigger entity for the MockTemplateDirectories event returning <see cref="HttpStatusCode.NotFound"/>.
    /// </summary>
    public static class MockTemplateDirectoriesHttpTriggerNotFound
    {
        /// <summary>
        /// Invokes the HTTP trigger.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Returns the <see cref="HttpResponseMessage"/> instance.</returns>
        public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, ILogger log)
        {
            var models = new List<object>();

            return req.CreateResponse(HttpStatusCode.NotFound, models);
        }
    }
}