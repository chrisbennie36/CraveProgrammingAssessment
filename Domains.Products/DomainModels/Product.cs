using Domains.Products.Events;
using Infrastructure.Aggregate.AggregateRoot;
using System;

namespace Domains.Products.DomainModels
{
    public class Product : AggregateRoot
    {
        public Product(ProductDetails details, bool isActive)
        {
            Id = Guid.NewGuid();
            Details = details;
        }

        public static Product CreateNew(ProductDetails details, bool isActive)
        {
            var product = new Product(details, isActive);

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
                    RegisterEvent(new ProductDeactivatedEvent { Id = Id });
                }

                if (value == true && _isActive == false)
                {
                    _isActive = value;
                    RegisterEvent(new ProductActivatedEvent { Id = Id });
                }
            }
        }
    }
}
