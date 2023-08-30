using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Domain.Interfaces;
using iText.Kernel;
using System;

namespace CurriculumWebAPI.Infrastructure.PDF
{
    public class PdfGenerator : IPdfGenerator
    {

        private string PdfFolderPath = 
            Path.Combine(Environment.CurrentDirectory, "PDFs");



        public async Task<string> Generate(Curriculum curriculum)
        {
            CreatePdfFolder();
            
            CreateHeader(curriculum);

            var filePath = PathCombiner(Guid.NewGuid().ToString(), ".pdf");

            return filePath;
        }


        void CreateHeader(Curriculum curriculum)
        {
        }





        void CreatePdfFolder()
        {
            if(!Directory.Exists(PdfFolderPath))
            {
                Directory.CreateDirectory(PdfFolderPath);
            }
        }

        string PathCombiner(string filePath)
        {
            var path = Path.Combine(PdfFolderPath, filePath);

            return path;
        }

        string PathCombiner(string filePath, string extension)
        {
            var path = Path.Combine(PdfFolderPath, filePath+extension);

            return path;
        }
    }
}