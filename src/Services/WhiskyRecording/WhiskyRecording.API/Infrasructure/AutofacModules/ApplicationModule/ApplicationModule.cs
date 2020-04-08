using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WhiskyArchive.BuildingBlocks.EventBus.Abstractions;
using WhiskyArchive.Services.WhiskyRecording.API.Application.Commands;
using WhiskyArchive.Services.WhiskyRecording.API.Application.Queries;
using WhiskyArchive.Services.WhiskyRecording.Domain.Model.Distilleries;
using WhiskyArchive.Services.WhiskyRecording.Domain.Model.Whiskys;
using WhiskyArchive.Services.WhiskyRecording.Infrastructure.Idempotency;
using WhiskyArchive.Services.WhiskyRecording.Infrastructure.Repositories;

namespace WhiskyArchive.Services.WhiskyRecording.API.Infrasructure.AutofacModules.ApplicationModule
{
    public class ApplicationModule : Autofac.Module
    {
        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new WhiskyQueries(c.Resolve<IWhiskyRepository>(),c.Resolve<IDistilleryRepository>()))
                       .As<IWhiskyQueries>()
                       .InstancePerLifetimeScope();

            builder.RegisterType<WhiskyRepository>()
                .As<IWhiskyRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DistilleryRepository>()
                .As<IDistilleryRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RequestManager>()
               .As<IRequestManager>()
               .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(CreateWhiskyRecordCommandHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

        }
    }
}
