# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Ortam deðiþkenleri
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=HttpsApi/tashansofrasi.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=Passw@rd1.

# Build ve publish aþamalarý
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TashanSofrasiSignalRApi/TashanSofrasiSignalRApi.csproj", "TashanSofrasiSignalRApi/"]
RUN dotnet restore "./TashanSofrasiSignalRApi/TashanSofrasiSignalRApi.csproj"
COPY . .
WORKDIR "/src/TashanSofrasiSignalRApi"
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TashanSofrasiSignalRApi.dll"]
