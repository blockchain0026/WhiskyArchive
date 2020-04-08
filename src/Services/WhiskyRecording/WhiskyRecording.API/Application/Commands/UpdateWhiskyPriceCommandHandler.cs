using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhiskyArchive.BuildingBlocks.EventBus.Abstractions;
using WhiskyArchive.Services.WhiskyRecording.Domain.Model.Whiskys;
using WhiskyArchive.Services.WhiskyRecording.Infrastructure.Idempotency;

namespace WhiskyArchive.Services.WhiskyRecording.API.Application.Commands
{
    // Regular CommandHandler
    public class UpdateWhiskyPriceCommandHandler
        : IRequestHandler<UpdateWhiskyPriceCommand, bool>
    {
        private readonly IWhiskyRepository _whiskyRepository;
        //private readonly IIdentityService _identityService;
        private readonly IMediator _mediator;
        private readonly IEventBus _eventBus;

        // Using DI to inject infrastructure persistence Repositories
        public UpdateWhiskyPriceCommandHandler(IMediator mediator, IWhiskyRepository whiskyRepository, IEventBus eventBus)
        {
            _whiskyRepository = whiskyRepository ?? throw new ArgumentNullException(nameof(whiskyRepository));
            //_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public async Task<bool> Handle(UpdateWhiskyPriceCommand message, CancellationToken cancellationToken)
        {
            var whisky = await this._whiskyRepository.GetByWhiskyIdAsync(message.WhiskyId);

            whisky.UpdatePrice(
                message.Price,
                message.CurrencyId,
                message.PriceReferenceId,
                new DateTime(message.PriceDateYear, message.PriceDateMonth, message.PriceDateDay),
                message.Seller,
                message.PriceNumber
                );

            _whiskyRepository.Update(whisky);

            try
            {
                await _whiskyRepository.UnitOfWork
                    .SaveEntitiesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Update whisky price failed. \n" +
                    "Error Message: " + ex.Message);

                return false;
            }

            return true;
        }
    }


    // Use for Idempotency in Command process
    public class UpdateWhiskyPriceIdentifiedCommandHandler : IdentifiedCommandHandler<UpdateWhiskyPriceCommand, bool>
    {
        public UpdateWhiskyPriceIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;        // Ignore duplicate requests for creating order.
        }
    }
}
