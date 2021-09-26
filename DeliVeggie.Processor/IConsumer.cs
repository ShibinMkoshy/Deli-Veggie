using DeliVeggie.Domain.Interface;
using System;

namespace DeliVeggie.Processor
{
    public interface IConsumer
    {
        void Consume(Func<IRequest, IResponse> data);
    }
}
