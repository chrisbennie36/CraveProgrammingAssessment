using Domains.Ordering.DomainModels.Orders;
using System.Threading.Tasks;

namespace Domains.Ordering.Interfaces.Orders
{
    public interface IOrderCommandRepository
    {
        Task Store(Order order);
    }
}
