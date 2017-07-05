using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Sample.Models.GitHub;

namespace Sample.Services
{
    /// <summary>
    /// This provides interfaces to the <see cref="GitHubService"/> class.
    /// </summary>
    public interface IGitHubService : IDisposable
    {
        /// <summary>
        /// Gets the list of ARM template directories.
        /// </summary>
        /// <param name="query">Query to filter out directories.</param>
        /// <returns>Returns the list of ARM template directories.</returns>
        Task<List<ContentModel>> GetArmTemplateDirectoriesAsync(string query = null);
    }
}