using Microsoft.Extensions.DependencyInjection;
using Project.Dal.Repositories.Abstracts;
using Project.Dal.Repositories.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bll.DependencyResolvers
{
    public static class RepositoryResolver
    {
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IAppRoleRepository, AppRoleRepository>();
            services.AddScoped<IAppUserRoleRepository, AppUserRoleRepository>();
            services.AddScoped<IAppUserProfileRepository, AppUserProfileRepository>();
        }
    }
}
