using Microsoft.Extensions.DependencyInjection;
using Project.Dal.ContextClasses;
using Project.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bll.DependencyResolvers
{
    public static class IdentityResolver
    {
        public static void AddIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(x =>
            {
                x.SignIn.RequireConfirmedEmail = true;
                x.Password.RequiredLength = 3;
                x.Password.RequireLowercase = false;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireUppercase = false;
                x.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<MyContext>();
        }
    }
}
