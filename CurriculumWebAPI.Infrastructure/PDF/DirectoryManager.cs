using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Infrastructure.PDF
{
    public class DirectoryManager
    {
        public static string PdfPath(string fileName)
        {
            var currentDir = Environment.CurrentDirectory;
            var folderPath = Path.Combine(currentDir, "PDFs");

            if (Directory.Exists(folderPath))
            {
                return CreatePdfFile(folderPath, fileName);
            }

            else
            {
                Directory.CreateDirectory(folderPath);
                return CreatePdfFile(folderPath, fileName);
            }
        }

        private static string CreatePdfFile(string folder, string fileName)
        {
            var file = Path.Combine(folder, fileName);

            File.Create(file).Close();

            return file;
        }
    }
}
