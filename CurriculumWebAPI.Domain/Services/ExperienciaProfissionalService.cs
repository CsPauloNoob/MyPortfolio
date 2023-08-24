using CurriculumWebAPI.Domain.Exceptions;
using CurriculumWebAPI.Domain.Interfaces;
using CurriculumWebAPI.Domain.Models.CurriculumBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Services
{
    public class ExperienciaProfissionalService
    {
        private readonly IRepositoryForCollections<Experiencia_Profissional> _repository;

        public ExperienciaProfissionalService
            (IRepositoryForCollections<Experiencia_Profissional> repository)
        {
            _repository = repository;
        }



        public async Task<bool> AddExpProfissional(Experiencia_Profissional[] expProfissional, string email)
        {
            var curriculumId = await _repository.GetCurriculumId(email);
            int result = 0;

            if (curriculumId is null)
                throw new DirectoryNotFoundException("Curriculo não encontrado no banco");

            foreach (var item in expProfissional)
            {
                item.CurriculumId = curriculumId;
                result += await _repository.AddNew(item);
            }

            if (result == expProfissional.Count())
                return true;

            return false;
        }


        public async Task<List<Experiencia_Profissional>> GetAllByEmail(string email)
        {
            var curricculumId = await _repository.GetCurriculumId(email);

            if (curricculumId is null)
                throw new NotFoundInDatabaseException("Id do curriculo não encontrado no banco");

            var expProfissional = await _repository.GetAllByCurriculumId(curricculumId);

            if (expProfissional.Count == 0)
                throw new NotFoundInDatabaseException("ítens não encontrados no banco de dados");

            return expProfissional;
        }


        public async Task<Experiencia_Profissional> GetById(int id, string email)
        {
            var expProfissional = await _repository.GetById(id);

            if (expProfissional is null)
                throw new NotFoundInDatabaseException("id não corresponde a nenhum objeto no banco");

            var curriculumId = await _repository.GetCurriculumId(email);

            if (curriculumId == expProfissional.CurriculumId)
                return expProfissional;

            else throw new NotFoundInDatabaseException("Id incorreto");
        }


        public async Task<bool> UpdateById(int id, string email, Experiencia_Profissional cursoExtra)
        {
            var curriculumId = await _repository.GetCurriculumId(email);
            var existingExpProfissional = await _repository.GetById(id);

            if (existingExpProfissional is null)
                throw new NotFoundInDatabaseException("Objeto não encontrado no banco de dados!");

            else if (existingExpProfissional.CurriculumId != curriculumId)
                throw new NotFoundInDatabaseException("id incorreto ou usuário não cadastrou um curriculo");

            #region Validações ruins
            //refatorar para operadores ternarios
            if (existingExpProfissional.Nome_Organizacao != cursoExtra.Nome_Organizacao)
                existingExpProfissional.Nome_Organizacao = cursoExtra.Nome_Organizacao;

            if (existingExpProfissional.Funcao != cursoExtra.Funcao)
                existingExpProfissional.Funcao = cursoExtra.Funcao;

            #endregion

            if (await _repository.Update(existingExpProfissional))
                return true;

            else
                throw new SaveFailedException("Erro ao salvar módulo de formação no banco!");
        }


        public async Task<bool> DeleteAllItems(string email)
        {
            var expProfissional = await GetAllByEmail(email);

            var result = await _repository.DeleteAllItems(expProfissional.ToArray());

            if (result < 1)
                throw new SaveFailedException("Não foi possível salvar as alterações no banco");

            else return true;
        }


        public async Task<bool> DeleteById(int id, string email)
        {
            var curriculumId = await _repository.GetCurriculumId(email);

            var existingExpProfissional = await _repository.GetById(id);

            if (existingExpProfissional is null || curriculumId != existingExpProfissional.CurriculumId)
                throw new NotFoundInDatabaseException("Objeto não encontrado no banco");

            var result = await _repository.DeleteByItem(existingExpProfissional);

            if (result > 0)
                return true;

            else
                throw new SaveFailedException("Não foi possível salvar as alterações no banco");
        }
    }
}
