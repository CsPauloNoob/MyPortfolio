using AutoMapper;
using CurriculumWebAPI.App.Extensions;
using CurriculumWebAPI.App.InputModels;
using CurriculumWebAPI.Domain.Exceptions;
using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Domain.Models.CurriculumBody;
using CurriculumWebAPI.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace CurriculumWebAPI.App.Controllers
{
    [ApiController]
    [Route("V1/api/pdfgen")]
    public class PDFGenController : ControllerBase
    {
        private readonly PdfService _pdfService;
        private readonly Mapper _mapper;

        public PDFGenController(PdfService pdfService, Mapper mapper)
        {
            _pdfService = pdfService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<HttpResponseMessage> Get()
        {
            var email = await this.GetEmailFromUser();

            try
            {
                byte[] pdfBytes = await _pdfService.CreatePdf(email);

                HttpResponseMessage response = new HttpResponseMessage();
                response.Content = new ByteArrayContent(pdfBytes);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = email + ".pdf"
                };

                return response;
            }

            catch(NotFoundInDatabaseException ex)
            {
                return new HttpResponseMessage() 
                { StatusCode = System.Net.HttpStatusCode.NotFound, ReasonPhrase = ex.Message };

            }

            catch (Exception ex)
            {
                return new HttpResponseMessage() 
                { StatusCode = System.Net.HttpStatusCode.InternalServerError, ReasonPhrase = ex.Message };
            }

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PostCurriculumInformation(CompleteCurriculumIM completeCurriculum)
        {
            #region teste

            /*completeCurriculum.Header.Nome = "Capô de Fusca da Silva";
            completeCurriculum.Header.PerfilProgramador = "Com certeza";
            completeCurriculum.Header.SobreMim = "Várias coisas";

            var contato = new ContatoInputModel
            {
                Email = "exemplo@dominio.com",
                LinkedIn = "https://www.linkedin.com/in/exemplo",
                Rua = "Rua das Flores",
                Bairro = "Jardim das Rosas",
                NumeroCasa = "123",
                Cidade = "São Paulo",
                Estado = "SP",
                Codigo = "55",  // Código do Brasil para telefone
                DDD = "11",
                NumeroTelefone_Celular = "987654321"
            };

            completeCurriculum.Cursos_Extra = new List<CursosExtraInputModel?>()
            {
                new CursosExtraInputModel()
                {
                    Nome_Curso = "Curso de finanças II",
                    Organizacao = "UDEMY CO. COPYRIGHT"
                },

                new CursosExtraInputModel()
                {
                    Nome_Curso = "Curso de finanças",
                    Organizacao = "Tabajara"
                }

            };

            completeCurriculum.Formacao = new List<FormacaoInputModel?>()
            {
                new FormacaoInputModel
                {
                    Instituicao = "Instituição A",
                    Curso = "Curso A",
                    AnoConclusao = "2020"
                },
                new FormacaoInputModel
                {
                    Instituicao = "Instituição B",
                    Curso = "Curso B",
                    AnoConclusao = "2021"
                }
            };

            completeCurriculum.ExpProfissional = new List<ExpProfissionalInputModel?>()
            {
                new ExpProfissionalInputModel
                {
                    Nome_Organizacao = "Organização A",
                    Funcao = "Desenvolvedor",
                    Descricao = "Trabalhei no desenvolvimento de aplicações web usando .NET e Angular."
                },
                new ExpProfissionalInputModel
                {
                    Nome_Organizacao = "Organização B",
                    Funcao = "Analista de Sistemas",
                    Descricao = "Fui responsável pela análise de sistemas e suporte técnico, além de liderar um time de desenvolvedores."
                }
            };

            completeCurriculum.Habilidades = new List<HabilidadeInputModel>()
            {
                new HabilidadeInputModel
                {
                    Nome_Habilidade = "Programação em C#",
                    Descricao = "Experiência avançada em desenvolvimento de aplicações com C# e .NET."
                },
                new HabilidadeInputModel
                {
                    Nome_Habilidade = "Desenvolvimento Web",
                    Descricao = "Conhecimento em desenvolvimento web com ASP.NET Core e Blazor."
                }
            };

            

            var curriculum = _mapper.Map<Curriculum>(completeCurriculum);

            //curriculum.Contato = _mapper.Map<Contato>(contato);*/

            #endregion

            await _pdfService.TestPdfServices(_mapper.Map<Curriculum>(completeCurriculum));

            return Ok();
        }

        [HttpGet("owner")]
        public async Task<HttpResponseMessage> GetOwnerPDF()
        {
            try
            {
                byte[] pdfBytes = await _pdfService.GetPauloCurriculumPdf();

                HttpResponseMessage response = new HttpResponseMessage();
                response.Content = new ByteArrayContent(pdfBytes);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Paulo.pdf"
                };

                return response;
            }

            catch (NotFoundInDatabaseException ex)
            {
                return new HttpResponseMessage()
                { StatusCode = System.Net.HttpStatusCode.NotFound, ReasonPhrase = ex.Message };

            }

            catch (Exception ex)
            {
                return new HttpResponseMessage()
                { StatusCode = System.Net.HttpStatusCode.InternalServerError, ReasonPhrase = ex.Message };
            }
        }

        [HttpGet("test")]
        [AllowAnonymous]
        public async Task<IActionResult> TestEndpoint()
        {
            return Ok();
        }


    }
}
