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
    public class GetListings
    {
        private readonly ILogger<GetListings> _logger;
        private readonly IMediator _mediator;

        public GetListings(ILogger<GetListings> logger, IMediator mediator)
        {
            _logger = logger;
            this._mediator = mediator;
        }

        [Function("GetListings")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req,
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

            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new BadRequestResult();
            }
        }
    }
}
