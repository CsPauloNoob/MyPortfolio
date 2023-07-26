using AutoMapper;
using CurriculumWebAPI.App.InputModels;
using CurriculumWebAPI.App.ViewModels;
using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Domain.Models.CurriculumBody;
using CurriculumWebAPI.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace CurriculumWebAPI.App.Controllers
{
    [ApiController]
    [Route("V1/[controller]")]
    public class CurriculumController : ControllerBase
    {
        private readonly Mapper _mapper;
        private readonly PdfService _pdfServices;
        private readonly CurriculumService _curriculoService;
        private readonly UserService _userService;


        public CurriculumController(CurriculumService curriculumService, 
            PdfService pdfServices ,Mapper mapper, UserService userService)
        {
            _mapper = mapper;
            _pdfServices = pdfServices;
            _curriculoService = curriculumService;
            _userService = userService;
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<CurriculumViewModel> Get()
        {
            var claims = User.Claims.ToList();

            var email = claims.FirstOrDefault(c => c.Type.Contains("email"))?.Value;
            var curriculum = await _curriculoService.GetByEmail(email);

            return _mapper.Map<CurriculumViewModel>(curriculum);
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> Post(CurriculumInputModel curriculum)
        {
            var claims = User.Claims.ToList();

            var email = claims.FirstOrDefault(c => c.Type.Contains("email"))?.Value;
            var user = await _userService.GetUserByEmail(email);

            var mapped = _mapper.Map<Curriculum>(curriculum);

            var curriculoResult = await _curriculoService.Save(mapped, email);
            
            if(curriculoResult is not null)
            {
                user.Curriculum = curriculoResult;

                var updateResult = await _userService.UpdateUser(user);

                if (updateResult)
                    return CreatedAtAction(nameof(Post), "Concluido com sucesso!");
            }


             return BadRequest("Erro ao salvar curriculo");
        }

        // -----> Resolver problemas
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("newcontato")]
        public async Task<IActionResult> NewContato(ContatoInputModel contatoInputModel)
        {
            if(ModelState.IsValid)
            {
                var claims = User.Claims.ToList();
                var email = claims.FirstOrDefault(c => c.Type.Contains("email"))?.Value;

                var user = await _userService.GetUserByEmail(email);

                if (user.Curriculum is null)
                    return BadRequest("O usuário não tem um curriculo cadastrado!");

                var contato = _mapper.Map<Contato>(contatoInputModel);

                contato.CurriculumId = user.Curriculum.Id;

                if (await _curriculoService.AddContato(contato))
                    return CreatedAtAction(nameof(_curriculoService.AddContato), new {id = user.Id});
            }

            return BadRequest("ModelState inválido!");
        }



        [AllowAnonymous]
        [HttpGet("owner")]
        public async Task<string> GetOwner()
        {
            return await  _pdfServices.GetPauloCurriculumPdf();
        }
    }
}