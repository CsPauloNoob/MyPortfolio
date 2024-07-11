using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task<int> AddNew(T entity);
        public Task<T> GetById(string id, bool IsComplet = false);
        public Task<T> GetByEmail(string email, bool IsComplet = false);
        public Task<bool> Update(T entity);
        public Task<bool> Delete(string id);

    }
}