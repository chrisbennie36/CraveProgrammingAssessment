namespace Domains.Ordering.Events.ServiceMethods
{
    public class ServiceMethodAddedEvent : BaseEvent
    {
        public string Name { get; set; }
    }
}
