using System.CodeDom;
using System.ComponentModel;
using System.Diagnostics;

namespace csTaskItem
{
    internal class Cycle
    {
        private string cycleValue;

        public Cycle()
        {
            cycleValue = string.Empty;
        }

        public Cycle(string s)
        {
            cycleValue = s;
        }

        public void SetValue(string s)
        {
            cycleValue = s;
        }

        public int Day()
        {
            return GetCycleValue('D', 'd');
        }

        public int Hour()
        {
            return GetCycleValue('H', 'h');
        }

        public int Minute()
        {
            return GetCycleValue('M', 'm');
        }

        public int Second()
        {
            return GetCycleValue('S', 's');
        }

        private int GetCycleValue(char upper, char lower)
        {
            if (!string.IsNullOrEmpty(cycleValue) && (cycleValue[0] == upper || cycleValue[0] == lower))
            {
                if (int.TryParse(cycleValue.Substring(1), out int result))
                {
                    return result;
                }
            }
            return 0;
        }

        public static explicit operator string(Cycle cycle)
        {
            return cycle.cycleValue;
        }
    }

    internal class TaskItem
    {
        public string Title { get; set; }
        public string FileName { get; set; }
        public string Script { get; set; }
        public string Arguments { get; set; }
        public string WorkingFolder { get; set; }
        public DateTime StartDateTime { get; set; }
        public Cycle CycleValue { get; private set; }
        public string Cycle
        {
            get => (string)CycleValue;
            set => CycleValue.SetValue(value);
        }
        public DateTime NextDateTime { get; set; }
        public bool EnableValue { get; set; }
        public string Enable
        {
            get => EnableValue ? "有効" : "無効";
            set => EnableValue = value == "有効" || value == "True";
        }

        private DateTime ThatDateTime = DateTime.Now;

        public TaskItem()
        {
            Title = string.Empty;
            FileName = string.Empty;
            Script = string.Empty;
            Arguments = string.Empty;
            WorkingFolder = string.Empty;
            StartDateTime = DateTime.Now;
            CycleValue = new Cycle();
            NextDateTime = DateTime.MinValue;
            Enable = "無効";
            ThatDateTime = DateTime.Now;
        }

        public TaskItem(List<string> strItem) : this()
        {
            Title = strItem[0];
            FileName = strItem[1];
            Script = strItem[2];
            Arguments = strItem[3];
            WorkingFolder = strItem[4];
            StartDateTime = !string.IsNullOrEmpty(strItem[5]) ? DateTime.Parse(strItem[5]) : DateTime.MinValue;
            Cycle = strItem[6];
            NextDateTime = InitNextDateTime();
            Enable = strItem[8];
        }

        public bool CheckCycle()
        {
            if (string.IsNullOrEmpty(Cycle))
            {
                return false;
            }

            char firstChar = Cycle[0];
            if (!"DdHhMmSs".Contains(firstChar))
            {
                return false;
            }

            string numberPart = Cycle.Substring(1);
            return int.TryParse(numberPart, out _);
        }

        public DateTime InitNextDateTime()
        {
            if (CheckCycle())
            {
                ThatDateTime = DateTime.Now;
                if (StartDateTime <= ThatDateTime)
                {
                    DateTime dt = StartDateTime;
                    while (dt <= ThatDateTime)
                    {
                        dt = dt.AddDays(CycleValue.Day()).AddHours(CycleValue.Hour()).AddMinutes(CycleValue.Minute()).AddSeconds(CycleValue.Second());
                    }

                    NextDateTime = dt;
                }
                else
                {
                    NextDateTime = StartDateTime;
                }
            }

            return NextDateTime;
        }

        public void CalcNextDateTime()
        {
            NextDateTime = NextDateTime.AddDays(CycleValue.Day()).AddHours(CycleValue.Hour()).AddMinutes(CycleValue.Minute()).AddSeconds(CycleValue.Second());
        }

        public string NextDateTimeToString()
        {
            return NextDateTime.ToString("yyyy/MM/dd HH:mm");
        }

        public object this[int index]
        {
            get
            {
                return index switch
                {
                    0 => Title,
                    1 => FileName,
                    2 => Script,
                    3 => Arguments,
                    4 => WorkingFolder,
                    5 => StartDateTime.ToString("yyyy/MM/dd HH:mm"),
                    6 => Cycle,
                    7 => NextDateTimeToString(),
                    8 => Enable,
                    _ => throw new IndexOutOfRangeException("Invalid index")
                };
            }
            set
            {
                switch (index)
                {
                    case 0: Title = (string)value; break;
                    case 1: FileName = (string)value; break;
                    case 2: Script = (string)value; break;
                    case 3: Arguments = (string)value; break;
                    case 4: WorkingFolder = (string)value; break;
                    case 5: StartDateTime = (DateTime)value; break;
                    case 6: Cycle = (string)value; break;
                    case 7: /*NextDateTime = DateTime.Parse((string)value);*/ break;
                    case 8: EnableValue = (bool)value; break;
                    default: throw new IndexOutOfRangeException("Invalid index");
                }
            }
        }
        public async Task StartProcess(string logPath)
        {
            string fileName = NormalizeFileName(FileName != string.Empty ? FileName : Script);
            string lfn = Title != "" ? Title : "Title";
            string logFileName = Path.Combine(logPath, $"{lfn}.log");
            bool flag = FileName != string.Empty;

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = Arguments,
                    WorkingDirectory = WorkingFolder,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = flag,
                    RedirectStandardError = flag,
                    UseShellExecute = !flag,
                    CreateNoWindow = true
                };

                using Process process = new Process { StartInfo = startInfo };
                process.Start();

                string output = "";
                string error = "";
                if (flag)
                {
                    output = await process.StandardOutput.ReadToEndAsync();
                    error = await process.StandardError.ReadToEndAsync();
                }
                process.WaitForExit();

                if (process.ExitCode == 0)
                {
                    File.AppendAllText(logFileName, $"Task {Title} started successfully.\nOutput: {output}\n");
                }
                else
                {
                    File.AppendAllText(logFileName, $"Task {Title} failed to start.\nError: {error}\n");
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(logFileName, $"Task {Title} encountered an error: {ex.Message}\n");
                throw;
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
    }
}
