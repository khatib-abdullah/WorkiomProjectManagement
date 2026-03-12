FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY NuGet.Config .
COPY common.props .
COPY src/ src/
RUN dotnet restore src/WorkiomProjectManagement.HttpApi.Host/WorkiomProjectManagement.HttpApi.Host.csproj
RUN dotnet publish src/WorkiomProjectManagement.HttpApi.Host/WorkiomProjectManagement.HttpApi.Host.csproj \
    -c Release --no-restore -o /app/publish
RUN dotnet dev-certs https -v -ep /app/publish/openiddict.pfx -p workiom-openiddict-2024

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "WorkiomProjectManagement.HttpApi.Host.dll"]
