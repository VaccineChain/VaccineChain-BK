#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["vaccine-chain-bk/vaccine-chain-bk.csproj", "vaccine-chain-bk/"]
RUN dotnet restore "vaccine-chain-bk/vaccine-chain-bk.csproj"
COPY . .
WORKDIR "/src/vaccine-chain-bk"
RUN dotnet build "vaccine-chain-bk.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "vaccine-chain-bk.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "vaccine-chain-bk.dll"]