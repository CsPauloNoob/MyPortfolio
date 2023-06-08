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

        public Curriculum GetById(int id)
        {
            var result = _repository.GetById(id);

            return result;
        }


        public bool Save(Curriculum curriculum)
        {
            if(curriculum is null)
                return false;

            var result = _repository.AddNew(curriculum);
            
            if(result)
                return true;

            return false;
        }
    }
}