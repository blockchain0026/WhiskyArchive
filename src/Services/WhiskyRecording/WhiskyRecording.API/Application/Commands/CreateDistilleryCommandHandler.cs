using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhiskyArchive.BuildingBlocks.EventBus.Abstractions;
using WhiskyArchive.Services.WhiskyRecording.Domain.Model.Distilleries;
using WhiskyArchive.Services.WhiskyRecording.Infrastructure.Idempotency;

namespace WhiskyArchive.Services.WhiskyRecording.API.Application.Commands
{
    // Regular CommandHandler
    public class CreateDistilleryCommandHandler
        : IRequestHandler<CreateDistilleryCommand, bool>
    {
        private readonly IDistilleryRepository _distilleryRepository;
        //private readonly IIdentityService _identityService;
        private readonly IMediator _mediator;
        private readonly IEventBus _eventBus;

        // Using DI to inject infrastructure persistence Repositories
        public CreateDistilleryCommandHandler(IMediator mediator, IDistilleryRepository distilleryRepository, IEventBus eventBus)
        {
            _distilleryRepository = distilleryRepository ?? throw new ArgumentNullException(nameof(distilleryRepository));
            //_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public async Task<bool> Handle(CreateDistilleryCommand message, CancellationToken cancellationToken)
        {
            // Add/Update the Buyer AggregateRoot
            // DDD patterns comment: Add child entities and value-objects through the Order Aggregate-Root
            // methods and constructor so validations, invariants and business logic 
            // make sure that consistency is preserved across the whole aggregate


            var distillery = new Distillery(
                new DistilleryName(message.ChineseTraditional, message.ChineseSimplified, message.English),
                message.Established,
                message.Introdution,
                message.SmwsCode
                );




            _distilleryRepository.Add(distillery);

            try
            {
                await _distilleryRepository.UnitOfWork
                    .SaveEntitiesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Create distillery failed. \n" +
                    "Error Message: " + ex.Message);

                return false;
            }


            return true;
        }
    }


    // Use for Idempotency in Command process
    public class CreateDistilleryIdentifiedCommandHandler : IdentifiedCommandHandler<CreateDistilleryCommand, bool>
    {
        public CreateDistilleryIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;        // Ignore duplicate requests for creating order.
        }
    }
}
