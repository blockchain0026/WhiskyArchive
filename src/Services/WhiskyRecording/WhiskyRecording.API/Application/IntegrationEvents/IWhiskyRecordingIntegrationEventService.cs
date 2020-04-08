using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhiskyArchive.BuildingBlocks.EventBus.Events;

namespace WhiskyArchive.Services.WhiskyRecording.API.Application.IntegrationEvents
{
    public interface IWhiskyRecordingIntegrationEventService
    {
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
