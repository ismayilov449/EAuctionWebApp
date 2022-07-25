using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusRabbitMQ
{
    public interface IRabbitMQPersistentConnection : IDisposable
    {
        public bool IsConnected { get; }
        public bool TryConnect();
        public IModel CreateModel();

    }
}
