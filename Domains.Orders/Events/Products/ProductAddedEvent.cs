using Domains.Ordering.Enums.Products;

namespace Domains.Ordering.Events.Products
{
    public class ProductAddedEvent : BaseEvent
    {
        public string Name { get; set; }
        public ProductType Type { get; set; }
        public bool IsActive { get; set; }
    }
}
