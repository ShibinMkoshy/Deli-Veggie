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

        public async Task<List<ProductsResponse>> GetProducts()
        {
            try
            {
                var collection = GetDatabase().GetCollection<Domain.Entity.Product>("products");
                var products = await collection.Find(data => true).ToListAsync();

                var response = products.Select(product => new ProductsResponse
                {
                    Id = product.Id.ToString(),
                    Name = product.Name
                }).ToList();
                return response;
            }
            catch(Exception ex)
            {

            }
            return null;
        }

        public async Task<ProductDetailsResponse> GetProductDetails(ProductDetailsRequest request)
        {
            try
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
            catch(Exception ex)
            {

            }
            return null;
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
