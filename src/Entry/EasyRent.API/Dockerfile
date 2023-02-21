FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY src .
RUN dotnet restore "Entry/EasyRent.API/EasyRent.API.csproj"

COPY src .
WORKDIR "/src/Entry/EasyRent.API"
RUN dotnet build "EasyRent.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EasyRent.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EasyRent.API.dll"]
