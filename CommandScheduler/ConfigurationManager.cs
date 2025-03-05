using System.Text.Json;

namespace CommandScheduler
{
    public class ConfigurationManager<T>
    {
        private string _filePath = null!;
        public string? GetFolder() => Path.GetDirectoryName(_filePath);

        public ConfigurationManager()
        {
            SetDefaultPath();
        }

        public ConfigurationManager(string filePath)
        {
            if (Path.IsPathRooted(filePath))
            {
                _filePath = filePath;
            }
            else
            {
                SetDefaultPath(filePath);
            }

        }

        private string MakeDataFolder()
        {
            string? processPath = Environment.ProcessPath;
            return processPath == null
                ? throw new InvalidOperationException("Process path is not available.")
                : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Path.GetFileNameWithoutExtension(processPath));
        }

        public void ChangeFileName(string fileName)
        {
            if (_filePath == null)
            {
                throw new InvalidOperationException("File path is not set.");
            }

            _filePath = Path.Combine(Path.GetDirectoryName(_filePath)!, fileName);
        }

        private void SetDefaultPath(string fileName = "")
        {
            string? executablePath = Environment.ProcessPath;
            if (executablePath != null)
            {
                _filePath = MakeDataFolder();

                string? folderPath = MakeDataFolder();
                if (folderPath == null)
                {
                    throw new InvalidOperationException("Folder path could not be determined.");
                }
                else
                {
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    _filePath = Path.Combine(folderPath, fileName != "" ? fileName : "config.json");
                }
            }
            else
            {
                throw new InvalidOperationException("Process path is not available.");
            }
        }

        public T? Load()
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<T>(json);
            }
            return default;
        }

        public void Save(T config)
        {
            string json = JsonSerializer.Serialize(config);
            File.WriteAllText(_filePath, json);
        }
    }
}
