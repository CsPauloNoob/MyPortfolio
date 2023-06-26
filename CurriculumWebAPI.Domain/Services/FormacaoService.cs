using CurriculumWebAPI.Domain.Models;
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

        public async Task<bool> AddFormacao(Formacao formacao)
        {
            //if(_repository)

            return true;
        }

    }
}