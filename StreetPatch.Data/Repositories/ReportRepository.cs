using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreetPatch.Data.Entities;

namespace StreetPatch.Data.Repositories
{
    public class ReportRepository : AbstractRepository<Report, StreetPatchDbContext>
    {
        public ReportRepository(StreetPatchDbContext context) : base(context)
        {
        }

        public async Task<Report> DeleteOrArchiveAsync(Guid guid, bool isSoft)
        {
            if (!isSoft)
            {
                return await base.DeleteAsync(guid);
            }

            var report = await base.GetAsync(guid);
            report.DeletedAt = DateTime.Now;
            return await base.UpdateAsync(report);

        }
    }
}
