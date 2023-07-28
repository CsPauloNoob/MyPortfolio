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

        public async Task<Curriculum> GetByEmail(string email)
        {

            var result = await _curriculumRepository.GetByEmail(email);

            if (result is null)
                throw new NotFoundInDatabaseException("Erro no banco ou objeto não encontrado");

            else return result;

        }


        public async Task<Curriculum> Save(Curriculum curriculum, string emailUser)
        {
            var isNew = await IsNewCurriculum(emailUser);

            if (isNew)
            {
                curriculum.Id = Guid.NewGuid().ToString();

                if (await _curriculumRepository.AddNew(curriculum) > 0)
                    return curriculum;
            }

            throw new SaveFailedException("Erro ao salvar curriculo");
        }


        private async Task<bool> IsNewCurriculum(string email)
        {
            try
            {
                var result = await _curriculumRepository.GetByEmail(email);
                return false;
            }

            catch(Exception)
            {
                return true;
            }
        }















        public async Task<bool> AddContato(Contato contato)
        {
            int result = await _contatoRepository.AddNew(contato);

            if (result > 0)
                return true;

            else return false;
        }

        public async Task<Contato> GetContatoFromCurriculumByEmail(string email)
        {
            var curriculum = await _curriculumRepository.GetByEmail(email);

            var contato = await _contatoRepository.GetById(curriculum.Id);

            if (contato is not null)
                return contato;

            else
                throw new NotFoundInDatabaseException("sessão de contato não preenchida.");
        }
    }
}