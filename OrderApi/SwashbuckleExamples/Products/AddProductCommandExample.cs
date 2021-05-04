using Domains.Products.Commands;
using Swashbuckle.AspNetCore.Filters;

namespace OrderingApi.SwashbuckleExamples.Products
{
    public class AddProductCommandExample : IExamplesProvider<AddProductCommand>
    {
        AddProductCommand IExamplesProvider<AddProductCommand>.GetExamples()
        {
            return new AddProductCommand
            {
                Name = Domains.Products.Enums.ProductName.Dinner,
                Type = Domains.Products.Enums.ProductType.Food
            };
        }
    }
}

