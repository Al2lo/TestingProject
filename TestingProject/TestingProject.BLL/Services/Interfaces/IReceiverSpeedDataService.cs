using TestingProject.BLL.DTOs;

namespace TestingProject.BLL.Services.Interfaces
{
    public interface IReceiverSpeedDataService
    {
        public Task WriteSpeedDataInFileAsync(AddSpeedDataDTO speedData, CancellationToken cancellationToken);
    }
}
