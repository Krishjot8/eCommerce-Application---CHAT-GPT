using eCommerce_App.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace eCommerce_App.Data
{
    public class eCommerceContextSeed
    {

        public static async Task SeedDataAsync(ECommerceContext context, ILoggerFactory loggerFactory) {

            try
            {

                var logger = loggerFactory.CreateLogger<eCommerceContextSeed>();


                if (!await context.ProductBrands.AnyAsync() &&
            !await context.ProductTypes.AnyAsync() &&
            !await context.Products.AnyAsync())
                {

                    var seedDirectory = GetSeedDirectory();

                    await SeedBrandsAsync(context, logger, seedDirectory);
                    await SeedTypesAsync(context, logger, seedDirectory);
                    await SeedProductsAsync(context, logger, seedDirectory);





                }

            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<eCommerceContextSeed>();
                logger.LogError(ex, message: "An error occured while seeding the database.");
                throw;
            }
        
        }

       
          private static async Task SeedBrandsAsync(ECommerceContext context, ILogger<eCommerceContextSeed> logger, string seedDirectory) 
          {


              try
              {
                  var brandJsonPath = Path.Combine(seedDirectory, "E:/Udemy/Rahul Sahay/eCommerce App/eCommerce App-API/eCommerce App/Data/ProductBrands.json");
                  var brandJson = await File.ReadAllTextAsync(brandJsonPath);
                  var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandJson);

                  await context.ProductBrands.AddRangeAsync(brands);
                  await context.SaveChangesAsync();
                  logger.LogInformation("Seeded Brands data.");

              }
              catch (Exception ex)
              {

                  logger.LogError(ex,"Error seeding Brands data");
              }

          }

        

        private static async Task SeedTypesAsync(ECommerceContext context, ILogger<eCommerceContextSeed> logger, string seedDirectory)
          {
              try
              {
                  var typeJsonPath = Path.Combine(seedDirectory, "E:/Udemy/Rahul Sahay/eCommerce App/eCommerce App-API/eCommerce App/Data/ProductTypes.json");
                  var typeJson = await File.ReadAllTextAsync(typeJsonPath);
                  var types = JsonSerializer.Deserialize<List<ProductType>>(typeJson);

                  await context.ProductTypes.AddRangeAsync(types);
                  await context.SaveChangesAsync();
                  logger.LogInformation("Seeded Types data.");

              }
              catch (Exception ex)
              {

                  logger.LogError(ex, "Error seeding Types data");
              }



          }


         
          private static async Task SeedProductsAsync(ECommerceContext context, ILogger<eCommerceContextSeed> logger, string seedDirectory)
          {

              try
              {
                  var productJsonPath = Path.Combine(seedDirectory, "E:/Udemy/Rahul Sahay/eCommerce App/eCommerce App-API/eCommerce App/Data/Products.json");
                  var productJson = await File.ReadAllTextAsync(productJsonPath);
                  var products = JsonSerializer.Deserialize<List<Product>>(productJson);

                  await context.Products.AddRangeAsync(products);
                  await context.SaveChangesAsync();
                  logger.LogInformation("Seeded Products data.");

              }
              catch (Exception ex)

              {

                  logger.LogError(ex, "Error seeding Products data");
              }



          }
       

        private static string GetSeedDirectory()
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            return Path.Combine(currentDirectory, "Data", "eCommerceContextSeed");
        }

    }
}
