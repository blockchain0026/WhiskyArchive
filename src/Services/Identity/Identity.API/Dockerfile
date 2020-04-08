FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 50544
EXPOSE 44327

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY src/Services/Identity/Identity.API/Identity.API.csproj src/Services/Identity/Identity.API/
COPY src/BuildingBlocks/WebHost/WebHost.Customization/WebHost.Customization.csproj src/BuildingBlocks/WebHost/WebHost.Customization/
RUN dotnet restore src/Services/Identity/Identity.API/Identity.API.csproj
COPY . .
WORKDIR /src/src/Services/Identity/Identity.API
RUN dotnet build Identity.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Identity.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Identity.API.dll"]
