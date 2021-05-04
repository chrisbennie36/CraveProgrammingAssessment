using Domains.Orders.Commands;
using Domains.Orders.Dtos;
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
                        AdditionalInstructions = "Medium Rare"
                    }
                }
            };
        }
    }
}
