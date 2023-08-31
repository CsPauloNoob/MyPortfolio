using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Domain.Interfaces;
using iText.Kernel;
using System;
using iText.IO.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Font;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Font.Constants;
using iText.Layout.Properties;

namespace CurriculumWebAPI.Infrastructure.PDF
{
    public class PdfGenerator : IPdfGenerator
    {

        private string PdfFolderPath = 
            Path.Combine(Environment.CurrentDirectory, "PDFs");



        public async Task<string> Generate(Curriculum curriculum)
        {
            CreatePdfFolder();
            var filePath = PathCombiner(Guid.NewGuid().ToString(), ".pdf");
            File.Create(filePath).Close();

            PdfWriter writer = new PdfWriter(filePath);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            CreateHeader(curriculum, document);

            return filePath;
        }

        void CreateHeader(Curriculum curriculum, Document doc)
        {
            doc.SetTextAlignment(TextAlignment.CENTER);
            
            doc.SetUnderline();
            doc.Add(new Paragraph("Paulin").SetFont(
                PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN)).SetFontSize(20));
            doc.Add(new Paragraph("Celola").SetMarginLeft(35));
            doc.Close();
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