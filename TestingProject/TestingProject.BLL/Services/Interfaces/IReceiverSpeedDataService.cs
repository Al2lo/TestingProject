using TestingProject.DAL.Entities;

namespace TestingProject.BLL.Services.Interfaces
{
    public interface IReceiverSpeedDataService
    {
        public Task WriteSpeedDataInFileAsync(SpeedData speedData, CancellationToken cancellationToken);
    }
}
