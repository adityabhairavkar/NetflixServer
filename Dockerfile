FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out


# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY --from=build-env /App/out .
COPY /MetricsConfig.xml .
COPY /NLog.Config .
# COPY /opentelemetry-dotnet-instrumentation-macos .
# COPY /opentelemetry-dotnet-instrumentation-macos/net ./opentelemetry-dotnet-instrumentation-macos/net
# COPY /opentelemetry-dotnet-instrumentation-macos/store ./opentelemetry-dotnet-instrumentation-macos/store
# COPY /opentelemetry-dotnet-instrumentation-macos/AdditionalDeps ./opentelemetry-dotnet-instrumentation-macos/AdditionalDeps
# ENV DOTNET_SHARED_STORE=/App/opentelemetry-dotnet-instrumentation-macos/Store
# ENV DOTNET_STARTUP_HOOKS=/App/opentelemetry-dotnet-instrumentation-macos/net/OpenTelemetry.AutoInstrumentation.StartupHook.dll
# ENV OTEL_DOTNET_AUTO_HOME=/App/opentelemetry-dotnet-instrumentation-macos
# ENV OTEL_DOTNET_AUTO_TRACES_CONSOLE_EXPORTER_ENABLED=false
# ENV OTEL_EXPORTER_OTLP_ENDPOINT="https://qauattraces02.logicmonitor.com/rest/api/v1/traces"
# ENV "OTEL_EXPORTER_OTLP_HEADERS"="Authorization=Bearer lmb_akNBSDRTQXVtM3VFNGE3eHlLMkk6WS9za2JDOGlZc2RJWFNHMk1mRTl4QT09LZjc5YzBhYjctM2JmMy00YjY2LTg4YWYtMTM1OTc3MjhhN2Y3L1zp2KX"
# ENV OTEL_SERVICE_NAME=NetflixServer
# ENV OTEL_RESOURCE_ATTRIBUTES=deployment.environment=staging,service.version=1.0.0

ENV ConfigPath=MetricsConfig.xml
#ENV LM_COMPANY=lmadityabhairavkar
#ENV LM_ACCESS_ID=a8ZCjQg7sax6965PgczV
#ENV LM_ACCESS_KEY='lma_NY6mA8a7}^8i]9+5dV9+]Mdki%qGRb=565sv=)4iV_86vWqPwe8}T4)-Ec(eLZGNlMzNkNzAtNjgzZS00ZWQ0LWJlN2UtYjczMDczYjUyZGY3L3cuEZQ'
ENTRYPOINT ["dotnet", "NetflixServer.dll"]
