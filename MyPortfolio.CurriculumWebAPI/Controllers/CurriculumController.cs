using AutoMapper;
using CurriculumWebAPI.App.ViewModel;
using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumWebAPI.App.Controllers
{
    [ApiController]
    [Route("V1/[controller]")]
    public class CurriculumController : ControllerBase
    {
        private readonly Mapper _mapper;
        private readonly CurriculumService _curriculoService;


        public CurriculumController(CurriculumService curriculumService, Mapper mapper)
        {
            _mapper = mapper;
            _curriculoService = curriculumService;
        }

        [HttpGet]
        public async Task<bool> GetCurriculumById(int id)
        {
            _curriculoService.GetById(id);



            return true;
        }

        [HttpPost]
        public async Task<IActionResult> AddCurriculum(CurriculumInputModel curriculum)
        {
            var mapped = _mapper.Map<Curriculum>(curriculum);

            var result = _curriculoService.Save(mapped);

            if(result)
                return Ok();

            else
                return BadRequest(result);
        }

    }
}