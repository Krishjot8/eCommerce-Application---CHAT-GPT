using eCommerce_App.Data;
using eCommerce_App.ExceptionMiddleware;
using eCommerce_App.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<IList<Product>> GetProductsAsync()
        {
            return await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {




            var product = await _context.Products
                 .Include(p => p.ProductType)
                 .Include(p => p.ProductBrand)
                 .FirstOrDefaultAsync(p => p.Id == id);



            return product;







        }



        public async Task<Product> AddProductAsync(Product product)
        {
            if (product == null) {
            
            
            throw new ArgumentNullException(nameof(product));
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product; 
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {

            if (product == null) { 
            
            
                throw new ArgumentNullException(nameof(product));
            
            }


            _context.Entry(product).State = EntityState.Modified;


            try

            {
                await _context.SaveChangesAsync();
             
            }

            catch (DbUpdateConcurrencyException) {


                if (! await ProductExistsAsync(product.Id))
                {

                    throw new NotFoundException("Product not Found");

                }
                else {


                    throw;
                
                }
            
            }

            return product; 
        }



        public async Task<Product>DeleteProductAsync(int id)
        {
            var product = await GetProductByIdAsync(id);

            if (product != null) {
            
            
                _context.Products.Remove(product);

                await _context.SaveChangesAsync();

                return product;

            }

            return null;
        }





















     
       

        public async Task<IList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<IList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }


   

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ProductExistsAsync(int productId)
        {
            return await _context.Products.AnyAsync(p => p.Id == productId);
        }
    }
}
