using AutoMapper;
using FrogExebitionAPI.DTO.ApplicatonUserDTOs;
using FrogExebitionAPI.DTO.ExebitionDTOs;
using FrogExebitionAPI.DTO.FrogDTOs;
using FrogExebitionAPI.DTO.FrogOnExebitionDTOs;
using FrogExebitionAPI.DTO.VoteDtos;
using FrogExebitionAPI.Models;

namespace FrogExebitionAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Frog, FrogDtoDetail>();
            CreateMap<FrogDtoDetail, Frog>();
            CreateMap<Frog, FrogDtoForCreate>();
            CreateMap<FrogDtoForCreate, Frog>();
            CreateMap<Frog, FrogDtoForUpdate>();
            CreateMap<FrogDtoForUpdate, Frog>();
            CreateMap<Frog, FrogDtoGeneral>();
            CreateMap<FrogDtoGeneral, Frog>();

            CreateMap<Exebition, ExebitionDtoDetail>();
            CreateMap<ExebitionDtoDetail, Exebition>();
            CreateMap<Exebition, ExebitionDtoForCreate>();
            CreateMap<ExebitionDtoForCreate, Exebition>();

            CreateMap<FrogOnExebition, FrogOnExebitionDtoDetail>();
            CreateMap<FrogOnExebitionDtoDetail, FrogOnExebition>();
            CreateMap<FrogOnExebition, FrogOnExebitionDtoForCreate>();
            CreateMap<FrogOnExebitionDtoForCreate, FrogOnExebition>();

            CreateMap<Vote, VoteDtoDetail>();
            CreateMap<VoteDtoDetail, Vote>();
            CreateMap<Vote, VoteDtoForCreate>();
            CreateMap<VoteDtoForCreate, Vote>();

            CreateMap<ApplicationUser, ApplicationUserDtoDetail>();
            CreateMap<ApplicationUserDtoDetail, ApplicationUser>();
            CreateMap<ApplicationUser, ApplicationUserDtoForUpdate>();
            CreateMap<ApplicationUserDtoForUpdate, ApplicationUser>();

        }
    }
}
