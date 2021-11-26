using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using StreetPatch.Data.Entities;
using StreetPatch.Data.Entities.Base;
using StreetPatch.Data.Entities.DTO;
using StreetPatch.Data.Entities.Enums;

namespace StreetPatch.Data.Mapping
{
    public class ReportMapping : Profile
    {
        public ReportMapping()
        {
            CreateMap<CreateReportDto, Report>()
                .ForMember(x => x.Type,
                    x => x.MapFrom(y => EnumMapper<ReportType>.MapFromString(y.ReportType)))
                .ForMember(x => x.Coordinates,
                    x => x.MapFrom(y => new Coordinates {Latitude = y.Latitude, Longitude = y.Longitude}));
        }

        private static class EnumMapper<T> where T : Enum
        {
            public static T MapFromString(string value)
            {
                return (T)Enum.Parse(typeof(T), value);
            }
        }
    }
}
