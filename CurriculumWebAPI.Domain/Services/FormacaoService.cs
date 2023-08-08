using CurriculumWebAPI.Domain.Exceptions;
using CurriculumWebAPI.Domain.Interfaces;
using CurriculumWebAPI.Domain.Models.CurriculumBody;

namespace CurriculumWebAPI.Domain.Services
{
    public class FormacaoService
    {
        private readonly IRepositoryForCollections<Formacao> _repository;

        public FormacaoService(IRepositoryForCollections<Formacao> repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddFormacao(Formacao formacao, string curriculumId)
        {

            formacao.CurriculumId = curriculumId;
            var result = await _repository.AddNew(formacao);

            if (result > 0)
                return true;

            return false;
        }

        public async Task<List<Formacao>> GetAllByEmail(string email)
        {
            var curricculumId = await _repository.GetCurriculumId(email);

            var formationList = await _repository.GetAllByCurriculumId(curricculumId);

            if(formationList.Count == 0 )
                throw new NotFoundInDatabaseException("ítens não encontrados no banco de dados");

            return formationList;
        }
    }
}