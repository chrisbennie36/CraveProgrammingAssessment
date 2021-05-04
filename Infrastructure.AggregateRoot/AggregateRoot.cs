using MediatR;
using System;
using System.Collections.Generic;

namespace Infrastructure.Aggregate.AggregateRoot
{
    public abstract class AggregateRoot
    {
        private List<INotification> _pendingChanges = new List<INotification>();

        public void RegisterEvent(INotification @event)
        {
            if (@event == null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            _pendingChanges.Add(@event);
        }

        public bool HasChanges()
        {
            return _pendingChanges.Count > 0;
        }

        public IReadOnlyList<INotification> PendingChanges
        {
            get
            {
                return _pendingChanges;
            }
        }
    }
}
