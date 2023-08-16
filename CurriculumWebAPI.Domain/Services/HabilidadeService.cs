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
    }
}