using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreetPatch.Data.Entities.Base
{
    public class EntityBase
    {
        [Key, Column(Order = 0)]
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedOn { get; set; }

        public DateTime LastAccessed { get; set; }
    }
}
