namespace SystemDeviceConfiguration
{
    partial class DeviceSettingsDefault
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
            this.DeviceDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.DeviceDestriptionLabel = new System.Windows.Forms.Label();
            this.DeviceSttingsMenuLabel = new System.Windows.Forms.Label();
            this.DeviceNameTextBox = new BaseClasses.TextBoxString();
            this.DeviceNameLabel = new System.Windows.Forms.Label();
            this.DeviceFullNameLabel = new System.Windows.Forms.Label();
            this.DeviceFullNameTextBox = new BaseClasses.TextBoxString();
            this.ServiceInformationLabel = new System.Windows.Forms.Label();
            this.CommentaryLabel = new System.Windows.Forms.Label();
            this.ServiceInformationTextBox = new BaseClasses.TextBoxString();
            this.CommentaryTextBox = new BaseClasses.TextBoxString();
            this.SaveSettingsDeviceButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DeviceDescriptionTextBox
            // 
            this.DeviceDescriptionTextBox.Location = new System.Drawing.Point(455, 40);
            this.DeviceDescriptionTextBox.Multiline = true;
            this.DeviceDescriptionTextBox.Name = "DeviceDescriptionTextBox";
            this.DeviceDescriptionTextBox.ReadOnly = true;
            this.DeviceDescriptionTextBox.Size = new System.Drawing.Size(349, 405);
            this.DeviceDescriptionTextBox.TabIndex = 48;
            // 
            // DeviceDestriptionLabel
            // 
            this.DeviceDestriptionLabel.AutoSize = true;
            this.DeviceDestriptionLabel.Location = new System.Drawing.Point(452, 24);
            this.DeviceDestriptionLabel.Name = "DeviceDestriptionLabel";
            this.DeviceDestriptionLabel.Size = new System.Drawing.Size(117, 13);
            this.DeviceDestriptionLabel.TabIndex = 49;
            this.DeviceDestriptionLabel.Text = "Описание устройства";
            // 
            // DeviceSttingsMenuLabel
            // 
            this.DeviceSttingsMenuLabel.AutoSize = true;
            this.DeviceSttingsMenuLabel.Location = new System.Drawing.Point(20, 24);
            this.DeviceSttingsMenuLabel.Name = "DeviceSttingsMenuLabel";
            this.DeviceSttingsMenuLabel.Size = new System.Drawing.Size(146, 13);
            this.DeviceSttingsMenuLabel.TabIndex = 50;
            this.DeviceSttingsMenuLabel.Text = "Меню настроек устройства";
            // 
            // DeviceNameTextBox
            // 
            this.DeviceNameTextBox.Location = new System.Drawing.Point(23, 74);
            this.DeviceNameTextBox.Name = "DeviceNameTextBox";
            this.DeviceNameTextBox.Size = new System.Drawing.Size(382, 20);
            this.DeviceNameTextBox.TabIndex = 51;
            this.DeviceNameTextBox.Value = "";
            // 
            // DeviceNameLabel
            // 
            this.DeviceNameLabel.AutoSize = true;
            this.DeviceNameLabel.Location = new System.Drawing.Point(20, 58);
            this.DeviceNameLabel.Name = "DeviceNameLabel";
            this.DeviceNameLabel.Size = new System.Drawing.Size(146, 13);
            this.DeviceNameLabel.TabIndex = 52;
            this.DeviceNameLabel.Text = "Наименование устройства:";
            // 
            // DeviceFullNameLabel
            // 
            this.DeviceFullNameLabel.AutoSize = true;
            this.DeviceFullNameLabel.Location = new System.Drawing.Point(20, 111);
            this.DeviceFullNameLabel.Name = "DeviceFullNameLabel";
            this.DeviceFullNameLabel.Size = new System.Drawing.Size(191, 13);
            this.DeviceFullNameLabel.TabIndex = 53;
            this.DeviceFullNameLabel.Text = "Наименование устройства (полное):";
            // 
            // DeviceFullNameTextBox
            // 
            this.DeviceFullNameTextBox.Location = new System.Drawing.Point(23, 127);
            this.DeviceFullNameTextBox.Name = "DeviceFullNameTextBox";
            this.DeviceFullNameTextBox.Size = new System.Drawing.Size(382, 20);
            this.DeviceFullNameTextBox.TabIndex = 54;
            this.DeviceFullNameTextBox.Value = "";
            // 
            // ServiceInformationLabel
            // 
            this.ServiceInformationLabel.AutoSize = true;
            this.ServiceInformationLabel.Location = new System.Drawing.Point(20, 270);
            this.ServiceInformationLabel.Name = "ServiceInformationLabel";
            this.ServiceInformationLabel.Size = new System.Drawing.Size(133, 13);
            this.ServiceInformationLabel.TabIndex = 64;
            this.ServiceInformationLabel.Text = "Служебная информация:";
            // 
            // CommentaryLabel
            // 
            this.CommentaryLabel.AutoSize = true;
            this.CommentaryLabel.Location = new System.Drawing.Point(20, 323);
            this.CommentaryLabel.Name = "CommentaryLabel";
            this.CommentaryLabel.Size = new System.Drawing.Size(80, 13);
            this.CommentaryLabel.TabIndex = 66;
            this.CommentaryLabel.Text = "Комментарий:";
            // 
            // ServiceInformationTextBox
            // 
            this.ServiceInformationTextBox.Location = new System.Drawing.Point(23, 286);
            this.ServiceInformationTextBox.Name = "ServiceInformationTextBox";
            this.ServiceInformationTextBox.Size = new System.Drawing.Size(382, 20);
            this.ServiceInformationTextBox.TabIndex = 68;
            this.ServiceInformationTextBox.Value = "";
            // 
            // CommentaryTextBox
            // 
            this.CommentaryTextBox.Location = new System.Drawing.Point(23, 339);
            this.CommentaryTextBox.Multiline = true;
            this.CommentaryTextBox.Name = "CommentaryTextBox";
            this.CommentaryTextBox.Size = new System.Drawing.Size(382, 71);
            this.CommentaryTextBox.TabIndex = 69;
            this.CommentaryTextBox.Value = "";
            // 
            // SaveSettingsDeviceButton
            // 
            this.SaveSettingsDeviceButton.Location = new System.Drawing.Point(253, 422);
            this.SaveSettingsDeviceButton.Name = "SaveSettingsDeviceButton";
            this.SaveSettingsDeviceButton.Size = new System.Drawing.Size(152, 23);
            this.SaveSettingsDeviceButton.TabIndex = 70;
            this.SaveSettingsDeviceButton.Text = "Сохранить изменения";
            this.SaveSettingsDeviceButton.UseVisualStyleBackColor = true;
            this.SaveSettingsDeviceButton.Click += new System.EventHandler(this.SaveSettingsDeviceButton_Click);
            // 
            // DeviceSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SaveSettingsDeviceButton);
            this.Controls.Add(this.CommentaryTextBox);
            this.Controls.Add(this.ServiceInformationTextBox);
            this.Controls.Add(this.CommentaryLabel);
            this.Controls.Add(this.ServiceInformationLabel);
            this.Controls.Add(this.DeviceFullNameTextBox);
            this.Controls.Add(this.DeviceFullNameLabel);
            this.Controls.Add(this.DeviceNameLabel);
            this.Controls.Add(this.DeviceNameTextBox);
            this.Controls.Add(this.DeviceSttingsMenuLabel);
            this.Controls.Add(this.DeviceDestriptionLabel);
            this.Controls.Add(this.DeviceDescriptionTextBox);
            this.Name = "DeviceSettings";
            this.Size = new System.Drawing.Size(820, 463);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox DeviceDescriptionTextBox;
        private System.Windows.Forms.Label DeviceDestriptionLabel;
        private System.Windows.Forms.Label DeviceSttingsMenuLabel;
        private BaseClasses.TextBoxString DeviceNameTextBox;
        private System.Windows.Forms.Label DeviceNameLabel;
        private System.Windows.Forms.Label DeviceFullNameLabel;
        private BaseClasses.TextBoxString DeviceFullNameTextBox;
        private System.Windows.Forms.Label ServiceInformationLabel;
        private System.Windows.Forms.Label CommentaryLabel;
        private BaseClasses.TextBoxString ServiceInformationTextBox;
        private BaseClasses.TextBoxString CommentaryTextBox;
        private System.Windows.Forms.Button SaveSettingsDeviceButton;
    }
}