using Domains.Ordering.QueryModels.Products;
using Domains.Ordering.QueryModels.ServiceMethods;

namespace Domains.Ordering.QueryModels.Orders
{
    public class OrderQueryModel : BaseQueryModel
    {
        public ProductQueryModel Product { get; set; }
        public ServiceMethodQueryModel ServiceMethod { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmail { get; set; }
        public int Additions { get; set; }
        public string PreparationMethod { get; set; }
    }
}
