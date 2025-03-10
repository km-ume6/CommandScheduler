using CommandSchedulerService2;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>(provider => new Worker(
            provider.GetRequiredService<ILogger<Worker>>(),
            args // �R�}���h���C��������n��
        ));
    })
    .UseWindowsService() // ������Windows�T�[�r�X�Ƃ��Đݒ�
    .Build();

await host.RunAsync();
