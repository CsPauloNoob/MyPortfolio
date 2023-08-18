using CurriculumWebAPI.Domain.Interfaces;
using CurriculumWebAPI.Domain.Models.CurriculumBody;
using CurriculumWebAPI.Infrastructure.Data.Context;
using CurriculumWebAPI.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using CurriculumWebAPI.Domain.Models;

namespace CurriculumWebAPI.Infrastructure.Data.Repositories
{
    public class FormacaoRepository : IRepositoryForCollections<Formacao>
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

        public Task<List<Formacao>> GetAllByCurriculumId(string id)
        {
            var queryResult = 
                _context.Formacao.Where(f => f.CurriculumId == id);

            return Task.FromResult(queryResult.ToList());
        }

        public async Task<Formacao> GetById(int id)
        {
            var formacao = await _context.Formacao.FindAsync(id);

            return formacao;
        }

        public async Task<bool> Update(Formacao entity)
        {
            _context.Formacao.Update(entity);

            var result = await _context.SaveChangesAsync();

            return result > 0 ? true : false;
        }

        public async Task<int> DeleteByItem(Formacao entity)
        {
            _context.Formacao.Remove(entity);

            var result = await _context.SaveChangesAsync();

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

        public async Task<int> DeleteAllItems(Formacao[] formacao)
        {
            _context.Formacao.RemoveRange(formacao);

            var result = await _context.SaveChangesAsync();

            return result;
        }
    }
}