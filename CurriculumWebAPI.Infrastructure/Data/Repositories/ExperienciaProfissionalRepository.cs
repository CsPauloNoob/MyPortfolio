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
    public class ExperienciaProfissionalRepository : IRepositoryForCollections<Experiencia_Profissional>
    {
        private readonly MyContext _context;

        public ExperienciaProfissionalRepository(MyContext context)
        {
            _context = context;
        }

        public async Task<int> AddNew(Experiencia_Profissional entity)
        {
            _context.Experiencias.Add(entity);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAllItems(Experiencia_Profissional[] entities)
        {
            _context.Experiencias.RemoveRange(entities);

            var result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<int> DeleteByItem(Experiencia_Profissional entity)
        {
            _context.Experiencias.Remove(entity);

            var result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<List<Experiencia_Profissional>> GetAllByCurriculumId(string curriculumId)
        {
            var queryResult = _context.
                Experiencias.Where(f => f.CurriculumId == curriculumId);

            return queryResult.ToList();
        }

        public async Task<Experiencia_Profissional> GetById(int id)
        {
            var result = await _context.Experiencias.FindAsync(id);

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

        public async Task<bool> Update(Experiencia_Profissional entity)
        {
            _context.Experiencias.Update(entity);

            var result = await _context.SaveChangesAsync();

            return result > 0 ? true : false;
        }
    }
}
