using AutoMapper;
using CurriculumWebAPI.App.InputModels;
using CurriculumWebAPI.Domain.Models.CurriculumBody;
using CurriculumWebAPI.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurriculumWebAPI.App.Controllers
{
    [ApiController]
    [Route("V1/[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly Mapper _mapper;
        private readonly ContatoService _contatoService;

        public ContatoController(Mapper mapper, ContatoService contatoService)
        {
            _mapper = mapper;
            _contatoService = contatoService;
        }

        [HttpPost]
        public async Task<IActionResult> AddContato(ContatoInputModel contatoInputModel)
        {
            if (ModelState.IsValid)
            {
                var contato = _mapper.Map<Contato>(contatoInputModel);

                if (await _contatoService.AddContato(contato))
                    return CreatedAtAction(nameof(AddContato), contato);


                else return BadRequest();
            }

            else return BadRequest("Model State Error!");

        }
    }
}
