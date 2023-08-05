using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Interfaces
{
    public interface IRepositoryForCollections<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllByEmailAsync(string email);
        //Resolver problema de se faz sentido enviar uma formacao por vez
    }
}