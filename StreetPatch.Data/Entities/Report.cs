using System;
using System.Collections.Generic;
using StreetPatch.Data.Entities.Base;
using StreetPatch.Data.Entities.Enums;

namespace StreetPatch.Data.Entities
{
    public class Report : EntityBase, ISoftDeletable
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Coordinates Coordinates { get; set; }
        public ReportType Type { get; set; }
        public ReportStatus Status { get; set; }
        public Guid CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }    
        public virtual List<Comment> Comments { get; set; }
        public virtual List<ApplicationUser> LikedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
