using DeliVeggie.Domain.Response;
using DeliVeggie.Domain.Entity;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliVeggie.Domain.Request;

namespace DeliVeggie.Repository
{
    public class ProductRepository : IProductRepository
    {
        protected static IMongoClient client;
        protected static IMongoDatabase database;

        private static IMongoDatabase GetDatabase()
        {
            try
            {
                client = new MongoClient("mongodb://localhost:27017");
                return client.GetDatabase("productsDB");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //private static IEnumerable<ProductsResponse> productResponses = new List<ProductsResponse>()
        //{
        //    new ProductsResponse{ Id = "1", Name = "Tomato"},
        //    new ProductsResponse{ Id = "2", Name = "Potato"},
        //    new ProductsResponse{ Id = "3", Name = "Carrot"}
        //};

        public async Task<List<ProductsResponse>> GetProducts()
        {
            var collection = GetDatabase().GetCollection<Domain.Entity.Product>("products");
            var products = await collection.Find(data => true).ToListAsync();

            //var products = productResponses;
            var response = products.Select(product => new ProductsResponse
            {
                Id = product.Id.ToString(),
                Name = product.Name
            }).ToList();
            return response;
        }

        public async Task<ProductDetailsResponse> GetProductDetails(ProductDetailsRequest request)
        {
            var collection = GetDatabase().GetCollection<Domain.Entity.Product>("products");
            var response = await collection.Find(a => a.Id == request.ProductId).FirstOrDefaultAsync();
            var reduction = GetPriceReduction((int)DateTime.Now.DayOfWeek);
            var priceAfterReduction = response.Price - (response.Price * reduction);
            return new ProductDetailsResponse()
            {
                Id = response.Id.ToString(),
                Name = response.Name,
                EntryDate = response.EntryDate,
                PriceWithReduction = priceAfterReduction
            };
        }

        public double GetPriceReduction(int dayOfWeek)
        {
            var collection = GetDatabase().GetCollection<PriceReduction>("PriceReductions");
            var response = collection.Find(a => a.DayOfWeek == dayOfWeek).FirstOrDefault();
            var result = response.Reduction;
            return result;
        }
    }
}
