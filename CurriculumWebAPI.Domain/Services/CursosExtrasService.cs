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
    public class CursosExtrasService
    {
        private readonly IRepositoryForCollections<Cursos_Extras> _repository;

        public CursosExtrasService(IRepositoryForCollections<Cursos_Extras> repository)
        {
            _repository = repository; 
        }



        public async Task<bool> AddCursosExtras(Cursos_Extras[] cursosExtras, string email)
        {
            var curriculumId = await _repository.GetCurriculumId(email);
            int result = 0;

            if (curriculumId is null)
                throw new DirectoryNotFoundException("Curriculo não encontrado no banco");

            foreach (var item in cursosExtras)
            {
                item.CurriculumId = curriculumId;
                result += await _repository.AddNew(item);
            }

            if (result == cursosExtras.Count())
                return true;

            return false;
        }


        public async Task<List<Cursos_Extras>> GetAllByEmail(string email)
        {
            var curricculumId = await _repository.GetCurriculumId(email);

            if (curricculumId is null)
                throw new NotFoundInDatabaseException("Id do curriculo não encontrado no banco");

            var cursosExtrasList = await _repository.GetAllByCurriculumId(curricculumId);

            if (cursosExtrasList.Count == 0)
                throw new NotFoundInDatabaseException("ítens não encontrados no banco de dados");

            return cursosExtrasList;
        }

    }
}