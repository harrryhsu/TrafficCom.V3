FROM mcr.microsoft.com/dotnet/sdk:6.0.418-focal
WORKDIR /app

# copy everything else and build
COPY . ./
RUN dotnet restore

CMD ["dotnet", "test", "TrafficCom.V3.Test", "-c", "Release"]