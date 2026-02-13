FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release


ENV DOTNET_NUGET_SIGNATURE_VERIFICATION=false

ENV HTTP_TIMEOUT=600

ENV DOTNET_GCHeapHardLimit=100000000 

WORKDIR /src

# 1. نسخ المكتبات المحلية أولاً
COPY ["ConstantsLib_Versions/*.nupkg", "local-packages/"]

# 2. نسخ ملفات المشروع لعمل Cache
COPY ["Catalog_Service_API/Catalog_Service_API.csproj", "Catalog_Service_API/"]
COPY ["Catalog_Service_Application/Catalog_Service_Application.csproj", "Catalog_Service_Application/"]
COPY ["Catalog_Service_Core/Catalog_Service_Core.csproj", "Catalog_Service_Core/"]
COPY ["Catalog_Service_Infrastructure/Catalog_Service_Infrastructure.csproj", "Catalog_Service_Infrastructure/"]

# 3. تشغيل الـ Restore مع تعطيل التحميل المتوازي (أبطأ لكنه أكثر استقراراً في حال ضعف الإنترنت)
RUN dotnet restore "Catalog_Service_API/Catalog_Service_API.csproj" \
    --source https://api.nuget.org/v3/index.json \
    --source /src/local-packages \
    --verbosity normal \
    --disable-parallel

# 4. نسخ الكود المتبقي وبدء البناء
COPY . .
WORKDIR "/src/Catalog_Service_API"
RUN dotnet build "Catalog_Service_API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog_Service_API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog_Service_API.dll"]