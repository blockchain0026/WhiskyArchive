FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY src/ApiGateways/Web.Bff.WhiskyArchive/Web.WhiskyArchive.HttpAggregator/Web.WhiskyArchive.HttpAggregator.csproj src/ApiGateways/Web.Bff.WhiskyArchive/Web.WhiskyArchive.HttpAggregator/
RUN dotnet restore src/ApiGateways/Web.Bff.WhiskyArchive/Web.WhiskyArchive.HttpAggregator/Web.WhiskyArchive.HttpAggregator.csproj
COPY . .
WORKDIR /src/src/ApiGateways/Web.Bff.WhiskyArchive/Web.WhiskyArchive.HttpAggregator
RUN dotnet build Web.WhiskyArchive.HttpAggregator.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Web.WhiskyArchive.HttpAggregator.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Web.WhiskyArchive.HttpAggregator.dll"]
