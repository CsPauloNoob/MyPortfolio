using AutoMapper;
using CurriculumWebAPI.App.Extensions;
using CurriculumWebAPI.App.InputModels;
using CurriculumWebAPI.App.ViewModels;
using CurriculumWebAPI.Domain.Models.CurriculumBody;
using CurriculumWebAPI.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CurriculumWebAPI.App.Controllers
{
    [Route("V1/api/[controller]")]
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
        

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        //CORRIGIR FORMACAO PARA ESTE MODELO

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

        // PUT api/<HabilidadesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<HabilidadesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
