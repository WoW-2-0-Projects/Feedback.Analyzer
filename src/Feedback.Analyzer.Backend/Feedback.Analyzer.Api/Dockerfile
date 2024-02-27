#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Feedback.Analyzer.Api/Feedback.Analyzer.Api.csproj", "Feedback.Analyzer.Api/"]
COPY ["Feedback.Analyzer.Infrastructure/Feedback.Analyzer.Infrastructure.csproj", "Feedback.Analyzer.Infrastructure/"]
COPY ["Feedback.Analyzer.Application/Feedback.Analyzer.Application.csproj", "Feedback.Analyzer.Application/"]
COPY ["Feedback.Analyzer.Domain/Feedback.Analyzer.Domain.csproj", "Feedback.Analyzer.Domain/"]
COPY ["Feedback.Analyzer.Persistence/Feedback.Analyzer.Persistence.csproj", "Feedback.Analyzer.Persistence/"]
RUN dotnet restore "./Feedback.Analyzer.Api/./Feedback.Analyzer.Api.csproj"
COPY . .
WORKDIR "/src/Feedback.Analyzer.Api"
RUN dotnet build "./Feedback.Analyzer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Feedback.Analyzer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Feedback.Analyzer.Api.dll"]