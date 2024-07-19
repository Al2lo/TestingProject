using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using TestingProject.BLL.Services.Interfaces;
using TestingProject.DAL.Entities;

namespace TestingProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeedDataController : ControllerBase
    {
        private readonly ILogger<SpeedDataController> _logger;
        private readonly IReceiverSpeedDataService _receiverSpeedDataService;
        private readonly ISenderSpeedDataService _senderSpeedDataService;

        public SpeedDataController(ILogger<SpeedDataController> logger, IReceiverSpeedDataService receiverSpeedDataService, ISenderSpeedDataService senderSpeedDataService)
        {
            _logger = logger;
            _receiverSpeedDataService = receiverSpeedDataService;
            _senderSpeedDataService = senderSpeedDataService;
        }

        [HttpGet("GetViolators/{dateTime}/{speed:double}")]
        public async Task<IEnumerable<SpeedData>> GetViolators(string dateTime, Double speed, CancellationToken cancellationToken)
        {
            if (DateTime.TryParseExact(dateTime, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDateTime))
            {
                _logger.LogInformation("system call method GetViolators");
                return await _senderSpeedDataService.GetCarsAsync(parsedDateTime, speed, cancellationToken);
            }
            else
                throw new Exception("Invalid date format. Please use dd.MM.yyyy.");
        }

        [HttpGet("GetMinMax/{dateTime}")]
        public async Task<IEnumerable<SpeedData>> GetMinMaxSpeedData(string dateTime, CancellationToken cancellationToken)
        {
            if (DateTime.TryParseExact(dateTime, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDateTime))
            {
                _logger.LogInformation("system call method get min and max speedData");
                return await _senderSpeedDataService.GetCarsWithMinMaxSpeedAsync(parsedDateTime, cancellationToken);
            }
            else
                throw new Exception("Invalid date format. Please use dd.MM.yyyy.");
        }

        [HttpPost("Add")]
        public async Task WtiteSpeedData(SpeedData speedData, CancellationToken cancellationToken)
        {
            _logger.LogInformation("system call method add speedData");
            await _receiverSpeedDataService.WriteSpeedDataInFileAsync(speedData, cancellationToken);
        }
    }
}
