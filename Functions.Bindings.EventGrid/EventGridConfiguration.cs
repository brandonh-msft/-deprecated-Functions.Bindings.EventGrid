using Microsoft.Azure.WebJobs.Host.Config;
using Newtonsoft.Json.Linq;

namespace Functions.Bindings.EventGrid
{
    public class EventGridConfiguration : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            // Convert JSON to an EventGrid item
            context.AddConverter<JObject, EventGridOutput>(i => i.ToObject<EventGridOutput>());

            // Output binding for Event Grid
            context
                .AddBindingRule<EventGridOutputAttribute>()
                .BindToCollector(a => new EventGridOutputAsyncCollector(a));
        }
    }
}
