using Microsoft.AspNetCore.Mvc;

namespace BackRoomProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SerialNumberController : ControllerBase
    {
        private readonly ILogger<SerialNumberController> _logger;
        public SerialNumberController(ILogger<SerialNumberController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "pki")]

        public IEnumerable<SerialNumber> Get()
        {
            return Enumerable.Range(1, 1).Select(index => new SerialNumber
            {
                serialNumber = SerialNumber.GetSerialNumber()
            })
                .ToArray();
        }
    }
}






