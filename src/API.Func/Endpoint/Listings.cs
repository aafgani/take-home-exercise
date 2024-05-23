using API.Core.UseCases.Listings.GetListings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static API.Core.Models.Enums;

namespace API.Func.Endpoint
{
    public class Listings
    {
        private readonly ILogger<Listings> _logger;
        private readonly IMediator _mediator;

        public Listings(ILogger<Listings> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Function(FunctionName.GetListings)]
        public async Task<IActionResult> GetAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req,
        string suburb, CategoryType categoryType = CategoryType.None, StatusType statusType = StatusType.None,
        int skip = 0, int take = 10)
        {
            try
            {
                var command = new GetListingsCommand
                {
                    CategoryType = categoryType,
                    StatusType = statusType,
                    Suburb = suburb,
                    Offset = skip,
                    Total = take
                };
                var result = await _mediator.Send(command);

                if (result.IsError)
                    return new BadRequestObjectResult(string.Join(",", result.Error));

                return new OkObjectResult(result.Result);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        [Function(FunctionName.InsertListing)]
        public async Task<IActionResult> PostAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                _logger.LogInformation(requestBody);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }

            return new OkObjectResult("Ok");
        }
    }
}
