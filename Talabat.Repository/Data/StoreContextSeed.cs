using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
    public class StoreContextSeed
    {

        public static async Task SeedAsync(StoreContext context,ILoggerFactory loggerFactory)
        {
            try
            {
                if( ! context.ProductBrands.Any())
                {
                    var BrandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

                    foreach (var brand in brands)
                    {
                        context.Set<ProductBrand>().Add(brand);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.ProductTypes.Any())
                {
                    var TypesData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);

                    foreach (var Type in types)
                    {
                        context.Set<ProductType>().Add(Type);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.Products.Any())
                {
                    var ProductsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                    foreach (var product in Products)
                    {
                        context.Set<Product>().Add(product);
                    }
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                var logger =loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex, ex.Message);
               
            }
            
        }
    }
}
