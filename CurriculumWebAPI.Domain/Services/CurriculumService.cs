using CurriculumWebAPI.Domain.Exceptions;
using CurriculumWebAPI.Domain.Interfaces;
using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Domain.Models.CurriculumBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Services
{
    public class CurriculumService
    {
        private readonly IRepository<Curriculum> _curriculumRepository;
        private readonly IRepository<Contato> _contatoRepository;

        public CurriculumService(IRepository<Curriculum> curriculumRepository, IRepository<Contato> contatoRepository)
        {
            _curriculumRepository = curriculumRepository;
            _contatoRepository = contatoRepository;
        }

        public async Task<Curriculum> GetById(string id)
        {
            var result = await _curriculumRepository.GetById(id);

            return result;
        }


        public async Task<Curriculum> Save(Curriculum curriculum, string emailUser)
        {
            var curriculumUser = await _curriculumRepository.GetByEmail(emailUser);

            if (curriculumUser is null)
            {
                curriculum.Id = Guid.NewGuid().ToString();

                if (await _curriculumRepository.AddNew(curriculum) > 0)
                    return curriculum;
            }

            throw new SaveFailedException("Erro ao salvar curriculo");
        }

        public async Task<bool> AddContato(Contato contato)
        {
            int result = await _contatoRepository.AddNew(contato);

            if (result > 0)
                return true;

            else return false;
        }
    }
}