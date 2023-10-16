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
        public async Task<List<RamEntity>> GetList(DateTime startDate, DateTime endDate, string appName)
        {
            var ramEntities = await _ramEntityRepository.GetListByPeriod(startDate,endDate, appName);

        

            return ramEntities.ToList();
        }

    }
}