namespace Sky.ProGate.ServiceAdmin
{
    partial class ServiceAdminForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceAdminForm));
            this.FormMenu = new System.Windows.Forms.MenuStrip();
            this.ApplicationFormMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitFormMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ServiceFormMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.StartServiceFormMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.StopServiceFormMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ServicePropertiesFormMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.cOMMANDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TaskFormMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.RunTaskFormMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.TaskPropertiesFormMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpFormMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.InfoFormMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ShowHelpFormMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.FormStatus = new System.Windows.Forms.StatusStrip();
            this.ConfigStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ServiceStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.FormPanel = new System.Windows.Forms.Panel();
            this.TaskProgressSplitter = new System.Windows.Forms.SplitContainer();
            this.TaskListView = new Sky.Library.Forms.TreeListView();
            this.ColHdr_Tsk_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColHdr_Tsk_Act = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColHdr_Tsk_StartTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColHdr_Tsk_EndTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColHdr_Tsk_LastRunDat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColHdr_Tsk_LastRunDur = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IconList = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.MessageStatusBox = new System.Windows.Forms.PictureBox();
            this.MessageTimeStampTextBox = new System.Windows.Forms.TextBox();
            this.MessagePercentTextBox = new System.Windows.Forms.TextBox();
            this.MessageCountTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.MessageStepTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.MessageTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MessageProgressBar = new System.Windows.Forms.ProgressBar();
            this.FormMenu.SuspendLayout();
            this.FormStatus.SuspendLayout();
            this.FormPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TaskProgressSplitter)).BeginInit();
            this.TaskProgressSplitter.Panel1.SuspendLayout();
            this.TaskProgressSplitter.Panel2.SuspendLayout();
            this.TaskProgressSplitter.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MessageStatusBox)).BeginInit();
            this.SuspendLayout();
            // 
            // FormMenu
            // 
            this.FormMenu.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ApplicationFormMenu,
            this.ServiceFormMenu,
            this.TaskFormMenu,
            this.HelpFormMenu});
            this.FormMenu.Location = new System.Drawing.Point(0, 0);
            this.FormMenu.Name = "FormMenu";
            this.FormMenu.Size = new System.Drawing.Size(784, 24);
            this.FormMenu.TabIndex = 1;
            this.FormMenu.Text = "menuStrip1";
            // 
            // ApplicationFormMenu
            // 
            this.ApplicationFormMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitFormMenu});
            this.ApplicationFormMenu.Name = "ApplicationFormMenu";
            this.ApplicationFormMenu.Size = new System.Drawing.Size(82, 20);
            this.ApplicationFormMenu.Text = "Application";
            // 
            // ExitFormMenu
            // 
            this.ExitFormMenu.Name = "ExitFormMenu";
            this.ExitFormMenu.Size = new System.Drawing.Size(96, 22);
            this.ExitFormMenu.Text = "Exit";
            this.ExitFormMenu.Click += new System.EventHandler(this.OnExit);
            // 
            // ServiceFormMenu
            // 
            this.ServiceFormMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartServiceFormMenu,
            this.StopServiceFormMenu,
            this.toolStripSeparator1,
            this.ServicePropertiesFormMenu,
            this.cOMMANDToolStripMenuItem});
            this.ServiceFormMenu.Name = "ServiceFormMenu";
            this.ServiceFormMenu.Size = new System.Drawing.Size(62, 20);
            this.ServiceFormMenu.Text = "Service";
            // 
            // StartServiceFormMenu
            // 
            this.StartServiceFormMenu.Name = "StartServiceFormMenu";
            this.StartServiceFormMenu.Size = new System.Drawing.Size(146, 22);
            this.StartServiceFormMenu.Text = "Start";
            this.StartServiceFormMenu.Click += new System.EventHandler(this.OnStartService);
            // 
            // StopServiceFormMenu
            // 
            this.StopServiceFormMenu.Name = "StopServiceFormMenu";
            this.StopServiceFormMenu.Size = new System.Drawing.Size(146, 22);
            this.StopServiceFormMenu.Text = "Stop";
            this.StopServiceFormMenu.Click += new System.EventHandler(this.OnStopService);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // ServicePropertiesFormMenu
            // 
            this.ServicePropertiesFormMenu.Name = "ServicePropertiesFormMenu";
            this.ServicePropertiesFormMenu.Size = new System.Drawing.Size(146, 22);
            this.ServicePropertiesFormMenu.Text = "Properties...";
            // 
            // cOMMANDToolStripMenuItem
            // 
            this.cOMMANDToolStripMenuItem.Name = "cOMMANDToolStripMenuItem";
            this.cOMMANDToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.cOMMANDToolStripMenuItem.Text = "COMMAND";
            this.cOMMANDToolStripMenuItem.Click += new System.EventHandler(this.OnCOMMAND);
            // 
            // TaskFormMenu
            // 
            this.TaskFormMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RunTaskFormMenu,
            this.toolStripSeparator3,
            this.TaskPropertiesFormMenu});
            this.TaskFormMenu.Name = "TaskFormMenu";
            this.TaskFormMenu.Size = new System.Drawing.Size(47, 20);
            this.TaskFormMenu.Text = "Task";
            // 
            // RunTaskFormMenu
            // 
            this.RunTaskFormMenu.Name = "RunTaskFormMenu";
            this.RunTaskFormMenu.Size = new System.Drawing.Size(146, 22);
            this.RunTaskFormMenu.Text = "Run...";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(143, 6);
            // 
            // TaskPropertiesFormMenu
            // 
            this.TaskPropertiesFormMenu.Name = "TaskPropertiesFormMenu";
            this.TaskPropertiesFormMenu.Size = new System.Drawing.Size(146, 22);
            this.TaskPropertiesFormMenu.Text = "Properties...";
            // 
            // HelpFormMenu
            // 
            this.HelpFormMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InfoFormMenu,
            this.toolStripSeparator2,
            this.ShowHelpFormMenu});
            this.HelpFormMenu.Name = "HelpFormMenu";
            this.HelpFormMenu.Size = new System.Drawing.Size(45, 20);
            this.HelpFormMenu.Text = "Help";
            // 
            // InfoFormMenu
            // 
            this.InfoFormMenu.Name = "InfoFormMenu";
            this.InfoFormMenu.Size = new System.Drawing.Size(149, 22);
            this.InfoFormMenu.Text = "Info...";
            this.InfoFormMenu.Click += new System.EventHandler(this.OnInfo);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(146, 6);
            // 
            // ShowHelpFormMenu
            // 
            this.ShowHelpFormMenu.Name = "ShowHelpFormMenu";
            this.ShowHelpFormMenu.Size = new System.Drawing.Size(149, 22);
            this.ShowHelpFormMenu.Text = "Show Help...";
            // 
            // FormStatus
            // 
            this.FormStatus.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConfigStatusLabel,
            this.ServiceStatusLabel});
            this.FormStatus.Location = new System.Drawing.Point(0, 272);
            this.FormStatus.Name = "FormStatus";
            this.FormStatus.Size = new System.Drawing.Size(784, 22);
            this.FormStatus.TabIndex = 3;
            this.FormStatus.Text = "statusStrip1";
            // 
            // ConfigStatusLabel
            // 
            this.ConfigStatusLabel.Name = "ConfigStatusLabel";
            this.ConfigStatusLabel.Size = new System.Drawing.Size(20, 17);
            this.ConfigStatusLabel.Text = "...";
            // 
            // ServiceStatusLabel
            // 
            this.ServiceStatusLabel.Name = "ServiceStatusLabel";
            this.ServiceStatusLabel.Size = new System.Drawing.Size(20, 17);
            this.ServiceStatusLabel.Text = "...";
            // 
            // FormPanel
            // 
            this.FormPanel.Controls.Add(this.TaskProgressSplitter);
            this.FormPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FormPanel.Location = new System.Drawing.Point(0, 24);
            this.FormPanel.Name = "FormPanel";
            this.FormPanel.Size = new System.Drawing.Size(784, 248);
            this.FormPanel.TabIndex = 6;
            // 
            // TaskProgressSplitter
            // 
            this.TaskProgressSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TaskProgressSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.TaskProgressSplitter.IsSplitterFixed = true;
            this.TaskProgressSplitter.Location = new System.Drawing.Point(0, 0);
            this.TaskProgressSplitter.Name = "TaskProgressSplitter";
            this.TaskProgressSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // TaskProgressSplitter.Panel1
            // 
            this.TaskProgressSplitter.Panel1.Controls.Add(this.TaskListView);
            // 
            // TaskProgressSplitter.Panel2
            // 
            this.TaskProgressSplitter.Panel2.Controls.Add(this.panel1);
            this.TaskProgressSplitter.Panel2.Resize += new System.EventHandler(this.OnMessagePanelRezied);
            this.TaskProgressSplitter.Size = new System.Drawing.Size(784, 248);
            this.TaskProgressSplitter.SplitterDistance = 145;
            this.TaskProgressSplitter.TabIndex = 8;
            // 
            // TaskListView
            // 
            this.TaskListView.AllowDrop = true;
            this.TaskListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColHdr_Tsk_Name,
            this.ColHdr_Tsk_Act,
            this.ColHdr_Tsk_StartTime,
            this.ColHdr_Tsk_EndTime,
            this.ColHdr_Tsk_LastRunDat,
            this.ColHdr_Tsk_LastRunDur});
            this.TaskListView.ControlMode = Sky.Library.Forms.TreeListView.enControlMode.List;
            this.TaskListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TaskListView.FullRowSelect = true;
            this.TaskListView.GridLines = true;
            this.TaskListView.HideSelection = false;
            this.TaskListView.Location = new System.Drawing.Point(0, 0);
            this.TaskListView.MultiSelect = false;
            this.TaskListView.Name = "TaskListView";
            this.TaskListView.OwnerDraw = true;
            this.TaskListView.Size = new System.Drawing.Size(784, 145);
            this.TaskListView.SmallImageList = this.IconList;
            this.TaskListView.TabIndex = 4;
            this.TaskListView.UseCompatibleStateImageBehavior = false;
            this.TaskListView.View = System.Windows.Forms.View.Details;
            // 
            // ColHdr_Tsk_Name
            // 
            this.ColHdr_Tsk_Name.Text = "Name";
            this.ColHdr_Tsk_Name.Width = 200;
            // 
            // ColHdr_Tsk_Act
            // 
            this.ColHdr_Tsk_Act.Text = "Active";
            this.ColHdr_Tsk_Act.Width = 80;
            // 
            // ColHdr_Tsk_StartTime
            // 
            this.ColHdr_Tsk_StartTime.Text = "Start Time";
            this.ColHdr_Tsk_StartTime.Width = 100;
            // 
            // ColHdr_Tsk_EndTime
            // 
            this.ColHdr_Tsk_EndTime.Text = "End Time";
            this.ColHdr_Tsk_EndTime.Width = 100;
            // 
            // ColHdr_Tsk_LastRunDat
            // 
            this.ColHdr_Tsk_LastRunDat.Text = "Last Run Date";
            this.ColHdr_Tsk_LastRunDat.Width = 150;
            // 
            // ColHdr_Tsk_LastRunDur
            // 
            this.ColHdr_Tsk_LastRunDur.Text = "Last Run Duration";
            this.ColHdr_Tsk_LastRunDur.Width = 150;
            // 
            // IconList
            // 
            this.IconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("IconList.ImageStream")));
            this.IconList.TransparentColor = System.Drawing.Color.White;
            this.IconList.Images.SetKeyName(0, "Task.Active");
            this.IconList.Images.SetKeyName(1, "Task.Inactive");
            this.IconList.Images.SetKeyName(2, "Task.Run");
            this.IconList.Images.SetKeyName(3, "Message.Status.Error");
            this.IconList.Images.SetKeyName(4, "Message.Status.Info");
            this.IconList.Images.SetKeyName(5, "Message.Status.Warning");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.MessageStatusBox);
            this.panel1.Controls.Add(this.MessageTimeStampTextBox);
            this.panel1.Controls.Add(this.MessagePercentTextBox);
            this.panel1.Controls.Add(this.MessageCountTextBox);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.MessageStepTextBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.MessageTextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.MessageProgressBar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 99);
            this.panel1.TabIndex = 7;
            // 
            // MessageStatusBox
            // 
            this.MessageStatusBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.MessageStatusBox.Location = new System.Drawing.Point(8, 24);
            this.MessageStatusBox.Name = "MessageStatusBox";
            this.MessageStatusBox.Size = new System.Drawing.Size(16, 16);
            this.MessageStatusBox.TabIndex = 17;
            this.MessageStatusBox.TabStop = false;
            // 
            // MessageTimeStampTextBox
            // 
            this.MessageTimeStampTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.MessageTimeStampTextBox.Location = new System.Drawing.Point(5, 70);
            this.MessageTimeStampTextBox.Name = "MessageTimeStampTextBox";
            this.MessageTimeStampTextBox.ReadOnly = true;
            this.MessageTimeStampTextBox.Size = new System.Drawing.Size(135, 23);
            this.MessageTimeStampTextBox.TabIndex = 18;
            // 
            // MessagePercentTextBox
            // 
            this.MessagePercentTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.MessagePercentTextBox.Location = new System.Drawing.Point(476, 70);
            this.MessagePercentTextBox.Name = "MessagePercentTextBox";
            this.MessagePercentTextBox.ReadOnly = true;
            this.MessagePercentTextBox.Size = new System.Drawing.Size(30, 23);
            this.MessagePercentTextBox.TabIndex = 14;
            this.MessagePercentTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // MessageCountTextBox
            // 
            this.MessageCountTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.MessageCountTextBox.Location = new System.Drawing.Point(412, 70);
            this.MessageCountTextBox.Name = "MessageCountTextBox";
            this.MessageCountTextBox.ReadOnly = true;
            this.MessageCountTextBox.Size = new System.Drawing.Size(50, 23);
            this.MessageCountTextBox.TabIndex = 12;
            this.MessageCountTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(506, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "%";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(461, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "=";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(401, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 16);
            this.label4.TabIndex = 13;
            this.label4.Text = "/";
            // 
            // MessageStepTextBox
            // 
            this.MessageStepTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.MessageStepTextBox.Location = new System.Drawing.Point(351, 70);
            this.MessageStepTextBox.Name = "MessageStepTextBox";
            this.MessageStepTextBox.ReadOnly = true;
            this.MessageStepTextBox.Size = new System.Drawing.Size(50, 23);
            this.MessageStepTextBox.TabIndex = 10;
            this.MessageStepTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Time && Progress:";
            // 
            // MessageTextBox
            // 
            this.MessageTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.MessageTextBox.Location = new System.Drawing.Point(5, 20);
            this.MessageTextBox.Name = "MessageTextBox";
            this.MessageTextBox.ReadOnly = true;
            this.MessageTextBox.Size = new System.Drawing.Size(760, 23);
            this.MessageTextBox.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Message && Status:";
            // 
            // MessageProgressBar
            // 
            this.MessageProgressBar.ForeColor = System.Drawing.Color.SteelBlue;
            this.MessageProgressBar.Location = new System.Drawing.Point(146, 70);
            this.MessageProgressBar.Name = "MessageProgressBar";
            this.MessageProgressBar.Size = new System.Drawing.Size(200, 23);
            this.MessageProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.MessageProgressBar.TabIndex = 6;
            // 
            // ServiceAdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 294);
            this.Controls.Add(this.FormPanel);
            this.Controls.Add(this.FormStatus);
            this.Controls.Add(this.FormMenu);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.FormMenu;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ServiceAdminForm";
            this.Text = "ProGate Service Admin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClose);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.FormMenu.ResumeLayout(false);
            this.FormMenu.PerformLayout();
            this.FormStatus.ResumeLayout(false);
            this.FormStatus.PerformLayout();
            this.FormPanel.ResumeLayout(false);
            this.TaskProgressSplitter.Panel1.ResumeLayout(false);
            this.TaskProgressSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TaskProgressSplitter)).EndInit();
            this.TaskProgressSplitter.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MessageStatusBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip FormMenu;
        private System.Windows.Forms.ToolStripMenuItem ApplicationFormMenu;
        private System.Windows.Forms.ToolStripMenuItem ExitFormMenu;
        private System.Windows.Forms.ToolStripMenuItem ServiceFormMenu;
        private System.Windows.Forms.ToolStripMenuItem StartServiceFormMenu;
        private System.Windows.Forms.ToolStripMenuItem StopServiceFormMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ServicePropertiesFormMenu;
        private System.Windows.Forms.ToolStripMenuItem TaskFormMenu;
        private System.Windows.Forms.ToolStripMenuItem RunTaskFormMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem TaskPropertiesFormMenu;
        private System.Windows.Forms.ToolStripMenuItem HelpFormMenu;
        private System.Windows.Forms.ToolStripMenuItem InfoFormMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ShowHelpFormMenu;
        private System.Windows.Forms.StatusStrip FormStatus;
        private Library.Forms.TreeListView TaskListView;
        private System.Windows.Forms.ColumnHeader ColHdr_Tsk_Name;
        private System.Windows.Forms.ColumnHeader ColHdr_Tsk_Act;
        private System.Windows.Forms.ColumnHeader ColHdr_Tsk_StartTime;
        private System.Windows.Forms.ColumnHeader ColHdr_Tsk_EndTime;
        private System.Windows.Forms.ColumnHeader ColHdr_Tsk_LastRunDat;
        private System.Windows.Forms.Panel FormPanel;
        private System.Windows.Forms.ToolStripStatusLabel ServiceStatusLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ProgressBar MessageProgressBar;
        private System.Windows.Forms.SplitContainer TaskProgressSplitter;
        private System.Windows.Forms.TextBox MessagePercentTextBox;
        private System.Windows.Forms.TextBox MessageCountTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox MessageStepTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox MessageTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList IconList;
        private System.Windows.Forms.PictureBox MessageStatusBox;
        private System.Windows.Forms.TextBox MessageTimeStampTextBox;
        private System.Windows.Forms.ColumnHeader ColHdr_Tsk_LastRunDur;
        private System.Windows.Forms.ToolStripStatusLabel ConfigStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem cOMMANDToolStripMenuItem;
    }
}

