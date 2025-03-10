using csTaskItem;
using Microsoft.VisualBasic;
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

            // �t�H�[���̃T�C�Y�ύX���֎~
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // �t�H�[������ʒ����ɕ\��
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeComponent();

            LoadColumnWidths();

            ProcessCommandLineArgs();   // �N���I�v�V�����̉��
        }

        /// <summary>
        /// �g���C�A�C�R���̃_�u���N���b�N�C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            ShowForm(); // �A�v���P�[�V�����E�B���h�E��\��
        }

        /// <summary>
        /// �A�v���P�[�V�����E�B���h�E��\������
        /// </summary>
        private void ShowForm()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
        }

        /// <summary>
        /// �t�H�[���̃��T�C�Y�C�x���g
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        /// <summary>
        /// �N���I�v�V�����̉��
        /// �p�����[�^���Q���A�ŏ��̃p�����[�^��"run"�ŁA�Q�ڂ����l�̏ꍇ�A�������s���[�h�Ƃ��ċN��
        /// </summary>
        private void ProcessCommandLineArgs()
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 2 && args[1].Contains("run", StringComparison.OrdinalIgnoreCase) && int.TryParse(args[2], out int index))
            {
                AutoMode.auto = true;
                AutoMode.index = index;
            }
        }

        /// <summary>
        /// ���C���E�B���h�E�̃{�^���N���b�N�C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// �e�R���g���[���̃v���p�e�B��TaskItem�ɔ��f
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
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

        /// <summary>
        /// �^�X�N���X�g��ListView�̕\���ɔ��f
        /// </summary>
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

        /// <summary>
        /// ListView�őI������Ă��鍀�ڂ̓��e���X�V
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
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
                        CheckBox checkBox => checkBox.Checked ? "�L��" : "����",
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

        /// <summary>
        /// �e�R���g���[���̕\����������
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
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
                            checkBox.Checked = selectedItem.SubItems[i].Text == "�L��";
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
                tasks.Add(Convert(item));
            }

            cmTasks.Save(tasks);
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (!e.Cancel)
            {
                if (!pauseWork && checkBoxLoop.Checked)
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
            config = cmConfig.Load();
            if (config == null)
            {
                config = new();
                cmConfig.Save(config);
            }

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

            // �R���g���[�������X�g�ɒǉ�
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
                MessageBox.Show("�X�^�[�g�A�b�v�ւ̓o�^�Ɏ��s���܂���: " + ex.Message);
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


        private void ToolStripMenuItemCreateService_Click(object sender, EventArgs e)
        {
            ManageService("create");
        }

        private void ToolStripMenuItemStartService_Click(object sender, EventArgs e)
        {
            ManageService("start");
        }

        private void ToolStripMenuItemStopService_Click(object sender, EventArgs e)
        {
            ManageService("stop");
        }

        private void ToolStripMenuItemDeleteService_Click(object sender, EventArgs e)
        {
            ManageService("delete");
        }

        private void ToolStripMenuItemGetServiceStatus_Click(object sender, EventArgs e)
        {
            ManageService("query");
        }

        private void ManageService(string command)
        {
            if (config == null)
            {
                MessageBox.Show("�ݒ肪�ǂݍ��܂�Ă��܂���B");
                return;
            }

            string servicePath = config.ServicePath;
            string serviceArgs = config.ServiceArg;
            string serviceName = Path.GetFileNameWithoutExtension(servicePath);

            if (string.IsNullOrEmpty(servicePath))
            {
                MessageBox.Show("�T�[�r�X�̎��s�t�@�C���p�X���ݒ肳��Ă��܂���B");
                return;
            }

            string scCommand = command.ToLower() switch
            {
                "create" => $"sc create {serviceName} binPath= \"{servicePath} {serviceArgs}\"",
                "delete" => $"sc delete {serviceName}",
                "start" => $"sc start {serviceName}",
                "stop" => $"sc stop {serviceName}",
                "query" => $"sc query {serviceName}",
                _ => throw new ArgumentException("�����ȃR�}���h�ł��B")
            };

            bool flag = command.ToLower() == "query" ? false : true;

            ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd.exe", "/c " + scCommand)
            {
                Verb = "runas",
                UseShellExecute = flag,
                CreateNoWindow = false,
                RedirectStandardOutput = !flag,
                RedirectStandardError = !flag
            };

            string output = "";
            string error = "";
            try
            {
                using Process? process = Process.Start(processStartInfo);
                if (process != null)
                {
                    if (!flag)
                    {
                        output = process.StandardOutput.ReadToEnd();
                        error = process.StandardError.ReadToEnd();
                    }
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        MessageBox.Show($"{command} �R�}���h���������܂����B\n{output}");
                    }
                    else
                    {
                        MessageBox.Show($"{command} �R�}���h�����s���܂����B\nEitCode:{process.ExitCode}\nOutput:{output}\nError:{error}");
                    }
                }
                else
                {
                    MessageBox.Show("�v���Z�X�̊J�n�Ɏ��s���܂����B");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{command} �R�}���h�̎��s���ɃG���[���������܂���: " + ex.Message);
            }
        }

        private void checkBoxLoop_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
