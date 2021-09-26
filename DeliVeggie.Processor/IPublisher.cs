using DeliVeggie.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliVeggie.Processor
{
    public interface IPublisher
    {
        IResponse Request(IRequest request);
    }
}
