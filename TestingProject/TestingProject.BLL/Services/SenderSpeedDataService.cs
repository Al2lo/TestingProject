using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text.Json;
using TestingProject.BLL.Services.Interfaces;
using TestingProject.DAL.Entities;
using TestingProject.DAL.Errors;

namespace TestingProject.BLL.Services
{
    internal class SenderSpeedDataService : ISenderSpeedDataService
    {
        private readonly ILogger<SenderSpeedDataService> _logger;
        private readonly string _directoryData;
        private readonly TimeSpan _startWorkTime;
        private readonly TimeSpan _endWorkTime;

        public SenderSpeedDataService(ILogger<SenderSpeedDataService> logger, IConfiguration configuration)
        {
            _logger = logger;
            var path = configuration.GetRequiredSection("AppSettings")["DirectoryPath"];

            string? startWorkTimeStr = configuration.GetRequiredSection("AppSettings")["StartWorkTime"];
            string? endWorkTimeStr = configuration.GetRequiredSection("AppSettings")["EndWorkTime"];

            if (TimeSpan.TryParseExact(startWorkTimeStr, "h\\:mm", CultureInfo.InvariantCulture, out TimeSpan startWorkTime)
                && TimeSpan.TryParseExact(endWorkTimeStr, "h\\:mm", CultureInfo.InvariantCulture, out TimeSpan endWorkTime))
            {
                _startWorkTime = startWorkTime;
                _endWorkTime = endWorkTime;
            }
            else
                throw new Exception(ErrorConsts.WorkTimeError);

            if (path == null)
                throw new Exception(ErrorConsts.DirectoryPathError);
           
            _directoryData = path;
        }

        public async Task<IEnumerable<SpeedData>> GetCarsAsync(DateTime dateTime, Double speed, CancellationToken cancellationToken)
        {
            if (!IsWorkTime())
                throw new Exception(ErrorConsts.SystemWorkError);

            var filePath = GetFilePath(dateTime);
            var outSpeedData = new List<SpeedData>();
            var lines = await File.ReadAllLinesAsync(filePath, cancellationToken);
            outSpeedData.AddRange(lines.Select(line => JsonSerializer.Deserialize<SpeedData>(line)).Where(x => x.Speed > speed));
       
            return outSpeedData;
        }

        public async Task<IEnumerable<SpeedData>> GetCarsWithMinMaxSpeedAsync(DateTime date, CancellationToken cancellationToken)
        {
            if (!IsWorkTime())
                throw new Exception(ErrorConsts.SystemWorkError);

            var filePath = GetFilePath(date);
            var outSpeedData = new List<SpeedData>();
            var lines = await File.ReadAllLinesAsync(filePath, cancellationToken);
            outSpeedData.AddRange(lines.Select(line => JsonSerializer.Deserialize<SpeedData>(line)));
            var minSpeed = outSpeedData.Min(x => x.Speed);
            var maxSpeed = outSpeedData.Max(x => x.Speed);

            return outSpeedData.Where(x => x.Speed == minSpeed || x.Speed == maxSpeed).ToList<SpeedData>();
        }

        private bool IsWorkTime()
        {
            var timeNow = DateTime.Now;
            var currentTimeSpan = new TimeSpan(timeNow.Hour, timeNow.Minute, timeNow.Second);
            return currentTimeSpan >= _startWorkTime && currentTimeSpan <= _endWorkTime;
        }

        private string GetFilePath(DateTime date)
        {
            return Path.Combine(_directoryData, $"{date.ToString("yyyy-MM-dd")}.json");
        }
    }
}
