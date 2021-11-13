using StreetPatch.Data.Entities.Base;
using StreetPatch.Data.Entities.Enums;

namespace StreetPatch.Data.Entities
{
    public class Report : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Coordinates Coordinates { get; set; }
        public ReportType Type { get; set; }
        public ReportStatus Status { get; set; }
        public ApplicationUser Creator { get; set; }
    }
}
