using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Domain.Interfaces;
using UglyToad.PdfPig;

namespace CurriculumWebAPI.Infrastructure.PDF
{
    public class PdfGenerator : IPdfGenerator
    {
        public async Task<string> Create(Curriculum curriculum)
        {
            
            return "";
        }
    }
}