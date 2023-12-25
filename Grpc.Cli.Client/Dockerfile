#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Grpc.Cli.Client/Grpc.Cli.Client.csproj", "Grpc.Cli.Client/"]
RUN dotnet restore "Grpc.Cli.Client/Grpc.Cli.Client.csproj"
COPY . .
WORKDIR "/src/Grpc.Cli.Client"
RUN dotnet build "Grpc.Cli.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Grpc.Cli.Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Grpc.Cli.Client.dll"]
ENTRYPOINT ["echo", "to \run exec: dotnet /app/bin/debug/net7.0/Grpc.Cli.Client.dll"]