using Domains.Ordering.DomainModels.ServiceMethods;
using Domains.Ordering.Interfaces.ServiceMethods;
using Infrastructure.EventPublisher.Interfaces;
using Logging.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domains.Ordering.Commands.ServiceMethods
{
    public class AddServiceMethodCommandHandler : IRequestHandler<AddServiceMethodCommand, Guid>
    {
        private readonly IServiceMethodCommandRepository _serviceMethodRepository;
        private readonly ILogger _logger;
        private readonly IEventPublisher<INotification> _eventPublisher;

        public AddServiceMethodCommandHandler(IServiceMethodCommandRepository serviceMethodRepository, ILogger logger, IEventPublisher<INotification> eventPublisher)
        {
            _serviceMethodRepository = serviceMethodRepository ?? throw new ArgumentNullException(nameof(serviceMethodRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventPublisher = eventPublisher ?? throw new ArgumentNullException(nameof(eventPublisher));
        }

        public async Task<Guid> Handle(AddServiceMethodCommand request, CancellationToken cancellationToken)
        {
            _logger.Debug("Handling AddServiceMethodCommand");

            var serviceMethod = MapServiceMethod(request);

            await _serviceMethodRepository.Store(serviceMethod).ConfigureAwait(false);

            foreach (var @event in serviceMethod.PendingChanges)
            {
                _eventPublisher.Publish(@event);
            }

            return serviceMethod.Id;
        }

        private ServiceMethod MapServiceMethod(AddServiceMethodCommand command)
        {
            var details = new ServiceMethodDetails(command.Name);

            return ServiceMethod.CreateNew(details);
        }
    }
}
