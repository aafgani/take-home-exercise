using API.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static API.Core.Models.Enums;

namespace API.Core.UseCases.Listings.GetListings
{
    public class GetListingsCommand : BasePaged, IRequest<EventResult<PagedResult<Listing>>>
    {
        public string Suburb { get; set; }
        public CategoryType CategoryType { get; set; }
        public StatusType StatusType { get; set; }
    }
}
