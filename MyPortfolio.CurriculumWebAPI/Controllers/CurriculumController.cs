using AutoMapper;
using CurriculumWebAPI.App.InputModels;
using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumWebAPI.App.Controllers
{
    [ApiController]
    [Route("V1/[controller]")]
    public class CurriculumController : ControllerBase
    {
        private readonly Mapper _mapper;
        private readonly PdfService _pdfServices;
        private readonly CurriculumService _curriculoService;


        public CurriculumController(CurriculumService curriculumService, PdfService pdfServices ,Mapper mapper)
        {
            _mapper = mapper;
            _pdfServices = pdfServices;
            _curriculoService = curriculumService;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> AddCurriculum(CurriculumInputModel curriculum)
        {
            var mapped = _mapper.Map<Curriculum>(curriculum);

            var result = await _curriculoService.Save(mapped);

            if(result)
                return Ok();

            else
                return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpGet("owner")]
        public async Task<string> GetOwner()
        {
            return await  _pdfServices.GetPauloCurriculumPdf();
        }
    }
}