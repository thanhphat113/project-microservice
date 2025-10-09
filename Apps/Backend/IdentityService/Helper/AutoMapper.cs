using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityService.DTOs;
using IdentityService.Models;

namespace IdentityService.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterLocalDTO, Account>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => "Local"))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => new User
                {
                    Name = src.Name,
                    Email = src.Identify
                }));

            CreateMap<User, LoginUserDTO>();

        }

    }
}