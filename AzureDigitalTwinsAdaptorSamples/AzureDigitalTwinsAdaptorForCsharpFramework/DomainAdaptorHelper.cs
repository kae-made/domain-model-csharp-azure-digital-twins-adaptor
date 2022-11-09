// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using ADTTestModel.Adaptor;
using Azure.Identity;
using Kae.DomainModel.Csharp.Framework.Adaptor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Kae.DomainModel.CSharp.Utility.Application.AzureDigitalTwinsFunction
{
    internal static class DomainAdaptorHelper
    {
        public static DomainModelAdaptor GetDomainAdaptor(IConfigurationRoot configuration,ILogger log)
        {
            var domainModelAdaptor = DomainModelAdaptorEntry.GetAdaptor(new WebAPILogger(log));
            var configForDomainModel = new Dictionary<string, IDictionary<string, object>>();
            var domainModelConfigKeys = domainModelAdaptor.ConfigurationKeys();
            foreach (var eeKey in domainModelConfigKeys.Keys)
            {
                configForDomainModel.Add(eeKey, new Dictionary<string, object>());
                if (eeKey == "AzureDigitalTwins")
                {
                    string adtInstanceUriKey = "ADTInstanceUri";
                    configForDomainModel[eeKey].Add(adtInstanceUriKey, configuration.GetConnectionString(adtInstanceUriKey));
                    string adtCredentialKey = "ADTCredential";
                    configForDomainModel[eeKey].Add(adtCredentialKey, new DefaultAzureCredential());
                }
                else
                {
                    foreach (var configKey in domainModelConfigKeys[eeKey])
                    {
                        configForDomainModel[eeKey].Add(configKey, configuration.GetConnectionString(configKey));
                    }
                }
            }
            domainModelAdaptor.Initialize(configForDomainModel);
            log.LogInformation("Domain Model Adaptoer has been initialized.");

            return domainModelAdaptor;
        }
    }
}
