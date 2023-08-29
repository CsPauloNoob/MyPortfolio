using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Domain.Interfaces;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Fonts.Standard14Fonts;
using UglyToad.PdfPig.Writer;

namespace CurriculumWebAPI.Infrastructure.PDF
{
    public class PdfGenerator : IPdfGenerator
    {
        public async Task<string> Create(Curriculum curriculum)
        {
            PdfDocumentBuilder builder = new PdfDocumentBuilder();

            PdfPageBuilder page = builder.AddPage(PageSize.A4);

            // Fonts must be registered with the document builder prior to use to prevent duplication.
            PdfDocumentBuilder.AddedFont font = builder.AddStandard14Font(Standard14Font.TimesRoman);

            page.AddText("Hello World!", 12, new PdfPoint(25, 700), font);

            byte[] documentBytes = builder.Build();

            return null;
        }
    }
}