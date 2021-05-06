using Domains.Ordering.Dtos.Orders;
using Domains.Ordering.Interfaces.Products;
using Domains.Ordering.Interfaces.ServiceMethods;
using FluentValidation;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Domains.Ordering.Validation.Orders
{
    public class OrderValidator : AbstractValidator<OrderDto>
    {
        private readonly IProductCommandRepository _productRepository;
        private readonly IServiceMethodCommandRepository _serviceMethodRepository;

        public OrderValidator(IProductCommandRepository productRepository, IServiceMethodCommandRepository serviceMethodRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _serviceMethodRepository = serviceMethodRepository ?? throw new ArgumentNullException(nameof(serviceMethodRepository));

            RuleFor(itm => itm).MustAsync(async (o, cancellation) => await ProductExists(o.ProductId).ConfigureAwait(false)).WithMessage(ErrorMessages.OrderedProductDoesNotExist);
            RuleFor(itm => itm).MustAsync(async (o, cancellation) => await ProductAvailable(o.ProductId).ConfigureAwait(false))
                .When(itm => ProductExists(itm.ProductId).Result == true).WithMessage(ErrorMessages.OrdredProductIsNotAvailable);
            RuleFor(itm => itm).MustAsync(async (o, cancellation) => await ServiceMethodExists(o.ServiceMethodId).ConfigureAwait(false)).WithMessage(ErrorMessages.OrderedServiceMethodDoesNotExist);
        }

        private async Task<bool> ProductExists(Guid productId)
        {
            var existingProducts = await _productRepository.GetProductIds().ConfigureAwait(false);

            return existingProducts.Contains(productId);
        }

        private async Task<bool> ServiceMethodExists(Guid serviceMethodId)
        {
            var existingServiceMethods = await _serviceMethodRepository.GetServiceMethodIds().ConfigureAwait(false);

            return existingServiceMethods.Contains(serviceMethodId);
        }

        private async Task<bool> ProductAvailable(Guid productId)
        {
            var availableProducts = await _productRepository.GetActiveProductIds().ConfigureAwait(false);

            return availableProducts.Contains(productId);
        }
    }
}
