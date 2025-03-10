using csConfigurationManager;
using csTaskItem;

namespace CommandSchedulerService2
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly string[] _args;
        private readonly ConfigurationManager<List<TaskItem>>? cmTasks;
        private List<TaskItem>? taskItems;
        private string folderPath = string.Empty;

        public Worker(ILogger<Worker> logger, string[] args)
        {
            _logger = logger;
            _args = args;

            if (args.Length == 1)
            {
                cmTasks = new(args[0]);
                if (cmTasks != null)
                {
                    taskItems = cmTasks.Load();
                    folderPath = cmTasks.GetFolder();
                }
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            if (taskItems != null)
            {
                foreach (var task in taskItems)
                {
                    _logger.LogInformation("Task: {title}, {fileName}, {script}, {arguments}, {workingFolder}, {startDateTime}, {cycle}, {nextDateTime}, {enable}",
                        task.Title, task.FileName, task.Script, task.Arguments, task.WorkingFolder, task.StartDateTime, task.Cycle, task.NextDateTime, task.Enable);
                    task.InitNextDateTime();
                }

                while (!stoppingToken.IsCancellationRequested)
                {
                    foreach (var task in taskItems)
                    {
                        if (task.NextDateTime < DateTime.Now && task.EnableValue)
                        {
                            _logger.LogInformation("Starting task: {title} at {time}", task.Title, DateTimeOffset.Now);
                            task.CalcNextDateTime();
                            if (folderPath != null)
                            {
                                try
                                {
                                    task.StartProcess(folderPath);
                                    _logger.LogInformation("Task {title} started successfully.", task.Title);
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError(ex, "Error starting task {title}", task.Title);
                                }
                            }
                        }
                    }

                    await Task.Delay(1000, stoppingToken);
                }
            }
            else
            {
                _logger.LogError("Task items are null. Please check the configuration.");
            }

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    if (_logger.IsEnabled(LogLevel.Information))
            //    {
            //        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //        _logger.LogInformation("Arguments: {args}", string.Join(", ", _args));
            //    }
            //    await Task.Delay(1000, stoppingToken);
            //}
        }
    }
}
