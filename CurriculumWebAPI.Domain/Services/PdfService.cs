using CurriculumWebAPI.Domain.Exceptions;
using CurriculumWebAPI.Domain.Interfaces;
using CurriculumWebAPI.Domain.Models;
using System.Security.Cryptography;

namespace CurriculumWebAPI.Domain.Services
{
    public class PdfService
    {
        private readonly IRepository<Curriculum> _repository;
        private readonly IPdfGenerator _pdfGenerator;
        private const string _personalEmail = "ps616131@gmail.com";

        public PdfService(IRepository<Curriculum>repository,IPdfGenerator pdfGenerator)
        {
            _repository = repository;
            _pdfGenerator = pdfGenerator;
        }



        public async Task<byte[]> CreatePdf(string email)
        {
            var curriculum = await _repository.GetByEmail(email, false);
            var fullCurriculum = await _repository.GetById(curriculum.Id, false);

            if (fullCurriculum is null)
                throw new NotFoundInDatabaseException("Objeto não encontrado na base de dados!");

            var pdfPath = await _pdfGenerator.Generate(fullCurriculum);

            return File.ReadAllBytes(pdfPath);
        }



        public async Task<byte[]> GetPauloCurriculumPdf()
        {
            var pdfPath = Path.Combine(Environment.CurrentDirectory, "PDFs/Paulo.pdf");
            var pdfBytes = await File.ReadAllBytesAsync(pdfPath);

            return pdfBytes;
        }

        public async Task<byte[]> TestPdfServices(Curriculum curriculum)
        {
            var pdf = await _pdfGenerator.Generate(curriculum);

            return await File.ReadAllBytesAsync(pdf);
        }

    }
}