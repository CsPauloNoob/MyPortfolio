using CurriculumWebAPI.Domain.Interfaces;
using CurriculumWebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<string> GetPauloCurriculumPdf()
        {
            var curriculum = await _repository.GetById("1");

            var pdfFile = await _pdfGenerator.Create(curriculum);

            return pdfFile;
        }
    }
}