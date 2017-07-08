using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Sample.Models.GitHub;

namespace Sample.FunctionApp
{
    /// <summary>
    /// This represents the HTTP trigger entity for the MockTemplateDirectories event returning <see cref="HttpStatusCode.OK"/>.
    /// </summary>
    public static class MockTemplateDirectoriesHttpTriggerOk
    {
        /// <summary>
        /// Invokes the HTTP trigger.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Returns the <see cref="HttpResponseMessage"/> instance.</returns>
        public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, ILogger log)
        {
            var formatter = new JsonMediaTypeFormatter()
                                {
                                    SerializerSettings = new JsonSerializerSettings()
                                                             {
                                                                 ContractResolver = new CamelCasePropertyNamesContractResolver(),
                                                                 NullValueHandling = NullValueHandling.Ignore,
                                                                 MissingMemberHandling = MissingMemberHandling.Ignore
                                                             }
                                };
            var models = new List<ContentModel>()
                             {
                                 new ContentModel() { Name = "abc", Url = "https://templates.io/abc" },
                                 new ContentModel() { Name = "xya", Url = "https://templates.io/xyz" }
                             };

            return req.CreateResponse(HttpStatusCode.OK, models, formatter);
        }
    }
}