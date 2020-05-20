#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["LS.Redis/LS.Redis.csproj", "LS.Redis/"]
RUN dotnet restore "LS.Redis/LS.Redis.csproj"
COPY . .
WORKDIR "/src/LS.Redis"
RUN dotnet build "LS.Redis.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LS.Redis.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LS.Redis.dll"]