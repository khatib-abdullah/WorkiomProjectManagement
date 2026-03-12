FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY NuGet.Config .
COPY common.props .
COPY src/ src/
RUN dotnet restore src/WorkiomProjectManagement.DbMigrator/WorkiomProjectManagement.DbMigrator.csproj
RUN dotnet publish src/WorkiomProjectManagement.DbMigrator/WorkiomProjectManagement.DbMigrator.csproj \
    -c Release --no-restore -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WorkiomProjectManagement.DbMigrator.dll"]
