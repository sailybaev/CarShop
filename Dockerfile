FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["CarShopFinal.csproj", "."]
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

RUN dotnet tool install --global dotnet-ef --version 9.0.0
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet ef migrations bundle --self-contained -r linux-x64 -o /app/efbundle

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
COPY --from=build /app/efbundle ./efbundle
COPY entrypoint.sh .
RUN chmod +x ./efbundle entrypoint.sh
ENTRYPOINT ["./entrypoint.sh"]
