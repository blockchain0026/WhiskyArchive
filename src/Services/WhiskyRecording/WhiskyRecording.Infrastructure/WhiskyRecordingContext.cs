using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhiskyArchive.Services.WhiskyRecording.Domain.Model.Distilleries;
using WhiskyArchive.Services.WhiskyRecording.Domain.Model.Whiskys;
using WhiskyArchive.Services.WhiskyRecording.Domain.SeedWork;
using WhiskyArchive.Services.WhiskyRecording.Infrastructure.EntityConfigurations;
using WhiskyArchive.Services.WhiskyRecording.Infrastructure.Idempotency;

namespace WhiskyArchive.Services.WhiskyRecording.Infrastructure
{
    public class WhiskyRecordingContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "whiskyrecording";
        public DbSet<Whisky> Whiskys { get; set; }
        public DbSet<Distillery> Distillerys { get; set; }
        public DbSet<WhiskyPrice> WhiskyPrices { get; set; }
        public DbSet<WhiskyImage> WhiskyImages { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<PriceReference> PriceReference { get; set; }
        public DbSet<ClientRequest> ClientRequests { get; set; }

        private readonly IMediator _mediator;

        private WhiskyRecordingContext(DbContextOptions<WhiskyRecordingContext> options) : base(options) { }

        public WhiskyRecordingContext(DbContextOptions<WhiskyRecordingContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));


            System.Diagnostics.Debug.WriteLine("WhiskyRecordingContext::ctor ->" + this.GetHashCode());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WhiskyEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DistilleryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WhiskyPriceEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WhiskyImageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CurrencyEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PriceReferenceEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 


            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed throught the DbContext will be commited
            var result = await base.SaveChangesAsync();

            await _mediator.DispatchDomainEventsAsync(this);

            return true;
        }

    }


    public class TrendAnalysisContextDesignFactory : IDesignTimeDbContextFactory<WhiskyRecordingContext>
    {
        public TrendAnalysisContextDesignFactory() : base()
        {
            //Debugger.Launch();
        }
        public WhiskyRecordingContext CreateDbContext(string[] args)
        {

            /*var optionsBuilder = new DbContextOptionsBuilder<ExecutionContext>()
                .UseSqlServer("Server=.;Initial Catalog=CryptoArbitrage.Services.ExecutionDb;Integrated Security=true");*/
            var optionsBuilder = new DbContextOptionsBuilder<WhiskyRecordingContext>()
                .UseSqlServer("Server=sql.data;Database=whiskyarchive.whiskyrecording;User Id=sa;Password=1Secure*Password1;",
                sqlServerOptionsAction: sqlOptions =>
                {
                    var assemblyName = "WhiskyRecording.API";
                    sqlOptions.MigrationsAssembly(assemblyName);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                });
            return new WhiskyRecordingContext(optionsBuilder.Options, new NoMediator());
        }

        class NoMediator : IMediator
        {
            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
            {
                return Task.CompletedTask;
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.FromResult<TResponse>(default(TResponse));
            }

            public Task Send(IRequest request, CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.CompletedTask;
            }
        }
    }
}
