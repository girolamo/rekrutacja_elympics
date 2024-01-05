FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY *.sln .
COPY aspnet_for_elympics/*.csproj ./aspnet_for_elympics/
RUN dotnet restore

COPY aspnet_for_elympics/. ./aspnet_for_elympics/
WORKDIR /source/aspnet_for_elympics
RUN dotnet publish -c Release -o /app


FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

EXPOSE 8080 443

ENTRYPOINT ["dotnet", "Aspnet_for_elympics.dll"]