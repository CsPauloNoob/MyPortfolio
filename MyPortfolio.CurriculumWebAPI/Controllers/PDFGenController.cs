using AutoMapper;
using CurriculumWebAPI.App.Extensions;
using CurriculumWebAPI.Domain.Exceptions;
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

        public PDFGenController(PdfService pdfService)
        {
            _pdfService = pdfService;
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

    }
}
