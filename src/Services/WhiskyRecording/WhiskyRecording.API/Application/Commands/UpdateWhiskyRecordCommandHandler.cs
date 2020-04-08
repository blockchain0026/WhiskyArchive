using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhiskyArchive.BuildingBlocks.EventBus.Abstractions;
using WhiskyArchive.Services.WhiskyRecording.Domain.Model.Distilleries;
using WhiskyArchive.Services.WhiskyRecording.Domain.Model.Whiskys;
using WhiskyArchive.Services.WhiskyRecording.Infrastructure.Idempotency;

namespace WhiskyArchive.Services.WhiskyRecording.API.Application.Commands
{
    public class UpdateWhiskyRecordCommandHandler
       : IRequestHandler<UpdateWhiskyRecordCommand, bool>
    {
        private readonly IWhiskyRepository _whiskyRepository;
        private readonly IDistilleryRepository _distilleryRepository;
        //private readonly IIdentityService _identityService;
        private readonly IMediator _mediator;
        private readonly IEventBus _eventBus;

        // Using DI to inject infrastructure persistence Repositories
        public UpdateWhiskyRecordCommandHandler(IMediator mediator, IWhiskyRepository whiskyRepository, IDistilleryRepository distilleryRepository, IEventBus eventBus)
        {
            _whiskyRepository = whiskyRepository ?? throw new ArgumentNullException(nameof(whiskyRepository));
            _distilleryRepository = distilleryRepository ?? throw new ArgumentNullException(nameof(distilleryRepository));
            //_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public async Task<bool> Handle(UpdateWhiskyRecordCommand message, CancellationToken cancellationToken)
        {
            // Add/Update the Buyer AggregateRoot
            // DDD patterns comment: Add child entities and value-objects through the Order Aggregate-Root
            // methods and constructor so validations, invariants and business logic 
            // make sure that consistency is preserved across the whole aggregate


            //Find existing whisky.
            var existingWhisky = await this._whiskyRepository.GetByWhiskyIdAsync(message.WhiskyId);

            if (existingWhisky == null)
            {
                throw new KeyNotFoundException("No existing whisky found with Id " + message.WhiskyId);
            }

            existingWhisky.UpdateName(message.WhiskyNameChinese, message.WhiskyNameEnglish);
            existingWhisky.UpdateBottler(message.WhiskyBottler);
            existingWhisky.UpdateDetail(
                message.Vintage,
                message.Bottled,
                message.StatedAge,
                message.CaskType,
                message.CaskNumber,
                message.NumberOfBottles,
                message.Strength,
                message.Size,
                message.Market);
            existingWhisky.UpdateNotes(message.Notes);

            _whiskyRepository.Update(existingWhisky);

            try
            {
                await _whiskyRepository.UnitOfWork
                    .SaveEntitiesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Update whisky failed. \n" +
                    "Error Message: " + ex.Message);

                return false;
            }






            /*this._eventBus.Publish(new TraceCreatedIntegrationEvent(
                trace.TraceId,
                trace.Investment.InvestmentId,
                trace.Market.ExchangeId,
                trace.Market.BaseCurrency,
                trace.Market.QuoteCurrency
                ));*/

            return true;
        }
    }


    // Use for Idempotency in Command process
    public class UpdateWhiskyRecordIdentifiedCommandHandler : IdentifiedCommandHandler<UpdateWhiskyRecordCommand, bool>
    {
        public UpdateWhiskyRecordIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;        // Ignore duplicate requests for creating order.
        }
    }
}
