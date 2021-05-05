using Domains.Products.Enums;
using Microsoft.Azure.Cosmos.Table;
using System;

namespace Domains.Orders.Repositories.Entities
{
    public class ProductEntity : TableEntity
    {
        public Guid Id { get; set; }
        public ProductName Name { get; set; }
        public ProductType Type { get; set; }
        public bool IsActive { get; set; }
    }
}
