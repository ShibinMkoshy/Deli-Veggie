
using DeliVeggie.Domain.Interface;
using DeliVeggie.Domain.Request;
using DeliVeggie.Domain.Response;
using DeliVeggie.Processor;
using DeliVeggie.Repository;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Products.ConsoleApp
{
    class Program
    {
        private static ServiceProvider _services;
        private static IProductRepository _productRepository;
        private static void Main(string[] args)
        {
            string rabbitMqConnectioString = "host=host.docker.internal;username=guest;password=guest";
            var bus = RabbitHutch.CreateBus(rabbitMqConnectioString);

            Console.WriteLine("Starting console application");
            _services = new ServiceCollection()
                .AddSingleton<IConsumer, Consumer>()
                .AddScoped<IProductRepository, ProductRepository>()
                .BuildServiceProvider();
            var _bus = _services.GetService<IConsumer>();
            _productRepository = _services.GetService<IProductRepository>();
            while (true)
            {
                _bus?.Consume(RequestHandler);
                
            }
        }

        private static IResponse RequestHandler(IRequest request)
        {

            if (request is TRequest<ProductDetailsRequest> productDetailsRequest)
            {
                Console.WriteLine($"API requested for product with ID {productDetailsRequest.Request.ProductId}");

                var details = Task.Run(async () =>
                {
                    return await _productRepository.GetProductDetails(new ProductDetailsRequest() 
                        { ProductId = productDetailsRequest.Request.ProductId } );
                }).GetAwaiter().GetResult();
                IResponse product = new TResponse<ProductDetailsResponse>() { Response = details };
                return product;

            }
            else
            {
                Console.WriteLine($"API requested all products.");

                var details = Task.Run(async () =>
                {
                    return await _productRepository.GetProducts();
                }).GetAwaiter().GetResult();
                IResponse data = new TResponse<List<ProductsResponse>>() { Response = details };
                return data;
            }

        }
    }
}
