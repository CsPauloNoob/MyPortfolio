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

    }
}