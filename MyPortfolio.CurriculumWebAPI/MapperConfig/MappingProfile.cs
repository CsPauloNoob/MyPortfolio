using AutoMapper;
using CurriculumWebAPI.App.ViewModel;
using CurriculumWebAPI.Domain.Models;

namespace CurriculumWebAPI.App.MapperConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Curriculum, CurriculumViewModel>()
                .ForMember(formacao => formacao.Formação, opts
                => opts.MapFrom(educ => educ.Educacao.Curso)).ReverseMap();
        }
    }
}