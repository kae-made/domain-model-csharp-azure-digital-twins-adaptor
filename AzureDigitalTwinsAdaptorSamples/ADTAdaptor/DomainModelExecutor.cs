// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Kae.DomainModel.Csharp.Framework.Adaptor;
using Microsoft.Extensions.Configuration;
using System.Web.Http;

namespace Kae.DomainModel.CSharp.Utility.Application.AzureDigitalTwinsFunction
{
    public static class DomainModelExecutor
    {
        static DomainModelAdaptor domainModelAdaptor;

        [FunctionName("DomainModelExecutor")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
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
                    return new InternalServerErrorResult();
                }
            }

            string classKeyLetter = "";
            if (req.Query.ContainsKey("classkeyletter"))
            {
                classKeyLetter = req.Query["classkeyletter"];
            }

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<RequestingParameters>(requestBody);

            ObjectResult resultResponse = null;
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(classKeyLetter))
                {
                    switch (data.OpType)
                    {
                        case "operation":
                            log.LogInformation($"Invoking '{data.Name}'...");
                            result = domainModelAdaptor.InvokeDomainOperation(data.Name, data);
                            log.LogInformation($"Invoked '{data.Name}'");
                            break;
                        default:
                            result = $"operator should be 'operation' but specified is '{data.OpType}";
                            log.LogError($"operator should be 'operation' but specified is '{data.OpType}'");
                            resultResponse = new BadRequestObjectResult(result);
                            break;
                    }
                }
                else
                {
                    switch (data.OpType)
                    {
                        case "operation":
                            log.LogInformation($"Invoking '{classKeyLetter}.{data.Name}'...");
                            result = domainModelAdaptor.InvokeDomainClassOperation(classKeyLetter, data.Name, data);
                            log.LogInformation($"Invoked '{classKeyLetter}.{data.Name}'");
                            break;
                        case "event":
                            log.LogInformation($"Sending '{classKeyLetter}.{data.Name}'...");
                            result = domainModelAdaptor.SendEvent(classKeyLetter, data.Name, data);
                            log.LogInformation($"Send '{classKeyLetter}.{data.Name}'");
                            break;
                        default:
                            log.LogError($"operator should be 'operation' or 'event' but specified is '{data.OpType}'");
                            resultResponse = new BadRequestObjectResult(result);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                log.LogError($"Invoke behavior action has been faild - {ex.Message}");
                result = ex.Message;
                return new InternalServerErrorResult();
            }

            return new OkObjectResult(result);
        }
    }
}
