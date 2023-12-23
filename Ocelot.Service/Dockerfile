#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Ocelot.Service/Ocelot.Service.csproj", "Ocelot.Service/"]
RUN dotnet restore "Ocelot.Service/Ocelot.Service.csproj"
COPY . .
WORKDIR "/src/Ocelot.Service"
RUN dotnet build "Ocelot.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ocelot.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ocelot.Service.dll"]