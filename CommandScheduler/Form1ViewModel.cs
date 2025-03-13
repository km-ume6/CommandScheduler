using csConfigurationManager;
using csTaskItem;
using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;

namespace CommandScheduler
{
    public partial class MainForm : Form
    {
        private TaskItem taskItem = new();
        private DoWorkEventArgs doWorkEventArgs = new(null);
        private ListViewItem? selectedItem;
        private Config? config = null;
        private readonly ConfigurationManager<Config> cmConfig = new();
        private readonly ConfigurationManager<List<TaskItem>> cmTasks = new("tasks.json");
        private readonly ConfigurationManager<Dictionary<string, int>> cmColumns = new("columnWidths.json");
        private TextBox? textBoxCurrent;
        private readonly List<int> IndexList = new();

        private void SaveColumnWidths()
        {
            var columnWidths = listViewTasks.Columns.Cast<ColumnHeader>()
                .ToDictionary(column => column.Text, column => column.Width);

            cmColumns.Save(columnWidths);
        }

        private void LoadColumnWidths()
        {
            var columnWidths = cmColumns.Load();
            if (columnWidths != null)
            {
                foreach (ColumnHeader column in listViewTasks.Columns)
                {
                    if (columnWidths.TryGetValue(column.Text, out int value))
                    {
                        column.Width = value;
                    }
                }
            }
        }

        string NormalizeFileName(string path)
        {
            if (File.Exists(path) && path.Contains(' '))
            {
                path = $"\"{path}\"";
            }

            return path;
        }

        private TaskItem Convert(ListViewItem lvItem)
        {
            var texts = lvItem.SubItems.Cast<ListViewItem.ListViewSubItem>()
                                       .Select(subItem => subItem.Text)
                                       .ToList();

            return new TaskItem(texts);
        }

        private void StartProcess(ListViewItem lvItem)
        {
            StartProcess(Convert(lvItem));
        }

        private void StartProcess(TaskItem taskItem)
        {
            //var start = new ProcessStartInfo
            //{
            //    FileName = taskItem.FileName,
            //    Arguments = $"{NormalizeFileName(taskItem.Script)} {NormalizeFileName(taskItem.Arguments)}",
            //    WorkingDirectory = taskItem.WorkingFolder,
            //    UseShellExecute = false,
            //    RedirectStandardOutput = true,
            //    RedirectStandardError = true
            //};

            //using (var process = Process.Start(start))
            //{
            //    if (process != null)
            //    {
            //        using (var reader = process.StandardOutput)
            //        {
            //            var result = reader.ReadToEnd();
            //            if (!string.IsNullOrEmpty(result))
            //            {
            //                var logFileName = Path.Combine(cmTasks.GetFolder()!, $"{taskItem.Title}.txt");
            //                if (!string.IsNullOrEmpty(logFileName))
            //                {
            //                    File.WriteAllText(NormalizeFileName(logFileName), result);
            //                }
            //            }
            //        }
            //    }
            //}

            _ =  taskItem.StartProcess(cmTasks.GetFolder()!);
        }

        void UpdateTaskList()
        {
            pauseWork = true;
            tasksForLoop.Clear();
            tasksForLoop.AddRange(listViewTasks.Items.Cast<ListViewItem>().Select(item => Convert(item)));
            pauseWork = false;
        }

        void LoopTasks()
        {
            pauseWork = true;
            IndexList.Clear();
            for (int index = 0; index < tasksForLoop.Count; index++)
            {
                var task = tasksForLoop[index];
                if (task.NextDateTime < DateTime.Now && task.EnableValue)
                {
                    IndexList.Add(index);
                    task.CalcNextDateTime();
                    BeginInvoke(new Action(InvokeFunction_SetNextDateTime));
                    Task.Run(() => StartProcess(task));
                }
            }
            pauseWork = false;
        }

        private void InvokeFunction_Clock()
        {
            if (textBoxDateTimeNow != null)
            {
                textBoxDateTimeNow.Text = DateTime.Now.ToString("HH:mm:ss");
            }
        }

        private void InvokeFunction_SetNextDateTime()
        {
            foreach (var index in IndexList)
            {
                if (listViewTasks.Items.Count > index)
                {
                    listViewTasks.Items[index].SubItems[7].Text = tasksForLoop[index].NextDateTimeToString();
                }
            }
        }

        private void ForStartup(bool register)
        {
            try
            {
                string appName = "CommandScheduler";
                string appPath = Application.ExecutablePath;

                using RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (registryKey != null)
                {
                    if (registryKey.GetValue(appName) == null)
                    {
                        if (register)
                        {
                            registryKey.SetValue(appName, appPath);
                        }
                    }
                    else
                    {
                        if (!register)
                        {
                            registryKey.DeleteValue(appName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("スタートアップへの登録に失敗しました: " + ex.Message);
            }

        }
    }
}
