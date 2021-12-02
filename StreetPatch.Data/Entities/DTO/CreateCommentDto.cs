using System;
using System.ComponentModel.DataAnnotations;

namespace StreetPatch.Data.Entities.DTO
{
    public class CreateCommentDto
    {
        [Required]
        public string ReportId { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
