using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Domain.Interfaces;
using System;
using iText.IO.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Font;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Font.Constants;
using iText.Layout.Properties;
using System.Reflection.Metadata.Ecma335;

namespace CurriculumWebAPI.Infrastructure.PDF
{
    public class PdfGenerator : IPdfGenerator
    {

        private string PdfFolderPath = 
            Path.Combine(Environment.CurrentDirectory, "PDFs");

        private const string DefaultCurriculumFont = StandardFonts.TIMES_ROMAN;


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

            doc.Add(ParagraphFactory(curriculum.Nome,
                18, textAlignment: TextAlignment.CENTER).SetUnderline());

            // Adiciona o parágrafo ao documento
            doc.Add(ContatoFormatter(curriculum));

            /*
            doc.Add(ParagraphFactory(curriculum.Nome,
                18, textAlignment : TextAlignment.CENTER).SetUnderline());

            doc.Add(ParagraphFactory (new Text("Endereco: ")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD))+
                curriculum.Contato.Endereco.Rua + ", " +
                curriculum.Contato.Endereco.NumeroCasa + " - " +
                curriculum.Contato.Endereco.Bairro + " - " +
                curriculum.Contato.Endereco.Cidade + "-" +
                curriculum.Contato.Endereco.Estado, 11));

            doc.Add(ParagraphFactory(new Text("Email: ")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD))
                + curriculum.Contato.Email, 11));
            */


            doc.Close();
        }



        Paragraph ParagraphFactory(string content, float fontSize,
            string font = DefaultCurriculumFont, 
            TextAlignment textAlignment = TextAlignment.JUSTIFIED)

        {
            var paragraph = new Paragraph(content).SetFontSize(fontSize)
                .SetFont(PdfFontFactory.CreateFont(font)).SetTextAlignment(textAlignment);

            return paragraph;
        }


        Paragraph ContatoFormatter(Curriculum curriculum)
        {
            var paragraph = new Paragraph();

            var enderecoText = new Text("Endereco: ")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD));
            paragraph.Add(enderecoText);

            // Concatenação das propriedades do endereço
            var enderecoConcatenado = curriculum.Contato.Endereco.Rua + ", " +
                curriculum.Contato.Endereco.NumeroCasa + " - " +
                curriculum.Contato.Endereco.Bairro + " - " +
                curriculum.Contato.Endereco.Cidade + "-" +
                curriculum.Contato.Endereco.Estado;

            paragraph.Add(enderecoConcatenado);

            return paragraph;
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