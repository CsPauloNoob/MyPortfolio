using AutoMapper;
using CurriculumWebAPI.App.Extensions;
using CurriculumWebAPI.App.InputModels;
using CurriculumWebAPI.App.ViewModels;
using CurriculumWebAPI.Domain.Exceptions;
using CurriculumWebAPI.Domain.Models.CurriculumBody;
using CurriculumWebAPI.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumWebAPI.App.Controllers
{

    [Route("V1/api/exp-profissional")]
    [ApiController]
    public class ExperienciaProfissionalController : ControllerBase
    {
        private readonly Mapper _mapper;
        private readonly ExperienciaProfissionalService _expProfissionalService;


        public ExperienciaProfissionalController(Mapper mapper, 
            ExperienciaProfissionalService expProfissional)
        {
            _mapper = mapper;
            _expProfissionalService = expProfissional;
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<ActionResult> Post(ExpProfissionalInputModel[] cursosExtraInputModel)
        {
            try
            {
                var email = await this.GetEmailFromUser();
                var result = await _expProfissionalService.AddExpProfissional(
                    _mapper.Map<Experiencia_Profissional[]>(cursosExtraInputModel), email);

                if (result)
                    return CreatedAtAction(nameof(Post), cursosExtraInputModel);

                else return StatusCode(500, "Erro desconhecido");
            }

            catch (NotFoundInDatabaseException ex)
            {
                return NotFound(ex.Message);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<ActionResult<List<ExpProfissionalViewModel>>> GetAll()
        {
            try
            {
                string email = await this.GetEmailFromUser();

                var expProfissional = _mapper.Map<List<ExpProfissionalViewModel>>(
                    await _expProfissionalService.GetAllByEmail(email));

                return Ok(expProfissional);
            }

            catch (NotFoundInDatabaseException ex)
            {
                return NotFound(ex.Message);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpProfissionalViewModel>> GetById(int id)
        {
            try
            {
                string email = await this.GetEmailFromUser();

                var expProfissional = _mapper.Map<ExpProfissionalViewModel>(
                    await _expProfissionalService.GetById(id, email));

                return Ok(expProfissional);
            }

            catch (NotFoundInDatabaseException ex)
            {
                return NotFound(ex.Message);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ExpProfissionalViewModel>> Put
            (int id, ExpProfissionalInputModel cursosExtraInputModel)
        {
            try
            {
                var email = await this.GetEmailFromUser();

                var result = await _expProfissionalService.UpdateById
                    (id, email, _mapper.Map<Experiencia_Profissional>
                    (cursosExtraInputModel));

                if (result)
                    return CreatedAtAction(nameof(Put), cursosExtraInputModel);

                else return BadRequest();

            }

            catch (NotFoundInDatabaseException ex)
            {
                return NotFound(ex.Message);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete]
        public async Task<ActionResult> DeleteAll()
        {
            try
            {
                string email = await this.GetEmailFromUser();

                var result = await _expProfissionalService.DeleteAllItems(email);

                if (result)
                    return NoContent();

                else return BadRequest();
            }

            catch (NotFoundInDatabaseException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            try
            {
                string email = await this.GetEmailFromUser();

                var result = await _expProfissionalService.DeleteById(id, email);

                if (result)
                    return NoContent();

                else
                    return BadRequest();
            }

            catch (NotFoundInDatabaseException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
