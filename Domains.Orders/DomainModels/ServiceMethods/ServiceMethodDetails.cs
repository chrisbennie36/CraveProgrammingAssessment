namespace Domains.Orders.DomainModels.ServiceMethods
{
    public class ServiceMethodDetails
    {
        public ServiceMethodDetails(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
