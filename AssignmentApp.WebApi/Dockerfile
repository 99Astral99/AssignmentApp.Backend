
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AssignmentApp.WebApi/AssignmentApp.WebApi.csproj", "AssignmentApp.WebApi/"]
COPY ["Application/AssignmentApp.Application.csproj", "Application/"]
COPY ["AssignmentApp.Domain/AssignmentApp.Domain.csproj", "AssignmentApp.Domain/"]
COPY ["AssignmentApp.Persistence/AssignmentApp.Persistence.csproj", "AssignmentApp.Persistence/"]
RUN dotnet restore "./AssignmentApp.WebApi/./AssignmentApp.WebApi.csproj"
COPY . .
WORKDIR "/src/AssignmentApp.WebApi"
RUN dotnet build "./AssignmentApp.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AssignmentApp.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AssignmentApp.WebApi.dll"]