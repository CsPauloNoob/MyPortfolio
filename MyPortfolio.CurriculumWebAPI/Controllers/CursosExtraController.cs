﻿using AutoMapper;
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

    [Route("V1/api/cursos_extras")]
    [ApiController]
    public class CursosExtraController : ControllerBase
    {
        private readonly CursosExtrasService _cursosExtrasService;
        private readonly Mapper _mapper;

        public CursosExtraController(CursosExtrasService cursosExtrasService, Mapper mapper)
        {
            _cursosExtrasService = cursosExtrasService;
            _mapper = mapper;
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<ActionResult> Post(CursosExtraInputModel[] cursosExtraInputModel)
        {
            try
            {
                var email = await this.GetEmailFromUser();
                var result = await _cursosExtrasService.AddCursosExtras(
                    _mapper.Map<Cursos_Extras[]>(cursosExtraInputModel), email);

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
        public async Task<ActionResult<List<CursosExtraViewModel>>> GetAll()
        {
            try
            {
                string email = await this.GetEmailFromUser();

                var cursosExtras = _mapper.Map<List<CursosExtraViewModel>>(
                    await _cursosExtrasService.GetAllByEmail(email));

                return Ok(cursosExtras);
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
        public async Task<ActionResult<CursosExtraViewModel>> GetById(int id)
        {
            try
            {
                string email = await this.GetEmailFromUser();

               var cursoExtras = _mapper.Map<CursosExtraViewModel>(
                   await _cursosExtrasService.GetById(id, email));

                return Ok(cursoExtras);
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
        public async Task<ActionResult<CursosExtraViewModel>> Put(int id, CursosExtraInputModel cursosExtraInputModel)
        {
            try
            {
                var email = await this.GetEmailFromUser();

                var result = await _cursosExtrasService.UpdateById
                    (id, email, _mapper.Map<Cursos_Extras>(cursosExtraInputModel));

                if (result)
                    return CreatedAtAction(nameof(Put), cursosExtraInputModel);

                else return BadRequest();

            }

            catch (NotFoundInDatabaseException ex)
            {
                return NotFound(ex.Message);
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

                var result = await _cursosExtrasService.DeleteAllItems(email);

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

                var result = await _cursosExtrasService.DeleteById(id, email);

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