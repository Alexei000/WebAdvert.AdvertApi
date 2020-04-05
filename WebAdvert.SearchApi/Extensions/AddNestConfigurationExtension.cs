using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using WebAdvert.SearchWorker;

namespace WebAdvert.SearchApi.Extensions
{
    public static class AddNestConfigurationExtension
    {
        public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            string elasticSearchUrl = configuration.GetSection("ES").GetValue<string>("url");

            var connectionSettings = new ConnectionSettings(new Uri(elasticSearchUrl))
                .DefaultIndex("adverts")
                .DefaultMappingFor<AdvertType>(d => d.IdProperty(x => x.Id));

            var client = new ElasticClient(connectionSettings);
            services.AddSingleton<IElasticClient>(client);
        }
    }
}
