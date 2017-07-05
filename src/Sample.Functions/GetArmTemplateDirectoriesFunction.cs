using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Sample.Extensions;
using Sample.Functions.FunctionFactories;
using Sample.Functions.ParameterOptions;
using Sample.Services;

namespace Sample.Functions
{
    /// <summary>
    /// This represents the function entity for the trigger.
    /// </summary>
    public class GetArmTemplateDirectoriesFunction : FunctionBase, IGetArmTemplateDirectoriesFunction
    {
        private readonly IGitHubService _gitHubService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetArmTemplateDirectoriesFunction"/> class.
        /// </summary>
        /// <param name="gitHubService"><see cref="IGitHubService"/> instance.</param>
        public GetArmTemplateDirectoriesFunction(IGitHubService gitHubService)
        {
            this._gitHubService = gitHubService.ThrowIfNullOrDefault();
        }

        /// <summary>
        /// Gets the <see cref="GetArmTemplateDirectoriesFunctionParameterOptions"/> instance.
        /// </summary>
        public GetArmTemplateDirectoriesFunctionParameterOptions Parameters => this.ParameterOptions as GetArmTemplateDirectoriesFunctionParameterOptions;

        /// <summary>
        /// Invokes the function.
        /// </summary>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <returns>Returns the <see cref="HttpResponseMessage"/> instance.</returns>
        public override async Task<HttpResponseMessage> InvokeAsync(HttpRequestMessage req)
        {
            var directories = await this._gitHubService.GetArmTemplateDirectoriesAsync(this.Parameters.Query).ConfigureAwait(false);

            return this.CreateOkResponse(req, directories);
        }
    }
}
