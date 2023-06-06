using AutoMapper;
using FrogExebitionAPI.Dto;
using FrogExebitionAPI.Models;

namespace FrogExebitionAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Frog, FrogDto>();
            CreateMap<Exebition, ExebitionDto>();
            CreateMap<FrogOnExebition, FrogOnExebitionDto>();
        }
    }
}
