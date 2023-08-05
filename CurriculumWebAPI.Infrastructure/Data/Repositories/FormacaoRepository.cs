using CurriculumWebAPI.Domain.Interfaces;
using CurriculumWebAPI.Domain.Models.CurriculumBody;
using CurriculumWebAPI.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Infrastructure.Data.Repositories
{
    public class FormacaoRepository : IRepository<Formacao>
    {
        private readonly MyContext _context;

        public FormacaoRepository(MyContext context)
        {
            _context = context;
        }



        public async Task<int> AddNew(Formacao entity)
        {
            _context.Formacao.Add(entity);

            return await _context.SaveChangesAsync();
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Formacao> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Formacao> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Formacao> Update(Formacao entity)
        {
            throw new NotImplementedException();
        }
    }
}
