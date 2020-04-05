using System;
using Microsoft.Extensions.Configuration;
using Nest;

namespace WebAdvert.SearchWorker
{
    public static class ElasticSearchHelper
    {
        private static IElasticClient _client;

        public static IElasticClient GetInstance(IConfiguration config)
        {
            if (_client == null)
            {
                string url = config.GetSection("ES").GetValue<string>("url");
                var settings = new ConnectionSettings(new Uri(url))
                    .DefaultIndex("adverts")
                    .DefaultMappingFor<AdvertType>(d => d.IdProperty(x => x.Id));
                _client = new ElasticClient(settings);
            }

            return _client;
        }
    }
}
