using Domains.Ordering.Enums.Products;

namespace Domains.Ordering.Events.Products
{
    public class ProductAddedEvent : BaseEvent
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
    }
}
