using AutoMapper;
using CurriculumWebAPI.App.Extensions;
using CurriculumWebAPI.App.InputModels;
using CurriculumWebAPI.Domain.Exceptions;
using CurriculumWebAPI.Domain.Models.CurriculumBody;
using CurriculumWebAPI.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumWebAPI.App.Controllers
{
    [ApiController]
    [Route("V1/[controller]")]
    public class FormacaoController : ControllerBase
    {
        private readonly FormacaoService _formacaoService;
        private readonly CurriculumService _curriculumService;
        private readonly Mapper _mapper;

        public FormacaoController(FormacaoService formacaoService,
            CurriculumService curriculumService, Mapper mapper)
        {
            _formacaoService = formacaoService;
            _curriculumService = curriculumService;
            _mapper = mapper;
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> AddFormacao([FromBody] FormacaoInputModel[] formacaoInputModel)
        {
            try
            {
                var email = await this.GetEmailFromUser();

                var curriculum = await _curriculumService.GetByEmail(email);

                foreach (var formacao in formacaoInputModel)
                {
                    await _formacaoService.
                        AddFormacao(_mapper.Map<Formacao>(formacao), curriculum.Id);
                }

                return CreatedAtAction(nameof(_formacaoService.AddFormacao), formacaoInputModel);

            }

            catch (NotFoundInDatabaseException ex)
            {
                return NotFound(ex.Message);
            }

            return BadRequest("Não foi possível salvar no banco");
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return null;
        }

    }
}