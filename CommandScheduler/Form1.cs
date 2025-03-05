using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using Button = System.Windows.Forms.Button;

namespace CommandScheduler
{
    public partial class MainForm : Form
    {
        private (bool auto, int index) AutoMode = (false, -1);
        private List<TaskItem> tasksForLoop = new();
        private bool pauseWork = false;
        private List<Control> controlList = new();
        private bool CancelCalculation = false;
        private NotifyIcon notifyIcon;

        public MainForm()
        {
            notifyIcon = new NotifyIcon();
            doWorkEventArgs.Cancel = false;

            // フォームのサイズ変更を禁止
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // フォームを画面中央に表示
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeComponent();

            LoadColumnWidths();

            ProcessCommandLineArgs();
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void ShowForm()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void ProcessCommandLineArgs()
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 2 && args[1].Contains("run", StringComparison.OrdinalIgnoreCase) && int.TryParse(args[2], out int index))
            {
                AutoMode.auto = true;
                AutoMode.index = index;
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                UpdateTaskItemFromControls();

                switch (button.Name)
                {
                    case "buttonAdd":
                        AddTaskItemToListView();
                        break;
                    case "buttonUpdate":
                        UpdateSelectedTaskItemInListView();
                        break;
                    case "buttonDelete":
                        DeleteSelectedTaskItemFromListView();
                        break;
                    case "buttonClear":
                        ClearControls();
                        break;
                }

                UpdateTaskList();
            }
        }

        private void UpdateTaskItemFromControls()
        {
            for (int i = 0; i < controlList.Count; i++)
            {
                switch (controlList[i])
                {
                    case TextBox textBox:
                        taskItem[i] = textBox.Text;
                        break;
                    case DateTimePicker dateTimePicker:
                        taskItem[i] = dateTimePicker.Value;
                        break;
                    case CheckBox checkBox:
                        taskItem[i] = checkBox.Checked;
                        break;
                    default:
                        throw new InvalidOperationException("Unsupported control type");
                }
            }
        }

        private void AddTaskItemToListView()
        {
            listViewTasks.Items.Add(new ListViewItem(new string[]
            {
                        taskItem.Title,
                        taskItem.FileName,
                        taskItem.Script,
                        taskItem.Arguments,
                        taskItem.WorkingFolder,
                        taskItem.StartDateTime.ToString("yyyy/MM/dd HH:mm"),
                        taskItem.Cycle,
                        taskItem.NextDateTimeToString(),
                        taskItem.Enable
            }));
        }

        private void UpdateSelectedTaskItemInListView()
        {
            if (listViewTasks.SelectedItems.Count > 0 && selectedItem != null)
            {
                for (int i = 0; i < controlList.Count; i++)
                {
                    selectedItem.SubItems[i].Text = controlList[i] switch
                    {
                        TextBox textBox => textBox.Text,
                        DateTimePicker dateTimePicker => dateTimePicker.Value.ToString("yyyy/MM/dd HH:mm"),
                        CheckBox checkBox => checkBox.Checked ? "有効" : "無効",
                        _ => throw new InvalidOperationException("Unsupported control type")
                    };
                }
            }
        }

        private void DeleteSelectedTaskItemFromListView()
        {
            if (listViewTasks.SelectedItems.Count > 0 && selectedItem != null)
            {
                listViewTasks.Items.Remove(selectedItem);
            }
        }

        private void ClearControls()
        {
            foreach (Control? control in controlList)
            {
                switch (control)
                {
                    case TextBox textBox:
                        textBox.Text = string.Empty;
                        break;
                    case DateTimePicker dateTimePicker:
                        dateTimePicker.Value = DateTime.Now;
                        break;
                    case CheckBox checkBox:
                        checkBox.Checked = false;
                        break;
                    default:
                        throw new InvalidOperationException("Unsupported control type");
                }
            }
        }

        private void ToolStripMenuItemSaveColumnWidth_Click(object sender, EventArgs e)
        {
            SaveColumnWidths();
        }

        private void listViewTasks_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected && e.Item != null)
            {
                CancelCalculation = true;

                selectedItem = e.Item;
                for (int i = 0; i < controlList.Count; i++)
                {
                    switch (controlList[i])
                    {
                        case TextBox textBox:
                            textBox.Text = selectedItem.SubItems[i].Text;
                            break;
                        case DateTimePicker dateTimePicker:
                            dateTimePicker.Value = DateTime.Parse(selectedItem.SubItems[i].Text);
                            break;
                        case CheckBox checkBox:
                            checkBox.Checked = selectedItem.SubItems[i].Text == "有効";
                            break;
                        default:
                            throw new InvalidOperationException("Unsupported control type");
                    }
                }
                CancelCalculation = false;

                textBoxNextDateTime.Text = taskItem.NextDateTimeToString();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            backgroundWorker1.CancelAsync();

            List<TaskItem> tasks = new();
            foreach (ListViewItem item in listViewTasks.Items)
            {
                tasks.Add(new TaskItem(item));
            }

            cmTasks.Save(tasks);
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (!e.Cancel)
            {
                if (!pauseWork)
                {
                    BeginInvoke(new Action(InvokeFunction_Clock));
                    LoopTasks();
                }

                Thread.Sleep(1000);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            doWorkEventArgs.Cancel = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var tasks = cmTasks.Load();
            if (tasks != null)
            {
                listViewTasks.Items.Clear();
                foreach (var task in tasks)
                {
                    task.InitNextDateTime();

                    var item = new ListViewItem(new string[]
                    {
                                (string)task[0],
                                (string)task[1],
                                (string)task[2],
                                (string)task[3],
                                (string)task[4],
                                (string)task[5],
                                (string)task[6],
                                (string)task[7],
                                (string)task[8]
                    });
                    listViewTasks.Items.Add(item);
                }
            }

            // コントロールをリストに追加
            controlList.Add(textBoxTitle);
            controlList.Add(textBoxCommand);
            controlList.Add(textBoxScript);
            controlList.Add(textBoxArguments);
            controlList.Add(textBoxFolder);
            controlList.Add(dateTimePickerStartDateTime);
            controlList.Add(textBoxCycle);
            controlList.Add(textBoxNextDateTime);
            controlList.Add(checkBoxEnable);

            UpdateTaskList();

            if (AutoMode.auto && AutoMode.index > 0 && listViewTasks.Items.Count >= AutoMode.index)
            {
                StartProcess(listViewTasks.Items[AutoMode.index - 1]);
                this.Close();
            }

            backgroundWorker1.RunWorkerAsync(doWorkEventArgs);
        }

        private void buttonReference_Click(object sender, EventArgs e)
        {
            if (textBoxCurrent != null)
            {
                if ((string?)textBoxCurrent.Tag == "tbFolder")
                {
                    using var folderBrowserDialog = new FolderBrowserDialog();
                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        textBoxCurrent.Text = folderBrowserDialog.SelectedPath;
                    }
                }
                else
                {
                    using var openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        textBoxCurrent.Text = openFileDialog.FileName;
                    }
                }
            }
        }

        private void textBox_MouseClick(object sender, MouseEventArgs e)
        {
            textBoxCurrent = (TextBox)sender;
        }

        private void ToolStripMenuItemRun_Click(object sender, EventArgs e)
        {
            if (listViewTasks.SelectedItems.Count > 0)
            {
                var selectedItem = listViewTasks.SelectedItems[0];
                Task.Run(() => StartProcess(selectedItem));
            }
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            taskItem.StartDateTime = dateTimePickerStartDateTime.Value;
            taskItem.Cycle = textBoxCycle.Text;
            taskItem.InitNextDateTime();
            textBoxNextDateTime.Text = taskItem.NextDateTimeToString();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ToolStripMenuItemRegisterForStartp_Click(object sender, EventArgs e)
        {
            try
            {
                string appName = "CommandScheduler";
                string appPath = Application.ExecutablePath;

                using RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (registryKey != null && registryKey.GetValue(appName) == null)
                {
                    registryKey.SetValue(appName, appPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("スタートアップへの登録に失敗しました: " + ex.Message);
            }
        }

        private void ToolStripMenuItemRegister_Click(object sender, EventArgs e)
        {
            ForStartup(register: true);
        }

        private void ToolStripMenuItemUnregister_Click(object sender, EventArgs e)
        {
            ForStartup(register: false);
        }
    }
}
