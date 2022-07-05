FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["CSharpAPI-minimal.csproj", "./"]
RUN dotnet restore "CSharpAPI-minimal.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "CSharpAPI-minimal.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CSharpAPI-minimal.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CSharpAPI-minimal.dll"]
