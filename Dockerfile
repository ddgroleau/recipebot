FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Server/PBC.Server.csproj", "Server/"]
RUN dotnet restore "Server/PBC.Server.csproj"
COPY . .

RUN export PATH="$PATH:/root/.dotnet/tools" && dotnet tool install --global dotnet-ef

WORKDIR "/src/Server"
RUN dotnet build "PBC.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PBC.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "PBC.Server.dll"]