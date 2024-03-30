using LiteEcommerceApi.Dots;
using LiteEcommerceApi.Helper;

namespace LiteEcommerceApi.Services
{
    public interface IProduct
    {
        public Task<Resposne> addProductService(ProductDots product);

        public Task<List<Models.Product>> getProducts();
    }
}
