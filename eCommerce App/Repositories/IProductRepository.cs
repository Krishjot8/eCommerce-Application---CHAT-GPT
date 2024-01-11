using eCommerce_App.Models;
using System.Collections.Generic;

namespace eCommerce_App.Repositories
{
    public interface IProductRepository
    {

      

        public Task<IList<Product>> GetProductsAsync();

        public Task<Product> GetProductByIdAsync(int id);

        public Task<IList<ProductBrand>> GetProductBrandsAsync();


        public Task<IList<ProductType>> GetProductTypesAsync();
    }
}
