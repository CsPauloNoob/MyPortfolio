using CurriculumWebAPI.Domain;
using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Infrastructure.Data.Repositories
{
    public class CurriculumReporitory : IRepository<Curriculum>
    {
        private readonly MyContext _context;

        public CurriculumReporitory(MyContext context)
        {
            _context = context;
        }

        public bool Delete(int id)
        {
            Curriculum? entity = _context.Curriculum.Find(id);

            if(entity!=null)
            {
                _context.Remove(entity);
                _context.SaveChangesAsync();
                return true;
            }

            return false;
        }



        public IEnumerable<Curriculum> GetAll()
        {
            var curriculos = _context.Curriculum.ToList();

            return curriculos;
        }



        public Curriculum GetById(int id)
        {
            var curriculo = _context.Curriculum.Find(id);


            return curriculo;
        }

        public bool AddNew(Curriculum curriculum)
        {
            _context.Curriculum.Add(curriculum);
            _context.SaveChangesAsync();

            return true;
        }

        public Curriculum Update(int id)
        {
            Curriculum? curriculo = _context.Curriculum.Find(id);

            if(curriculo != null)
            {
                _context.Update(curriculo);
                _context.SaveChangesAsync();
                return curriculo;
            }

            return null;
        }
    }
}
