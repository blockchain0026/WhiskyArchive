FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 58983
EXPOSE 44356

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY src/Services/WhiskyRecording/WhiskyRecording.API/WhiskyRecording.API.csproj src/Services/WhiskyRecording/WhiskyRecording.API/
COPY src/Services/WhiskyRecording/WhiskyRecording.Infrastructure/WhiskyRecording.Infrastructure.csproj src/Services/WhiskyRecording/WhiskyRecording.Infrastructure/
COPY src/Services/WhiskyRecording/WhiskyRecording.Domain/WhiskyRecording.Domain.csproj src/Services/WhiskyRecording/WhiskyRecording.Domain/
COPY src/BuildingBlocks/EventBus/IntegrationEventLogEF/IntegrationEventLogEF.csproj src/BuildingBlocks/EventBus/IntegrationEventLogEF/
COPY src/BuildingBlocks/EventBus/EventBus/EventBus.csproj src/BuildingBlocks/EventBus/EventBus/
COPY src/BuildingBlocks/EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj src/BuildingBlocks/EventBus/EventBusRabbitMQ/
RUN dotnet restore src/Services/WhiskyRecording/WhiskyRecording.API/WhiskyRecording.API.csproj
COPY . .
WORKDIR /src/src/Services/WhiskyRecording/WhiskyRecording.API
RUN dotnet build WhiskyRecording.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WhiskyRecording.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WhiskyRecording.API.dll"]
