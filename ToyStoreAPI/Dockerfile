# Use the official .NET Core SDK image as the build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
 
# Copy the project file and restore dependencies
COPY ["ToyStoreAPI/ToyStoreAPI.csproj", "ToyStoreAPI/"]
RUN dotnet restore "ToyStoreAPI/ToyStoreAPI.csproj"
 
# Copy the rest of the source code
COPY . .
WORKDIR "/src/ToyStoreAPI"
 
# Build the application
RUN dotnet build "ToyStoreAPI.csproj" -c Release -o /app/build
 
# Publish the application
FROM build AS publish
RUN dotnet publish "ToyStoreAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false
 
# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
EXPOSE 8081
ENTRYPOINT ["dotnet", "ToyStoreAPI.dll"]