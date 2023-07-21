using AutoMapper;
using CurriculumWebAPI.App.InputModels;
using CurriculumWebAPI.Domain.Models;
using CurriculumWebAPI.Domain.Models.ComplexTypes;
using CurriculumWebAPI.Domain.Models.CurriculumBody;

namespace CurriculumWebAPI.App.MapperConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CurriculumInputModel, Curriculum>()
                .ForMember(c => c.Habilidade, opt => opt.Ignore())
                .ForMember(c => c.Formacao, opt => opt.Ignore())
                .ForMember(c => c.Cursos, opt => opt.Ignore())
                .ForMember(c => c.Experiencia_Profissional, opt => opt.Ignore());

            CreateMap<UserInputModel, User>()
                .ForMember(u => u.Curriculum, opt => opt.Ignore());

            CreateMap<ContatoInputModel, Contato>()
            .ForMember(dest => dest.Telefone, opt => opt.MapFrom(src => new PhoneNumber { Codigo = src.Codigo, DDD = src.DDD, Numero = src.Numero.ToString() }))
            .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => new Address { Rua = src.Rua, Bairro = src.Bairro, NumeroCasa = src.NumeroCasa, Cidade = src.Cidade, Estado = src.Estado }));






            // Conversão personalizada para PhoneNumber
            /*CreateMap<string, PhoneNumber>()
                .ConvertUsing(src => new PhoneNumber { Numero = src });

            // Conversão personalizada para Address
            CreateMap<ContatoInputModel, Address>()
                .ConvertUsing(src => new Address { Rua = src.Rua, Bairro = src.Bairro, NumeroCasa = src.NumeroCasa, Cidade = src.Cidade, Estado = src.Estado });*/
        }
    }
}