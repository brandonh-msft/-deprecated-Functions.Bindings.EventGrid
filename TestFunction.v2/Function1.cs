using Functions.Bindings.EventGrid;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace TestFunction.v2
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async System.Threading.Tasks.Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, [EventGridOutput(TopicEndpointSetting = @"topicEndpoint", SasKeySetting = @"sasKey")]IAsyncCollector<EventGridOutput> events, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            await events.AddAsync(new EventGridOutput(@"my/subject", "someType") { Data = new { check = "this", @out = false } });

            return new OkResult();
        }
    }
}
