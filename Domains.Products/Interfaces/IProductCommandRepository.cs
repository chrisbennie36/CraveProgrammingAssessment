using Domains.Products.DomainModels;
using System.Threading.Tasks;

namespace Domains.Products.Interfaces
{
    public interface IProductCommandRepository
    {
        Task Store(Product product);
    }
}
