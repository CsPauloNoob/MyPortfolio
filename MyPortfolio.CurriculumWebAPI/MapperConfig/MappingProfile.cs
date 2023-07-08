using AutoMapper;
using CurriculumWebAPI.App.InputModels;
using CurriculumWebAPI.Domain.Models;

namespace CurriculumWebAPI.App.MapperConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Curriculum, CurriculumInputModel>();

            CreateMap<UserInputModel, User>()
                .ForMember(c => c.Curriculum, opt => opt.Ignore());
        }
    }
}