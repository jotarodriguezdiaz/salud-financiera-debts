# Montar imagen docker build -t debts .
# levantar imagen docker run -d -p 5054:5054 --name app-debts debts

# Establecer la imagen base de .NET 7.0 con ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5065

# Establecer la imagen base de .NET 7.0 SDK para la construcción
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiar los archivos csproj y restaurar las dependencias
COPY ["Debts/Debts.csproj", "Debts/"]
COPY ["Debts.Application/Debts.Application.csproj", "Debts.Application/"]
COPY ["Debts.Domain/Debts.Domain.csproj", "Debts.Domain/"]
COPY ["Debts.Infrastructure/Debts.Infrastructure.csproj", "Debts.Infrastructure/"]
RUN dotnet restore "Debts/Debts.csproj"

# Copiar todo el código fuente y construir la aplicación
COPY . .
WORKDIR "/src/Debts"
RUN dotnet build "Debts.csproj" -c Release -o /app/build

# Publicar la aplicación
FROM build AS publish
WORKDIR "/src/Debts"
RUN dotnet publish "Debts.csproj" -c Release -o /app/publish

# Crear la imagen final
FROM base AS final
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Production
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:5065
ENTRYPOINT ["dotnet", "Debts.dll"]
