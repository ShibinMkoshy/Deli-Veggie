using DeliVeggie.Domain.Interface;

namespace DeliVeggie.Domain.Response
{
    public class TResponse<T>: IResponse
    {
        public T Response { get; set; }
    }
}