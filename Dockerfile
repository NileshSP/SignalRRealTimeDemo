FROM microsoft/dotnet-framework:4.7.2-runtime-windowsservercore-ltsc2016
WORKDIR /app
LABEL maintainer "Nilesh Patel"

# copy .sln, .csproj, .config & restore nuget packages
COPY *.sln .
COPY SampleSignalRRealTime/*.csproj ./SampleSignalRRealTime/
COPY SampleSignalRRealTime/*.config ./SampleSignalRRealTime/
RUN nuget restore

# copy everything else and build app
COPY SampleSignalRRealTime/. ./SampleSignalRRealTime/
WORKDIR /app/SampleSignalRRealTime
RUN msbuild /p:Configuration=Release


FROM microsoft/aspnet:4.7.2 AS runtime
WORKDIR /inetpub/wwwroot
COPY --from=build /app/SampleSignalRRealTime/. ./

CMD ASPNET_URLS=http://*:$PORT dotnet SampleSignalRRealTime.dll
ENTRYPOINT ["dotnet", "SampleSignalRRealTime.dll"]

##.net core with sql file process
#FROM microsoft/dotnet-framework:4.7.2-sdk AS build
#WORKDIR /app
#
#COPY ./SampleSignalRRealTime/*.csproj ./
##RUN dotnet restore SampleSignalRRealTime.csproj
#COPY ./SampleSignalRRealTime ./
#RUN dotnet build SampleSignalRRealTime.csproj -c Release 
##--no-restore
#
#RUN dotnet publish SampleSignalRRealTime.csproj -c Release -o out --no-restore
#
#FROM microsoft/dotnet-framework:4.7.2-runtime
#WORKDIR /app
#COPY --from=builder /app/out .
#CMD ASPNETCORE_URLS=http://*:$PORT dotnet SampleSignalRRealTime.dll
##ENTRYPOINT ["dotnet", "SampleSignalRRealTime.dll"]