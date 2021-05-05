using Domains.Orders.Interfaces.ServiceMethods;
using Domains.Orders.Validation;
using Domains.Products.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Domains.Orders.UnitTests.Validation
{
    public class OrderValidatorTests
    {
        private Mock<IProductCommandRepository> _productRepository;
        private Mock<IServiceMethodCommandRepository> _serviceMethodRepository;

        private OrderValidator _validator;

        public OrderValidatorTests()
        {
            _productRepository = new Mock<IProductCommandRepository>();
            _serviceMethodRepository = new Mock<IServiceMethodCommandRepository>();

            _validator = new OrderValidator(_productRepository.Object, _serviceMethodRepository.Object);
        }

        [Fact]
        public void OrderValidatorTests_ThrowsError_WhenRequstedProductDoesNotExist()
        {
            var serviceMethodId = Guid.NewGuid();

            _productRepository.Setup(r => r.GetProductIds()).ReturnsAsync(new List<Guid>());
            _serviceMethodRepository.Setup(r => r.GetServiceMethodIds()).ReturnsAsync(new List<Guid> { serviceMethodId });

            var result = _validator.Validate(new Dtos.OrderDto { ProductId = Guid.NewGuid(), ServiceMethodId = serviceMethodId, AdditionalInstructions = "UnitTest" });

            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
        }

        [Fact]
        public void OrderValidatorTests_ThrowsError_WhenRequstedProductIsNotAvailable()
        {
            var productId = Guid.NewGuid();
            var serviceMethodId = Guid.NewGuid();

            _productRepository.Setup(r => r.GetActiveProductIds()).ReturnsAsync(new List<Guid> { productId});
            _serviceMethodRepository.Setup(r => r.GetServiceMethodIds()).ReturnsAsync(new List<Guid> { serviceMethodId });

            var result = _validator.Validate(new Dtos.OrderDto { ProductId = productId, ServiceMethodId = serviceMethodId, AdditionalInstructions = "UnitTest" });

            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
        }

        [Fact]
        public void OrderValidatorTests_ThrowsError_WhenRequstedServiceMethodDoesNotExist()
        {
            var productId = Guid.NewGuid();

            _productRepository.Setup(r => r.GetProductIds()).ReturnsAsync(new List<Guid> { productId });
            _serviceMethodRepository.Setup(r => r.GetServiceMethodIds()).ReturnsAsync(new List<Guid>());

            var result = _validator.Validate(new Dtos.OrderDto { ProductId = productId, ServiceMethodId = Guid.NewGuid(), AdditionalInstructions = "UnitTest" });

            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
        }
    }
}
