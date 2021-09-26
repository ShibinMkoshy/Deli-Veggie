#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DeliVeggie.API/DeliVeggie.API.csproj", "DeliVeggie.API/"]
COPY ["DeliVeggie.Processor/DeliVeggie.Processor.csproj", "DeliVeggie.Processor/"]
COPY ["DeliVeggie.Domain/DeliVeggie.Domain.csproj", "DeliVeggie.Domain/"]
COPY ["DeliVeggie.Repository/DeliVeggie.Repository.csproj", "DeliVeggie.Repository/"]
RUN dotnet restore "DeliVeggie.API/DeliVeggie.API.csproj"
COPY . .
WORKDIR "/src/DeliVeggie.API"
RUN dotnet build "DeliVeggie.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeliVeggie.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliVeggie.API.dll"]