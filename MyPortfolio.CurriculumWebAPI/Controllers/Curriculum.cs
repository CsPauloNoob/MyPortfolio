using CurriculumWebAPI.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.CurriculumWebAPI.Models;

namespace MyPortfolio.CurriculumWebAPI.Controllers
{
    [ApiController]
    [Route("V1/[controller]")]
    public class Curriculum : ControllerBase
    {
        private readonly CurriculumService _curriculoService;


        public Curriculum(CurriculumService curriculumService)
        {
            _curriculoService = curriculumService;
        }

        [HttpGet]
        public Task<Models.Curriculum> GetCurriculumById(int id)
        {
            _curriculoService.
            return 
        }
    }
}