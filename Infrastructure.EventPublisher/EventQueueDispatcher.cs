using Infrastructure.EventPublisher.Interfaces;
using Logging.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.EventPublisher
{
    public class EventQueueDispatcher
    {
        private const int PollIntervalInMilliseconds = 150;

        private readonly IMediator _mediator;
        private readonly IQueue<INotification> _queue;
        private readonly ILogger _logger;
        private readonly Thread _thread;

        /// <summary>
        /// Initializes a new instance of <see cref="EventQueueDispatcher"/>
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="queue"></param>
        public EventQueueDispatcher(IMediator mediator, IQueue<INotification> queue, ILogger logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _queue = queue ?? throw new ArgumentNullException(nameof(queue));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _thread = new Thread(async () => await Run().ConfigureAwait(false));
        }

        /// <summary>
        /// Starts the notification dispatch
        /// </summary>
        public void Start()
        {
            _thread.Start();
        }

        private async Task PublishEvent(INotification notification)
        {
            await _mediator.Publish(notification).ConfigureAwait(false);
        }

        private async Task Run()
        {
            while (true)
            {
                INotification notification;
                while ((notification = _queue.Dequeue()) != null)
                {
                    try
                    {
                        await PublishEvent(notification).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, Guid.NewGuid());
                    }
                }

                Thread.Sleep(PollIntervalInMilliseconds);
            }
        }
    }
}
