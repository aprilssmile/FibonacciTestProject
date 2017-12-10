using System;
using System.Threading.Tasks;
using EasyNetQ;
using Transport.Interfaces;

namespace Transport.RabbitMQ
{
    public class RabbitMqProvider : IDisposable, IDataSender
    {
        private readonly IBus _bus;

        public RabbitMqProvider(string connectionString)
        {
            _bus = RabbitHutch.CreateBus(connectionString);
        }

        public void Subscribe<T>(string subscriptionId, Action<T> action)
            where T : class
        {
            // todo optimize with task sheduler. Info:
            // http://www.mariuszwojcik.com/blog/How-to-process-messages-in-parallel-using-EasyNetQ
            var factory = new TaskFactory();

            _bus.SubscribeAsync<T>(subscriptionId, message =>
                factory.StartNew(() => action(message)), config => config.WithAutoDelete());
        }

        public async Task SendResult<T>(T message) where T : class
        {
            await _bus.PublishAsync(message);
        }

        public void Dispose()
        {
            _bus.Dispose();
        }
    }
}
