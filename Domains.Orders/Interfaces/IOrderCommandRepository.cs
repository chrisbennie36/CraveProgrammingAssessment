using Domains.Orders.DomainModels;
using System.Threading.Tasks;

namespace Domains.Orders.Interfaces
{
    public interface IOrderCommandRepository
    {
        Task Store(Order order);
    }
}
