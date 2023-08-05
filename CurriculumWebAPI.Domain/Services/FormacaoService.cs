using CurriculumWebAPI.Domain.Interfaces;
using CurriculumWebAPI.Domain.Models.CurriculumBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Services
{
    public class FormacaoService
    {
        private readonly IRepository<Formacao> _repository;

        public FormacaoService(IRepository<Formacao> repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddFormacao(Formacao formacao, string curriculumId)
        {
            formacao.CurriculumId = curriculumId;
            var result = await _repository.AddNew(formacao);

            if(result>0)
                return true;

            return false;
        }

        public async Task<List<Formacao>> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}