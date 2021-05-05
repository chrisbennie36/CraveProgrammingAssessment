using Domains.Products.Enums;
using System;

namespace Domains.Products.QueryModels
{
    public class ProductQueryModel
    {
        public Guid Id { get; set; }
        public ProductName Name { get; set; }
        public ProductType Type { get; set; }
        public bool IsActive { get; set; }
    }
}
