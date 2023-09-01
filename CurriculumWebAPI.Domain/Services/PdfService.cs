using CurriculumWebAPI.Domain.Exceptions;
using CurriculumWebAPI.Domain.Interfaces;
using CurriculumWebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Domain.Services
{
    public class PdfService
    {
        private readonly IRepository<Curriculum> _repository;
        private readonly IPdfGenerator _pdfGenerator;

        public PdfService(IRepository<Curriculum>repository,IPdfGenerator pdfGenerator)
        {
            _repository = repository;
            _pdfGenerator = pdfGenerator;
        }

        public async Task<byte[]> CreatePdf(string email)
        {
            var curriculumId = await _repository.GetByEmail(email);
            var fullCurriculum = await _repository.GetById(curriculumId.Id);

            if (fullCurriculum is null)
                throw new NotFoundInDatabaseException("Objeto não encontrado na base de dados!");

            var pdfPath = await _pdfGenerator.Generate(fullCurriculum);

            return File.ReadAllBytes(pdfPath);
        }


        public async Task<string> GetPauloCurriculumPdf()
        {
            var curriculum = await _repository.GetById("1");

            var pdfFile = await _pdfGenerator.Generate(curriculum);

            return pdfFile;
        }
    }
}