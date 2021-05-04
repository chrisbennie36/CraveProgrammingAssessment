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

            RuleFor(itm => itm).MustAsync(async (o, cancellation) => await ProductExists(o.ProductId).ConfigureAwait(false)).WithMessage("Ordered product does not exist");
            RuleFor(itm => itm).MustAsync(async (o, cancellation) => await ProductAvailable(o.ProductId).ConfigureAwait(false)).WithMessage("Ordered product is not currently available");
        }

        private async Task<bool> ProductExists(Guid productId)
        {
            var existingProducts = await _productIdsQueryRepository.GetAllProductIds().ConfigureAwait(false);

            return existingProducts.Contains(productId);
        }

        private async Task<bool> ProductAvailable(Guid productId)
        {
            var availableProducts = await _productIdsQueryRepository.GetActiveProductIds().ConfigureAwait(false);

            return availableProducts.Contains(productId);
        }
    }
}
