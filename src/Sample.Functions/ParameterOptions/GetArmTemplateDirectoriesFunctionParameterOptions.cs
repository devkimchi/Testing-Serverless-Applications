namespace Sample.Functions.ParameterOptions
{
    /// <summary>
    /// This represents the options entity for the <see cref="GetArmTemplateDirectoriesFunction"/> parameters.
    /// </summary>
    public class GetArmTemplateDirectoriesFunctionParameterOptions : FunctionParameterOptions
    {
        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        public string Query { get; set; }
    }
}