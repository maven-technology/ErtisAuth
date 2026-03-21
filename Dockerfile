FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
ARG TARGETARCH
WORKDIR /src

COPY ["ErtisAuth.WebAPI/ErtisAuth.WebAPI.csproj", "ErtisAuth.WebAPI/"]
RUN dotnet restore "ErtisAuth.WebAPI/ErtisAuth.WebAPI.csproj" -a $TARGETARCH

COPY . .

WORKDIR "/src/ErtisAuth.WebAPI"
RUN dotnet publish "ErtisAuth.WebAPI.csproj" -c Release -a $TARGETARCH -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
WORKDIR /app
EXPOSE 80

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://0.0.0.0:80

ENTRYPOINT ["dotnet", "ErtisAuth.WebAPI.dll"]
