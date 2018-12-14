FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 61035
EXPOSE 44325

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY ../GateWay/GateWay.csproj ../GateWay/
RUN dotnet restore ../GateWay/GateWay.csproj
COPY . .
WORKDIR /src/../GateWay
RUN dotnet build GateWay.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish GateWay.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "GateWay.dll"]
