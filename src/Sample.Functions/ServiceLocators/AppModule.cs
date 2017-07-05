using System.Net.Http;
using System.Net.Http.Formatting;

using Autofac;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using Sample.Extensions;
using Sample.Functions.FunctionFactories;
using Sample.Models.Settings;
using Sample.Services;

namespace Sample.Functions.ServiceLocators
{
    /// <summary>
    /// Implements the app module.
    /// </summary>
    public class AppModule : Module
    {
        /// <summary>
        /// Add registrations to the container builder.
        /// </summary>
        /// <param name="containerBuilder">The container builder.</param>
        protected override void Load(ContainerBuilder containerBuilder)
        {
            // Settings
            containerBuilder.RegisterType<FunctionAppSettings>().As<IFunctionAppSettings>().SingleInstance();

            var serializerSettings = new JsonSerializerSettings
                                     {
                                         ContractResolver = new CamelCasePropertyNamesContractResolver(),
                                         Converters = { new StringEnumConverter() },
                                         Formatting = Formatting.Indented,
                                         NullValueHandling = NullValueHandling.Ignore,
                                         MissingMemberHandling = MissingMemberHandling.Ignore
                                     };
            containerBuilder.Register(_ => serializerSettings).As<JsonSerializerSettings>().SingleInstance();

            var jsonMediaTypeFormatter = new JsonMediaTypeFormatter { SerializerSettings = serializerSettings };
            containerBuilder.Register(_ => jsonMediaTypeFormatter).As<MediaTypeFormatter>().SingleInstance();

            var httpClient = new HttpClient();
            containerBuilder.Register(_ => httpClient).As<HttpClient>().SingleInstance();

            // Functions
            containerBuilder.RegisterAssemblyTypes(typeof(IFunction).Assembly)
                            .Where(t => t.Name.EndsWithEquivalent("Function"))
                            .AsImplementedInterfaces()
                            .InstancePerDependency();

            // Services
            containerBuilder.RegisterAssemblyTypes(typeof(IService).Assembly)
                            .Where(t => t.Name.EndsWithEquivalent("Service"))
                            .AsImplementedInterfaces()
                            .InstancePerDependency();
        }
    }
}