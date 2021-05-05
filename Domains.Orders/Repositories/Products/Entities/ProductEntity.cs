using Domains.Ordering.Enums.Products;
using Microsoft.Azure.Cosmos.Table;
using System;

namespace Domains.Ordering.Repositories.Products.Entities
{
    public class ProductEntity : TableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ProductType Type { get; set; }
        public bool IsActive { get; set; }
    }
}
