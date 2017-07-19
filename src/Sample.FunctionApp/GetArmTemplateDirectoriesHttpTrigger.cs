using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Sample.Extensions;
using Sample.Functions;
using Sample.Functions.FunctionFactories;
using Sample.Functions.ParameterOptions;

namespace Sample.FunctionApp
{
    /// <summary>
    /// This represents the HTTP trigger entity for the GetArmTemplateDirectories event.
    /// </summary>
    public static class GetArmTemplateDirectoriesHttpTrigger
    {
        /// <summary>
        /// Gets or sets the <see cref="Functions.FunctionFactories.FunctionFactory"/> instance.
        /// </summary>
        public static FunctionFactory FunctionFactory { get; set; } = new FunctionFactory();

        /// <summary>
        /// Invokes the HTTP trigger.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Returns the <see cref="HttpResponseMessage"/> instance.</returns>
        public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, ILogger log)
        {
            var options = new GetArmTemplateDirectoriesFunctionParameterOptions() { Query = GetQuery(req) };
            var res = await FunctionFactory.Create<IGetArmTemplateDirectoriesFunction>(log)
                                           .InvokeAsync(req, options)
                                           .ConfigureAwait(false);
            return res;
        }

        private static string GetQuery(HttpRequestMessage req)
        {
            var query = req.GetQueryNameValuePairs().SingleOrDefault(p => p.Key.IsEquivalentTo("q")).Value;

            return query;
        }
    }
}