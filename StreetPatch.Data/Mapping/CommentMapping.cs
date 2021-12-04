using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using StreetPatch.Data.Entities;
using StreetPatch.Data.Entities.DTO;

namespace StreetPatch.Data.Mapping
{
    public class CommentMapping : Profile
    {
        public CommentMapping()
        {
            CreateMap<UpdateCommentDto, Comment>()
                .ForMember(x => x.Id,
                    x => x.MapFrom(y => y.Id))
                .ForMember(x => x.Content,
                    x => x.MapFrom(y => y.Content));

            CreateMap<CreateCommentDto, Comment>()
                .ForMember(x => x.ReportId, x => x.Ignore());
        }
    }
}
