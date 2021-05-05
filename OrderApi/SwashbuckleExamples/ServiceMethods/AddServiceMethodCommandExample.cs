using Domains.Orders.Commands.ServiceMethods;
using Swashbuckle.AspNetCore.Filters;

namespace OrderingApi.SwashbuckleExamples.Products
{
    public class AddServiceMethodCommandExample : IExamplesProvider<AddServiceMethodCommand>
    {
        AddServiceMethodCommand IExamplesProvider<AddServiceMethodCommand>.GetExamples()
        {
            return new AddServiceMethodCommand
            {
                Name = "Room Service"
            };
        }
    }
}

