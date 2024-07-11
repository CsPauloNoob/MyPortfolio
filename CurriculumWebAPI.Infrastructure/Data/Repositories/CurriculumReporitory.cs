using CurriculumWebAPI.Domain.Exceptions;
using CurriculumWebAPI.Domain.Interfaces;
using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Infrastructure.Data.Context;
using CurriculumWebAPI.Infrastructure.IdentityConfigs;
using Microsoft.EntityFrameworkCore;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

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



        public async Task<Curriculum> GetById(string id, bool IsComplet)
        {
            var curriculo = _context.Curriculum.Include
                (c => c.Experiencia_Profissional).Include
                (c => c.Cursos).Include(c => c.Habilidade).Include
                (c => c.Contato).Include
                (c => c.Formacao).FirstOrDefault
                (c => c.Id == id);


            return curriculo;
        }

        public async Task<Curriculum> GetByEmail(string email, bool IsComplet)
        {
            email = email.Normalize();
            ApplicationUser user =  null!;
            Curriculum curriculum = null!;


            if (!IsComplet)
                user = _context.Users.Where(u => u.Email == email).Include(c => c.Curriculum).FirstOrDefault()!;

            else
                curriculum = _context.Curriculum.Include
                    (c => c.Experiencia_Profissional).Include
                    (c => c.Cursos).Include(c => c.Habilidade).Include
                    (c => c.Contato).Include
                    (c => c.Formacao).FirstOrDefault() !;

            if (user?.Curriculum is null)
            {
                return curriculum;
            }

            else
                return user.Curriculum;
        }

        public async Task<int> AddNew(Curriculum curriculum)
        {
            _context.Curriculum.Add(curriculum);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> Update(Curriculum entity)
        {
            _context.Curriculum.Update(entity);

            var result = await _context.SaveChangesAsync();

            return result > 0 ? true : false;
        }
    }
}