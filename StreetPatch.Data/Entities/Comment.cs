using System;
using System.Collections.Generic;
using StreetPatch.Data.Entities.Base;

namespace StreetPatch.Data.Entities
{
    public class Comment : EntityBase
    {
        public string Content { get; set; }

        public Guid ReportId { get; set; }
        public virtual Report Report { get; set; }
        public virtual List<ApplicationUser> LikedBy { get; set; }
    }
}
