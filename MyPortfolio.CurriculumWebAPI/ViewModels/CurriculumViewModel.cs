namespace CurriculumWebAPI.App.ViewModels
{
    public class CurriculumViewModel
    {
        public CurriculumHeaderVM? Header { get; set; }

        public ContatoViewModel? Contato { get; set; }

        public ExpProfissionalViewModel? Exp_Profissional { get; set;}

        public FormacaoViewModel? Formacao { get; set; }

        public CursosExtraViewModel? CursosExtra { get; set; }
    }
}
