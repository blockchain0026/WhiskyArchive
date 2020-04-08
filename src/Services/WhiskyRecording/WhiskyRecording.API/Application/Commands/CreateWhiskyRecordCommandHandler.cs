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
    // Regular CommandHandler
    public class CreateWhiskyRecordCommandHandler
        : IRequestHandler<CreateWhiskyRecordCommand, bool>
    {
        private readonly IWhiskyRepository _whiskyRepository;
        private readonly IDistilleryRepository _distilleryRepository;
        //private readonly IIdentityService _identityService;
        private readonly IMediator _mediator;
        private readonly IEventBus _eventBus;

        // Using DI to inject infrastructure persistence Repositories
        public CreateWhiskyRecordCommandHandler(IMediator mediator, IWhiskyRepository whiskyRepository, IDistilleryRepository distilleryRepository, IEventBus eventBus)
        {
            _whiskyRepository = whiskyRepository ?? throw new ArgumentNullException(nameof(whiskyRepository));
            _distilleryRepository = distilleryRepository ?? throw new ArgumentNullException(nameof(distilleryRepository));
            //_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public async Task<bool> Handle(CreateWhiskyRecordCommand message, CancellationToken cancellationToken)
        {
            // Add/Update the Buyer AggregateRoot
            // DDD patterns comment: Add child entities and value-objects through the Order Aggregate-Root
            // methods and constructor so validations, invariants and business logic 
            // make sure that consistency is preserved across the whole aggregate


            //Find existing distillery.
            var existingDistillery = await this._distilleryRepository.GetByDistilleryNameAsync(message.DistilleryName);

            if (existingDistillery == null)
            {
                throw new KeyNotFoundException("No exist distillery found with Name " + message.DistilleryName);
            }

            var whisky = Whisky.From(existingDistillery, message.WhiskyNameChinese, message.WhiskyNameEnglish,
                message.WhiskyBottler, message.Vintage, message.Bottled, message.StatedAge,
                message.CaskType, message.CaskNumber, message.NumberOfBottles, message.Strength,
                message.Size, message.Market, message.Rating, message.Notes);


            if (message.Price != null)
            {
                var priceDate = new DateTime(
                    message.PriceDateYear ?? throw new ArgumentNullException(nameof(message.PriceDateYear)),
                    message.PriceDateMonth ?? throw new ArgumentNullException(nameof(message.PriceDateMonth)),
                    message.PriceDateDay ?? throw new ArgumentNullException(nameof(message.PriceDateDay))
                    );

                whisky.UpdatePrice(
                    (decimal)message.Price,
                    message.CurrencyId ?? throw new ArgumentNullException(nameof(message.CurrencyId)),
                    message.PriceReferenceId ?? throw new ArgumentNullException(nameof(message.PriceReferenceId)),
                    priceDate,
                    message.Seller ?? null
                    );
            }

            if (message.ImageUrls != null)
            {
                if (message.ImageUrls.Any())
                {
                    whisky.UpdateImages(message.ImageUrls);
                }
            }

            _whiskyRepository.Add(whisky);

            try
            {
                await _whiskyRepository.UnitOfWork
                    .SaveEntitiesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Create whisky failed. \n" +
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
    public class CreateWhiskyRecordIdentifiedCommandHandler : IdentifiedCommandHandler<CreateWhiskyRecordCommand, bool>
    {
        public CreateWhiskyRecordIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;        // Ignore duplicate requests for creating order.
        }
    }
}
