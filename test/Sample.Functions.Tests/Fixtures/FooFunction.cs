using System;
using System.Net.Http;
using System.Threading.Tasks;

using Sample.Functions.FunctionFactories;
using Sample.Functions.ParameterOptions;

namespace Sample.Functions.Tests.Fixtures
{
    /// <summary>
    /// This provides interfaces to the <see cref="FooFunction"/> class.
    /// </summary>
    public interface IFooFunction : IFunction
    {
    }

    /// <summary>
    /// This represents the parameter options entity for the <see cref="FooFunction"/> class.
    /// </summary>
    public class FooFunctionParameterOptions : FunctionParameterOptions
    {
        /// <summary>
        /// Gets or sets the Bar value.
        /// </summary>
        public string Bar { get; set; }
    }

    /// <summary>
    /// This represents the Foo function entity.
    /// </summary>
    public class FooFunction : FunctionBase, IFooFunction
    {
        /// <inheritdoc />
        public override Task<HttpResponseMessage> InvokeAsync<TOptions>(HttpRequestMessage req, TOptions options = default(TOptions))
        {
            throw new NotImplementedException();
        }
    }
}
