FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 59530
EXPOSE 44386

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY OnlineCourse/OnlineCourse.csproj OnlineCourse/
RUN dotnet restore OnlineCourse/OnlineCourse.csproj
COPY . .
WORKDIR /src/OnlineCourse
RUN dotnet build OnlineCourse.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish OnlineCourse.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OnlineCourse.dll"]
