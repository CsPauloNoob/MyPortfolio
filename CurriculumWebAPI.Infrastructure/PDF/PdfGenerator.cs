using CurriculumWebAPI.Domain.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurriculumWebAPI.Domain.Interfaces;
using iTextSharp.text.pdf.parser;

namespace CurriculumWebAPI.Infrastructure.PDF
{
    public class PdfGenerator : IPdfGenerator
    {
        public async Task<string> Create(Curriculum curriculum)
        {
            
            string pdfName = Guid.NewGuid().ToString()+".pdf";
            string pdfFile = DirectoryManager.PdfPath(pdfName);

            Document doc = new Document();

            // Criar um escritor de PDF
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(pdfFile, FileMode.Create));

            // Abrir o documento para escrita
            doc.Open();

            CurriculumTop(doc, curriculum);

            /*
            // Adicionar linhas personalizadas
            PdfContentByte contentByte = writer.DirectContent;
            contentByte.SetLineWidth(1); // Espessura da linha

            contentByte.MoveTo(50, 700); // Posição inicial da linha
            contentByte.LineTo(550, 700); // Posição final da linha
            contentByte.Stroke(); // Desenhar a linha

            // Fechar o documento*/
            doc.Close();

            return pdfFile;
        }

        void CurriculumTop(Document doc, Curriculum curriculum)
        {
            //Fontes
            Font nameFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24);
            Font secondTitle = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
            Font infoFontUnderLine = FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.UNDERLINE);
            Font infoFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);


            doc.Add(new Paragraph(curriculum.Nome, nameFont));
            doc.Add(new Paragraph("Desenvolvedor Back-End Web .NET", infoFontUnderLine));

            doc.Add(new Paragraph("\n"));

            doc.Add(new Paragraph("Contato", secondTitle));
            doc.Add(new Paragraph(curriculum.Email, infoFont));
            
        }
    }
}