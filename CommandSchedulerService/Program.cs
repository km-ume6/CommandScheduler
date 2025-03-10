using CommandSchedulerService;

var builder = Host.CreateApplicationBuilder(args);

// ログの設定を追加
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Windowsプラットフォームでのみイベントログを追加
if (OperatingSystem.IsWindows())
{
    builder.Logging.AddEventLog();
}

// Register args before using it in Worker
builder.Services.AddSingleton<string[]>(args);
//builder.Services.AddHostedService<Worker>(sp => new Worker(sp.GetRequiredService<ILogger<Worker>>(), sp.GetRequiredService<string[]>()));

var host = builder.Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Host built successfully. Starting the host...");

CreateHostBuilder(args).Build().Run();
logger.LogInformation("Host is running.");

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole();
            if (OperatingSystem.IsWindows())
            {
                logging.AddEventLog();
            }
        })
        .ConfigureServices((hostContext, services) =>
        {
            //services.AddHostedService<Worker>();
        })
        .UseWindowsService();
