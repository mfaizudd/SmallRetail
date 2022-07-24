#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SmallRetail.Web/SmallRetail.Web.csproj", "SmallRetail.Web/"]
COPY ["SmallRetail.Services/SmallRetail.Services.csproj", "SmallRetail.Services/"]
COPY ["SmallRetail.Data/SmallRetail.Data.csproj", "SmallRetail.Data/"]
RUN dotnet restore "SmallRetail.Web/SmallRetail.Web.csproj"
COPY . .
WORKDIR "/src/SmallRetail.Web"
RUN dotnet build "SmallRetail.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SmallRetail.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmallRetail.Web.dll"]