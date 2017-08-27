FROM microsoft/aspnetcore-build:2.0 AS build-env

WORKDIR /app

# copy csproj and restore as distinct layers

COPY . .
#COPY *.csproj ./
RUN dotnet restore VotingApp.csproj

# copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o obj/publish VotingApp.csproj

# build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /app/obj/publish ./
ENTRYPOINT ["dotnet", "VotingApp.dll"]