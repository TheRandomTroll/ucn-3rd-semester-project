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
            CreateMap<CreateCommentDto, Comment>();
        }
    }
}
