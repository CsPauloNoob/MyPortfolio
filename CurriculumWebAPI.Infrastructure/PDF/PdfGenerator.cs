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
using CurriculumWebAPI.Domain.Models.ComplexTypes;
using CurriculumWebAPI.Domain.Models.CurriculumBody;
using iText.Layout.Borders;
using iText.Kernel.Colors;
using iText.Layout.Font;

namespace CurriculumWebAPI.Infrastructure.PDF
{
    public class PdfGenerator : IPdfGenerator
    {

        private string PdfFolderPath =
            Path.Combine(Environment.CurrentDirectory, "PDFs");

        private const string DefaultCurriculumFont = StandardFonts.TIMES_ROMAN;
        private const float LineSpacing = 0.6f;


        public async Task<string> Generate(Curriculum curriculum)
        {
            CreatePdfFolder();
            var filePath = PathCombiner(Guid.NewGuid().ToString(), ".pdf");
            File.Create(filePath).Close();

            PdfWriter writer = new PdfWriter(filePath);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            WriteCurriculumPDF(curriculum, document);

            return filePath;
        }






        void WriteCurriculumPDF(Curriculum curriculum, Document doc)
        {
            //Adiciona o nome
            doc.Add(TitleFormatter(curriculum.Nome,
                24,textAlignment: TextAlignment.CENTER).SetUnderline());

            doc.Add(ContatoAddresFormatter(curriculum));
            doc.Add(ContatoEmailFormatter(curriculum.Contato.Email));
            doc.Add(ContatoPhoneFormatter(curriculum.Contato.Telefone));
            doc.Add(PerilProgrammerFormatter(curriculum.PerfilProgramador));

            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph(" "));

            //Adiciona todo o cabeçalho
            doc.Add(TitleFormatter("Sobre mim", 16,
                textAlignment: TextAlignment.CENTER));

            doc.Add(SobreMimFormatter(curriculum.SobreMim));

            //adiciona sessão Habilidades
            if (curriculum.Habilidade is not null)
            {
                doc.Add(TitleFormatter("Habilidades", 16,
                    textAlignment: TextAlignment.CENTER));

                doc.Add(HabilidadesFormatter(curriculum.Habilidade));
            }

            doc.Close();
        }

        #region Header Formatter
        Paragraph TitleFormatter(string content, float fontSize,
            string font = DefaultCurriculumFont,
            TextAlignment textAlignment = TextAlignment.JUSTIFIED)

        {
            var paragraph = new Paragraph(content)
                .SetFontSize(fontSize)
                .SetFont(PdfFontFactory.CreateFont(font))
                .SetTextAlignment(textAlignment);

            return paragraph;
        }


        Paragraph ContatoAddresFormatter(Curriculum curriculum)
        {
            var paragraph = new Paragraph();
            paragraph.SetMultipliedLeading(LineSpacing);

            // Concatenação das propriedades do endereço
            var enderecoConcatenado = curriculum.Contato.Endereco.Rua +
                ", " + curriculum.Contato.Endereco.NumeroCasa + " - " +
                curriculum.Contato.Endereco.Bairro + " - " +
                curriculum.Contato.Endereco.Cidade + "-" +
                curriculum.Contato.Endereco.Estado;

            var endereconContent = new Text(enderecoConcatenado)
                .SetFont(PdfFontFactory.CreateFont(DefaultCurriculumFont))
                .SetFontSize(10);

            var enderecoText = new Text("Endereco: ")
                .SetFont(PdfFontFactory.CreateFont(DefaultCurriculumFont))
                .SetBold()
                .SetFontSize(10);

            paragraph.Add(enderecoText).SetMarginLeft(40);
            paragraph.Add(endereconContent);

            return paragraph;
        }

        //Teste

        Paragraph ContatoEmailFormatter(string email)
        {
            var paragraph = new Paragraph();
            paragraph.SetMultipliedLeading(LineSpacing);

            var emailContent = new Text(email)
                .SetFont(PdfFontFactory.CreateFont(DefaultCurriculumFont))
                .SetFontSize(10);

            var emailText = new Text("Email: ")
                .SetFont(PdfFontFactory.CreateFont(DefaultCurriculumFont))
                .SetBold()
                .SetFontSize(10);


            paragraph.Add(emailText).SetMarginLeft(40);
            paragraph.Add(emailContent);

            return paragraph;
        }


        Paragraph ContatoPhoneFormatter(PhoneNumber phone)
        {
            var paragraph = new Paragraph();
            paragraph.SetMultipliedLeading(LineSpacing);

            var telefoneContent = new Text(phone.ToString())
                .SetFont(PdfFontFactory.CreateFont(DefaultCurriculumFont))
                .SetFontSize(10);

            var telefoneText = new Text("Telefone: ")
                .SetFont(PdfFontFactory.CreateFont(DefaultCurriculumFont))
                .SetBold()
                .SetFontSize(10);

            paragraph.Add(telefoneText).SetMarginLeft(40);
            paragraph.Add(telefoneContent);

            return paragraph;
        }


        Paragraph PerilProgrammerFormatter(string perfilProgramador)
        {
            var paragraph = new Paragraph();
            paragraph.SetMultipliedLeading(LineSpacing);

            var perfilContent = new Text(perfilProgramador)
                .SetFont(PdfFontFactory.CreateFont(DefaultCurriculumFont))
                .SetFontSize(10);

            var perfilText = new Text("LinkedIn: ")
                .SetFont(PdfFontFactory.CreateFont(DefaultCurriculumFont))
                .SetFontSize(10)
                .SetBold();

            paragraph.Add(perfilText).SetMarginLeft(40);
            paragraph.Add(perfilContent);

            return paragraph;
        }

        #endregion

        Paragraph SobreMimFormatter(string sobreMim)
        {
            var paragraph = new Paragraph();
            paragraph.SetMultipliedLeading(LineSpacing);

            var sobreMimContent = new Text(sobreMim)
                .SetFont(PdfFontFactory.CreateFont(DefaultCurriculumFont))
                .SetFontSize(10);

            paragraph.Add(sobreMimContent)
                .SetFixedLeading(12f);

            return paragraph;
        }


        Table HabilidadesFormatter(List<Habilidades> habilidades)
        {
            Table table = new Table(UnitValue.CreatePercentArray(new float[] {10, 40}));
            table.SetMarginLeft(40);

            foreach (var item in habilidades)
            {
                Cell cellNome = InstanceCellhabilidadeNome(item.Nome_Habilidade);
                Cell cellDesc = InstanceCellHabilidadeDesc(item.Descricao, item.Nome_Habilidade.Length);

                table.AddCell(cellNome);
                table.AddCell(cellDesc);
            }

            return table;
        }


        Cell InstanceCellhabilidadeNome(string content)
        {
            var cell = new Cell();

            cell.Add(new Paragraph(content)
                    .SetFont(PdfFontFactory.CreateFont(DefaultCurriculumFont))
                    .SetFontSize(10)
                    .SetItalic()
                    .SetTextAlignment(TextAlignment.RIGHT))
                    .SetBorder(new DashedBorder(ColorConstants.WHITE, 1));

            return cell;
        }

        Cell InstanceCellHabilidadeDesc(string content, int Nomelength)
        {
            var cell = new Cell();

            if(Nomelength > 12)
            cell.Add(new Paragraph(content)
                    .SetMarginLeft(8)
                    .SetFont(PdfFontFactory.CreateFont(DefaultCurriculumFont))
                    .SetMarginTop(5)
                    .SetFontSize(10))
                    .SetBorder(new DashedBorder(ColorConstants.WHITE, 1));

            else
                cell.Add(new Paragraph(content)
                    .SetMarginLeft(8)
                    .SetFont(PdfFontFactory.CreateFont(DefaultCurriculumFont))
                    .SetFontSize(10))
                    .SetBorder(new DashedBorder(ColorConstants.WHITE, 1));

            return cell;
        }


        void CreatePdfFolder()
        {
            if (!Directory.Exists(PdfFolderPath))
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
            var path = Path.Combine(PdfFolderPath, filePath + extension);

            return path;
        }
    }
}