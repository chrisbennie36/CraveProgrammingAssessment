using Domains.Ordering.Enums.Products;
using Domains.Ordering.Events;

namespace Domains.Ordering.Events.Products
{
    public class ProductDeactivatedEvent : BaseEvent
    {
        public ProductType Type { get; set; }
    }
}
