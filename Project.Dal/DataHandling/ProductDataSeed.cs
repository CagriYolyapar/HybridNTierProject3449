using Microsoft.EntityFrameworkCore;
using Project.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus.DataSets;

namespace Project.Dal.DataHandling
{
    public static class ProductDataSeed
    {
        public static void SeedProducts(ModelBuilder builder)
        {
            List<Product> products = new();

            for (int i = 1; i < 11; i++)
            {
                Product p = new()
                {
                    Id = i,
                    ProductName = new Commerce("tr").ProductName(),
                    UnitPrice = Convert.ToDecimal(new Commerce("tr").Price()),
                    UnitsInStock = 100,
                    CategoryId = i,
                    ImagePath = new Images().DataUri(100,100),
                    CreatedDate = DateTime.Now,
                    Status = Entities.Enums.DataStatus.Inserted
                };

                products.Add(p);
            }

            builder.Entity<Product>().HasData(products);
        }
    }
}
