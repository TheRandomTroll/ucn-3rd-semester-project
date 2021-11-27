using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetPatch.Data.Entities.DTO
{
    public class UpdateReportDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ReportStatus { get; set; }

        [Required]
        public string ReportType { get; set; }
    }
}
