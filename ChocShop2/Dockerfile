#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ChocShop2/ChocShop2.csproj", "ChocShop2/"]
RUN dotnet restore "ChocShop2/ChocShop2.csproj"
COPY . .
WORKDIR "/src/ChocShop2"
RUN dotnet build "ChocShop2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChocShop2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChocShop2.dll"]