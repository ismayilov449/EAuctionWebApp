#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ESourcing.Order/ESourcing.Orders.csproj", "ESourcing.Order/"]
COPY ["EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBusRabbitMQ/"]
COPY ["Services/Order/Order.Application/ESouring.Ordering.Application.csproj", "Services/Order/Order.Application/"]
COPY ["Services/Order/Order.Domain/ESourcing.Ordering.Domain.csproj", "Services/Order/Order.Domain/"]
COPY ["Services/Order/Order.Infrastructure/ESourcing.Ordering.Infrastructure.csproj", "Services/Order/Order.Infrastructure/"]
RUN dotnet restore "ESourcing.Order/ESourcing.Orders.csproj"
COPY . .
WORKDIR "/src/ESourcing.Order"
RUN dotnet build "ESourcing.Orders.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ESourcing.Orders.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ESourcing.Orders.dll"]