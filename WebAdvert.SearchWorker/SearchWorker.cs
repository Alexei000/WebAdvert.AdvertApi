using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Nest;
using Newtonsoft.Json;
using JsonSerializer = Amazon.Lambda.Serialization.Json.JsonSerializer;

[assembly:LambdaSerializer(typeof(JsonSerializer))]
namespace WebAdvert.SearchWorker
{
    public class SearchWorker
    {
        public SearchWorker() : this(ElasticSearchHelper.GetInstance(ConfigurationHelper.Instance))
        {
        }

        private readonly IElasticClient _client;

        public SearchWorker(IElasticClient client)
        {
            _client = client;
        }

        // ReSharper disable once UnusedMember.Global
        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        public async Task Function(SNSEvent sns, ILambdaContext context)
        {
            foreach (var record in sns.Records)
            {
                context.Logger.LogLine($"Received message: {record.Sns.Message}");
                var message = JsonConvert.DeserializeObject<AdvertConfirmedMessage>(record.Sns.Message);
                var advertDocument = MappingHelper.Map(message);

                await _client.IndexDocumentAsync(advertDocument).ConfigureAwait(false);
            }
        }
    }
}
