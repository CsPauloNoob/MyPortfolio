using CurriculumWebAPI.Domain.Interfaces;
using CurriculumWebAPI.Domain.Models.CurriculumBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Services
{
    public class ContatoService
    {
        private readonly IRepository<Contato> _repository;

        public ContatoService(IRepository<Contato> repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddContato(Contato contato)
        {
            if(await _repository.AddNew(contato) > 0)
            return true;

            else return false;
        }
    }
}
