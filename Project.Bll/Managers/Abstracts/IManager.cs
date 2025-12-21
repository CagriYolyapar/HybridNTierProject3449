using Project.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bll.Managers.Abstracts
{
    public interface IManager<T> where T:class,IEntity
    {
        //BL for Queries
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        List<T> GetActives();
        List<T> GetPassives();
        List<T> GetUpdateds();
        Task<List<T>> GetLastDatasAsync(int count);
        Task<List<T>> GetFirstDatasAsync(int count);

        //BL for Commands
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task SoftDeleteAsync(int id);
        Task<string> HardDeleteAsync(int id);

    }
}
