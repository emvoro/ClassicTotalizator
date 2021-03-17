#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["ClassicTotalizator/ClassicTotalizator.API/ClassicTotalizator.API.csproj", "ClassicTotalizator.API/"]
COPY ["ClassicTotalizator/ClassicTotalizator.DAL/ClassicTotalizator.DAL.csproj", "ClassicTotalizator.DAL/"]
COPY ["ClassicTotalizator/ClassicTotalizator.BLL/ClassicTotalizator.BLL.csproj", "ClassicTotalizator.BLL/"]
RUN dotnet restore "ClassicTotalizator/ClassicTotalizator.API/ClassicTotalizator.API.csproj"
COPY . .
WORKDIR "/src/ClassicTotalizator.API"
RUN dotnet build "ClassicTotalizator.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClassicTotalizator.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClassicTotalizator.API.dll"]