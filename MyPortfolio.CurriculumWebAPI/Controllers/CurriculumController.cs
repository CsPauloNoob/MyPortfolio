using AutoMapper;
using CurriculumWebAPI.App.Extensions;
using CurriculumWebAPI.App.InputModels;
using CurriculumWebAPI.App.ViewModels;
using CurriculumWebAPI.Domain.Exceptions;
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
            PdfService pdfServices, Mapper mapper, UserService userService)
        {
            _mapper = mapper;
            _pdfServices = pdfServices;
            _curriculoService = curriculumService;
            _userService = userService;
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var email = await this.GetEmailFromUser();
                var curriculum = await _curriculoService.GetByEmail(email);

                return Ok(_mapper.Map<CurriculumViewModel>(curriculum));
            }

            catch (NotFoundInDatabaseException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> Post(CurriculumInputModel curriculum)
        {

            var email = await this.GetEmailFromUser();

            try
            {
                var mapped = _mapper.Map<Curriculum>(curriculum);

                var curriculoResult = await _curriculoService.Save(mapped, email);
                //Recupera user para vincular curriculo novo
                var user = await _userService.GetUserByEmail(email);

                user.Curriculum = curriculoResult;

                //Dá update no user com curriculo
                var updateResult = await _userService.UpdateUser(user);

                if (updateResult)
                    return CreatedAtAction(nameof(Post), "Concluido com sucesso!");
            }

            catch (NotFoundInDatabaseException ex)
            {
                return BadRequest(ex.Message);
            }

            return BadRequest("Erro ao salvar curriculo");
        }

        #region Contato Controllers

        // -----> Resolver problemas
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("contato")]
        public async Task<IActionResult> NewContato(ContatoInputModel contatoInputModel)
        {
            if (ModelState.IsValid)
            {
                var claims = User.Claims.ToList();
                var email = claims.FirstOrDefault(c => c.Type.Contains("email"))?.Value;

                var user = await _userService.GetUserByEmail(email);

                if (user.Curriculum is null)
                    return BadRequest("O usuário não tem um curriculo cadastrado!");

                var contato = _mapper.Map<Contato>(contatoInputModel);

                //Seta Id do curriculo para relacionar com contato
                contato.CurriculumId = user.Curriculum.Id;

                //Salva no banco e retorna bool para validação
                if (await _curriculoService.AddContato(contato))
                    return CreatedAtAction(nameof(_curriculoService.AddContato), contatoInputModel);
            }

            return BadRequest("ModelState inválido!");
        }


        // --> Modelo de implementação
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("contato")]
        public async Task<IActionResult> GetContato()
        {
            var email = await this.GetEmailFromUser();

            try
            {
                var contato = await _curriculoService.GetContatoFromCurriculumByEmail(email);

                return Ok(_mapper.Map<ContatoViewModel>(contato));
            }

            catch (NotFoundInDatabaseException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion



        [AllowAnonymous]
        [HttpGet("owner")]
        public async Task<string> GetOwner()
        {
            return await _pdfServices.GetPauloCurriculumPdf();
        }
    }
}