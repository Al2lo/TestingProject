using TestingProject.BLL.DTOs;
using TestingProject.DAL.Entities;

namespace TestingProject.BLL.Services.Interfaces
{
    public interface IReceiverSpeedDataService
    {
        public Task WriteSpeedDataInFileAsync(AddSpeedDataDTO speedData, CancellationToken cancellationToken);
    }
}
