using Domains.Ordering.Events.ServiceMethods;
using Infrastructure.Aggregate.AggregateRoot;
using System;

namespace Domains.Ordering.DomainModels.ServiceMethods
{
    public class ServiceMethod : AggregateRoot
    {
        public ServiceMethod(Guid id, ServiceMethodDetails details)
        {
            Id = id;
            Details = details;
        }

        public static ServiceMethod CreateNew(ServiceMethodDetails details)
        {
            var id = Guid.NewGuid();

            var serviceMethod = new ServiceMethod(id, details);

            serviceMethod.RegisterEvent(new ServiceMethodAddedEvent
            {
                Id = serviceMethod.Id,
                Name = serviceMethod.Details.Name
            });

            return serviceMethod;
        }

        public Guid Id { get; }
        public ServiceMethodDetails Details { get; }
    }
}
