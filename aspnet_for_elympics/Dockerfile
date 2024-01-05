FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /source

COPY *.sln .
COPY aspnet_for_elympics/*.csproj ./aspnet_for_elympics/
RUN dotnet restore

COPY aspnet_for_elympics/. ./aspnet_for_elympics/
WORKDIR /source/aspnet_for_elympics
RUN dotnet publish -c Release -o /app

WORKDIR /app

EXPOSE 8081

ENTRYPOINT ["dotnet", "Aspnet_for_elympics.dll"]