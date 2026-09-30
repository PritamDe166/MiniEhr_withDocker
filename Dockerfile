# ---------- Stage 1: build (big SDK image) ----------
  FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
  WORKDIR /src
  
  # Copy only the project file first, then restore (cached unless dependencies change)
  COPY src/MiniEHR_withDocker.Api/MiniEHR_withDocker.Api.csproj src/MiniEHR_withDocker.Api/
  RUN dotnet restore src/MiniEHR_withDocker.Api/MiniEHR_withDocker.Api.csproj
  
  # Now copy the source and publish
  COPY src/ src/
  RUN dotnet publish src/MiniEHR_withDocker.Api/MiniEHR_withDocker.Api.csproj \
      -c Release -o /app/publish --no-restore
  
  # ---------- Stage 2: runtime (small ASP.NET image) ----------
  FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
  WORKDIR /app
  COPY --from=build /app/publish .
  
  EXPOSE 8080
  USER $APP_UID
  ENTRYPOINT ["dotnet", "MiniEHR_withDocker.Api.dll"]