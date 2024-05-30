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

        public async Task<Curriculum> GetHeaderByEmail(string email)
        {

            var result = await _curriculumRepository.GetByEmail(email);

            if (result is null)
                throw new NotFoundInDatabaseException("Erro no banco ou objeto não encontrado");

            else return result;

        }
        
        // TODO: resposta generica, string magica e implementar RESULT VALUES;
        public async Task<Curriculum> HeaderSave(Curriculum curriculum, string emailUser)
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


        public async Task<bool> HeaderUpdate(Curriculum curriculum, string email)
        {
            var existingCurriculum = await _curriculumRepository.GetByEmail(email);

            if (existingCurriculum is null)
                throw new NotFoundInDatabaseException("Objeto não encontrado no banco de dados!");

            #region Validações ruins

            if (existingCurriculum.Nome != curriculum.Nome)
                existingCurriculum.Nome = curriculum.Nome;

            if (existingCurriculum.SobreMim != curriculum.SobreMim)
                existingCurriculum.SobreMim = curriculum.SobreMim;

            if (existingCurriculum.PerfilProgramador != curriculum.PerfilProgramador)
                existingCurriculum.PerfilProgramador = curriculum.PerfilProgramador;

            #endregion

            if (await _curriculumRepository.Update(existingCurriculum))
                return true;
            else
                throw new SaveFailedException("Erro ao salvar módulo de formação no banco!");
        }


        //Problema consultar o curriculum em novas contas
        private async Task<bool> IsNewCurriculum(string email)
        {
            
            try
            {
                var headerCurriculum = await _curriculumRepository.GetByEmail(email);

                if (headerCurriculum is not null)
                    return false;

                else return true;
            }

            catch(Exception)
            {
                return false;
            }
        }












        #region Contato Services


        public async Task<bool> AddContato(Contato contato)
        {
            int result = await _contatoRepository.AddNew(contato);

            if (result > 0)
                return true;

            else return false;
        }

        public async Task<Contato> GetContatoFromCurriculumByEmail(string email, bool shotException = true)
        {
            var curriculum = await _curriculumRepository.GetByEmail(email);

            if (curriculum is null)
                throw new NotFoundInDatabaseException("Curriculum não encontrado no banco");
            
            var contato = await _contatoRepository.GetById(curriculum.Id);

            if (shotException && contato is null)
                throw new NotFoundInDatabaseException("sessão de contato não preenchida.");

            return contato;
        }


        public async Task<Contato> UpdateContato(Contato contato)
        {
            await _contatoRepository.Update(contato);

            return contato;
        }

        #endregion
    }
}