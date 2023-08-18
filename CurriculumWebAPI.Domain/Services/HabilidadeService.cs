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
    public class HabilidadeService
    {
        private readonly IRepositoryForCollections<Habilidades> _repository;

        public HabilidadeService(IRepositoryForCollections<Habilidades> repository)
        {
            _repository = repository;
        }

        
        public async Task<bool> CreateHabilidade(Habilidades[] habilidades, string email)
        {
            var curriculumId = await _repository.GetCurriculumId(email);
            int result = 0;

            if(curriculumId is null)
                throw new DirectoryNotFoundException("Curriculo não encontrado no banco");

            foreach (var item in habilidades)
            {
                item.CurriculumId = curriculumId;
                result += await _repository.AddNew(item);
            }

            if (result == habilidades.Count())
                return true;

            else throw new SaveFailedException("Não foi possível salvar todos os objetos no banco");
        }


        public async Task<List<Habilidades>> GetAllByEmail(string email)
        {
            var curricculumId = await _repository.GetCurriculumId(email);

            var habilidadeList = await _repository.GetAllByCurriculumId(curricculumId);

            if (habilidadeList.Count == 0)
                throw new NotFoundInDatabaseException("ítens não encontrados no banco de dados");

            return habilidadeList;
        }

        public async Task<Habilidades> GetById(int id, string email)
        {
            var habilidade = await _repository.GetById(id);

            if (habilidade is null)
                throw new NotFoundInDatabaseException("id não corresponde a nenhum mobjeto no banco");

            var curriculumId = await _repository.GetCurriculumId(email);

            if (curriculumId == habilidade.CurriculumId)
                return habilidade;

            else throw new NotFoundInDatabaseException("Id incorreto");
        }


        public async Task<bool> UpdateById(int id, string email, Habilidades habilidade)
        {
            var curriculumId = await _repository.GetCurriculumId(email);
            var existingHabilidade = await _repository.GetById(id);

            if (existingHabilidade is null)
                throw new NotFoundInDatabaseException("Objeto não encontrado no banco de dados!");

            else if (existingHabilidade.CurriculumId != curriculumId)
                throw new NotFoundInDatabaseException("id incorreto ou usuário não cadastrou um curriculo");

            #region Validações ruins

            if (existingHabilidade.Nome_Habilidade != habilidade.Nome_Habilidade)
                existingHabilidade.Nome_Habilidade = habilidade.Nome_Habilidade;

            if (existingHabilidade.Descricao != habilidade.Descricao)
                existingHabilidade.Descricao = habilidade.Descricao;

            #endregion

            if (await _repository.Update(existingHabilidade))
                return true;
            else
                throw new SaveFailedException("Erro ao salvar módulo de formação no banco!");
        }


        public async Task<bool> DeleteById(int id, string email)
        {
            var curriculumId = await _repository.GetCurriculumId(email);

            var existingHabilidade = await _repository.GetById(id);

            if (existingHabilidade is null || curriculumId != existingHabilidade.CurriculumId)
                throw new NotFoundInDatabaseException("Objeto não encontrado no banco");

            var result = await _repository.DeleteByItem(existingHabilidade);

            if (result > 0)
                return true;

            else
                throw new SaveFailedException("Não foi possível salvar as alterações no banco");
        }


        public async Task<bool> DeleteAllItems(string email)
        {
            var habilidadeList = await GetAllByEmail(email);

            var result = await _repository.DeleteAllItems(habilidadeList.ToArray());

            if (result < 1)
                throw new SaveFailedException("Não foi possível salvar as alterações no banco");

            else return true;
        }
    }
}