# Stage 1: Base runtime image
# Uses the lightweight ASP.NET runtime (no SDK) for the final image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Stage 2: Build
# Uses the full .NET SDK which includes tools needed to restore, build, and publish
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy only the .csproj file first (before source code) for Docker layer caching.
# If the .csproj hasn't changed, Docker reuses the cached restore layer and skips
# the slow NuGet package download — even if source code has changed.
COPY ["src/Presentation/Acme.Cinedex.MoviesService.WebService/Acme.Cinedex.MoviesService.WebService.csproj", "src/Presentation/Acme.Cinedex.MoviesService.WebService/"]

# Restore NuGet packages for the WebService project.
# Since WebService references all other projects (Application, Domain, Persistence, etc.),
# dotnet restore walks the entire dependency graph and restores everything transitively.
RUN dotnet restore "src/Presentation/Acme.Cinedex.MoviesService.WebService/Acme.Cinedex.MoviesService.WebService.csproj"

# Now copy the full source code. This is done after restore so that code changes
# don't invalidate the cached restore layer above.
COPY src/ src/

# Set working directory to the WebService project and build it
WORKDIR "/src/src/Presentation/Acme.Cinedex.MoviesService.WebService"
RUN dotnet build "./Acme.Cinedex.MoviesService.WebService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 3: Publish
# Creates an optimized, deployment-ready output from the build
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

# /p:UseAppHost=false skips generating a platform-specific executable,
# since the container will use `dotnet` to run the DLL directly
RUN dotnet publish "./Acme.Cinedex.MoviesService.WebService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 4: Final runtime image
# Copies only the published output into the lightweight base image (no SDK, no source code)
# This keeps the final image small and secure
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Acme.Cinedex.MoviesService.WebService.dll"]
