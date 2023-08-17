using CurriculumWebAPI.Domain.Exceptions;
using CurriculumWebAPI.Domain.Interfaces;
using CurriculumWebAPI.Domain.Models.CurriculumBody;
using CurriculumWebAPI.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Infrastructure.Data.Repositories
{
    public class HabilidadeRepository : IRepositoryForCollections<Habilidades>
    {
        private readonly MyContext _context;

        public HabilidadeRepository(MyContext context)
        {
            _context = context;
        }

        public async Task<int> AddNew(Habilidades entity)
        {
            _context.Habilidades.Add(entity);

            return await _context.SaveChangesAsync();
        }

        public Task<int> DeleteAllItems(Habilidades[] entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteByItem(Habilidades entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Habilidades>> GetAllByCurriculumId(string curriculumId)
        {
            var queryResult =_context.
                Habilidades.Where(f => f.CurriculumId == curriculumId);

            return queryResult.ToList();
        }

        public async Task<Habilidades> GetById(int id)
        {
            var result = await _context.Habilidades.FindAsync(id);

            return result;
        }

        public async Task<string> GetCurriculumId(string email)
        {
            var user = await _context.Users.Include(c => c.Curriculum)
                .FirstOrDefaultAsync(c => c.Email == email);


            //APLICAR ESTE MODELO PARA OS SEGUINTES REPOS
            if(user.Curriculum is null)
                throw new NotFoundInDatabaseException("Id do curriculo não encontrado no banco");

            return user.Curriculum.Id;
        }

        public Task<bool> Update(Habilidades entity)
        {
            throw new NotImplementedException();
        }
    }
}
