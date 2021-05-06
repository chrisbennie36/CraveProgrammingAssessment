﻿using Domains.Ordering.Commands.Orders;
using Domains.Ordering.Interfaces.Products;
using Domains.Ordering.Interfaces.ServiceMethods;
using FluentValidation;
using System;

namespace Domains.Ordering.Validation.Orders
{
    public class PlaceOrdersCommandValidator : AbstractValidator<PlaceOrdersCommand>
    {
        private readonly IProductCommandRepository _productRepository;
        private readonly IServiceMethodCommandRepository _serviceMethodRepository;

        public PlaceOrdersCommandValidator(IProductCommandRepository productRepository, IServiceMethodCommandRepository serviceMethodRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _serviceMethodRepository = serviceMethodRepository ?? throw new ArgumentNullException(nameof(serviceMethodRepository));

            RuleFor(itm => itm.CustomerDetails).NotNull().WithMessage(ErrorMessages.NoCustomerDataSupplied);
            RuleFor(itm => itm.CustomerDetails.Name).NotEmpty().When(itm => itm.CustomerDetails != null).WithErrorCode(ErrorMessages.NoCustomerNameSupplied);
            RuleForEach(itm => itm.Orders).SetValidator(new OrderValidator(_productRepository, _serviceMethodRepository));
        }
    }
}
