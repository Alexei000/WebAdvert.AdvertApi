using System.Threading.Tasks;
using AdvertApi.Models;

namespace AdvertApi.Services
{
    public interface IAdvertStorageService
    {
        Task<string> Add(AdvertModel model);
        Task<AdvertDbModel> GetById(string id);
        Task Confirm(ConfirmAdvertModel model);

        Task<bool> CheckHealthAsync();
    }
}
