using Autofac;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

using Sample.Functions.FunctionFactories;
using Sample.Functions.ServiceLocators;
using Sample.Functions.Tests.Fixtures;

using Xunit;

namespace Sample.Functions.Tests.FunctionFactories
{
    /// <summary>
    /// This represents the test entity for the <see cref="FunctionFactory"/> class.
    /// </summary>
    public class FunctionFactoryTests
    {
        /// <summary>
        /// Tests whether the method should return result or not.
        /// </summary>
        [Fact]
        public void Given_That_Function_ShouldBe_Created()
        {
            var handler = new RegistrationHandler()
                              {
                                  RegisterTypeAsInstancePerDependency = p => p.RegisterType<FooFunction>()
                                                                              .As<IFooFunction>()
                                                                              .InstancePerDependency()
                              };
            var factory = new FunctionFactory(handler);

            var logger = new Mock<ILogger>();
            var function = factory.Create<IFooFunction>(logger.Object);

            function.Log.Should().NotBeNull();
            function.ServiceLocator.Should().NotBeNull();
        }
    }
}
