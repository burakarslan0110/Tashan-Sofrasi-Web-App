# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8082
EXPOSE 8083

# Ortam deðiþkenleri
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=HttpsWeb/tashansofrasi.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=Passw@rd1.

USER root
RUN apt-get update && apt-get install -y libfontconfig1 libfreetype6 libpng16-16 libharfbuzz0b libx11-6 libxext6 libxrender1 libxcb1 libx11-xcb1 libxcb-shm0 libxcb-render0 libc6 libgcc1 libstdc++6 libuuid1

USER app

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TashanSofrasiWebApp/TashanSofrasiWebApp.csproj", "TashanSofrasiWebApp/"]
COPY ["TashanSofrasi.DataAccessLayer/TashanSofrasi.DataAccessLayer.csproj", "TashanSofrasi.DataAccessLayer/"]
COPY ["TashanSofrasi.EntityLayer/TashanSofrasi.EntityLayer.csproj", "TashanSofrasi.EntityLayer/"]
COPY ["TashanSofrasiSignalRApi/TashanSofrasiSignalRApi.csproj", "TashanSofrasiSignalRApi/"]
COPY ["TashanSofrasi.BusinessLayer/TashanSofrasi.BusinessLayer.csproj", "TashanSofrasi.BusinessLayer/"]
COPY ["TashanSofrasi.DTOLayer/TashanSofrasi.DTOLayer.csproj", "TashanSofrasi.DTOLayer/"]
RUN dotnet restore "./TashanSofrasiWebApp/TashanSofrasiWebApp.csproj"
COPY . .
WORKDIR "/src/TashanSofrasiWebApp"
RUN dotnet build "./TashanSofrasiWebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TashanSofrasiWebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TashanSofrasiWebApp.dll"]