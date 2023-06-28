using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public bool AddNew(T entity);
        public T GetById(int id);
        public IEnumerable<T> GetAll();
        public T Update(int id);
        public bool Delete(int id);
    }
}
