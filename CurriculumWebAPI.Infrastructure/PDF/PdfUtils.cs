using CurriculumWebAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurriculumWebAPI.Infrastructure.PDF
{
    internal class PdfUtils
    {

        static string? DefaultFolder;

        public static string PdfPath(string fileName)
        {
            var currentDir = Environment.CurrentDirectory;
            var path = Path.Combine(currentDir, "PDFs");

            DefaultFolder = path;

            if (Directory.Exists(path))
            {
                return CreatePdfFile(path, fileName);
            }

            else
            {
                Directory.CreateDirectory(path);
                return CreatePdfFile(path, fileName);
            }
        }

        public static string GetHtml(Curriculum curriculum)
        {
            var html = LargeHtmlContent();
            var numeroTelefone = curriculum.Contato.Telefone.ToString();
            var habilidades = string.Empty;
            var formacao = string.Empty;
            var experiencia = string.Empty;
            var cursos = string.Empty;

            html = html.Replace("{{Nome}}", curriculum.Nome);
            html = html.Replace("{{Profissão}}", curriculum.PerfilProgramador);
            html = html.Replace("{{Email}}", curriculum.Contato.Email);
            html = html.Replace("{{Telefone}}", numeroTelefone);

            if (string.IsNullOrEmpty(curriculum.Contato.LinkedIn))
                html = html.Replace("<p>LinkedIn: {{LinkedIn}}</p>", "");

            else
                html = html.Replace("{{LinkedIn}}", curriculum.Contato.LinkedIn);

            html = html.Replace("{{SobreMim}}", curriculum.SobreMim);

            foreach(var h in curriculum.Habilidade)
            {
                habilidades += $"<li><h3>{h.Nome_Habilidade}</h3> {h.Descricao} </li>";
            }

            foreach (var e in curriculum.Experiencia_Profissional)
            {
                experiencia += $"<li><h3>{e.Funcao} - {e.Nome_Organizacao}</h3> {e.Descricao} </li>";
            }

            foreach (var f in curriculum.Formacao)
            {
                formacao += $"<li><h3>{f.Curso} - {f.Instituicao}</h3> ANO DE CONCLUSÃO: {f.AnoConclusao} </li>";
            }

            foreach (var c in curriculum.Cursos)
            {
                cursos += $"<li><h3>{c.Nome_Curso}</h3> {c.Organizacao} </li>";
            }

            html = html.Replace("{{#Skills}}", habilidades);
            html = html.Replace("{{#Experiencias}}", experiencia);
            html = html.Replace("{{#Formacoes}}", formacao);
            html = html.Replace("{{#Cursos}}", cursos);

            return html;
        }

        private static string CreatePdfFile(string folder, string fileName)
        {
            var file = Path.Combine(folder, fileName);

            File.Create(file).Close();

            return file;
        }


        private static string LargeHtmlContent()
        {
            //return "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Currículo</title>\r\n    <style>\r\n        body {\r\n            font-family: Arial, sans-serif;\r\n            margin: 0;\r\n            padding: 0;\r\n            background: #f4f4f4;\r\n        }\r\n        .container {\r\n            width: 800px;\r\n            margin: 0 auto;\r\n            background: #fff;\r\n            padding: 20px;\r\n            border-radius: 8px;\r\n            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);\r\n        }\r\n        h1 {\r\n            font-size: 24px;\r\n            margin: 0;\r\n        }\r\n        h2 {\r\n            font-size: 20px;\r\n            margin: 10px 0;\r\n        }\r\n        .contact-info {\r\n            margin-bottom: 20px;\r\n        }\r\n        .contact-info p {\r\n            margin: 5px 0;\r\n        }\r\n        .section {\r\n            margin-bottom: 20px;\r\n        }\r\n        .section-title {\r\n            font-size: 18px;\r\n            margin-bottom: 10px;\r\n            border-bottom: 2px solid #333;\r\n            padding-bottom: 5px;\r\n        }\r\n        .skills ul,\r\n        .experience ul,\r\n        .education ul,\r\n        .courses ul {\r\n            list-style-type: none;\r\n            padding: 0;\r\n        }\r\n        .skills li,\r\n        .experience li,\r\n        .education li,\r\n        .courses li {\r\n            background: #eaeaea;\r\n            margin-bottom: 5px;\r\n            padding: 10px;\r\n            border-radius: 4px;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"container\">\r\n        <h1>{{Nome}}</h1>\r\n        <h2>{{Profissão}}</h2>\r\n        <div class=\"contact-info\">\r\n            <p>Email: {{Email}}</p>\r\n            <p>Telefone: {{Telefone}}</p>\r\n            <p>LinkedIn: {{LinkedIn}}</p>\r\n        </div>\r\n        <div class=\"section\">\r\n            <div class=\"section-title\">Sobre Mim</div>\r\n            <p>{{SobreMim}}</p>\r\n        </div>\r\n        <div class=\"section\">\r\n            <div class=\"section-title\">Skills</div>\r\n            <ul class=\"skills\">\r\n                {{#Skills}}\r\n            </ul>\r\n        </div>\r\n        <div class=\"section\">\r\n            <div class=\"section-title\">Experiência</div>\r\n            <ul class=\"experience\">\r\n                {{#Experiencias}}\r\n            </ul>\r\n        </div>\r\n        <div class=\"section\">\r\n            <div class=\"section-title\">Formação</div>\r\n            <ul class=\"education\">\r\n                {{#Formacoes}}\r\n            </ul>\r\n        </div>\r\n        <div class=\"section\">\r\n            <div class=\"section-title\">Cursos Extras</div>\r\n            <ul class=\"courses\">\r\n                {{#Cursos}}\r\n            </ul>\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>";
            //return "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Currículo</title>\r\n    <style>\r\n        body {\r\n            font-family: Arial, sans-serif;\r\n            margin: 0;\r\n            padding: 0;\r\n            background: #f4f4f4;\r\n        }\r\n        .container {\r\n            width: 800px;\r\n            margin: 0 auto;\r\n            background: #fff;\r\n            padding: 20px;\r\n            border-radius: 8px;\r\n            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);\r\n        }\r\n        h1 {\r\n            font-size: 24px;\r\n            margin: 0;\r\n        }\r\n        h2 {\r\n            font-size: 20px;\r\n            margin: 10px 0;\r\n        }\r\n        .contact-info {\r\n            margin-bottom: 20px;\r\n        }\r\n        .contact-info p {\r\n            margin: 5px 0;\r\n        }\r\n        .section {\r\n            margin-bottom: 20px;\r\n        }\r\n        .section-title {\r\n            font-size: 18px;\r\n            margin-bottom: 10px;\r\n            border-bottom: 2px solid #333;\r\n            padding-bottom: 5px;\r\n        }\r\n        .skills ul,\r\n        .experience ul,\r\n        .education ul,\r\n        .courses ul {\r\n            list-style-type: none;\r\n            padding: 0;\r\n        }\r\n        .skills li,\r\n        .experience li,\r\n        .education li,\r\n        .courses li {\r\n            background: #eaeaea;\r\n            margin-bottom: 5px;\r\n            padding: 10px;\r\n            border-radius: 4px;\r\n            box-shadow: 0 0 3px rgba(0, 0, 0, 0.05);\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"container\">\r\n        <h1>{{Nome}}</h1>\r\n        <h2>{{Profissão}}</h2>\r\n        <div class=\"contact-info\">\r\n            <p>Email: {{Email}}</p>\r\n            <p>Telefone: {{Telefone}}</p>\r\n            <p>LinkedIn: {{LinkedIn}}</p>\r\n        </div>\r\n        <div class=\"section\">\r\n            <div class=\"section-title\">Sobre Mim</div>\r\n            <p>{{SobreMim}}</p>\r\n        </div>\r\n        <div class=\"section\">\r\n            <div class=\"section-title\">Skills</div>\r\n            <ul class=\"skills\">\r\n                {{#Skills}}\r\n            </ul>\r\n        </div>\r\n        <div class=\"section\">\r\n            <div class=\"section-title\">Experiência</div>\r\n            <ul class=\"experience\">\r\n                {{#Experiencias}}\r\n            </ul>\r\n        </div>\r\n        <div class=\"section\">\r\n            <div class=\"section-title\">Formação</div>\r\n            <ul class=\"education\">\r\n                {{#Formacoes}}\r\n            </ul>\r\n        </div>\r\n        <div class=\"section\">\r\n            <div class=\"section-title\">Cursos Extras</div>\r\n            <ul class=\"courses\">\r\n                {{#Cursos}}\r\n            </ul>\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>";
            return "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Currículo</title>\r\n    <style>\r\n        body {\r\n            font-family: Arial, sans-serif;\r\n            margin: 0;\r\n            padding: 0;\r\n            background: #f4f4f4;\r\n        }\r\n        .container {\r\n            width: 842px;\r\n            margin: 0 auto;\r\n            background: #fff;\r\n            padding: 20px;\r\n            border-radius: 8px;\r\n            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);\r\n        }\r\n        h1 {\r\n            font-size: 42px;\r\n            margin: 0;\r\n        }\r\n        h2 {\r\n            font-size: 24px;\r\n            margin: 5px 0 35px;\r\n            color: grey;\r\n        }\r\n\r\n        h3{\r\n            font-size: 18px;\r\n            margin: 20px 0 5px 0;\r\n        }\r\n        .contact-info {\r\n            margin-bottom: 20px;\r\n        }\r\n        .contact-info p {\r\n            margin: 5px 0;\r\n        }\r\n        .section {\r\n            margin-bottom: 20px;\r\n            text-align: justify;\r\n        }\r\n        .section-title {\r\n            font-size: 20px;\r\n            border-bottom: 1px solid #333;\r\n            \r\n        }\r\n\r\n        ul{\r\n            list-style:disc;\r\n        }\r\n\r\n        li{\r\n            font-size: 14px;\r\n            box-shadow: none;\r\n        }\r\n\r\n\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"container\">\r\n        <h1>{{Nome}}</h1>\r\n        <h2><i>{{Profissão}}</i></h2>\r\n        <div class=\"contact-info\">\r\n            <p>Email: {{Email}}</p>\r\n            <p>Telefone: {{Telefone}}</p>\r\n            <p>LinkedIn: {{LinkedIn}}</p>\r\n        </div>\r\n        <div class=\"section\">\r\n            <div class=\"section-title\">Sobre Mim</div>\r\n            <p>{{SobreMim}}</p>\r\n        </div>\r\n        <div class=\"section\">\r\n            <div class=\"section-title\">Habilidades</div>\r\n            <ul class=\"skills\">\r\n                {{#Skills}}\r\n            </ul>\r\n        </div>\r\n        <div class=\"section\">\r\n            <div class=\"section-title\">Experiência</div>\r\n            <ul class=\"experience\">\r\n                {{#Experiencias}}\r\n            </ul>\r\n        </div>\r\n        <div class=\"section\">\r\n            <div class=\"section-title\">Formação</div>\r\n            <ul class=\"education\">\r\n                {{#Formacoes}}\r\n            </ul>\r\n        </div>\r\n        <div class=\"section\">\r\n            <div class=\"section-title\">Cursos Extras</div>\r\n            <ul class=\"courses\">\r\n                {{#Cursos}}\r\n            </ul>\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>";
        }
    }
}