using CurriculumWebAPI.Domain.Interfaces;
using CurriculumWebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Services
{
    public class CurriculumService
    {
        private readonly IRepository<Curriculum> _repository;

        public CurriculumService(IRepository<Curriculum> repository)
        {
            _repository = repository;
        }

        public async Task<Curriculum> GetById(string id)
        {
            var result = await _repository.GetById(id);

            return result;
        }


        public async Task<bool> Save(Curriculum curriculum)
        {
            return true;
        }
    }
}