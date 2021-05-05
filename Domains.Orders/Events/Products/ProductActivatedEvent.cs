using Domains.Ordering.Enums.Products;

namespace Domains.Ordering.Events.Products
{
    public class ProductActivatedEvent : BaseEvent
    {
        public ProductType Type { get; set; }
    }
}
