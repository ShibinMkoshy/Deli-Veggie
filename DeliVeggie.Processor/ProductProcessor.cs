using DeliVeggie.Domain.Interface;
using DeliVeggie.Domain.Request;
using DeliVeggie.Domain.Response;
using DeliVeggie.Repository;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliVeggie.Processor
{
    public class ProductProcessor : IProductProcessor
    {
        //private readonly IBus bus;
        private readonly IPublisher _publisher;

        public ProductProcessor(IPublisher publisher)
        {
            _publisher = publisher;
        }

        

        public ProductDetailsResponse GetProductDetails(int id)
        {
            var request = new TRequest<ProductDetailsRequest>() { Request = new ProductDetailsRequest() { 
                        ProductId = id} };
            //var req = new ProductDetailsRequest() { ProductId = id.ToString() };
            //var resp = bus.Rpc.Request<ProductDetailsRequest, ProductDetailsResponse>(req);
            var data = _publisher.Request(request);
            if (data is not TResponse<ProductDetailsResponse> response)
            {
                return null;
            }
            else
            {

                return response.Response;
            }
            
        }

        public List<ProductsResponse> GetProducts()
        {
            //var resp = bus.Rpc.Request<ProductsRequest, List<ProductsResponse>>(new ProductsRequest());
            var request = new TRequest<ProductsRequest>() { Request = new ProductsRequest() };

            var data = _publisher.Request(request);
            if (data is not TResponse<List<ProductsResponse>> response)
            {
                return null;
            }
            else
            {
                return response.Response;
            }
            
        }
    }
}
