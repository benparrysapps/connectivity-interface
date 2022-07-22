using Microsoft.AspNetCore.
    Mvc;

namespace BackRoomProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RfDataController : ControllerBase
    {
        public string test;

        private readonly ILogger<RfDataController> _logger;
        public RfDataController(ILogger<RfDataController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "id")]

        public IEnumerable<RfData> Get()
        {
            string[] scanData = RfData.GetRfData();
            if(scanData == null)
            {
                scanData = new string[] { "No RFID Tag in field", "No RFID Tag in field", "No RFID Tag in field", "No RFID Tag in field", "No RFID Tag in field", "No RFID Tag in field" };
            }

            return Enumerable.Range(1, 1).Select(index => new RfData
            { 
                scanDataEPC = scanData[0],
                timeStamp = scanData[1],
                antennaAmount = scanData[2],
                signalStrength = scanData[3],
                powerLevel = scanData[5],
            }).ToArray();
        }

    }
}





