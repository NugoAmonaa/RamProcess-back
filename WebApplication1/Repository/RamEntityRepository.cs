using RamProcessingTool.Entity;
using VacationScaffold.RepositoryImplementation;

namespace WebApplication1.Repository
{
    public class RamEntityRepository : BaseRepository<RamEntity>, IRamEntityRepository
    {
        public RamEntityRepository(ProjectContext context) : base(context)
        {
        }
    }
}
