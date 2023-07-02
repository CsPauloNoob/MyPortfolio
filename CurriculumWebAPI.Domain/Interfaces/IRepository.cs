using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task<bool> AddNew(T entity);
        public Task<T> GetById(string id);
        public IEnumerable<T> GetAll();
        public Task<T> Update(string id);
        public Task<bool> Delete(string id);
    }
}
