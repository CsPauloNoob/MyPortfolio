using CurriculumWebAPI.Domain.Exceptions;
using CurriculumWebAPI.Domain.Interfaces;
using CurriculumWebAPI.Domain.Models.CurriculumBody;
using CurriculumWebAPI.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Infrastructure.Data.Repositories
{
    public class CursosExtrasRepository : IRepositoryForCollections<Cursos_Extras>
    {
        private readonly MyContext _context;

        public CursosExtrasRepository(MyContext myContext)
        {
                _context = myContext;
        }





        public async Task<int> AddNew(Cursos_Extras entity)
        {
            _context.Cursos_Extras.Add(entity);

            return await _context.SaveChangesAsync();
        }


        public async Task<int> DeleteAllItems(Cursos_Extras[] entities)
        {
            _context.Cursos_Extras.RemoveRange(entities);

            return await _context.SaveChangesAsync();
        }


        public async Task<int> DeleteByItem(Cursos_Extras entity)
        {
            _context.Cursos_Extras.Remove(entity);

            return await _context.SaveChangesAsync();
        }


        public Task<List<Cursos_Extras>> GetAllByCurriculumId(string curriculumId)
        {
            var queryResult = _context.Cursos_Extras.
                Where(c => c.CurriculumId == curriculumId);


            return Task.FromResult(queryResult.ToList());
        }


        public async Task<Cursos_Extras> GetById(int id)
        {
            var result = await _context.Cursos_Extras.FindAsync(id);

            return result;
        }


        public async Task<string> GetCurriculumId(string email)
        {
            var user = await _context.Users.Include(c => c.Curriculum)
               .FirstOrDefaultAsync(c => c.Email == email);

            if (user.Curriculum is null)
                throw new NotFoundInDatabaseException("Id do curriculo não encontrado no banco");

            return user.Curriculum.Id;
        }


        public async Task<bool> Update(Cursos_Extras entity)
        {
            _context.Cursos_Extras.Update(entity);

            var result = await _context.SaveChangesAsync();

            return result > 0 ? true : false;
        }
    }
}
