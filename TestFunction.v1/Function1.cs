using System.Net.Http;
using System.Threading.Tasks;
using Functions.Bindings.EventGrid;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace TestFunction.v1
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, [EventGridOutput(TopicEndpoint = @"topicEndpoint", SasKey = @"sasKey")]IAsyncCollector<EventGridOutput> events, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            await events.AddAsync(new EventGridOutput(@"my/subject", "someType") { Data = new { check = "this", @out = false } });

            return req.CreateResponse();
        }
    }
}
