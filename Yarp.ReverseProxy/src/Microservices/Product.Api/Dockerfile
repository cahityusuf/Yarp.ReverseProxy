FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app

COPY ./*.csproj ./

COPY *.sln .

RUN dotnet restore ./*.csproj

COPY . .

RUN dotnet publish -c release ./*.csproj -o /publish/


FROM base AS final

WORKDIR /app

COPY --from=build /publish .


ENTRYPOINT [ "dotnet","Product.Api.dll" ]