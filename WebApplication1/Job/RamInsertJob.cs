using Quartz;
using RamProcessingTool.Entity;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using WebApplication1.Repository;

public class RamInsertJob : IJob
{
    private readonly IRamEntityRepository _ramEntityRepository;
    private readonly IConfiguration _configuration;


    public RamInsertJob(IRamEntityRepository ramEntityRepository, IConfiguration configuration)
    {
        _ramEntityRepository = ramEntityRepository;
        _configuration = configuration;

    }

    public async Task Execute(IJobExecutionContext context)
    {

        Process[] processes = Process.GetProcesses();
        var processNames = _configuration.GetSection("ProcessNames").Get<string[]>();

        foreach (var process in processes)
        {
            DateTime date = DateTime.Now;
            RamEntity ramEntity = new RamEntity()
            {
                Name = $"{process.ProcessName}",
                Date = date,
                Size = process.PrivateMemorySize64 / 1024 / 1024
            };
            if (processNames.Contains(process.ProcessName.ToLower())) // Check if the process name is in the configuration
            {
                await _ramEntityRepository.Insert(ramEntity);
            }

        }

        await _ramEntityRepository.SaveChanges();

    }
}
