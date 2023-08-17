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
    [Route("V1/api/[controller]")]
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

                var result = await _formacaoService.AddFormacao(
                    _mapper.Map<Formacao[]>(formacaoInputModel), email);

                if (result)
                    return CreatedAtAction(nameof(Get), formacaoInputModel);

                else return StatusCode(500, "Erro desconhecido");
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
                return NotFound(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public async Task<ActionResult<FormacaoViewModel>> Put(int id, FormacaoInputModel formacaoInputModel)
        {

            try
            {
                var email = await this.GetEmailFromUser();

                var formacao = _mapper.Map<FormacaoViewModel>( 
                    await _formacaoService.UpdateFormacao
                    (id,_mapper.Map<Formacao>(formacaoInputModel), email));

                

                return Ok(formacao);
            }

            catch(NotFoundInDatabaseException ex)
            {
                return NotFound(ex.Message);
            }

            catch(SaveFailedException ex)
            {
                return StatusCode(500,ex.Message);
            }
            
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            try
            {
                await _formacaoService.DeleteFormacao(id);

                return Ok();
            }

            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete]
        public async Task<ActionResult> DeleteAll()
        {
            try
            {
                var email = await this.GetEmailFromUser();

                await _formacaoService.DeleteAll(email);

                return Ok();
            }

            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}