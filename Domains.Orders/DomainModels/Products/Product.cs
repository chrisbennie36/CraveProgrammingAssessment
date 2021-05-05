using Domains.Ordering.Events.Products;
using Infrastructure.Aggregate.AggregateRoot;
using System;

namespace Domains.Ordering.DomainModels.Products
{
    public class Product : AggregateRoot
    {
        public Product(Guid id, ProductDetails details, bool isActive)
        {
            Id = id;
            Details = details;
            _isActive = isActive;
        }

        public static Product CreateNew(ProductDetails details, bool isActive)
        {
            var id = Guid.NewGuid();

            var product = new Product(id, details, isActive);

            product.RegisterEvent(new ProductAddedEvent
            {
                Id = product.Id,
                Name = product.Details.Name,
                Type = product.Details.Type,
                IsActive = isActive
            });

            return product;
        }

        public Guid Id { get; }
        public ProductDetails Details { get; }

        private bool _isActive;
        public bool IsActive 
        {
            get 
            {
                return _isActive;
            }
            set
            {
                if (value == false && _isActive == true)
                {
                    _isActive = value;
                    RegisterEvent(new ProductDeactivatedEvent { Id = Id, Type = Details.Type });
                }

                if (value == true && _isActive == false)
                {
                    _isActive = value;
                    RegisterEvent(new ProductActivatedEvent { Id = Id, Type = Details.Type });
                }
            }
        }
    }
}
