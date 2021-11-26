using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetPatch.Data.Entities.DTO
{
    public class CreateReportDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ReportType { get; set; }
        [Required]
        [Range(-90, 90)]
        public double Longitude { get; set; }
        [Required]
        [Range(-180, 180)]
        public double Latitude { get; set; }

    }
}
