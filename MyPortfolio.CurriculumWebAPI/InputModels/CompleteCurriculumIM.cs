namespace CurriculumWebAPI.App.InputModels
{

    #pragma warning disable
    public class CompleteCurriculumIM
    {
        public CurriculumHeaderIM Header { get; set; }

        public ContatoInputModel Contato { get; set; }

        public List<HabilidadeInputModel> Habilidades { get; set; }

        public List<CursosExtraInputModel?> Cursos_Extra { get; set; }

        public List<FormacaoInputModel?> Formacao { get; set; }

        public List<ExpProfissionalInputModel?> ExpProfissional { get; set;}
    }
}
