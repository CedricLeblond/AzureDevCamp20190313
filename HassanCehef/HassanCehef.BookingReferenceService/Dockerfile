FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY HassanCehef/HassanCehef.BookingReferenceService/HassanCehef.BookingReferenceService.csproj HassanCehef/HassanCehef.BookingReferenceService/
RUN dotnet restore HassanCehef/HassanCehef.BookingReferenceService/HassanCehef.BookingReferenceService.csproj
COPY . .
WORKDIR /src/HassanCehef/HassanCehef.BookingReferenceService
RUN dotnet build HassanCehef.BookingReferenceService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish HassanCehef.BookingReferenceService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HassanCehef.BookingReferenceService.dll"]
