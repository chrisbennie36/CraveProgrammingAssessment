using Domains.Ordering.Enums.Products;

namespace Domains.Ordering.QueryModels.Products
{
    public class ProductQueryModel : BaseQueryModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
    }
}
