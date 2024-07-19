﻿using TestingProject.DAL.Entities;

namespace TestingProject.BLL.Services.Interfaces
{
    public interface ISenderSpeedDataService
    {
        public Task<IEnumerable<SpeedData>> GetCarsAsync(DateTime dateTime, Double speed, CancellationToken cancellationToken);
        public Task<IEnumerable<SpeedData>> GetCarsWithMinMaxSpeed(DateTime date, CancellationToken cancellationToken);
    }
}