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
    public static class CategoryDataSeed
    {
        public static void SeedCategories(ModelBuilder builder)
        {
            List<Category> categories = new List<Category>();

            for(int i = 1; i < 11; i++)
            {
                Category c = new()
                {
                    Id = i,
                    CategoryName = new Commerce("tr").Categories(1)[0],
                    Description = new Lorem("tr").Sentence(10),
                    CreatedDate = DateTime.Now,
                    Status = Entities.Enums.DataStatus.Inserted
                };

                categories.Add(c);
            }

            builder.Entity<Category>().HasData(categories);
        }
    }
}
