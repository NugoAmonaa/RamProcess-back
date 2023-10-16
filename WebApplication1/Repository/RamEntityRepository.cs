using Microsoft.EntityFrameworkCore;
using RamProcessingTool.Entity;
using VacationScaffold.RepositoryImplementation;

namespace WebApplication1.Repository
{
    public class RamEntityRepository : BaseRepository<RamEntity>, IRamEntityRepository
    {
        public RamEntityRepository(ProjectContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RamEntity>> GetListByPeriod(DateTime startDate, DateTime endDate, string selectedAppName)
        {
            var query = _context.RamEntities
        .Where(entity => entity.Date >= startDate && entity.Date <= endDate);

            if (selectedAppName != "All")
            {
                query = query.Where(entity => entity.Name == selectedAppName);
            }

            var ramEntities = await query.ToListAsync();

            return ramEntities;
        }


    }
}
