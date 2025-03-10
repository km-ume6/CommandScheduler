namespace CommandScheduler
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            listViewTasks = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            columnHeader7 = new ColumnHeader();
            columnHeader8 = new ColumnHeader();
            columnHeader9 = new ColumnHeader();
            contextMenuStrip1 = new ContextMenuStrip(components);
            ToolStripMenuItemSaveColumnWidth = new ToolStripMenuItem();
            ToolStripMenuItemRun = new ToolStripMenuItem();
            ToolStripMenuItemForStartp = new ToolStripMenuItem();
            ToolStripMenuItemRegister = new ToolStripMenuItem();
            ToolStripMenuItemUnregister = new ToolStripMenuItem();
            ToolStripMenuItemAboutService = new ToolStripMenuItem();
            ToolStripMenuItemStartService = new ToolStripMenuItem();
            ToolStripMenuItemStopService = new ToolStripMenuItem();
            ToolStripMenuItemCreateService = new ToolStripMenuItem();
            ToolStripMenuItemDeleteService = new ToolStripMenuItem();
            ToolStripMenuItemGetServiceStatus = new ToolStripMenuItem();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBoxTitle = new TextBox();
            textBoxCommand = new TextBox();
            textBoxArguments = new TextBox();
            dateTimePickerStartDateTime = new DateTimePicker();
            label4 = new Label();
            buttonAdd = new Button();
            buttonUpdate = new Button();
            buttonDelete = new Button();
            textBoxFolder = new TextBox();
            label5 = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            textBoxDateTimeNow = new TextBox();
            buttonReference = new Button();
            label6 = new Label();
            textBoxScript = new TextBox();
            label7 = new Label();
            label8 = new Label();
            textBoxNextDateTime = new TextBox();
            textBoxCycle = new TextBox();
            checkBoxEnable = new CheckBox();
            buttonClear = new Button();
            notifyIcon1 = new NotifyIcon(components);
            contextMenuStripTray = new ContextMenuStrip(components);
            openToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            checkBoxLoop = new CheckBox();
            contextMenuStrip1.SuspendLayout();
            contextMenuStripTray.SuspendLayout();
            SuspendLayout();
            // 
            // listViewTasks
            // 
            listViewTasks.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5, columnHeader6, columnHeader7, columnHeader8, columnHeader9 });
            listViewTasks.ContextMenuStrip = contextMenuStrip1;
            listViewTasks.FullRowSelect = true;
            listViewTasks.Location = new Point(12, 210);
            listViewTasks.MultiSelect = false;
            listViewTasks.Name = "listViewTasks";
            listViewTasks.Size = new Size(776, 228);
            listViewTasks.TabIndex = 1;
            listViewTasks.UseCompatibleStateImageBehavior = false;
            listViewTasks.View = View.Details;
            listViewTasks.ItemSelectionChanged += listViewTasks_ItemSelectionChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "タイトル";
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "コマンド";
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "スクリプト";
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "オプション";
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "フォルダ";
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "開始";
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "周期";
            // 
            // columnHeader8
            // 
            columnHeader8.Text = "次回";
            // 
            // columnHeader9
            // 
            columnHeader9.Text = "有効?";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { ToolStripMenuItemSaveColumnWidth, ToolStripMenuItemRun, ToolStripMenuItemForStartp, ToolStripMenuItemAboutService });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(154, 100);
            // 
            // ToolStripMenuItemSaveColumnWidth
            // 
            ToolStripMenuItemSaveColumnWidth.Name = "ToolStripMenuItemSaveColumnWidth";
            ToolStripMenuItemSaveColumnWidth.Size = new Size(153, 24);
            ToolStripMenuItemSaveColumnWidth.Text = "列幅保存";
            ToolStripMenuItemSaveColumnWidth.Click += ToolStripMenuItemSaveColumnWidth_Click;
            // 
            // ToolStripMenuItemRun
            // 
            ToolStripMenuItemRun.Name = "ToolStripMenuItemRun";
            ToolStripMenuItemRun.Size = new Size(153, 24);
            ToolStripMenuItemRun.Text = "実行";
            ToolStripMenuItemRun.Click += ToolStripMenuItemRun_Click;
            // 
            // ToolStripMenuItemForStartp
            // 
            ToolStripMenuItemForStartp.DropDownItems.AddRange(new ToolStripItem[] { ToolStripMenuItemRegister, ToolStripMenuItemUnregister });
            ToolStripMenuItemForStartp.Name = "ToolStripMenuItemForStartp";
            ToolStripMenuItemForStartp.Size = new Size(153, 24);
            ToolStripMenuItemForStartp.Text = "スタートアップ";
            // 
            // ToolStripMenuItemRegister
            // 
            ToolStripMenuItemRegister.Name = "ToolStripMenuItemRegister";
            ToolStripMenuItemRegister.Size = new Size(122, 26);
            ToolStripMenuItemRegister.Text = "登録";
            ToolStripMenuItemRegister.Click += ToolStripMenuItemRegister_Click;
            // 
            // ToolStripMenuItemUnregister
            // 
            ToolStripMenuItemUnregister.Name = "ToolStripMenuItemUnregister";
            ToolStripMenuItemUnregister.Size = new Size(122, 26);
            ToolStripMenuItemUnregister.Text = "削除";
            ToolStripMenuItemUnregister.Click += ToolStripMenuItemUnregister_Click;
            // 
            // ToolStripMenuItemAboutService
            // 
            ToolStripMenuItemAboutService.DropDownItems.AddRange(new ToolStripItem[] { ToolStripMenuItemStartService, ToolStripMenuItemStopService, ToolStripMenuItemCreateService, ToolStripMenuItemDeleteService, ToolStripMenuItemGetServiceStatus });
            ToolStripMenuItemAboutService.Name = "ToolStripMenuItemAboutService";
            ToolStripMenuItemAboutService.Size = new Size(153, 24);
            ToolStripMenuItemAboutService.Text = "サービス";
            // 
            // ToolStripMenuItemStartService
            // 
            ToolStripMenuItemStartService.Name = "ToolStripMenuItemStartService";
            ToolStripMenuItemStartService.Size = new Size(122, 26);
            ToolStripMenuItemStartService.Text = "開始";
            ToolStripMenuItemStartService.Click += ToolStripMenuItemStartService_Click;
            // 
            // ToolStripMenuItemStopService
            // 
            ToolStripMenuItemStopService.Name = "ToolStripMenuItemStopService";
            ToolStripMenuItemStopService.Size = new Size(122, 26);
            ToolStripMenuItemStopService.Text = "停止";
            ToolStripMenuItemStopService.Click += ToolStripMenuItemStopService_Click;
            // 
            // ToolStripMenuItemCreateService
            // 
            ToolStripMenuItemCreateService.Name = "ToolStripMenuItemCreateService";
            ToolStripMenuItemCreateService.Size = new Size(122, 26);
            ToolStripMenuItemCreateService.Text = "登録";
            ToolStripMenuItemCreateService.Click += ToolStripMenuItemCreateService_Click;
            // 
            // ToolStripMenuItemDeleteService
            // 
            ToolStripMenuItemDeleteService.Name = "ToolStripMenuItemDeleteService";
            ToolStripMenuItemDeleteService.Size = new Size(122, 26);
            ToolStripMenuItemDeleteService.Text = "削除";
            ToolStripMenuItemDeleteService.Click += ToolStripMenuItemDeleteService_Click;
            // 
            // ToolStripMenuItemGetServiceStatus
            // 
            ToolStripMenuItemGetServiceStatus.Name = "ToolStripMenuItemGetServiceStatus";
            ToolStripMenuItemGetServiceStatus.Size = new Size(122, 26);
            ToolStripMenuItemGetServiceStatus.Text = "状況";
            ToolStripMenuItemGetServiceStatus.Click += ToolStripMenuItemGetServiceStatus_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 49);
            label1.Name = "label1";
            label1.Size = new Size(52, 20);
            label1.TabIndex = 2;
            label1.Text = "コマンド";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 113);
            label2.Name = "label2";
            label2.Size = new Size(62, 20);
            label2.TabIndex = 3;
            label2.Text = "オプション";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 147);
            label3.Name = "label3";
            label3.Size = new Size(53, 20);
            label3.TabIndex = 4;
            label3.Text = "フォルダ";
            // 
            // textBoxTitle
            // 
            textBoxTitle.Location = new Point(97, 12);
            textBoxTitle.Name = "textBoxTitle";
            textBoxTitle.Size = new Size(591, 27);
            textBoxTitle.TabIndex = 5;
            textBoxTitle.Tag = "tbTitle";
            // 
            // textBoxCommand
            // 
            textBoxCommand.Location = new Point(97, 45);
            textBoxCommand.Name = "textBoxCommand";
            textBoxCommand.Size = new Size(591, 27);
            textBoxCommand.TabIndex = 6;
            textBoxCommand.Tag = "tbCommand";
            textBoxCommand.MouseClick += textBox_MouseClick;
            textBoxCommand.DoubleClick += buttonReference_Click;
            // 
            // textBoxArguments
            // 
            textBoxArguments.Location = new Point(97, 111);
            textBoxArguments.Name = "textBoxArguments";
            textBoxArguments.Size = new Size(591, 27);
            textBoxArguments.TabIndex = 7;
            textBoxArguments.Tag = "tbOption";
            textBoxArguments.MouseClick += textBox_MouseClick;
            textBoxArguments.DoubleClick += buttonReference_Click;
            // 
            // dateTimePickerStartDateTime
            // 
            dateTimePickerStartDateTime.CustomFormat = "yyyy/MM/dd HH:mm";
            dateTimePickerStartDateTime.Format = DateTimePickerFormat.Custom;
            dateTimePickerStartDateTime.Location = new Point(57, 177);
            dateTimePickerStartDateTime.Name = "dateTimePickerStartDateTime";
            dateTimePickerStartDateTime.Size = new Size(160, 27);
            dateTimePickerStartDateTime.TabIndex = 8;
            dateTimePickerStartDateTime.ValueChanged += dateTimePicker_ValueChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 182);
            label4.Name = "label4";
            label4.Size = new Size(39, 20);
            label4.TabIndex = 9;
            label4.Text = "開始";
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(694, 12);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(94, 29);
            buttonAdd.TabIndex = 10;
            buttonAdd.Text = "追加";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += button_Click;
            // 
            // buttonUpdate
            // 
            buttonUpdate.Location = new Point(694, 45);
            buttonUpdate.Name = "buttonUpdate";
            buttonUpdate.Size = new Size(94, 29);
            buttonUpdate.TabIndex = 11;
            buttonUpdate.Text = "更新";
            buttonUpdate.UseVisualStyleBackColor = true;
            buttonUpdate.Click += button_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(694, 77);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(94, 29);
            buttonDelete.TabIndex = 12;
            buttonDelete.Text = "削除";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += button_Click;
            // 
            // textBoxFolder
            // 
            textBoxFolder.Location = new Point(97, 144);
            textBoxFolder.Name = "textBoxFolder";
            textBoxFolder.Size = new Size(591, 27);
            textBoxFolder.TabIndex = 13;
            textBoxFolder.Tag = "tbFolder";
            textBoxFolder.MouseClick += textBox_MouseClick;
            textBoxFolder.DoubleClick += buttonReference_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 16);
            label5.Name = "label5";
            label5.Size = new Size(53, 20);
            label5.TabIndex = 14;
            label5.Text = "タイトル";
            // 
            // backgroundWorker1
            // 
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            // 
            // textBoxDateTimeNow
            // 
            textBoxDateTimeNow.Font = new Font("Yu Gothic UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 128);
            textBoxDateTimeNow.Location = new Point(655, 179);
            textBoxDateTimeNow.Name = "textBoxDateTimeNow";
            textBoxDateTimeNow.ReadOnly = true;
            textBoxDateTimeNow.Size = new Size(62, 25);
            textBoxDateTimeNow.TabIndex = 15;
            textBoxDateTimeNow.TextAlign = HorizontalAlignment.Right;
            // 
            // buttonReference
            // 
            buttonReference.Location = new Point(694, 144);
            buttonReference.Name = "buttonReference";
            buttonReference.Size = new Size(94, 27);
            buttonReference.TabIndex = 16;
            buttonReference.Text = "参照";
            buttonReference.UseVisualStyleBackColor = true;
            buttonReference.Click += buttonReference_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 81);
            label6.Name = "label6";
            label6.Size = new Size(62, 20);
            label6.TabIndex = 17;
            label6.Text = "スクリプト";
            // 
            // textBoxScript
            // 
            textBoxScript.Location = new Point(97, 78);
            textBoxScript.Name = "textBoxScript";
            textBoxScript.Size = new Size(591, 27);
            textBoxScript.TabIndex = 18;
            textBoxScript.Tag = "tbScript";
            textBoxScript.MouseClick += textBox_MouseClick;
            textBoxScript.DoubleClick += buttonReference_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(223, 182);
            label7.Name = "label7";
            label7.Size = new Size(117, 20);
            label7.TabIndex = 19;
            label7.Text = "周期(D?/H?/M?)";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(382, 182);
            label8.Name = "label8";
            label8.Size = new Size(69, 20);
            label8.TabIndex = 21;
            label8.Text = "次回実行";
            // 
            // textBoxNextDateTime
            // 
            textBoxNextDateTime.BorderStyle = BorderStyle.FixedSingle;
            textBoxNextDateTime.Enabled = false;
            textBoxNextDateTime.Location = new Point(457, 178);
            textBoxNextDateTime.Name = "textBoxNextDateTime";
            textBoxNextDateTime.Size = new Size(125, 27);
            textBoxNextDateTime.TabIndex = 22;
            textBoxNextDateTime.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxCycle
            // 
            textBoxCycle.Location = new Point(346, 177);
            textBoxCycle.Name = "textBoxCycle";
            textBoxCycle.Size = new Size(30, 27);
            textBoxCycle.TabIndex = 23;
            textBoxCycle.TextChanged += dateTimePicker_ValueChanged;
            // 
            // checkBoxEnable
            // 
            checkBoxEnable.AutoSize = true;
            checkBoxEnable.Location = new Point(588, 181);
            checkBoxEnable.Name = "checkBoxEnable";
            checkBoxEnable.Size = new Size(61, 24);
            checkBoxEnable.TabIndex = 24;
            checkBoxEnable.Text = "有効";
            checkBoxEnable.UseVisualStyleBackColor = true;
            // 
            // buttonClear
            // 
            buttonClear.Location = new Point(694, 111);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new Size(94, 27);
            buttonClear.TabIndex = 25;
            buttonClear.Text = "クリア";
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += button_Click;
            // 
            // notifyIcon1
            // 
            notifyIcon1.ContextMenuStrip = contextMenuStripTray;
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "コマンドスケジューラー";
            notifyIcon1.Visible = true;
            notifyIcon1.DoubleClick += NotifyIcon_DoubleClick;
            // 
            // contextMenuStripTray
            // 
            contextMenuStripTray.ImageScalingSize = new Size(20, 20);
            contextMenuStripTray.Items.AddRange(new ToolStripItem[] { openToolStripMenuItem, exitToolStripMenuItem });
            contextMenuStripTray.Name = "contextMenuStripTray";
            contextMenuStripTray.Size = new Size(115, 52);
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(114, 24);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(114, 24);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // checkBoxLoop
            // 
            checkBoxLoop.AutoSize = true;
            checkBoxLoop.Checked = true;
            checkBoxLoop.CheckState = CheckState.Checked;
            checkBoxLoop.Location = new Point(723, 181);
            checkBoxLoop.Name = "checkBoxLoop";
            checkBoxLoop.Size = new Size(65, 24);
            checkBoxLoop.TabIndex = 26;
            checkBoxLoop.Text = "Loop";
            checkBoxLoop.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(checkBoxLoop);
            Controls.Add(buttonClear);
            Controls.Add(checkBoxEnable);
            Controls.Add(textBoxCycle);
            Controls.Add(textBoxNextDateTime);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(textBoxScript);
            Controls.Add(label6);
            Controls.Add(buttonReference);
            Controls.Add(textBoxDateTimeNow);
            Controls.Add(label5);
            Controls.Add(textBoxFolder);
            Controls.Add(buttonDelete);
            Controls.Add(buttonUpdate);
            Controls.Add(buttonAdd);
            Controls.Add(label4);
            Controls.Add(dateTimePickerStartDateTime);
            Controls.Add(textBoxArguments);
            Controls.Add(textBoxCommand);
            Controls.Add(textBoxTitle);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listViewTasks);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "CommandScheduler";
            FormClosing += Form1_FormClosing;
            FormClosed += Form1_FormClosed;
            Load += Form1_Load;
            contextMenuStrip1.ResumeLayout(false);
            contextMenuStripTray.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView listViewTasks;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBoxTitle;
        private TextBox textBoxCommand;
        private TextBox textBoxArguments;
        private DateTimePicker dateTimePickerStartDateTime;
        private Label label4;
        private Button buttonAdd;
        private Button buttonUpdate;
        private Button buttonDelete;
        private TextBox textBoxFolder;
        private Label label5;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem ToolStripMenuItemSaveColumnWidth;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private TextBox textBoxDateTimeNow;
        private ToolStripMenuItem ToolStripMenuItemRun;
        private Button buttonReference;
        private Label label6;
        private TextBox textBoxScript;
        private ColumnHeader columnHeader6;
        private Label label7;
        private ColumnHeader columnHeader7;
        private Label label8;
        private ColumnHeader columnHeader8;
        private TextBox textBoxNextDateTime;
        private TextBox textBoxCycle;
        private ColumnHeader columnHeader9;
        private CheckBox checkBoxEnable;
        private Button buttonClear;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStripTray;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem ToolStripMenuItemForStartp;
        private ToolStripMenuItem ToolStripMenuItemRegister;
        private ToolStripMenuItem ToolStripMenuItemUnregister;
        private ToolStripMenuItem ToolStripMenuItemAboutService;
        private ToolStripMenuItem ToolStripMenuItemStartService;
        private ToolStripMenuItem ToolStripMenuItemStopService;
        private ToolStripMenuItem ToolStripMenuItemCreateService;
        private ToolStripMenuItem ToolStripMenuItemDeleteService;
        private ToolStripMenuItem ToolStripMenuItemGetServiceStatus;
        private CheckBox checkBoxLoop;
    }
}
