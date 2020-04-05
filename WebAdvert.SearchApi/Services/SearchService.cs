using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using WebAdvert.SearchWorker;

namespace WebAdvert.SearchApi.Services
{
    public class SearchService : ISearchService
    {
        private readonly IElasticClient _client;

        public SearchService(IElasticClient client)
        {
            _client = client;
        }

        public async Task<List<AdvertType>> SearchAsync(string keyword)
        {
            Func<QueryContainerDescriptor<AdvertType>, QueryContainer> queryExpression = 
                query => query.Term(field => field.Title, keyword.ToLower());

            var searchResponse = await _client.SearchAsync<AdvertType>(search =>
                search.Query(queryExpression
                )).ConfigureAwait(false);
            return searchResponse.Hits.Select(hit => hit.Source).ToList();
        }
    }
}
