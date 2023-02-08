using Chitragupta;
using OpenTelemetry.Logs;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
Action<ResourceBuilder> configureResource = r => r.AddService(
    serviceName: builder.Configuration.GetValue<string>("ServiceName") ?? AppDomain.CurrentDomain.FriendlyName,
    serviceVersion: typeof(Program).Assembly.GetName().Version?.ToString() ?? "unknown",
    serviceInstanceId: Environment.MachineName);
builder.Logging.AddOpenTelemetry(options =>
{
  // Note: See appsettings.json Logging:OpenTelemetry section for configuration.
  string logExporter = Environment.GetEnvironmentVariable("UseLogExporter") ?? "console";
  string ep = Environment.GetEnvironmentVariable("OtlpEndpoint") ?? "http://localhost:4317";
  Uri endpoint = new Uri(ep);
  var resourceBuilder = ResourceBuilder.CreateDefault();
  configureResource(resourceBuilder);
  options.SetResourceBuilder(resourceBuilder);

  switch (logExporter)
  {
    case "otlp":
      options.AddOtlpExporter(otlpOptions =>
      {
        // Use IConfiguration directly for Otlp exporter endpoint option.
        otlpOptions.Endpoint = endpoint;
      });
      break;
    default:
      options.AddConsoleExporter();
      break;
  }
});


//builder.Logging.AddEventLog();
var app = builder.Build();

// Configure LM Metrics 
//MetricsCounter m = new MetricsCounter();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

