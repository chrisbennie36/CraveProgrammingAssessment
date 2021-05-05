using Domains.Ordering.Enums.Products;

namespace Domains.Ordering.DomainModels.Products
{
    public class ProductDetails
    {
        public ProductDetails(string name, ProductType type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }
        public ProductType Type { get; }
    }
}
