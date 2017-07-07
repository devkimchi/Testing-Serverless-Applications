using System.Configuration;

using FluentAssertions;

using Sample.Functions.ServiceLocators;
using Sample.Functions.Tests.Fixtures;
using Sample.Models.Settings;

using Xunit;

namespace Sample.Functions.Tests.ServiceLocators
{
    /// <summary>
    /// This represents the test entity for the <see cref="ServiceLocatorBuilder"/> class.
    /// </summary>
    public class ServiceLocatorBuilderTests : IClassFixture<ServiceLocatorBuilderFixture>
    {
        private readonly ServiceLocatorBuilderFixture _fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceLocatorBuilderTests"/> class.
        /// </summary>
        /// <param name="fixture"><see cref="ServiceLocatorBuilderFixture"/> instance.</param>
        public ServiceLocatorBuilderTests(ServiceLocatorBuilderFixture fixture)
        {
            this._fixture = fixture;
        }

        /// <summary>
        /// Tests whether the locator loads the module or not.
        /// </summary>
        [Fact]
        public void Given_AppModule_ShouldResolve_Dependency()
        {
            var locator = this._fixture.ArrangeLocator(p => p.RegisterModule<AppModule>());

            var appSettings = locator.GetInstance<IFunctionAppSettings>();

            appSettings.StorageAccount.ConnectionString.Should()
                       .BeEquivalentTo(ConfigurationManager.AppSettings["AzureWebJobsStorage"]);
        }
    }
}
