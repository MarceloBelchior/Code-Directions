#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["OpenSquare.CodeHB.API/OpenSquare.CodeHB.API.csproj", "OpenSquare.CodeHB.API/"]
COPY ["OpenSquare.CodeHB.Domain/OpenSquare.CodeHB.Domain.csproj", "OpenSquare.CodeHB.Domain/"]
COPY ["OpenSquare.CodeHB.DI/OpenSquare.CodeHB.DI.csproj", "OpenSquare.CodeHB.DI/"]
COPY ["OpenSquare.CodeHB.HttpClient/OpenSquare.CodeHB.HttpClient.csproj", "OpenSquare.CodeHB.HttpClient/"]
COPY ["OpenSquare.CodeHB.Helper/OpenSquare.CodeHB.Helper.csproj", "OpenSquare.CodeHB.Helper/"]
COPY ["OpenSquare.CodeHB.Service/OpenSquare.CodeHB.Service.csproj", "OpenSquare.CodeHB.Service/"]
RUN dotnet restore "OpenSquare.CodeHB.API/OpenSquare.CodeHB.API.csproj"
COPY . .
WORKDIR "/src/OpenSquare.CodeHB.API"
RUN dotnet build "OpenSquare.CodeHB.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OpenSquare.CodeHB.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpenSquare.CodeHB.API.dll"]
