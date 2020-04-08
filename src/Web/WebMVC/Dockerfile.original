FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["src/Web/WebMVC/WebMVC.csproj", "src/Web/WebMVC/"]
RUN dotnet restore "src/Web/WebMVC/WebMVC.csproj"
COPY . .
WORKDIR "/src/src/Web/WebMVC"
RUN dotnet build "WebMVC.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebMVC.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebMVC.dll"]