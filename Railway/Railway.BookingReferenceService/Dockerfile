FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Railway/Railway.BookingReferenceService/Railway.BookingReferenceService.csproj Railway/Railway.BookingReferenceService/
RUN dotnet restore Railway/Railway.BookingReferenceService/Railway.BookingReferenceService.csproj
COPY . .
WORKDIR /src/Railway/Railway.BookingReferenceService
RUN dotnet build Railway.BookingReferenceService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Railway.BookingReferenceService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Railway.BookingReferenceService.dll"]
