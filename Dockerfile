FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5432

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# FROM mcr.microsoft.com/dotnet/sdk:6.0.401-alpine3.16-amd64 AS build

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80

WORKDIR /src
COPY . ./
RUN dotnet restore "PixelPlusMediaCore.sln"
RUN dotnet build "PixelPlusMediaCore.sln" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PixelPlusMediaCore.sln" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PixelPlusMedia.API.dll"]