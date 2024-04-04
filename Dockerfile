FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia il file del progetto e ripristina le dipendenze
COPY ["SpicyLand/SpicyLand.csproj", "SpicyLand/"]
RUN dotnet restore "SpicyLand/SpicyLand.csproj"

# Copia tutti i file dell'applicazione e compila il progetto
COPY . .
WORKDIR "/app/SpicyLand"
RUN dotnet build "SpicyLand.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SpicyLand.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpicyLand.dll"]
