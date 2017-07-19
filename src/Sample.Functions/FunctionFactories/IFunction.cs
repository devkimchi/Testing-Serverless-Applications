using System;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Practices.ServiceLocation;

using Sample.Functions.ParameterOptions;

namespace Sample.Functions.FunctionFactories
{
    /// <summary>
    /// This provides interfaces to the <see cref="FunctionBase"/> class.
    /// </summary>
    public interface IFunction : IDisposable
    {
        /// <summary>
        /// Gets or sets the <see cref="ILogger"/> instance.
        /// </summary>
        ILogger Log { get; set; }

        /// <summary>
        /// Gets or sets the service locator for the function.
        /// </summary>
        IServiceLocator ServiceLocator { get; set; }

        /// <summary>
        /// Invokes the function.
        /// </summary>
        /// <typeparam name="TOptions">Type of <see cref="FunctionParameterOptions"/>.</typeparam>
        /// <param name="req"><see cref="HttpRequestMessage"/> instance.</param>
        /// <param name="options"><see cref="TOptions"/> instance.</param>
        /// <returns>Returns the <see cref="HttpResponseMessage"/> instance.</returns>
        Task<HttpResponseMessage> InvokeAsync<TOptions>(HttpRequestMessage req, TOptions options = default(TOptions));
    }
}