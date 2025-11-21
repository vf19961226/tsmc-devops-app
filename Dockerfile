# --- Stage 1: Build Environment ---
# 使用 .NET SDK 映像檔進行編譯和發布
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 複製專案檔，並還原 NuGet 套件
COPY ["TsmcDevOpsApp/TsmcDevOpsApp.csproj", "TsmcDevOpsApp/"]
RUN dotnet restore "TsmcDevOpsApp/TsmcDevOpsApp.csproj"

# 複製所有程式碼並發布
COPY . .
WORKDIR "/src/TsmcDevOpsApp"
RUN dotnet publish "TsmcDevOpsApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# --- Stage 2: Final Runtime Image ---
# 使用 .NET ASPNET 映像檔，體積更小，只包含運行所需的環境
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# 為了配合 K8s，將服務公開在 8080 端口 (一般是 80，但 8080 較安全且常用)
EXPOSE 8080

# 將發布後的檔案複製到運行環境
COPY --from=build /app/publish .

# 設定環境變數讓 Kestrel 監聽 8080 端口
ENV ASPNETCORE_URLS=http://+:8080

# 啟動應用程式
ENTRYPOINT ["dotnet", "TsmcDevOpsApp.dll"]