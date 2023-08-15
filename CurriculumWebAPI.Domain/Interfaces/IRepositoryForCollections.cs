using CurriculumWebAPI.Domain.Models.CurriculumBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Interfaces
{
    public interface IRepositoryForCollections<T> where T : class
    {
        public Task<int> AddNew(T entity);
        public Task<List<T>> GetAllByCurriculumId(string curriculumId);
        public Task<T> GetById(int id);
        public Task<string> GetCurriculumId(string email);
        public Task<bool> Update(T entity);
        public Task<int> DeleteByItem(T entity);
        public Task<int> DeleteAllItems(T[] entities);
    }
}