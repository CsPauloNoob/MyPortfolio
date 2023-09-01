using AutoMapper;
using CurriculumWebAPI.App.Extensions;
using CurriculumWebAPI.App.InputModels;
using CurriculumWebAPI.App.Utitlities;
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
    [Route("V1/api/curriculum")]
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
        public async Task<IActionResult> HeaderGet()
        {
            try
            {
                var email = await this.GetEmailFromUser();
                var curriculum = await _curriculoService.GetHeaderByEmail(email);

                return Ok(_mapper.Map<CurriculumHeaderVM>(curriculum));
            }

            catch (NotFoundInDatabaseException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> HeaderPost(CurriculumHeaderIM curriculum)
        {

            var email = await this.GetEmailFromUser();

            try
            {
                var mapped = _mapper.Map<Curriculum>(curriculum);

                var curriculoResult = await _curriculoService.HeaderSave(mapped, email);
                //Recupera user para vincular curriculo novo
                var user = await _userService.GetUserByEmail(email);

                user.Curriculum = curriculoResult;

                //Dá update no user com curriculo
                var updateResult = await _userService.UpdateUser(user);

                if (updateResult)
                    return CreatedAtAction(nameof(HeaderPost), "Curriculum criado com sucesso!");
            }

            catch (NotFoundInDatabaseException ex)
            {
                return NotFound(ex.Message);
            }

            return BadRequest("Erro ao salvar curriculo");
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut()]
        public async Task<IActionResult> HeaderPut(CurriculumHeaderIM curriculum)
        {
            try
            {
                var email = await this.GetEmailFromUser();

                var result = await _curriculoService.HeaderUpdate(
                    _mapper.Map<Curriculum>(curriculum), email);

                if (result)
                    return CreatedAtAction(nameof(HeaderPut), curriculum);

                return BadRequest();
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


        #region Contato Controllers

        // -----> Resolver problemas
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("contato")]
        public async Task<IActionResult> NewContato(ContatoInputModel contatoInputModel)
        {

            try
            {
                var email = await this.GetEmailFromUser();
                var contatoIfExists = await _curriculoService.
                    GetContatoFromCurriculumByEmail(email, false);

                if (contatoIfExists is not null)
                    return BadRequest("Não é possível sobrescrever, " +
                        "para modificar use o endpoint de PUT");

                var user = await _userService.GetUserByEmail(email);

                if (user.Curriculum is null)
                    return BadRequest("O usuário não tem um curriculo cadastrado!");

                var contato = _mapper.Map<Contato>(contatoInputModel);

                //Seta Id do curriculo para relacionar com contato
                contato.CurriculumId = user.Curriculum.Id;

                //Salva no banco e retorna bool para validação
                if (await _curriculoService.AddContato(contato))

                    return CreatedAtAction(nameof(
                        _curriculoService.AddContato), contatoInputModel);

                return BadRequest();
            }

            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        
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

            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("contato/update")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> PutContato([FromBody] ContatoInputModel contatoInputModel)
        {
            var email = await this.GetEmailFromUser();

            try
            {
                var contato = await _curriculoService.GetContatoFromCurriculumByEmail(email);

                //Atualiza todos os campos fornecidos pelo controller
                contato.Email = contatoInputModel.Email;
                contato.Telefone = ObjectUpdater.GetPhoneNumberFromInputModel(contatoInputModel);
                contato.Endereco = ObjectUpdater.GetAddressFromInputModel(contatoInputModel);

                contato = await _curriculoService.UpdateContato(contato);



                return Ok(new { contato.Email, contato.Telefone, contato.Endereco });
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

        #endregion



        [AllowAnonymous]
        [HttpGet("owner")]
        public async Task<string> GetOwner()
        {
            return await _pdfServices.GetPauloCurriculumPdf();
        }
    }
}