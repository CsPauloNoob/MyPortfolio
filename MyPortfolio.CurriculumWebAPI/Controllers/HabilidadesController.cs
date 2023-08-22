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
    [Route("V1/api/habilidade")]
    [ApiController]
    public class HabilidadesController : ControllerBase
    {
        private readonly HabilidadeService _habilidadeService;
        private readonly Mapper _mapper;

        public HabilidadesController(HabilidadeService habilidadeService, Mapper mapper)
        {
            _habilidadeService = habilidadeService;
            _mapper = mapper;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<ActionResult<List<HabilidadeViewModel>>> GetAll()
        {
            try
            {
                string email = await this.GetEmailFromUser();

                var habilidade = _mapper.Map<List<HabilidadeViewModel>>(
                    await _habilidadeService.GetAllByEmail(email));

                return Ok(habilidade);
            }

            catch(NotFoundInDatabaseException ex)
            {
                return NotFound(ex.Message);
            }

            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<HabilidadeViewModel>> Get(int id)
        {
            try
            {
                string email = await this.GetEmailFromUser();

                var habilidadeViewModel = _mapper
                    .Map<HabilidadeViewModel>(await _habilidadeService.GetById(id, email));

                return Ok(habilidadeViewModel);
            }

            catch(NotFoundInDatabaseException ex)
            {
                return NotFound(ex.Message);
            }

            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<ActionResult<HabilidadeViewModel>> Post(HabilidadeInputModel[] habilidadeInputModel)
        {
            try
            {
                var email = await this.GetEmailFromUser();

                var result = await _habilidadeService.CreateHabilidade(
                    _mapper.Map<Habilidades[]>(habilidadeInputModel), email);

                if(result)
                    return CreatedAtAction(nameof(Post), habilidadeInputModel);

                return BadRequest();
            }

            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, HabilidadeInputModel habilidadeInputModel)
        {
            try
            {
                var email = await this.GetEmailFromUser();

                var result = await _habilidadeService.UpdateById
                    (id, email, _mapper.Map<Habilidades>(habilidadeInputModel));

                if(result)
                    return CreatedAtAction(nameof(Put), habilidadeInputModel);

                else return BadRequest();

            }

            catch(NotFoundInDatabaseException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                string email = await this.GetEmailFromUser();

                var result = await _habilidadeService.DeleteById(id, email);

                if(result)
                    return NoContent();

                else
                    return BadRequest();
            }

            catch (NotFoundInDatabaseException ex)
            {
                return BadRequest(ex.Message);
            }

            catch(Exception ex)
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

                var result = await _habilidadeService.DeleteAllItems(email);

                if(result)
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
    }
}