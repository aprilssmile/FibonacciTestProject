using System;
using System.Configuration;
using FibbonacciCalculations;
using FibbonacciCalculations.DTO;
using Logger;
using Transport.RabbitMQ;
using Transport.WebApiClient;

namespace App2
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new NLogProvider();

            var rabbitMq = ConfigurationManager.AppSettings["RabbitMqConnectionString"];
            using (var bus = new RabbitMqProvider(rabbitMq, logger))
            {
                var webApiAddress = ConfigurationManager.AppSettings["WebApiEndpointAddress"];
                using (var sender = new HttpClientProvider(webApiAddress, logger))
                {
                    var fibonacciService = new FibonacciService(sender, logger);
                    bus.Subscribe<CalculationDTO>("fibonacciQueue", fibonacciService.Handle);

                    Console.WriteLine("Press any key to stop");
                    Console.ReadLine();
                }
            }
        }
    }
}
