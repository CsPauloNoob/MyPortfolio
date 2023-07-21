using CurriculumWebAPI.Domain.Interfaces;
using CurriculumWebAPI.Domain.Models.CurriculumBody;
using CurriculumWebAPI.Infrastructure.Data.Context;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Infrastructure.Data.Repositories
{
    public class ContatoRepository : IRepository<Contato>
    {
        private readonly MyContext _context;

        public ContatoRepository(MyContext context)
        {
            _context = context;
        }

        public async Task<int> AddNew(Contato entity)
        {
            await _context.Contato.AddAsync(entity);

            return await _context.SaveChangesAsync();
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contato> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Contato> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Contato> Update(string id)
        {
            throw new NotImplementedException();
        }
    }
}
