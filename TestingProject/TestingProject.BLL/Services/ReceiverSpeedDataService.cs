using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using TestingProject.BLL.DTOs;
using TestingProject.BLL.Services.Interfaces;
using TestingProject.DAL.Entities;

namespace TestingProject.BLL.Services
{
    internal class ReceiverSpeedDataService : IReceiverSpeedDataService
    {
        private readonly IValidator<AddSpeedDataDTO> _validator;
        private readonly ILogger<ReceiverSpeedDataService> _logger;
        private readonly string _directoryData;

        public ReceiverSpeedDataService(ILogger<ReceiverSpeedDataService> logger, IConfiguration configuration, IValidator<AddSpeedDataDTO> validator) 
        {
            _logger = logger;
            _validator = validator;
            var path = configuration.GetRequiredSection("AppSettings")["DirectoryPath"];

            if (path == null)
                throw new Exception("directory path can not be null");

            _directoryData = path;
        }

        public async Task WriteSpeedDataInFileAsync(AddSpeedDataDTO addSpeedDataDTO, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(addSpeedDataDTO);

            if (!validationResult.IsValid)
                throw new Exception("data is not valid");

            SpeedData speedData = new()
            {
                DateTime = DateTime.ParseExact(addSpeedDataDTO.DateTimeFormatString, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                Speed = addSpeedDataDTO.Speed,
                CarNumber = addSpeedDataDTO.CarNumber
            };
            var file = GetFilePath(speedData.DateTime);
            await File.AppendAllTextAsync(file, JsonConvert.SerializeObject(speedData) + Environment.NewLine, cancellationToken);
            _logger.LogInformation($"Speed data saved: {speedData.DateTime} - {speedData.CarNumber} - {speedData.Speed}");
        }

        private string GetFilePath(DateTime date)
        {
            Directory.CreateDirectory(_directoryData);

            return Path.Combine(_directoryData, $"{date.ToString("yyyy-MM-dd")}.json");
        }
    }
}
