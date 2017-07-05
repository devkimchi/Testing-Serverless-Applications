using Sample.Functions.FunctionFactories;
using Sample.Functions.ParameterOptions;

namespace Sample.Functions
{
    /// <summary>
    /// This provides interfaces to the <see cref="GetArmTemplateDirectoriesFunction"/> class.
    /// </summary>
    public interface IGetArmTemplateDirectoriesFunction : IFunction
    {
        /// <summary>
        /// Gets the <see cref="GetArmTemplateDirectoriesFunctionParameterOptions"/> instance.
        /// </summary>
        GetArmTemplateDirectoriesFunctionParameterOptions Parameters { get; }
    }
}