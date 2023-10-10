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
        private readonly IConfiguration _configuration;



        public RamController(IRamEntityRepository ramEntityRepository, IConfiguration configuration)
        {
            _ramEntityRepository = ramEntityRepository;
            _configuration = configuration;

        }


        [HttpGet("Applications")]
        public string[] GetApplications()
        {
          
                var processNames = _configuration.GetSection("ProcessNames").Get<string[]>();
                return processNames;
            
            
        }
        [HttpGet]
        public async Task<List<RamEntity>> GetAverageRamSizeByMinute()
        {
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