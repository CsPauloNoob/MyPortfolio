using CurriculumWebAPI.Domain.Exceptions;
using CurriculumWebAPI.Domain.Interfaces;
using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CurriculumWebAPI.Infrastructure.Data.Repositories
{
    public class CurriculumReporitory : IRepository<Curriculum>
    {
        private readonly MyContext _context;

        public CurriculumReporitory(MyContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(string id)
        {
            Curriculum? entity = _context.Curriculum.Find(id);

            if(entity!=null)
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }



        public async Task<Curriculum> GetById(string id)
        {
            var curriculo = _context.Curriculum.Find(id);


            return curriculo;
        }

        public async Task<Curriculum> GetByEmail(string email)
        {
            var user = _context.Users.Include(c => c.Curriculum).FirstOrDefault(u => u.Email == email);

            if (user.Curriculum is null)
                throw new NotFoundInDatabaseException("Curriculum não existe");

            return user.Curriculum;
        }

        public async Task<int> AddNew(Curriculum curriculum)
        {
            _context.Curriculum.Add(curriculum);
            return await _context.SaveChangesAsync();
        }

        public async Task<Curriculum> Update(Curriculum entity)
        {
            return null;
        }
    }
}