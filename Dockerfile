#.net core with sql file process
FROM microsoft/dotnet-framework:4.7.2-sdk AS builder
WORKDIR /app

COPY ./SampleSignalRRealTime/*.csproj ./
#RUN dotnet restore SampleSignalRRealTime.csproj
COPY ./SampleSignalRRealTime ./
RUN dotnet build SampleSignalRRealTime.csproj -c Release 
#--no-restore

RUN dotnet publish SampleSignalRRealTime.csproj -c Release -o out --no-restore

FROM microsoft/dotnet-framework:4.7.2-runtime
WORKDIR /app
COPY --from=builder /app/out .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet SampleSignalRRealTime.dll
#ENTRYPOINT ["dotnet", "SampleSignalRRealTime.dll"]