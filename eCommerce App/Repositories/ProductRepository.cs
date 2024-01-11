using eCommerce_App.Data;
using eCommerce_App.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_App.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly ECommerceContext _context;

        public ProductRepository(ECommerceContext context)
        {
            _context = context;
        }

        public async Task<IList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                 .Include(p => p.ProductType)
                 .Include(p => p.ProductBrand)
                 .FirstOrDefaultAsync(p=>p.Id == id);

        }

        public Task<IList<Product>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}
