using Microsoft.AspNetCore.Mvc;
using RamProcessingTool.Entity;
using System.Diagnostics;
using WebApplication1.Repository;

namespace RamProcessingTool.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RamController : ControllerBase
    {
        public IRamEntityRepository _ramEntityRepository;



        public RamController(IRamEntityRepository ramEntityRepository)
        {
            _ramEntityRepository = ramEntityRepository;
        }
        [HttpPost]
        public async Task<IEnumerable<RamEntity>> GenerateRamsAsync()
        {

            for (int i = 10; i < 30; i++)
            {
                int count = 0;

                DateTime date = DateTime.Now.AddDays(i);
                while (count != 8640)
                {
                    Process[] processes = Process.GetProcesses();

                    long totalMemoryUsageKB = 0;


                    foreach (var process in processes)
                    {
                        totalMemoryUsageKB += process.PrivateMemorySize64 / 1024;

                    }
                    date = date.AddSeconds(10);

                    RamEntity ramEntity = new RamEntity()
                    {
                        Name = $"{nameof(RamEntity)}",
                        Date = date,
                        Size = totalMemoryUsageKB / 1024
                    };

                    count++;
                    await _ramEntityRepository.Insert(ramEntity);

                }
                await _ramEntityRepository.SaveChanges();
            }

            //Process[] processes = Process.GetProcesses();
            //foreach(Process process in processes)
            //{
            //    try
            //    {


            //    Console.WriteLine(process.PrivateMemorySize/1024);
            //    }
            //        catch(Exception ex)
            //    {
            //        //Console.WriteLine(ex.ToString());
            //    }
            //}
            return Enumerable.Empty<RamEntity>();
        }

        //[HttpPost]
        //public IEnumerable<RamEntity> GenerateRams()
        //{
        //    int count = 0;
        //    while (count != 100)
        //    {
        //        Process[] processes = Process.GetProcesses();

        //        long totalMemoryUsageKB = 0;


        //        foreach (var process in processes)
        //        {
        //            totalMemoryUsageKB += process.PrivateMemorySize64 / 1024;

        //        }
        //        RamEntity ramEntity = new RamEntity()
        //        {
        //            Name = $"{nameof(RamEntity)}",
        //            Date = DateTime.Now,
        //            Size = totalMemoryUsageKB / 1024
        //        };
        //        _ramEntityRepository.Insert(ramEntity);
        //        _ramEntityRepository.SaveChanges();
        //        Thread.Sleep(100); 

        //    }
        //    return Enumerable.Empty<RamEntity>();
        //}
        //[HttpGet]
        //public async Task<IEnumerable<RamEntity>> getRams()
        //{

        //    return await _ramEntityRepository.GetList();
        //}
        [HttpGet]
        public async Task<List<RamEntity>> GetAverageRamSizeByMinute()
        {
            // Get the list of RamEntity objects from your repository
            var ramEntities = await _ramEntityRepository.GetList();

            // Group the entities by the minute part of the Date property within each hour and calculate the average size
            //var result = await ramEntities
            //    //.GroupBy(entity => new { entity.Date.Hour, entity.Date.Minute })
            //    //.Select(group =>
            //    //{
            //    //    return new
            //    //    {
            //    //        Date = group.Key.Hour,
            //    //        size = group.Average(entity => entity.Size)
            //    //    };
            //    //})
            //    //.OrderBy(group => group.Date)
            //    //.ThenBy(group => group.minutes)
            //    .ToListAsync();

            return ramEntities.ToList();
        }

    }
}