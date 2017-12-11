using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyNetQ;
using Transport.Interfaces;

namespace Transport.RabbitMQ
{
    public class RabbitMqProvider : IDisposable, IDataSender
    {
        private readonly ILogger _logger;
        private readonly IBus _bus;
        private readonly IList<ISubscriptionResult> _subcriptions;

        public RabbitMqProvider(string connectionString, ILogger logger)
        {
            _logger = logger;
            _bus = RabbitHutch.CreateBus(connectionString);
            _subcriptions = new List<ISubscriptionResult>();
        }

        public void Subscribe<T>(string subscriptionId, Action<T> action)
            where T : class
        {
            try
            {
                // todo optimize with task sheduler. Info:
                // http://www.mariuszwojcik.com/blog/How-to-process-messages-in-parallel-using-EasyNetQ
                var factory = new TaskFactory();

                var subscription = _bus.SubscribeAsync<T>(subscriptionId, message =>
                    factory.StartNew(() => action(message)), config => config.WithAutoDelete());

                _subcriptions.Add(subscription);

                _logger.Info($"Created '{subscriptionId}' subscription");
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
            }
        }

        public Task SendResult<T>(T message) where T : class
        {
            return _bus.PublishAsync(message);
        }

        public void Dispose()
        {
            foreach (var subcription in _subcriptions)
            {
                subcription.Dispose();
            }

            _bus.Dispose();
        }
    }
}
