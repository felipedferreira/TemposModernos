FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Presentation/Acme.Cinedex.MoviesService.WebService/Acme.Cinedex.MoviesService.WebService.csproj", "src/Presentation/Acme.Cinedex.MoviesService.WebService/"]
RUN dotnet restore "src/Presentation/Acme.Cinedex.MoviesService.WebService/Acme.Cinedex.MoviesService.WebService.csproj"
COPY . .
WORKDIR "/src/src/Presentation/Acme.Cinedex.MoviesService.WebService"
RUN dotnet build "./Acme.Cinedex.MoviesService.WebService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Acme.Cinedex.MoviesService.WebService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Acme.Cinedex.MoviesService.WebService.dll"]
