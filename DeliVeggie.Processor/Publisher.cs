using DeliVeggie.Domain.Interface;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliVeggie.Processor
{
    public class Publisher:IPublisher
    {
        IBus bus;
        public Publisher()
        {
            bus = RabbitHutch.CreateBus("host=host.docker.internal;username=guest;password=guest;timeout=120");
        }

        public IResponse Request(IRequest request)
        {
            return bus.Rpc.Request<IRequest, IResponse>(request);
        }
    }
}
