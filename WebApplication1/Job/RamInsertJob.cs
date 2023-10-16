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
    string[] _processNames = null;


    public RamInsertJob(IRamEntityRepository ramEntityRepository, IConfiguration configuration)
    {
        _ramEntityRepository = ramEntityRepository;
        _configuration = configuration;
        _processNames = _configuration.GetSection("ProcessNames").Get<string[]>();

    }

    public async Task Execute(IJobExecutionContext context)
    {

        Process[] processes = Process.GetProcesses();

        foreach (var processName in _processNames)
        {
            Process[] proc = Process.GetProcessesByName(processName);
            int a = (int)proc.Select(pr => pr.PrivateMemorySize64 / 1024 / 1024).Sum();
            DateTime date = DateTime.Now;
            RamEntity ramEntity = new RamEntity()
            {
                Name = $"{processName}",
                Date = date,
                Size = a
            };

            await _ramEntityRepository.Insert(ramEntity);


        }

        await _ramEntityRepository.SaveChanges();

    }
}
