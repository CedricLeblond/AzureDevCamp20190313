FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY TrainTrain.Api/TrainTrain.Api.csproj TrainTrain.Api/
COPY TrainTrain/TrainTrain.csproj TrainTrain/
RUN dotnet restore TrainTrain.Api/TrainTrain.Api.csproj
COPY . .
WORKDIR /src/TrainTrain.Api
RUN dotnet build TrainTrain.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish TrainTrain.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TrainTrain.Api.dll"]
