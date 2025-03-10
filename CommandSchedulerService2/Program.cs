using CommandSchedulerService2;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>(provider => new Worker(
            provider.GetRequiredService<ILogger<Worker>>(),
            args // コマンドライン引数を渡す
        ));
    })
    .UseWindowsService() // ここでWindowsサービスとして設定
    .Build();

await host.RunAsync();
