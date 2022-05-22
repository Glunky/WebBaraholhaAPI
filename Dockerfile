FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebBaraholkaAPI/WebBaraholkaAPI.csproj", "WebBaraholkaAPI/"]
RUN dotnet restore "WebBaraholkaAPI/WebBaraholkaAPI.csproj"
COPY . .
WORKDIR "/src/WebBaraholkaAPI"
RUN dotnet build "WebBaraholkaAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebBaraholkaAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebBaraholkaAPI.dll"]
