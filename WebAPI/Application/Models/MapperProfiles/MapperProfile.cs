using AutoMapper;
using WebAPI.Application.DTOs;
using WebAPI.Application.Models.ViewModel;

namespace WebAPI.Application.Models.MapperProfiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<LanguageRequest, Language>();
            CreateMap<Language, LanguageViewModel>();
            CreateMap<Language, LanguageResponse>();
        }
    }
}
