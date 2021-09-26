#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DeliVeggie.ConsoleApp/DeliVeggie.ConsoleApp.csproj", "DeliVeggie.ConsoleApp/"]
COPY ["DeliVeggie.Processor/DeliVeggie.Processor.csproj", "DeliVeggie.Processor/"]
COPY ["DeliVeggie.Domain/DeliVeggie.Domain.csproj", "DeliVeggie.Domain/"]
COPY ["DeliVeggie.Repository/DeliVeggie.Repository.csproj", "DeliVeggie.Repository/"]
RUN dotnet restore "DeliVeggie.ConsoleApp/DeliVeggie.ConsoleApp.csproj"
COPY . .
WORKDIR "/src/DeliVeggie.ConsoleApp"
RUN dotnet build "DeliVeggie.ConsoleApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeliVeggie.ConsoleApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliVeggie.ConsoleApp.dll"]