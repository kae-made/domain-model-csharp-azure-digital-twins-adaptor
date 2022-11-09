// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.Core.Pipeline;
using Azure.DigitalTwins.Core;
using Azure.Identity;
using Azure.Messaging.EventHubs;
using Kae.Utility.Json;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Kae.DomainMode.Csharp.AzureDigitalTwins.AzureIoTHubBinder
{
    public static class ReceiveReportedPropertiesToTwinGraph
    {
        static readonly string deviceIdKey = "iothub-connection-device-id";
        static readonly string modelIdKey = "dt-dataschema";

        static DigitalTwinsClient adtClient = null;
        static readonly HttpClient httpClient = new HttpClient();

        [FunctionName("ReceiveReportedPropertiesToTwinGraph")]
        public static async Task Run([EventHubTrigger("rpupdate-to-adt", Connection = "rpuconnectionstring")] EventData[] events, ILogger log)
        {
            var exceptions = new List<Exception>();

            try
            {
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
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
            }

            foreach (EventData eventData in events)
            {
                try
                {
                    // Replace these two lines with your processing logic.
                    log.LogInformation($"C# Event Hub trigger function processed a message: {eventData.EventBody}");
                    string deviceId = "";
                    if (eventData.SystemProperties.Count > 0)
                    {
                        log.LogInformation("System Propertyes - ");
                    }
                    foreach (var pk in eventData.SystemProperties.Keys)
                    {
                        log.LogInformation($"  {pk}={eventData.SystemProperties[pk]}");
                        if (pk == deviceIdKey)
                        {
                            deviceId = eventData.SystemProperties[pk] as string;
                        }
                    }
                    if (eventData.Properties.Count > 0)
                    {
                        log.LogInformation("Application Properties - ");
                    }
                    foreach (var pk in eventData.Properties.Keys)
                    {
                        log.LogInformation($"  {pk}={eventData.Properties[pk]}");
                    }

                    dynamic receivedData = Newtonsoft.Json.JsonConvert.DeserializeObject($"{eventData.EventBody}");
                    if ((!string.IsNullOrEmpty(deviceId)) && (!(receivedData.properties is null)))
                    {
                        if (!(receivedData.properties.reported is null))
                        {
                            log.LogInformation("Try update!");
                            var reportedContens = receivedData.properties.reported;
                            var dataContents = JsonHelper.GetJsonContent(reportedContens, null) as IDictionary<string, object>;
                            await TwinGraphHelper.UpdateTwinGraphAsync(adtClient, deviceId, dataContents, log);
                            log.LogInformation($"Updated Twin Properties of {deviceId}");
                        }
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

            // Once processing of the batch is complete, if any messages in the batch failed processing throw an exception so that there is a record of the failure.

            if (exceptions.Count > 1)
                throw new AggregateException(exceptions);

            if (exceptions.Count == 1)
                throw exceptions.Single();
        }
    }
}
