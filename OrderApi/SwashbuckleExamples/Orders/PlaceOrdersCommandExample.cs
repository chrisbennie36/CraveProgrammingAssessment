using Domains.Ordering.Commands.Orders;
using Domains.Ordering.Dtos.Ordering;
using Domains.Ordering.Dtos.Orders;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;

namespace OrderingApi.SwashbuckleExamples
{
    public class PlaceOrdersCommandExample : IExamplesProvider<PlaceOrdersCommand>
    {
        PlaceOrdersCommand IExamplesProvider<PlaceOrdersCommand>.GetExamples()
        {
            return new PlaceOrdersCommand
            {
                CustomerDetails = new CustomerDetailsDto
                {
                    Email = "test@test.com",
                    Name = "TEST",
                    PhoneNumber = "1233456"
                },
                Orders = new List<OrderDto>
                {
                    new OrderDto
                    {
                        ProductId = Guid.NewGuid(),
                        ServiceMethodId = Guid.NewGuid(),
                        AdditionalModifiers = new AdditionalModifiersDto
                        {
                            Additions = 1,
                            PreparationMethod = "Rare"
                        }
                    }
                }
            };
        }
    }
}
