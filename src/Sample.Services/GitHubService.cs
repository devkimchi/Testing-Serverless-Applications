using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Sample.Extensions;
using Sample.Models.Enums;
using Sample.Models.GitHub;
using Sample.Models.Settings;

namespace Sample.Services
{
    /// <summary>
    /// This represents the service entity for the GitHub REST API calls.
    /// </summary>
    public class GitHubService : ServiceBase, IGitHubService
    {
        private static MediaTypeWithQualityHeaderValue acceptHeader = new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json");
        private static ProductInfoHeaderValue userAgentHeader = new ProductInfoHeaderValue("Mozilla", "5.0");
        private static List<string> directoriesToExclude = new[] { ".github", "1-CONTRIBUTION-GUIDE" }.ToList();

        private readonly IFunctionAppSettings _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="GitHubService"/> class.
        /// </summary>
        /// <param name="appSettings"><see cref="IFunctionAppSettings"/> instance.</param>
        /// <param name="httpClient"><see cref="HttpClient"/> instance.</param>
        public GitHubService(IFunctionAppSettings appSettings, HttpClient httpClient)
            : base(httpClient)
        {
            this._appSettings = appSettings.ThrowIfNullOrDefault();
        }

        /// <summary>
        /// Gets the list of ARM template directories.
        /// </summary>
        /// <param name="query">Query to filter out directories.</param>
        /// <returns>Returns the list of ARM template directories.</returns>
        public async Task<List<ContentModel>> GetArmTemplateDirectoriesAsync(string query = null)
        {
            this.AddRequestHeaders();

            var github = this._appSettings.GitHub;
            var requestUri = $"{github.ApiBaseUri}{string.Format(github.RepositoryContentUri, github.AzureUsername, github.AzureQuickstartTemplatesRepository)}";
            this.Response = await this.HttpClient.GetAsync(requestUri).ConfigureAwait(false);
            this.Response.EnsureSuccessStatusCode();

            var contents = await this.Response.Content.ReadAsAsync<List<ContentModel>>().ConfigureAwait(false);

            this.RemoveRequestHeaders();

            return contents.Where(IsContentEligible)
                           .Where(p => query.IsNullOrWhiteSpace() || p.Name.ContainsEquivalent(query))
                           .ToList();
        }

        private static bool IsContentEligible(ContentModel model) => model.ContentType.Equals(ContentType.Directory) && !directoriesToExclude.ContainsEquivalent(model.Name);

        private void AddRequestHeaders()
        {
            // https://gist.github.com/BellaCode/c0ba0a842bbe22c9215e
            this.HttpClient.DefaultRequestHeaders.Accept.Add(acceptHeader);
            this.HttpClient.DefaultRequestHeaders.UserAgent.Add(userAgentHeader);
        }

        private void RemoveRequestHeaders()
        {
            this.HttpClient.DefaultRequestHeaders.Accept.Remove(acceptHeader);
            this.HttpClient.DefaultRequestHeaders.UserAgent.Remove(userAgentHeader);
        }
    }
}
