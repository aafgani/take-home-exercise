using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.UseCases.Listings.GetListings
{
    public class GetListingsValidator : AbstractValidator<GetListingsCommand>
    {
        public GetListingsValidator()
        {
            RuleFor(x => x.Suburb)
                .NotEmpty();
        }
    }
}
