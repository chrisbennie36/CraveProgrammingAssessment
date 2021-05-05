using Domains.Orders.Commands;
using Domains.Orders.Interfaces.ServiceMethods;
using Domains.Products.Interfaces;
using FluentValidation;
using System;

namespace Domains.Orders.Validation
{
    public class PlaceOrdersCommandValidator : AbstractValidator<PlaceOrdersCommand>
    {
        private readonly IProductCommandRepository _productRepository;
        private readonly IServiceMethodCommandRepository _serviceMethodRepository;

        public PlaceOrdersCommandValidator(IProductCommandRepository productRepository, IServiceMethodCommandRepository serviceMethodRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _serviceMethodRepository = serviceMethodRepository ?? throw new ArgumentNullException(nameof(serviceMethodRepository));

            RuleForEach(itm => itm.Orders).SetValidator(new OrderValidator(_productRepository, _serviceMethodRepository));
        }
    }
}
