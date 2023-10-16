using RamProcessingTool.Entity;

namespace WebApplication1.Repository
{
    
    public interface IRamEntityRepository : IBaseRepository<RamEntity>
    {
        Task<IEnumerable<RamEntity>> GetListByPeriod(DateTime startDate, DateTime endDate, string selectedAppName);
    }
}

