using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using WhiskyArchive.BuildingBlocks.EventBus.Abstractions;
using WhiskyArchive.BuildingBlocks.EventBus.Events;
using WhiskyArchive.BuildingBlocks.EventBus.IntegrationEventLogEF.Services;
using WhiskyArchive.BuildingBlocks.EventBus.IntegrationEventLogEF.Utilities;
using WhiskyArchive.Services.WhiskyRecording.Infrastructure;

namespace WhiskyArchive.Services.WhiskyRecording.API.Application.IntegrationEvents
{
    public class WhiskyRecordingIntegrationEventService : IWhiskyRecordingIntegrationEventService
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly WhiskyRecordingContext _whiskyRecordingContext;
        private readonly IIntegrationEventLogService _eventLogService;

        public WhiskyRecordingIntegrationEventService(IEventBus eventBus, WhiskyRecordingContext whiskyRecordingContext,
        Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory)
        {
            _whiskyRecordingContext = whiskyRecordingContext ?? throw new ArgumentNullException(nameof(whiskyRecordingContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _integrationEventLogServiceFactory(_whiskyRecordingContext.Database.GetDbConnection());
        }

        public async Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            await SaveEventAndOrderingContextChangesAsync(evt);
            _eventBus.Publish(evt);
            //await _eventLogService.MarkEventAsPublishedAsync(evt);
        }

        private async Task SaveEventAndOrderingContextChangesAsync(IntegrationEvent evt)
        {
            //Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
            //See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency            
            await ResilientTransaction.New(_whiskyRecordingContext)
                .ExecuteAsync(async () => {
                    // Achieving atomicity between original ordering database operation and the IntegrationEventLog thanks to a local transaction
                    await _whiskyRecordingContext.SaveChangesAsync();
                    //await _eventLogService.SaveEventAsync(evt, _executionContext.Database.CurrentTransaction.GetDbTransaction());
                });
        }
    }
}
