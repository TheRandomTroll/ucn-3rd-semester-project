using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetPatch.Data.Entities.DTO
{
    class EditCommentDto
    {
        [Required]
        public string Content { get; set; }
    }
}
