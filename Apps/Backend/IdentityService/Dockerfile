FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src
COPY *.csproj ./
RUN dotnet restore

COPY . ./

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

RUN apt-get update && apt-get install -y --no-install-recommends netcat-openbsd && rm -rf /var/lib/apt/lists/*

COPY wait-for-sql.sh /wait-for-sql.sh
RUN chmod +x /wait-for-sql.sh

EXPOSE 9001
ENTRYPOINT [ "/wait-for-sql.sh" ]
CMD ["dotnet", "IdentityService.dll"]


