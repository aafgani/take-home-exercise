using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API.Core.Behaviour
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Handling {typeof(TRequest).Name} : {JsonConvert.SerializeObject(request)}");

            var stopwatch = Stopwatch.StartNew();
            var response = await next();
            stopwatch.Stop();

            logger.LogInformation($"Handled {typeof(TResponse).Name} : {JsonConvert.SerializeObject(response)}");
            logger.LogInformation($" Execution time={stopwatch.ElapsedMilliseconds} ms");

            return response;
        }
    }
}
