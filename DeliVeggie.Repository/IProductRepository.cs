using DeliVeggie.Domain.Entity;
using DeliVeggie.Domain.Request;
using DeliVeggie.Domain.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliVeggie.Repository
{
    public interface IProductRepository
    {
        Task<List<ProductsResponse>> GetProducts();
        Task<ProductDetailsResponse> GetProductDetails(ProductDetailsRequest request);
        double GetPriceReduction(int dayOfWeek);
    }
}
