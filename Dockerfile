# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["src/People.Api/People.Api.csproj", "src/People.Api/"]
COPY ["src/People.Data/People.Data.csproj", "src/People.Data/"]
COPY ["src/People.sln", "./"]
RUN dotnet restore "src/People.Api/People.Api.csproj"

# Copy
COPY src/ src/

# Build and publish
WORKDIR /src/src/People.Api
RUN dotnet publish "People.Api.csproj" \
    -c Release \
    -r linux-x64 \
    --self-contained false \    
    -o /app/publish 

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
WORKDIR /app

# Create non-root user
RUN addgroup -S appuser && adduser -S appuser -G appuser

# Copy published application
COPY --from=build /app/publish ./

# Change ownership and switch user
RUN chown -R appuser:appuser /app
USER appuser

# Expose port
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_ENVIRONMENT=Development

# Start the app
ENTRYPOINT ["dotnet", "./People.Api.dll"]