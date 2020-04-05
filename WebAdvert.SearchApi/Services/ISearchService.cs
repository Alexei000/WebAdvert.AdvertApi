using System.Collections.Generic;
using System.Threading.Tasks;
using WebAdvert.SearchWorker;

namespace WebAdvert.SearchApi.Services
{
    public interface ISearchService
    {
        Task<List<AdvertType>> SearchAsync(string keyword);
    }
}
