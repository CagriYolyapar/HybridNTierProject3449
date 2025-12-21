using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Dal.ContextClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bll.DependencyResolvers
{
    public static class DbContextResolver
    {
        public static void AddDbContextService(this IServiceCollection services)
        {
            ServiceProvider serviceProvider = services.BuildServiceProvider();

            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();

            services.AddDbContext<MyContext>(x => x.UseSqlServer(configuration.GetConnectionString("MyConnection")).UseLazyLoadingProxies());
        }
    }
}
