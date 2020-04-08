using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WhiskyArchive.BuildingBlocks.EventBus.Abstractions
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
