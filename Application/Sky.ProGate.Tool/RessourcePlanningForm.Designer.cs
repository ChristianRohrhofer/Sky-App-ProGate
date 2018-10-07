namespace Sky.ProGate.Tool
{
    partial class RessourcePlanningForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        private void InitializeComponent()
        {
            this.FormMenu = new System.Windows.Forms.MenuStrip();
            this.ApplicationFormMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitFormMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewFormMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.GridFormMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AddRowFormMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveRowFormMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExpandFormMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.CollapseFormMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.FormProjectPlanningGrid = new Sky.ProGate.Application.Forms.ProjectPlanningGrid();
            this.ProjectFolderGrid = new Sky.ProGate.Application.Forms.FolderGrid();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FormMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FormProjectPlanningGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProjectFolderGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // FormMenu
            // 
            this.FormMenu.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ApplicationFormMenu,
            this.ViewFormMenu,
            this.GridFormMenu});
            this.FormMenu.Location = new System.Drawing.Point(0, 0);
            this.FormMenu.Name = "FormMenu";
            this.FormMenu.Size = new System.Drawing.Size(1383, 24);
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
            // ViewFormMenu
            // 
            this.ViewFormMenu.Name = "ViewFormMenu";
            this.ViewFormMenu.Size = new System.Drawing.Size(48, 20);
            this.ViewFormMenu.Text = "View";
            // 
            // GridFormMenu
            // 
            this.GridFormMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddRowFormMenu,
            this.RemoveRowFormMenu,
            this.toolStripSeparator1,
            this.ExpandFormMenu,
            this.CollapseFormMenu});
            this.GridFormMenu.Name = "GridFormMenu";
            this.GridFormMenu.Size = new System.Drawing.Size(43, 20);
            this.GridFormMenu.Text = "Grid";
            // 
            // AddRowFormMenu
            // 
            this.AddRowFormMenu.Name = "AddRowFormMenu";
            this.AddRowFormMenu.Size = new System.Drawing.Size(151, 22);
            this.AddRowFormMenu.Text = "Add Row";
            this.AddRowFormMenu.Click += new System.EventHandler(this.OnAddRow);
            // 
            // RemoveRowFormMenu
            // 
            this.RemoveRowFormMenu.Name = "RemoveRowFormMenu";
            this.RemoveRowFormMenu.Size = new System.Drawing.Size(151, 22);
            this.RemoveRowFormMenu.Text = "Remove Row";
            this.RemoveRowFormMenu.Click += new System.EventHandler(this.OnRemoveRow);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(148, 6);
            // 
            // ExpandFormMenu
            // 
            this.ExpandFormMenu.Name = "ExpandFormMenu";
            this.ExpandFormMenu.Size = new System.Drawing.Size(151, 22);
            this.ExpandFormMenu.Text = "Expand";
            this.ExpandFormMenu.Click += new System.EventHandler(this.OnExpand);
            // 
            // CollapseFormMenu
            // 
            this.CollapseFormMenu.Name = "CollapseFormMenu";
            this.CollapseFormMenu.Size = new System.Drawing.Size(151, 22);
            this.CollapseFormMenu.Text = "Collapse";
            this.CollapseFormMenu.Click += new System.EventHandler(this.OnCollapse);
            // 
            // FormProjectPlanningGrid
            // 
            this.FormProjectPlanningGrid.AllowDrop = true;
            this.FormProjectPlanningGrid.AllowUserToAddRows = false;
            this.FormProjectPlanningGrid.AllowUserToDeleteRows = false;
            this.FormProjectPlanningGrid.AllowUserToOrderColumns = true;
            this.FormProjectPlanningGrid.AllowUserToResizeColumns = false;
            this.FormProjectPlanningGrid.AllowUserToResizeRows = false;
            this.FormProjectPlanningGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.FormProjectPlanningGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FormProjectPlanningGrid.ColumnHeadersVisible = false;
            this.FormProjectPlanningGrid.GridColor = System.Drawing.SystemColors.Control;
            this.FormProjectPlanningGrid.Location = new System.Drawing.Point(603, 70);
            this.FormProjectPlanningGrid.Name = "FormProjectPlanningGrid";
            this.FormProjectPlanningGrid.RowHeadersVisible = false;
            this.FormProjectPlanningGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.FormProjectPlanningGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.FormProjectPlanningGrid.Size = new System.Drawing.Size(540, 312);
            this.FormProjectPlanningGrid.TabIndex = 2;
            // 
            // ProjectFolderGrid
            // 
            this.ProjectFolderGrid.AllowDrop = true;
            this.ProjectFolderGrid.AllowUserToAddRows = false;
            this.ProjectFolderGrid.AllowUserToDeleteRows = false;
            this.ProjectFolderGrid.AllowUserToResizeColumns = false;
            this.ProjectFolderGrid.AllowUserToResizeRows = false;
            this.ProjectFolderGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProjectFolderGrid.ColumnHeadersVisible = false;
            this.ProjectFolderGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.ProjectFolderGrid.Location = new System.Drawing.Point(55, 70);
            this.ProjectFolderGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ProjectFolderGrid.Name = "ProjectFolderGrid";
            this.ProjectFolderGrid.RowHeadersVisible = false;
            this.ProjectFolderGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ProjectFolderGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.ProjectFolderGrid.Size = new System.Drawing.Size(513, 312);
            this.ProjectFolderGrid.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // RessourcePlanningForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1383, 562);
            this.Controls.Add(this.FormProjectPlanningGrid);
            this.Controls.Add(this.ProjectFolderGrid);
            this.Controls.Add(this.FormMenu);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.FormMenu;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "RessourcePlanningForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.FormMenu.ResumeLayout(false);
            this.FormMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FormProjectPlanningGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProjectFolderGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Application.Forms.FolderGrid ProjectFolderGrid;
        private System.Windows.Forms.MenuStrip FormMenu;
        private System.Windows.Forms.ToolStripMenuItem ApplicationFormMenu;
        private System.Windows.Forms.ToolStripMenuItem ExitFormMenu;
        private System.Windows.Forms.ToolStripMenuItem ViewFormMenu;
        private System.Windows.Forms.ToolStripMenuItem GridFormMenu;
        private System.Windows.Forms.ToolStripMenuItem AddRowFormMenu;
        private System.Windows.Forms.ToolStripMenuItem RemoveRowFormMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ExpandFormMenu;
        private System.Windows.Forms.ToolStripMenuItem CollapseFormMenu;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private Sky.ProGate.Application.Forms.ProjectPlanningGrid FormProjectPlanningGrid;
    }
}

