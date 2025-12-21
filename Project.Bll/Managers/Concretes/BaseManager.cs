using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Project.Bll.Managers.Abstracts;
using Project.Dal.Repositories.Abstracts;
using Project.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bll.Managers.Concretes
{
    public abstract class BaseManager<T> : IManager<T> where T : class, IEntity
    {
        private readonly IRepository<T> _repository;

        protected BaseManager(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.Status = Entities.Enums.DataStatus.Inserted;
            await _repository.CreateAsync(entity);
        }

        public List<T> GetActives()
        {
            return _repository.Where(x => x.Status != Entities.Enums.DataStatus.Deleted).ToList();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<T>> GetFirstDatasAsync(int count)
        {
            IQueryable<T> values = _repository.Where(x => x.Status != Entities.Enums.DataStatus.Deleted);

            return await values.OrderBy(x => x.CreatedDate).Take(count).ToListAsync();
        }

        public async Task<List<T>> GetLastDatasAsync(int count)
        {
            IQueryable<T> values = _repository.Where(x => x.Status != Entities.Enums.DataStatus.Deleted);

            return await values.OrderByDescending(x => x.CreatedDate).Take(count).ToListAsync();
        }

        public List<T> GetPassives()
        {
            return _repository.Where(x => x.Status == Entities.Enums.DataStatus.Deleted).ToList();
        }

        public List<T> GetUpdateds()
        {
            return _repository.Where(x => x.Status == Entities.Enums.DataStatus.Updated).ToList();
        }

        public async Task<string> HardDeleteAsync(int id)
        {
            T entity = await GetByIdAsync(id);
            if (entity != null && entity.Status == Entities.Enums.DataStatus.Deleted)
            {
                await _repository.DeleteAsync(entity);
                return $"Silme işlemi basarılıdır..Silinen id =>  {entity.Id}...Silinmek istenen {id} verisiydi";
            }
            return "Sadece bulunabilen pasif verileri silebilirsiniz";

        }


        //Refactor (null gelirse bunu engelleyiniz)
        public async Task SoftDeleteAsync(int id)
        {
            T entity = await GetByIdAsync(id);
            entity.DeletedDate = DateTime.Now;
            entity.Status = Entities.Enums.DataStatus.Deleted;
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            entity.Status = Entities.Enums.DataStatus.Updated;
            entity.UpdatedDate = DateTime.Now;
            T originalEntity = await GetByIdAsync(entity.Id);

            await _repository.UpdateAsync(originalEntity, entity);
        }
    }
}
