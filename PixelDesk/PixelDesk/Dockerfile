#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5010

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PixelDesk/PixelDesk.csproj", "PixelDesk/"]
RUN dotnet restore "PixelDesk/PixelDesk.csproj"
COPY . .
WORKDIR "/src/PixelDesk"
RUN dotnet build "PixelDesk.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PixelDesk.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PixelDesk.dll"]