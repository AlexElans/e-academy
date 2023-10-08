namespace SystemDeviceConfiguration
{
    partial class SystemDeviceConfigurationUserControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemDeviceConfigurationUserControl));
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.availableDevicesListView = new System.Windows.Forms.ListView();
            this.systemFormulaTreeView = new System.Windows.Forms.TreeView();
            this.deviceSettingPanel = new System.Windows.Forms.Panel();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.ReadedFilesComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.LoadToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SaveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SavedFilesComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.SetDefaultConfigNameToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mainTableLayoutPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.AutoSize = true;
            this.mainTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainTableLayoutPanel.ColumnCount = 3;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mainTableLayoutPanel.Controls.Add(this.availableDevicesListView, 0, 0);
            this.mainTableLayoutPanel.Controls.Add(this.systemFormulaTreeView, 2, 0);
            this.mainTableLayoutPanel.Controls.Add(this.deviceSettingPanel, 1, 0);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 1;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(1206, 518);
            this.mainTableLayoutPanel.TabIndex = 0;
            // 
            // availableDevicesListView
            // 
            this.availableDevicesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.availableDevicesListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.availableDevicesListView.HideSelection = false;
            this.availableDevicesListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.availableDevicesListView.Location = new System.Drawing.Point(3, 3);
            this.availableDevicesListView.Name = "availableDevicesListView";
            this.availableDevicesListView.Size = new System.Drawing.Size(121, 512);
            this.availableDevicesListView.TabIndex = 0;
            this.availableDevicesListView.UseCompatibleStateImageBehavior = false;
            this.availableDevicesListView.View = System.Windows.Forms.View.List;
            // 
            // systemFormulaTreeView
            // 
            this.systemFormulaTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.systemFormulaTreeView.Location = new System.Drawing.Point(956, 3);
            this.systemFormulaTreeView.Name = "systemFormulaTreeView";
            this.systemFormulaTreeView.Size = new System.Drawing.Size(1067, 512);
            this.systemFormulaTreeView.TabIndex = 0;
            // 
            // deviceSettingPanel
            // 
            this.deviceSettingPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.deviceSettingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deviceSettingPanel.Location = new System.Drawing.Point(130, 3);
            this.deviceSettingPanel.Name = "deviceSettingPanel";
            this.deviceSettingPanel.Size = new System.Drawing.Size(820, 512);
            this.deviceSettingPanel.TabIndex = 2;
            // 
            // mainPanel
            // 
            this.mainPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainPanel.Controls.Add(this.mainTableLayoutPanel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(3, 28);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1206, 518);
            this.mainPanel.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.mainPanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1212, 549);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // toolStrip1
            // 
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.ReadedFilesComboBox,
            this.LoadToolStripButton,
            this.SaveToolStripButton,
            this.SavedFilesComboBox,
            this.toolStripLabel2,
            this.toolStripSeparator1,
            this.SetDefaultConfigNameToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1212, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(130, 22);
            this.toolStripLabel1.Text = "Сохранённые данные:";
            // 
            // ReadedFilesComboBox
            // 
            this.ReadedFilesComboBox.Name = "ReadedFilesComboBox";
            this.ReadedFilesComboBox.Size = new System.Drawing.Size(121, 25);
            // 
            // LoadToolStripButton
            // 
            this.LoadToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.LoadToolStripButton.Name = "LoadToolStripButton";
            this.LoadToolStripButton.Size = new System.Drawing.Size(65, 22);
            this.LoadToolStripButton.Text = "Загрузить";
            // 
            // SaveToolStripButton
            // 
            this.SaveToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.SaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SaveToolStripButton.Name = "SaveToolStripButton";
            this.SaveToolStripButton.Size = new System.Drawing.Size(70, 22);
            this.SaveToolStripButton.Text = "Сохранить";
            // 
            // SavedFilesComboBox
            // 
            this.SavedFilesComboBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.SavedFilesComboBox.Name = "SavedFilesComboBox";
            this.SavedFilesComboBox.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(140, 22);
            this.toolStripLabel2.Text = "Редактируемые данные:";
            // 
            // SetDefaultConfigNameToolStripButton
            // 
            this.SetDefaultConfigNameToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SetDefaultConfigNameToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SetDefaultConfigNameToolStripButton.Name = "SetDefaultConfigNameToolStripButton";
            this.SetDefaultConfigNameToolStripButton.Size = new System.Drawing.Size(161, 22);
            this.SetDefaultConfigNameToolStripButton.Text = "Установить по-умолчанию";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // SystemDeviceConfigurationUserControl
            // 
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SystemDeviceConfigurationUserControl";
            this.Size = new System.Drawing.Size(1212, 549);
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.ListView availableDevicesListView;
        private System.Windows.Forms.TreeView systemFormulaTreeView;
        private System.Windows.Forms.Panel deviceSettingPanel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox ReadedFilesComboBox;
        private System.Windows.Forms.ToolStripButton LoadToolStripButton;
        private System.Windows.Forms.ToolStripButton SaveToolStripButton;
        private System.Windows.Forms.ToolStripComboBox SavedFilesComboBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton SetDefaultConfigNameToolStripButton;
    }

}