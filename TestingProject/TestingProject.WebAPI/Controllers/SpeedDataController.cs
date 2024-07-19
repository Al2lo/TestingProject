using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using TestingProject.BLL.DTOs;
using TestingProject.BLL.Services.Interfaces;
using TestingProject.DAL.Entities;
using TestingProject.DAL.Errors;

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

        [HttpGet("violators")]
        public async Task<IEnumerable<SpeedData>> GetViolators([FromQuery] string dateTime, [FromQuery] Double speed, CancellationToken cancellationToken)
        {
            if (DateTime.TryParseExact(dateTime, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDateTime))
            {
                _logger.LogInformation("system call method GetViolators");
                return await _senderSpeedDataService.GetCarsAsync(parsedDateTime, speed, cancellationToken);
            }
            else
                throw new Exception(ErrorConsts.DataFormatError);
        }

        [HttpGet("minMaxSpeedData")]
        public async Task<IEnumerable<SpeedData>> GetMinMaxSpeedData([FromQuery] string dateTime, CancellationToken cancellationToken)
        {
            if (DateTime.TryParseExact(dateTime, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDateTime))
            {
                _logger.LogInformation("system call method get min and max speedData");
                return await _senderSpeedDataService.GetCarsWithMinMaxSpeedAsync(parsedDateTime, cancellationToken);
            }
            else
                throw new Exception(ErrorConsts.DataFormatError);
        }

        [HttpPost("speedData")]
        public async Task WtiteSpeedData([FromBody] AddSpeedDataDTO addSpeedDataDTO, CancellationToken cancellationToken)
        {
            _logger.LogInformation("system call method add speedData");
            await _receiverSpeedDataService.WriteSpeedDataInFileAsync(addSpeedDataDTO, cancellationToken);
        }
    }
}
