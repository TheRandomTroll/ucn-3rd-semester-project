using AutoMapper;
using Baseline.ImTools;
using ImTools;
using StreetPatch.Data.Entities;
using StreetPatch.Data.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace StreetPatch.Data.Mapping
{
    public class MappingUser : Profile
    {
        public MappingUser() {
        CreateMap<ApplicationUser, SignUpDto>();
        CreateMap<SignUpDto, ApplicationUser>()
    .ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Email));
            }
    }
}
