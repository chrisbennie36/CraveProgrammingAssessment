using Domains.Orders.Commands;
using Domains.Orders.Interfaces;
using FluentValidation;
using System;

namespace Domains.Orders.Validation
{
    public class PlaceOrdersCommandValidator : AbstractValidator<PlaceOrdersCommand>
    {
        private readonly IProductIdsQueryRepository _productIdsQueryRepository;

        public PlaceOrdersCommandValidator(IProductIdsQueryRepository productIdsQueryRepository)
        {
            _productIdsQueryRepository = productIdsQueryRepository ?? throw new ArgumentNullException(nameof(productIdsQueryRepository));

            RuleForEach(itm => itm.Orders).SetValidator(new OrderValidator(_productIdsQueryRepository));
        }
    }
}
