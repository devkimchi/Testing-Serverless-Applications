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

        /// <inheritdoc />
        public override async Task<HttpResponseMessage> InvokeAsync<TOptions>(HttpRequestMessage req, TOptions options = default(TOptions))
        {
            var @params = options as GetArmTemplateDirectoriesFunctionParameterOptions;
            var directories = await this._gitHubService.GetArmTemplateDirectoriesAsync(@params.Query).ConfigureAwait(false);

            return this.CreateOkResponse(req, directories);
        }
    }
}
