using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json.Linq;

namespace Functions.Bindings.EventGrid
{

    public class EventGridOutputAsyncCollector : IAsyncCollector<EventGridOutput>
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        private EventGridOutputAttribute _attribute;

        public EventGridOutputAsyncCollector(EventGridOutputAttribute attr)
        {
            this._attribute = attr;
        }

        public async Task AddAsync(EventGridOutput item, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (_httpClient.DefaultRequestHeaders.Contains(@"aeg-sas-key"))
                _httpClient.DefaultRequestHeaders.Remove(@"aeg-sas-key");
            _httpClient.DefaultRequestHeaders.Add(@"aeg-sas-key", _attribute.SasKey);


            var response = await _httpClient.PostAsync(_attribute.TopicEndpoint, new StringContent(new JArray(JObject.FromObject(item)).ToString(), System.Text.Encoding.Default, @"application/json"));
            response.EnsureSuccessStatusCode();
        }

        public Task FlushAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // do nothing
            return Task.CompletedTask;
        }
    }
}
