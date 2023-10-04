using Quartz;
using RamProcessingTool.Entity;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WebApplication1.Repository;

public class RamInsertJob : IJob
{
    private readonly IRamEntityRepository _ramEntityRepository;
    

    public RamInsertJob(IRamEntityRepository ramEntityRepository)
    {
        _ramEntityRepository = ramEntityRepository;
    }

    public async Task Execute(IJobExecutionContext context)
    {

            Process[] processes = Process.GetProcesses();
            long totalMemoryUsageKB = 0;

            foreach (var process in processes)
            {
                totalMemoryUsageKB += process.PrivateMemorySize64 / 1024;
            }

            DateTime date = DateTime.Now;
            RamEntity ramEntity = new RamEntity()
            {
                Name = $"{nameof(RamEntity)}",
                Date = date,
                Size = totalMemoryUsageKB / 1024
            };

            await _ramEntityRepository.Insert(ramEntity);
            await _ramEntityRepository.SaveChanges();

       
    }
}
