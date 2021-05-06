using Domains.Ordering.Commands.Orders;
using Domains.Ordering.Dtos.Orders;
using Domains.Ordering.Interfaces.Products;
using Domains.Ordering.Interfaces.ServiceMethods;
using Domains.Ordering.Validation.Orders;
using Moq;
using System.Linq;
using Xunit;

namespace Domains.Ordering.UnitTests.Validation.Orders
{
    public class PlaceOrdersCommandValidatorTests
    {
        private Mock<IProductCommandRepository> _productRepository;
        private Mock<IServiceMethodCommandRepository> _serviceMethodRepository;

        private PlaceOrdersCommandValidator _validator;

        public PlaceOrdersCommandValidatorTests()
        {
            _productRepository = new Mock<IProductCommandRepository>();
            _serviceMethodRepository = new Mock<IServiceMethodCommandRepository>();

            _validator = new PlaceOrdersCommandValidator(_productRepository.Object, _serviceMethodRepository.Object);
        }

        [Fact]
        public void PlaceOrdersCommandValidatorTests_ThrowsError_WhenNoCustomerDataSupplied()
        {
            var command = new PlaceOrdersCommand();

            var result = _validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Equal(ErrorMessages.NoCustomerDataSupplied, result.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void PlaceOrdersCommandValidatorTests_ThrowsError_WhenNoCustomerNameSupplied()
        {
            var command = new PlaceOrdersCommand { CustomerDetails = new CustomerDetailsDto() };

            var result = _validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Equal(ErrorMessages.NoCustomerNameSupplied, result.Errors.Single().ErrorMessage);
        }
    }
}
