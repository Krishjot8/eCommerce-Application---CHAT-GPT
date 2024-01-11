using eCommerce_App.Models;
using System.Text.Json;

namespace eCommerce_App.Data
{
    public class eCommerceContextSeed
    {

        public static async Task SeedAsync(ECommerceContext context)
        {

            if (!context.ProductBrands.Any())

            {

                var brandsData = File.ReadAllText("E:/Udemy/Rahul Sahay/eCommerce App/eCommerce App-API/eCommerce App/Data/ProductBrands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                context.ProductBrands.AddRange(brands);

            
            
            
            }

            if (!context.ProductTypes.Any())
            {

                var typesData = File.ReadAllText("E:/Udemy/Rahul Sahay/eCommerce App/eCommerce App-API/eCommerce App/Data/ProductTypes.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                context.ProductTypes.AddRange(types);




            }

            if (!context.Products.Any())
            { 
            
            var productsdata = File.ReadAllText("E:/Udemy/Rahul Sahay/eCommerce App/eCommerce App-API/eCommerce App/Data/Products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsdata);
                context.Products.AddRange(products);
            
            }


            if(context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();

        }

    }
}
