using System.Configuration;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using FibbonacciCalculations;
using Logger;
using Owin;
using Transport.Interfaces;
using Transport.RabbitMQ;
using Unity;
using Unity.AspNet.WebApi;
using Unity.Injection;
using Unity.Lifetime;

namespace WebApiApp1
{
    public class Startup
    {
        public static UnityContainer Container;

        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Services.Add(typeof(IExceptionLogger), new ExceptionLogger());

            RegisterDependencies(config);
            appBuilder.UseWebApi(config);
        }

        private static void RegisterDependencies(HttpConfiguration config)
        {
            
            Container = new UnityContainer();
            var connectionString = ConfigurationManager.AppSettings["RabbitMqConnectionString"];

            Container.RegisterType<ILogger, NLogProvider>(new SingletonLifetimeManager());

            Container.RegisterType<IDataSender, RabbitMqProvider>(new SingletonLifetimeManager(),
                new InjectionConstructor(connectionString, new ResolvedParameter<ILogger>()));

            Container.RegisterType<FibonacciService>(new SingletonLifetimeManager());

            config.DependencyResolver = new UnityDependencyResolver(Container);
        }
    }
}
