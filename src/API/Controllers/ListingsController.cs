using System;
using System.Linq;
using System.Threading.Tasks;
using API.Core;
using API.Core.Models;
using API.Core.UseCases.Listings.GetListings;
using API.Infrastructure.Managers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static API.Core.Models.Enums;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class ListingsController : ControllerBase
    {
        private readonly ILogger<ListingsController> _logger;
        private IMediator _mediator;

        public ListingsController(ILogger<ListingsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("getlistings")]
        public async Task<IActionResult> GetListingsAsync(string suburb, CategoryType categoryType = CategoryType.None, StatusType statusType = StatusType.None, int skip = 0, int take = 10)
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
                return BadRequest(string.Join(",", result.Error));

            return Ok(JsonConvert.SerializeObject(result.Result));

        }
    }
}
