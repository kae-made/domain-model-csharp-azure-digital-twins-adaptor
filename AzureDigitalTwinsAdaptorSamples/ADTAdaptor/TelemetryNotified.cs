// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Azure.Messaging.EventGrid;
using Kae.Utility.Json;
using System.Collections.Generic;
using Kae.DomainModel.Csharp.Framework.Adaptor;
using Microsoft.Extensions.Configuration;

namespace Kae.DomainModel.CSharp.Utility.Application.AzureDigitalTwinsFunction
{
    public static class TelemetryNotified
    {
        static DomainModelAdaptor domainModelAdaptor;

        [FunctionName("TelemetryNotified")]
        public static void Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        {
            string twinId = eventGridEvent.Id;
            string receivedData = System.Text.Encoding.UTF8.GetString(eventGridEvent.Data);
            log.LogInformation($"{receivedData} to '{twinId}'");

            if (domainModelAdaptor == null)
            {
                var configuration = new ConfigurationBuilder().AddJsonFile("local.settings.json", true).AddEnvironmentVariables().Build();
                try
                {
                    domainModelAdaptor = DomainAdaptorHelper.GetDomainAdaptor(configuration, log);
                }
                catch (Exception ex)
                {
                    log.LogError(ex.Message);
                    return;
                }
            }

            try
            {
                var identites = new Dictionary<string, string>();
                var twinIdFrags = twinId.Split(new char[] { ';' });
                foreach (var twinIdFrag in twinIdFrags)
                {
                    var idFrags = twinIdFrag.Split(new char[] { '=' });
                    identites.Add(idFrags[0], idFrags[1]);
                }
                dynamic receivedJson = Newtonsoft.Json.JsonConvert.DeserializeObject(receivedData);
                var receivedEvent = JsonHelper.GetJsonContent(receivedJson.data, null) as IDictionary<string, object>;
                foreach (var eventKey in receivedEvent.Keys)
                {
                    string evtClassKLNumbPart = eventKey.Substring(0, eventKey.LastIndexOf("_"));
                    string classKeyLetter = evtClassKLNumbPart.Substring(0, evtClassKLNumbPart.LastIndexOf("_"));
                    var sendResult = domainModelAdaptor.SendEvent(classKeyLetter, eventKey, new RequestingParameters() { Name = eventKey, Identities = identites, Parameters = (IDictionary<string, object>)receivedEvent[eventKey] });
                    log.LogInformation($"Send '{eventKey}' to '{twinId}'");
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
            }
        }
    }
}
