using Domains.Products.Enums;

namespace Domains.Products.DomainModels
{
    public class ProductDetails
    {
        public ProductDetails(ProductName name, ProductType type)
        {
            Name = name;
            Type = type;
        }

        public ProductName Name { get; }
        public ProductType Type { get; }
    }
}
