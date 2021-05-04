using Domains.Orders.Interfaces;
using Domains.Orders.Validation;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Domains.Orders.UnitTests.Validation
{
    public class OrderValidatorTests
    {
        private Mock<IProductIdsQueryRepository> _repository;

        private OrderValidator _validator;

        public OrderValidatorTests()
        {
            _repository = new Mock<IProductIdsQueryRepository>();

            _validator = new OrderValidator(_repository.Object);
        }

        [Fact]
        public void OrderValidatorTests_ThrowsError_WhenRequstedProductDoesNotExist()
        {
            _repository.Setup(r => r.GetAllProductIds()).ReturnsAsync(new List<Guid>());

            var result = _validator.Validate(new Dtos.OrderDto { ProductId = Guid.NewGuid(), AdditionalInstructions = "UnitTest" });

            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
        }

        [Fact]
        public void OrderValidatorTests_ThrowsError_WhenRequstedProductIsNotAvailable()
        {
            var productId = Guid.NewGuid();

            _repository.Setup(r => r.GetActiveProductIds()).ReturnsAsync(new List<Guid> { productId});

            var result = _validator.Validate(new Dtos.OrderDto { ProductId = productId, AdditionalInstructions = "UnitTest" });

            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
        }
    }
}
