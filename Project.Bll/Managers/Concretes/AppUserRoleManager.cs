using Project.Bll.Managers.Abstracts;
using Project.Dal.Repositories.Abstracts;
using Project.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bll.Managers.Concretes
{
    public class AppUserRoleManager(IAppUserRoleRepository repository) : BaseManager<AppUserRole>(repository),IAppUserRoleManager
    {
        private readonly IAppUserRoleRepository _repository = repository;
    }
}
