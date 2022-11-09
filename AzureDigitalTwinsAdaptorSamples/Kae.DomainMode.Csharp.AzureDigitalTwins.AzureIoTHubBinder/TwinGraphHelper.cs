using Azure;
// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Azure.DigitalTwins.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kae.DomainMode.Csharp.AzureDigitalTwins.AzureIoTHubBinder
{
    internal static class TwinGraphHelper
    {
        public static async Task UpdateTwinGraphAsync(DigitalTwinsClient adtClient, string deviceId, IDictionary<string, object> dataContents, ILogger log)
        {
            var updateTwins = new JsonPatchDocument();
            foreach (var propKey in dataContents.Keys)
            {
                if (!propKey.StartsWith("$"))
                {
                    log.LogInformation($"Updating {propKey}=>{dataContents[propKey]} of {deviceId}");
                    updateTwins.AppendReplace($"/{propKey}", dataContents[propKey]);
                    try
                    {
                        await adtClient.UpdateDigitalTwinAsync(deviceId, updateTwins);
                        log.LogInformation("Replaced");
                    }
                    catch (Exception ex)
                    {
                        log.LogInformation($"{ex.Message}");
                        log.LogInformation($"{propKey} of {deviceId} has not been added.");
                        updateTwins = new JsonPatchDocument();
                        updateTwins.AppendAdd($"/{propKey}", dataContents[propKey]);
                        await adtClient.UpdateDigitalTwinAsync(deviceId, updateTwins);
                        log.LogInformation("Added");
                    }
                }
            }
        }
    }
}
