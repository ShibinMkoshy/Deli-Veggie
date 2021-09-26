using DeliVeggie.Domain.Interface;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliVeggie.Processor
{
    public class Consumer : IConsumer
    {
        public readonly IBus bus;
        public Consumer()
        {
            string rabbitMqConnectioString = "host=host.docker.internal;username=guest;password=guest;timeout=120";
            //string rabbitMqConnectioString = "host=localhost";
            bus = RabbitHutch.CreateBus(rabbitMqConnectioString);
        }

        public void Consume(Func<IRequest, IResponse> data)
        {
            bus.Rpc.Respond<IRequest, IResponse>(data);
        }
    }
}
