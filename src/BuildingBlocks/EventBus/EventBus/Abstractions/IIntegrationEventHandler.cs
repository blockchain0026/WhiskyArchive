using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WhiskyArchive.BuildingBlocks.EventBus.Events;

namespace WhiskyArchive.BuildingBlocks.EventBus.Abstractions
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
         where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }

    public interface IIntegrationEventHandler
    {
    }
}
