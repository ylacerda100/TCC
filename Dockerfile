FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 587

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_16.x | bash \
    && apt-get install nodejs -yq

RUN apt-get update && DEBIAN_FRONTEND=noninteractive apt-get install -y locales

RUN sed -i -e 's/# en_US.UTF-8 UTF-8/en_US.UTF-8 UTF-8/' /etc/locale.gen && \
    dpkg-reconfigure --frontend=noninteractive locales && \
    update-locale LANG=en_US.UTF-8

ENV LANG en_US.UTF-8 

WORKDIR /src
COPY ["src/TCC.UI.Web/TCC.UI.Web.csproj", "src/TCC.UI.Web/"]
COPY ["src/TCC.Application/TCC.Application.csproj", "src/TCC.Application/"]
COPY ["src/TCC.Domain/TCC.Domain.csproj", "src/TCC.Domain/"]
COPY ["src/TCC.Infra.CrossCutting.Identity/TCC.Infra.CrossCutting.Identity.csproj", "src/TCC.Infra.CrossCutting.Identity/"]
COPY ["src/TCC.Infra.Data/TCC.Infra.Data.csproj", "src/TCC.Infra.Data/"]
COPY ["src/TCC.Infra.CrossCutting.IoC/TCC.Infra.CrossCutting.IoC.csproj", "src/TCC.Infra.CrossCutting.IoC/"]
COPY ["src/TCC.Tests.Integration/TCC.Tests.Integration.csproj", "src/TCC.Tests.Integration/"]
COPY ["src/TCC.Tests.Unit/TCC.Tests.Unit.csproj", "src/TCC.Tests.Unit/"]
COPY ["src/TCC.UI.Web/package.json", "src/TCC.UI.Web/"]

WORKDIR "/src/src/TCC.UI.Web"
RUN npm install

WORKDIR /src

RUN dotnet restore "/src/src/TCC.UI.Web/TCC.UI.Web.csproj"

COPY . .

WORKDIR "/src/src/TCC.UI.Web"

RUN dotnet build "TCC.UI.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TCC.UI.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false
RUN cp -R node_modules /app/publish/

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TCC.UI.Web.dll"]