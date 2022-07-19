FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as base 
EXPOSE 80
EXPOSE 443
WORKDIR /app
COPY /src/Awarean.Operational.Cutler.Api/Awarean.Operational.Cutler.Api.csproj Awarean.Operational.Cutler.Api
COPY /src/Awarean.Operational.Cutler.Infra/Awarean.Operational.Cutler.Infra.csproj Awarean.Operational.Cutler.Infra
COPY /src/Awarean.Operational.Cutler.Domain/Awarean.Operational.Cutler.Domain.csproj Awarean.Operational.Cutler.Domain
COPY /src/Awarean.Operational.Cutler.Application/Awarean.Operational.Cutler.Application.csproj Awarean.Operational.Cutler.Application

FROM base as build
RUN dotnet restore Awarean.Operational.Cutler.Api

FROM base as publish
COPY /src/Awarean.Operational.Cutler.Api Awarean.Operational.Cutler.Api
COPY /src/Awarean.Operational.Cutler.Infra Awarean.Operational.Cutler.Infra
COPY /src/Awarean.Operational.Cutler.Domain Awarean.Operational.Cutler.Domain
COPY /src/Awarean.Operational.Cutler.Application Awarean.Operational.Cutler.Application
RUN dotnet publish Awarean.Operational.Cutler.Api -c Release -o out --no-restore


FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "Awarean.Operational.Cutler.Api.dll"]
