using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreetPatch.Data.Entities;

namespace StreetPatch.Data.Repositories
{
    public class CommentRepository : AbstractRepository<Comment, StreetPatchDbContext>
    {
        public CommentRepository(StreetPatchDbContext context) : base(context)
        {
        }
    }
}
