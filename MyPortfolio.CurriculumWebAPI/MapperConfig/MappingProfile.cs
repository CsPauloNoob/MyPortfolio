using AutoMapper;
using CurriculumWebAPI.App.InputModels;
using CurriculumWebAPI.App.ViewModels;
using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Domain.Models.ComplexTypes;
using CurriculumWebAPI.Domain.Models.CurriculumBody;

namespace CurriculumWebAPI.App.MapperConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            #region Curriculum Mapper

            CreateMap<CurriculumInputModel, Curriculum>()
                .ForMember(c => c.Habilidade, opt => opt.Ignore())
                .ForMember(c => c.Formacao, opt => opt.Ignore())
                .ForMember(c => c.Cursos, opt => opt.Ignore())
                .ForMember(c => c.Experiencia_Profissional, opt => opt.Ignore());

            CreateMap<UserInputModel, User>()
                .ForMember(u => u.Curriculum, opt => opt.Ignore());

            CreateMap<ContatoInputModel, Contato>()
            .ForMember(dest => dest.Telefone, opt => opt.MapFrom(src =>
            new PhoneNumber
            {
                Codigo = src.Codigo,
                DDD = src.DDD,
                Numero =
            src.NumeroTelefone_Celular.ToString()
            }))
            .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src =>
            new Address
            {
                Rua = src.Rua,
                Bairro = src.Bairro,
                NumeroCasa =
            src.NumeroCasa,
                Cidade = src.Cidade,
                Estado = src.Estado
            }));


            CreateMap<Curriculum, CurriculumViewModel>();
            #endregion

            #region Contato Mapper
            //CreateMap<Contato, ContatoViewModel>();

            CreateMap<Contato, ContatoViewModel>()
            .ForMember(dest => dest.NumeroCasa, opt => opt.MapFrom(src => src.Endereco.NumeroCasa))
            .ForMember(dest => dest.DDD, opt => opt.MapFrom(src => src.Telefone.DDD))
            .ForMember(dest => dest.NumeroTelefone_Celular, opt => opt.MapFrom(src => src.Telefone.Numero))
            .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Telefone.Codigo))
            .ForMember(dest => dest.Bairro, opt => opt.MapFrom(src => src.Endereco.Bairro))
            .ForMember(dest => dest.Rua, opt => opt.MapFrom(src => src.Endereco.Rua))
            .ForMember(dest => dest.Cidade, opt => opt.MapFrom(src => src.Endereco.Cidade))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Endereco.Estado));

            #endregion


            #region Formacao Mappper

            CreateMap<FormacaoInputModel, Formacao>()
            .ForMember(dest => dest.AnoConclusao, opt => opt.MapFrom(src => int.Parse(src.AnoConclusao)))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CurriculumId, opt => opt.Ignore())
            .ForMember(dest => dest.Curriculum, opt => opt.Ignore());


            CreateMap<Formacao, FormacaoViewModel>();

            #endregion


            #region habilidades Mapper
            CreateMap<HabilidadeInputModel, Habilidades>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CurriculumId, opt => opt.Ignore())
                .ForMember(dest => dest.Curriculum, opt => opt.Ignore());


            CreateMap<Habilidades, HabilidadeViewModel>();

            #endregion


            #region Cursos Extra Mapper
            CreateMap<CursosExtraInputModel, Cursos_Extras>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CurriculumId, opt => opt.Ignore())
            .ForMember(dest => dest.Curriculum, opt => opt.Ignore());

            CreateMap<Cursos_Extras, CursosExtraViewModel>();

            #endregion


            #region Experiencia Profissional Mapper
            CreateMap<ExpProfissionalInputModel, Experiencia_Profissional>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CurriculumId, opt => opt.Ignore())
            .ForMember(dest => dest.Curriculum, opt => opt.Ignore());

            CreateMap<Experiencia_Profissional, ExpProfissionalViewModel>();
            #endregion
        }
    }
}