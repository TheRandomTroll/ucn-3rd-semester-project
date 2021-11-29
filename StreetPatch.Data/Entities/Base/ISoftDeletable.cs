using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetPatch.Data.Entities.Base
{
    public interface ISoftDeletable
    {
        public DateTime? DeletedAt { get; set; }
    }
}
