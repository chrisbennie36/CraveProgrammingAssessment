using Domains.Ordering.Enums.Products;

namespace Domains.Ordering.DomainModels.Products
{
    public class ProductDetails
    {
        public ProductDetails(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }
        public string Type { get; }
    }
}
