using AutoMapper;
using CurriculumWebAPI.App.Extensions;
using CurriculumWebAPI.App.InputModels;
using CurriculumWebAPI.App.ViewModels;
using CurriculumWebAPI.Domain.Exceptions;
using CurriculumWebAPI.Domain.Models.CurriculumBody;
using CurriculumWebAPI.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

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
        public async Task<IActionResult> Post([FromBody] FormacaoInputModel[] formacaoInputModel)
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



        /// <summary>
        /// Retorna uma lista com todos as Formações no banco associados ao usuário
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<ActionResult<List<FormacaoViewModel>>> Get()
        {
            try
            {
                var email = await this.GetEmailFromUser();

                var formacao = await _formacaoService.GetAllByEmail(email);

                List<FormacaoViewModel> formacaoViewModel = new List<FormacaoViewModel>();

                foreach(var item in formacao)
                {
                    formacaoViewModel.Add(_mapper.Map<FormacaoViewModel>(item));
                }

                return Ok(formacaoViewModel);
            }

            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}