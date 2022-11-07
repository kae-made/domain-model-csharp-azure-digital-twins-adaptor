using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Core.Pipeline;
using Azure.DigitalTwins.Core;
using Azure.Identity;
using Azure.Messaging.EventHubs;
using Kae.Utility.Json;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Kae.DomainMode.Csharp.AzureDigitalTwins.AzureIoTHubBinder
{
    public static class ReceiveD2CToTwinGraph
    {
        static readonly string deviceIdKey = "iothub-connection-device-id";
        static readonly string modelIdKey = "dt-dataschema";

        static DigitalTwinsClient adtClient = null;
        static readonly HttpClient httpClient = new HttpClient();

        [FunctionName("ReceiveD2CToTwinGraph")]
        public static async Task Run([EventHubTrigger("d2cmsg-to-adt", Connection = "d2cconnectionstring")] EventData[] events, ILogger log)
        {
            var exceptions = new List<Exception>();

            if (adtClient == null)
            {
                var configuration = new ConfigurationBuilder().AddJsonFile("local.settings.json", true).AddEnvironmentVariables().Build();
                string adtUrl = configuration.GetConnectionString("ADT");
                // var credential = new ManagedIdentityCredential("https://digitaltwins.azure.net");
                var credential = new DefaultAzureCredential();
                adtClient = new DigitalTwinsClient(new Uri(adtUrl), credential,
                        new DigitalTwinsClientOptions
                        {
                            Transport = new HttpClientTransport(httpClient)
                        });
            }

            foreach (EventData eventData in events)
            {
                try
                {
                    // Replace these two lines with your processing logic.
                    log.LogInformation($"C# Event Hub trigger function processed a message: {eventData.EventBody}");
                    if (eventData.SystemProperties.Count > 0)
                    {
                        log.LogInformation("System Properties:");
                    }
                    foreach (var key in eventData.SystemProperties.Keys)
                    {
                        log.LogInformation($" - {key}:{eventData.SystemProperties[key]}");
                    }
                    if (eventData.Properties.Count > 0)
                    {
                        log.LogInformation("Application Properties:");
                    }
                    foreach (var key in eventData.Properties.Keys)
                    {
                        log.LogInformation($" - {key}:{eventData.Properties[key]}");
                    }

                    string deviceId = "";
                    if (eventData.SystemProperties.ContainsKey(deviceIdKey))
                    {
                        deviceId = (string)eventData.SystemProperties[deviceIdKey];
                    }
                    string modelId = "";
                    if (eventData.SystemProperties.ContainsKey(modelIdKey))
                    {
                        modelId = (string)eventData.SystemProperties[modelIdKey];
                    }
                    log.LogInformation($"deviceId={deviceId},modelId={modelId}");
                    if (!string.IsNullOrEmpty(deviceId) && !string.IsNullOrEmpty(modelId))
                    {
                        log.LogInformation("Try update!");
                        var dataContents = new Dictionary<string, object>();
                        var eventBody = Newtonsoft.Json.JsonConvert.DeserializeObject($"{eventData.EventBody}");
                        if (eventBody is JObject)
                        {
                            var eventBodyJObject = (JObject)eventBody;
                            foreach (var property in eventBodyJObject.Properties())
                            {
                                string propertyName = property.Name;
                                foreach (var child in property.Children())
                                {
                                    dataContents.Add(propertyName, JsonHelper.GetJsonContent(child, dataContents));
                                }
                            }
                        }
                        await TwinGraphHelper.UpdateTwinGraphAsync(adtClient, deviceId, dataContents, log);
                    }

                    await Task.Yield();
                }
                catch (Exception e)
                {
                    // We need to keep processing the rest of the batch - capture this exception and continue.
                    // Also, consider capturing details of the message that failed processing so it can be processed again later.
                    exceptions.Add(e);
                }
            }
        }

    }
}
