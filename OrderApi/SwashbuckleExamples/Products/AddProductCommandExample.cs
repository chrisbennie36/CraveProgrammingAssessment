using Domains.Ordering.Commands.Products;
using Domains.Ordering.Enums.Products;
using Swashbuckle.AspNetCore.Filters;

namespace OrderingApi.SwashbuckleExamples.Products
{
    public class AddProductCommandExample : IExamplesProvider<AddProductCommand>
    {
        AddProductCommand IExamplesProvider<AddProductCommand>.GetExamples()
        {
            return new AddProductCommand
            {
                Name = ProductName.Delerium,
                Type = ProductType.Drink
            };
        }
    }
}

