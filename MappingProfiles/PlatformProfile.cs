using AutoMapper;
using PlatformService.Models;
using PlatformService.Dtos;

namespace PlatformService.MappingProfiles
{
    public class PlatformProfile : Profile
    {
        public PlatformProfile()
        {
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();    
        }
    }
}