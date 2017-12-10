using System;
using System.Configuration;
using FibbonacciCalculations;
using Microsoft.Owin.Hosting;
using Unity;

namespace WebApiApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                Console.WriteLine("App needs parameter with count async calculations to start");
                return;
            }

            int ayncCalculationsCount;
            if (!int.TryParse(args[0], out ayncCalculationsCount))
            {
                Console.WriteLine("Invalid argument type. Required integer");
                return;
            }

            var baseAddress = ConfigurationManager.AppSettings["WebApiEndpointAddress"];
            using (WebApp.Start<Startup>(baseAddress))
            {
                var fibonacciService = Startup.Container.Resolve<FibonacciService>();
                fibonacciService.InitCalculations(ayncCalculationsCount);

                Console.WriteLine("Press any key to stop");
                Console.ReadKey();
            }
        }
    }
}
