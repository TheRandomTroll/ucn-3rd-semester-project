using Microsoft.EntityFrameworkCore;

namespace StreetPatch.Data.Entities.Base
{
    [Owned]
    public class Coordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
