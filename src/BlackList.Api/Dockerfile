#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/BlackList.Api/BlackList.Api.csproj", "src/BlackList.Api/"]
COPY ["src/BlackList.Application/BlackList.Application.csproj", "src/BlackList.Application/"]
COPY ["src/BlackList.Domain/BlackList.Domain.csproj", "src/BlackList.Domain/"]
COPY ["src/BlackList.Persistence/BlackList.Persistence.csproj", "src/BlackList.Persistence/"]
RUN dotnet restore "src/BlackList.Api/BlackList.Api.csproj"
COPY . .
WORKDIR /src/BlackList.Api
RUN dotnet build "BlackList.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlackList.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlackList.Api.dll"]

# Command to deploy to Heroku
# CMD ASPNETCORE_URLS="http://*:$PORT" dotnet BlackList.Api.dll