// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Kae.Utility.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.DomainModel.CSharp.Utility.Application.AzureDigitalTwinsFunction
{
    public class WebAPILogger : Logger
    {
        ILogger logger;

        public WebAPILogger(ILogger logger)
        {
            this.logger = logger;
        }

        protected override async Task LogInternal(Level level, string log, string timestamp)
        {
            switch (level)
            {
                case Level.Info:
                    logger.LogInformation($"{timestamp}: {log}");
                    break;
                case Level.Warn:
                    logger.LogWarning($"{timestamp}: {log}");
                    break;
                case Level.Erro:
                    logger.LogError($"{timestamp}: {log}");
                    break;
            }
        }
    }
}
