using API.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API.Core.UseCases.Listings.GetListings
{
    public class GetListingsHandler : IRequestHandler<GetListingsCommand, EventResult<PagedResult<Listing>>>
    {
        private IListManager _listManager;

        public GetListingsHandler(IListManager listManager)
        {
            _listManager = listManager;
        }

        public async Task<EventResult<PagedResult<Listing>>> Handle(GetListingsCommand request, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                PagedResult<Listing> listings = _listManager.GetListings(request.Suburb, request.CategoryType, request.StatusType, request.Offset, request.Total);

                return new EventResult<PagedResult<Listing>>
                {
                    Result = listings,
                };
            });
        }
    }
}
