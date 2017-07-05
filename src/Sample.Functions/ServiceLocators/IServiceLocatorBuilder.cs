using System;

using Autofac.Core;

using Microsoft.Practices.ServiceLocation;

namespace Sample.Functions.ServiceLocators
{
    /// <summary>
    /// This provides interfaces to the <see cref="ServiceLocatorBuilder"/> class.
    /// </summary>
    public interface IServiceLocatorBuilder : IDisposable
    {
        /// <summary>
        /// Builds the service locator.
        /// </summary>
        /// <returns>Returns the <see cref="IServiceLocator"/>.</returns>
        IServiceLocator Build();

        /// <summary>
        /// Registers a module.
        /// </summary>
        /// <typeparam name="TModule">The type of the module.</typeparam>
        /// <param name="handler"><see cref="RegistrationHandler"/> instance.</param>
        /// <returns>Returns the <see cref="IServiceLocatorBuilder"/> instance.</returns>
        IServiceLocatorBuilder RegisterModule<TModule>(RegistrationHandler handler = null) where TModule : IModule, new();
    }
}