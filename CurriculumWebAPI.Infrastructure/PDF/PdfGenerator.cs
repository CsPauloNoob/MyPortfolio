using CurriculumWebAPI.Domain.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurriculumWebAPI.Domain.Interfaces;

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

            // Adicionar um parágrafo com estilo
            Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
            Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            Font underlinedFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.UNDERLINE);

            doc.Add(new Paragraph("Texto normal", normalFont));
            doc.Add(new Paragraph("Texto em negrito", boldFont));
            doc.Add(new Paragraph("Texto sublinhado", underlinedFont));

            // Adicionar linhas personalizadas
            PdfContentByte contentByte = writer.DirectContent;
            contentByte.SetLineWidth(1); // Espessura da linha

            contentByte.MoveTo(50, 700); // Posição inicial da linha
            contentByte.LineTo(550, 700); // Posição final da linha
            contentByte.Stroke(); // Desenhar a linha

            // Fechar o documento
            doc.Close();

            return pdfFile;
        }
    }
}