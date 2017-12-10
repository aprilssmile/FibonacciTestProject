using System.Configuration;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using FibbonacciCalculations;
using Owin;
using Transport.Interfaces;
using Transport.RabbitMQ;
using Unity;
using Unity.AspNet.WebApi;
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

            config.Services.Add(typeof(IExceptionLogger), new ConsoleExceptionLogger());

            Container = new UnityContainer();
            var connectionString = ConfigurationManager.AppSettings["RabbitMqConnectionString"];

            Container.RegisterInstance<IDataSender>(new RabbitMqProvider(connectionString));
            Container.RegisterType<FibonacciService>(new SingletonLifetimeManager());

            config.DependencyResolver = new UnityDependencyResolver(Container);

            appBuilder.UseWebApi(config);
        }
    }
}
