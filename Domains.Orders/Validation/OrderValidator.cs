using Domains.Orders.Dtos;
using Domains.Orders.Interfaces;
using FluentValidation;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Domains.Orders.Validation
{
    public class OrderValidator : AbstractValidator<OrderDto>
    {
        private readonly IProductIdsQueryRepository _productIdsQueryRepository;

        public OrderValidator(IProductIdsQueryRepository productIdsQueryRepository)
        {
            _productIdsQueryRepository = productIdsQueryRepository ?? throw new ArgumentNullException(nameof(productIdsQueryRepository));

            RuleFor(itm => itm).MustAsync(async (o, cancellation) => await ProductExists(o.ProductId).ConfigureAwait(false)).WithErrorCode(ErrorCodes.ProductDoesNotExist);
        }

        private async Task<bool> ProductExists(Guid productId)
        {
            var existingProducts = await _productIdsQueryRepository.GetProductIds().ConfigureAwait(false);

            return existingProducts.Contains(productId);
        }
    }
}
