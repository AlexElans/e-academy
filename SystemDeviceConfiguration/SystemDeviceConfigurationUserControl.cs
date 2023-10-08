using BaseInterfaces.DevicesLibrary;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SystemDeviceConfiguration
{
  /// <summary>
  /// Класс пользовательского интерфейса элемента управления для настройки конфигурации оборудования.
  /// </summary>
  public partial class SystemDeviceConfigurationUserControl: UserControl
  {
    /// <summary>
    /// Выбранное устройство, для которого требюуется отобразить окно настроек.
    /// </summary>
    private dynamic SelectedDevice = null;
    /// <summary>
    /// Путь к директории с имеющимися файлами конфигураций оборудования.
    /// </summary>
    private string configHardwareDirectoryPath;
    /// <summary>
    /// Путь к директории, в которой располагаются DLL-файлы подключаемого оборудования.
    /// </summary>
    private string deviceDLLDirectoryPath;
    /// <summary>
    /// Данные редактируемой конфигурации оборудования.
    /// </summary>
    private List<object> temporarySystemDevices = null;
    /// <summary>
    /// Меню обработки состава конфигурации оборудования.
    /// </summary>
    private ContextMenuStrip systemFormulaContextMenuStrip;
    /// <summary>
    /// Конструктор пользовательского элемента управления для настройки конфигурации оборудования.
    /// </summary>
    public SystemDeviceConfigurationUserControl() : this(null, null) { }
    /// <summary>
    /// Конструктор пользовательского элемента управления для настройки конфигурации оборудования.
    /// </summary>
    /// <param name="in_configHardwareDirectoryPath">Путь к папке с имеющимися файлами конфигураций оборудования.</param>
    /// <param name="in_deviceDLLDirectoryPath"> Путь к директории, в которой располагаются DLL-файлы  подключаемого оборудования.</param>
    public SystemDeviceConfigurationUserControl(string in_configHardwareDirectoryPath, string in_deviceDLLDirectoryPath = null)
    {
      InitializeComponent();
      SetConfigHardwareDirectoryPath(in_configHardwareDirectoryPath);
      SetDeviceDLLDirectoryPath(in_deviceDLLDirectoryPath);
      LoadExistDevices();
      LoadExistConfigHardwareFiles();
      PrepareContextMenuStrip();

      this.temporarySystemDevices = new List<object>();

      this.LoadToolStripButton.Click += TryLoadSystemDeviceConfiguration;
      this.SaveToolStripButton.Click += SaveSystemDeviceConfiguration;
      this.availableDevicesListView.SelectedIndexChanged += SelectAvailableDevice;
      this.systemFormulaTreeView.AfterSelect += SelectAddedDevice;
      this.systemFormulaTreeView.NodeMouseClick += OpenContextMenuStrip;
      this.SetDefaultConfigNameToolStripButton.Click += SetDefaultConfigNameToolStripButton_Click;
      this.ParentChanged += SystemDeviceConfigurationUserControl_ParentChanged;
    }
    /// <summary>
    /// Устанавливает путь к папке с имеющимися файлами конфигураций оборудования.
    /// </summary>
    /// <param name="in_ConfigHardwareDirectoryPath">Путь к папке с имеющимися файлами конфигураций оборудования.</param>
    public void SetConfigHardwareDirectoryPath(string in_ConfigHardwareDirectoryPath)
    {
      if (!string.IsNullOrWhiteSpace(in_ConfigHardwareDirectoryPath) && System.IO.Directory.Exists(in_ConfigHardwareDirectoryPath)) this.configHardwareDirectoryPath = in_ConfigHardwareDirectoryPath;
      else
        this.configHardwareDirectoryPath = SystemDeviceConfiguration.ConfigHardwareDirectoryPath;
    }
    /// <summary>
    /// Устанавливает путь к директории, в которой располагаются DLL-файлы  подключаемого оборудования.
    /// </summary>
    /// <param name="in_deviceDLLDirectoryPath">Путь к директории, в которой располагаются DLL-файлы  подключаемого оборудования.</param>
    public void SetDeviceDLLDirectoryPath(string in_deviceDLLDirectoryPath)
    {
      if (!string.IsNullOrWhiteSpace(in_deviceDLLDirectoryPath) && System.IO.Directory.Exists(in_deviceDLLDirectoryPath)) this.deviceDLLDirectoryPath = in_deviceDLLDirectoryPath;
      else
        this.deviceDLLDirectoryPath = SystemDeviceConfiguration.DeviceDLLDirectoryPath;
    }
    /// <summary>
    /// Загружает список доступных устройств для их возможного назначения в качество составных частей системы.
    /// </summary>
    public void LoadExistDevices()
    {
      var availableDevices = SystemDeviceConfiguration.GenerateAvailableDevicesListBox(this.deviceDLLDirectoryPath);
      object device;
      this.availableDevicesListView.Items.Clear();
      ListViewItem temporaryListViewItem;
      for (int i = 0; i < availableDevices?.Count; i++)
      {
        device = availableDevices[i];
        if (!(device is IIntegralInterface))
        {
          temporaryListViewItem = new ListViewItem((device as IDeviceInterface).DeviceName);
          temporaryListViewItem.Tag = device.GetType();
          this.availableDevicesListView.Items.Add(temporaryListViewItem);
        }
      }
      availableDevices = null;
      device = null;
      temporaryListViewItem = null;
    }
    /// <summary>
    /// Загружает наименования файлов конфигураций, доступных в заданной директории для дальнейшей загрузки их данных.
    /// </summary>
    public void LoadExistConfigHardwareFiles()
    {
      string[] filePaths;
      try
      {
        ReadedFilesComboBox.Items.Clear();
        SavedFilesComboBox.Items.Clear();
        filePaths = System.IO.Directory.GetFiles(this.configHardwareDirectoryPath, $"*{SystemDeviceConfiguration.ConfigurationFileExtension}");
        for (int i = (filePaths?.Length ?? 0) - 1; i > -1; i--)
        {
          if (!SystemDeviceConfiguration.CanLoadConfiguration(filePaths[i]))
          {
            filePaths[i] = null;
          }
        }
        for (int i = 0; i < filePaths?.Length; i++)
        {
          if (filePaths[i] != null)
          {
            ReadedFilesComboBox.Items.Add(System.IO.Path.GetFileNameWithoutExtension(filePaths[i]));
            SavedFilesComboBox.Items.Add(System.IO.Path.GetFileNameWithoutExtension(filePaths[i]));
          }
        }
      }
      finally
      {
        filePaths = null;
      }
    }
    /// <summary>
    /// Производит загрузку данных конфигурации оборудования системы из заданной директории, с заданным наименованием загружаемого файла с настройками, если такие данные найдены.
    /// </summary>
    /// <param name="sender">Объект, спровоцировавший вызов события.</param>
    /// <param name="e">Агрументы события.</param>
    private void TryLoadSystemDeviceConfiguration(object sender, EventArgs e)
    {
      if (ReadedFilesComboBox.Text == "" && ReadedFilesComboBox.Items.Contains(SystemDeviceConfiguration.DefaultFileConfigurationFileName))
      {
        ReadedFilesComboBox.SelectedIndex = ReadedFilesComboBox.Items.IndexOf(SystemDeviceConfiguration.DefaultFileConfigurationFileName);
      }
      if (System.IO.File.Exists($"{this.configHardwareDirectoryPath?.TrimEnd(System.IO.Path.DirectorySeparatorChar)}{System.IO.Path.DirectorySeparatorChar}{ReadedFilesComboBox.Text}{SystemDeviceConfiguration.ConfigurationFileExtension}"))
      {
        this.temporarySystemDevices = SystemDeviceConfiguration.LoadConfiguration(this.configHardwareDirectoryPath, ReadedFilesComboBox.Text, true);
        UpdateSystemFormulaTreeView();
        deviceSettingPanel.Controls.Clear();
      }
    }
    /// <summary>
    /// Производит сохранение данных конфигурации оборудования системы в заданную директорию, с заданным наименованием сохраняемого файла с настройками.
    /// </summary>
    /// <param name="sender">Объект, спровоцировавший вызов события.</param>
    /// <param name="e">Агрументы события.</param>
    private void SaveSystemDeviceConfiguration(object sender, EventArgs e)
    {
      if (SavedFilesComboBox.Text == "" && SavedFilesComboBox.Items.Contains(SystemDeviceConfiguration.DefaultFileConfigurationFileName))
      {
        SavedFilesComboBox.SelectedIndex = SavedFilesComboBox.Items.IndexOf(SystemDeviceConfiguration.DefaultFileConfigurationFileName);
      }
      SystemDeviceConfiguration.SaveConfiguration(this.configHardwareDirectoryPath, SavedFilesComboBox.Text, this.temporarySystemDevices);
      LoadExistConfigHardwareFiles();
    }
    /// <summary>
    /// Вызывается при срабатывании события нажатия на кнопку установки конфигурации оборудования по-умолчанию.
    /// </summary>
    /// <param name="sender">Объект, спровоцировавший вызов события.</param>
    /// <param name="e">Агрументы события.</param>
    private void SetDefaultConfigNameToolStripButton_Click(object sender, EventArgs e)
    {
      if (ReadedFilesComboBox.Items.Count > 0 && ReadedFilesComboBox.SelectedIndex > -1 && (ReadedFilesComboBox.SelectedItem as string) != SystemDeviceConfiguration.DefaultFileConfigurationFileName)
      {
        TryLoadSystemDeviceConfiguration(null, EventArgs.Empty);
        SystemDeviceConfiguration.LoadConfiguration(this.configHardwareDirectoryPath, ReadedFilesComboBox.Text);
      }
    }
    /// <summary>
    /// Вызывается при срабатывании события изменения родительского окна.
    /// </summary>
    /// <param name="sender">Объект, спровоцировавший вызов события.</param>
    /// <param name="e">Агрументы события.</param>
    private void SystemDeviceConfigurationUserControl_ParentChanged(object sender, EventArgs e)
    {
      if (this.ParentForm != null) this.ParentForm.FormClosing += ParentForm_FormClosing;
    }
    /// <summary>
    /// Производит проверку переходных панелей перед закрытием родительского окна.
    /// </summary>
    /// <param name="sender">Объект, спровоцировавший вызов события.</param>
    /// <param name="e">Агрументы события.</param>
    private void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (!string.IsNullOrWhiteSpace(SystemDeviceConfiguration.LoadedConfigurationFileName))
        SystemDeviceConfiguration.CheckAllSocketAdapterData(this.temporarySystemDevices);
    }
    /// <summary>
    /// Производит обновление компонента отображения состава системы.
    /// </summary>
    private void UpdateSystemFormulaTreeView()
    {
      systemFormulaTreeView.Nodes.Clear();
      TreeView temporaryTreeView = systemFormulaTreeView;
      object device;
      object integralDevice;
      List<object> subracksList;
      List<object> otherdevices;
      string temporaryString;
      List<object> deviceChainIndexed;
      BaseClasses.PairBaseClass<int, object> deviceChainIndexedItem;
      List<dynamic> deviceChainAddition;
      TreeNode temporaryTreeNode;
      subracksList = this.temporarySystemDevices.FindAll(x => x is ISubrackFrameInterface);
      otherdevices = this.temporarySystemDevices.FindAll(x => !(x is ISubrackFrameInterface));
      for (int i = 0; i < subracksList?.Count; i++)
      {
        device = subracksList[i];
        temporaryString = device.ToString();
        temporaryTreeNode = new TreeNode(temporaryString);
        temporaryTreeNode.Tag = device;
        temporaryTreeView.Nodes.Add(temporaryTreeNode);
        temporaryTreeNode = null;
        deviceChainIndexed = (device as ISubrackFrameInterface).DeviceChainIndexed;
        if (deviceChainIndexed?.Count > 0)
        {
          for (int i2 = 0; i2 < deviceChainIndexed.Count; i2++)
          {
            deviceChainIndexedItem = (BaseClasses.PairBaseClass<int, object>)deviceChainIndexed[i2];
            if (deviceChainIndexedItem?.Value2 != null)
            {
              temporaryString = deviceChainIndexedItem.Value2.ToString();
              temporaryTreeNode = new TreeNode(temporaryString);
              temporaryTreeNode.Tag = deviceChainIndexedItem.Value2;
              temporaryTreeView.Nodes[i].Nodes.Add(temporaryTreeNode);
              temporaryTreeNode = null;
            }
          }
        }
        deviceChainAddition = (device as ISubrackFrameInterface).DeviceChainIntegral;
        for (int i2 = 0; i2 < deviceChainAddition?.Count; i2++)
        {
          integralDevice = deviceChainAddition[i];
          if (integralDevice != null)
          {
            temporaryString = integralDevice.ToString();
            temporaryTreeNode = new TreeNode(temporaryString);
            temporaryTreeNode.Tag = integralDevice;
            temporaryTreeView.Nodes[i].Nodes.Add(temporaryTreeNode);
            temporaryTreeNode = null;
          }
        }
      }
      for (int i = 0; i < otherdevices.Count; i++)
      {
        device = otherdevices[i];
        temporaryString = device.ToString();
        temporaryTreeNode = new TreeNode(temporaryString);
        temporaryTreeNode.Tag = device;
        temporaryTreeView.Nodes.Add(temporaryTreeNode);
        temporaryTreeNode = null;
      }
      systemFormulaTreeView.ExpandAll();
      temporaryTreeView = null;
      device = null;
      integralDevice = null;
      subracksList = null;
      otherdevices = null;
      temporaryString = null;
      deviceChainIndexed = null;
      deviceChainIndexedItem = null;
      deviceChainAddition = null;
      temporaryTreeNode = null;
    }
    /// <summary>
    /// Производит загрузку пользовательского интерфейса устройства при его выборе в списке доступных устройств.
    /// </summary>
    /// <param name="sender">Объект, спровоцировавший вызов события.</param>
    /// <param name="e">Агрументы события.</param>
    private void SelectAvailableDevice(object sender, EventArgs e)
    {
      deviceSettingPanel.Controls.Clear();
      object device = null;
      bool generating = true;
      if (availableDevicesListView.SelectedIndices.Count > 0 && availableDevicesListView.SelectedIndices[0] > -1)
      {
        try
        {
          device = Activator.CreateInstance((availableDevicesListView.SelectedItems[0].Tag as Type), new object[] { generating });
        }
        catch
        {
          try
          {
            device = Activator.CreateInstance((availableDevicesListView.SelectedItems[0].Tag as Type));
          }
          catch //(Exception currentException)
          {
            device = null;
            //throw new System.Reflection.TargetInvocationException($"Cant create device object of {nameof(IDeviceInterface)}", currentException);
          }
        }
        var deviceControl = (device as IDeviceSettingsInterface)?.CreateDeviceSettingsUserControl() ?? new DeviceSettingsDefault(device);
        SelectedDevice = device;
        device = null;
        (deviceControl as IDeviceSettingsAbstract).DataInvalidate += TryAddToFormula;
        deviceSettingPanel.Controls.Add(deviceControl);
        systemFormulaTreeView.SelectedNode = null;
      }
    }
    /// <summary>
    /// Производит загрузку пользовательского интерфейса устройства при его выборе в списке устройств оборудования.
    /// </summary>
    /// <param name="sender">Объект, спровоцировавший вызов события.</param>
    /// <param name="e">Агрументы события.</param>
    private void SelectAddedDevice(object sender, TreeViewEventArgs e)
    {
      deviceSettingPanel.Controls.Clear();
      object device;
      device = systemFormulaTreeView.SelectedNode.Tag;
      var deviceControl = (device as IDeviceSettingsInterface)?.CreateDeviceSettingsUserControl() ?? new DeviceSettingsDefault(device);
      (deviceControl as IDeviceSettingsAbstract).DataInvalidate += ReturnFocusToFormula;
      deviceSettingPanel.Controls.Add(deviceControl);
    }
    /// <summary>
    /// Подготавливает меню обработки состава конфигурации оборудования.
    /// </summary>
    private void PrepareContextMenuStrip()
    {
      this.systemFormulaContextMenuStrip = new ContextMenuStrip();
      this.systemFormulaContextMenuStrip.SuspendLayout();
      this.systemFormulaContextMenuStrip.Name = nameof(this.systemFormulaContextMenuStrip);
      this.systemFormulaContextMenuStrip.ResumeLayout(false);
    }
    /// <summary>
    /// Событие, происходящее при открытии контестного меню в поле конфигурации системы.
    /// </summary>
    /// <param name="sender">Объект, спровоцировавший вызов события.</param>
    /// <param name="e">Агрументы события.</param>
    private void OpenContextMenuStrip(object sender, TreeNodeMouseClickEventArgs e)
    {
      object device;
      this.systemFormulaContextMenuStrip.Tag = null;
      if (e.Button == MouseButtons.Right)
      {
        systemFormulaTreeView.SelectedNode = e.Node;
        if (e.Node.Tag != null)
        {
          this.systemFormulaContextMenuStrip.Items.Clear();
          this.systemFormulaContextMenuStrip.Tag = e.Node.Tag;
          device = e.Node.Tag;
          if (e.Node.Level > 0)
          {
            if (device is IDeviceSubrackItemIndexedInterface)
            {
              this.systemFormulaContextMenuStrip.Items.AddRange(new ToolStripItem[] {
                            new ToolStripMenuItem("Извлечь", null, EjectDevice),
                            new ToolStripMenuItem("Удалить", null, DeleteDevice),
                            });
              systemFormulaContextMenuStrip.Show(systemFormulaTreeView, e.Location);
            }
          }
          else
          {
            this.systemFormulaContextMenuStrip.Items.AddRange(new ToolStripItem[] {
                            new ToolStripMenuItem("Удалить", null, DeleteDevice),
                            });
            systemFormulaContextMenuStrip.Show(systemFormulaTreeView, e.Location);
          }
          //this.systemFormulaContextMenuStrip.Opening += BusButtonsContextMenuStrip_Opening;

        }
      }
      device = null;
    }
    /// <summary>
    /// Вызывается при нажатии на меню замыкания или размыкания шинного реле.
    /// </summary>
    /// <param name="sender">Объект, спровоцировавший вызов события.</param>
    /// <param name="e">Агрументы события.</param>
    private void EjectDevice(object sender, EventArgs e)
    {
      var varDevice = ((ContextMenuStrip)(sender as ToolStripMenuItem).GetCurrentParent()).Tag;
      var varDeviceIDeviceSubrackItemIndexedInterface = varDevice as IDeviceSubrackItemIndexedInterface;
      if (varDeviceIDeviceSubrackItemIndexedInterface != null)
      {
        var varSubrack = this.temporarySystemDevices.Find(x => x is ISubrackFrameInterface && (x as ISubrackFrameInterface).SubrackNumber == varDeviceIDeviceSubrackItemIndexedInterface.SubrackNumber) as ISubrackFrameInterface;
        dynamic out_EjectedDevice;
        varSubrack?.EjectDeviceFromPosition(varDeviceIDeviceSubrackItemIndexedInterface.SubrackItemNumber, out out_EjectedDevice);
        varDeviceIDeviceSubrackItemIndexedInterface.SubrackItemNumber = 0;
        varDeviceIDeviceSubrackItemIndexedInterface.SubrackNumber = 0;
        SystemDeviceConfiguration.TryAddDevice(varDeviceIDeviceSubrackItemIndexedInterface, this.temporarySystemDevices);
        varSubrack = null;
        out_EjectedDevice = null;
        UpdateSystemFormulaTreeView();
      }
      varDevice = null;
      varDeviceIDeviceSubrackItemIndexedInterface = null;
    }
    /// <summary>
    /// Вызывается при нажатии на меню замыкания или размыкания шинного реле.
    /// </summary>
    /// <param name="sender">Объект, спровоцировавший вызов события.</param>
    /// <param name="e">Агрументы события.</param>
    private void DeleteDevice(object sender, EventArgs e)
    {
      var varDevice = ((ContextMenuStrip)(sender as ToolStripMenuItem).GetCurrentParent()).Tag;
      if (varDevice is IDeviceSubrackItemIndexedInterface)
      {
        var varDeviceIDeviceSubrackItemIndexedInterface = varDevice as IDeviceSubrackItemIndexedInterface;
        if (varDeviceIDeviceSubrackItemIndexedInterface?.SubrackNumber > 0)
        {
          var varSubrack = this.temporarySystemDevices.Find(x => x is ISubrackFrameInterface && (x as ISubrackFrameInterface).SubrackNumber == varDeviceIDeviceSubrackItemIndexedInterface.SubrackNumber) as ISubrackFrameInterface;
          dynamic out_EjectedDevice;
          varSubrack?.EjectDeviceFromPosition(varDeviceIDeviceSubrackItemIndexedInterface.SubrackItemNumber, out out_EjectedDevice);
          varSubrack = null;
          out_EjectedDevice = null;
        }
        else
        {
          SystemDeviceConfiguration.TryRemoveDevice(varDevice, this.temporarySystemDevices);
        }
      }
      else
      {
        SystemDeviceConfiguration.TryRemoveDevice(varDevice, this.temporarySystemDevices);
      }
      UpdateSystemFormulaTreeView();
    }
    /// <summary>
    /// Производит попытку добавить устройство в качество составной части системы.
    /// </summary>
    /// <param name="sender">Объект, спровоцировавший вызов события.</param>
    /// <param name="e">Агрументы события.</param>
    private void TryAddToFormula(object sender, EventArgs e)
    {
      if (availableDevicesListView.SelectedIndices.Count > 0 && availableDevicesListView.SelectedIndices[0] > -1)
      {
        var device = SelectedDevice;
        var deviceControl = (device as IDeviceSettingsInterface)?.GetDeviceSettingsUserControl();
        if (deviceControl != null)
        {
          (deviceControl as IDeviceSettingsAbstract).DataInvalidate -= TryAddToFormula;
        }
        PerformActionOnDevice(device);
        availableDevicesListView.Focus();
        SelectAvailableDevice(availableDevicesListView, EventArgs.Empty);
      }
    }
    /// <summary>
    /// Устанавливает фокус ввода на пользовательский элемент управления с данными конфигурации оборудования.
    /// </summary>
    /// <param name="sender">Объект, спровоцировавший вызов события.</param>
    /// <param name="e">Агрументы события.</param>
    private void ReturnFocusToFormula(object sender, EventArgs e)
    {
      object device;
      device = systemFormulaTreeView.SelectedNode.Tag;
      var deviceControl = (device as IDeviceSettingsInterface)?.GetDeviceSettingsUserControl();
      if (deviceControl != null)
      {
        (deviceControl as IDeviceSettingsAbstract).DataInvalidate -= ReturnFocusToFormula;
      }
      PerformActionOnDevice(device, false, sender);
      systemFormulaTreeView.Focus();
    }
    /// <summary>
    /// Производит проверку устройства и выполняет необходимые действия над ним.
    /// </summary>
    /// <param name="device">устройство, над которым производится действий.</param>
    /// <param name="newDevice">Флаг, определяющий, что обработка должна производиться как с вновь добавляемым устройством.</param>
    /// <param name="additionData">дополнительные данные для выполнения действия.</param>
    private void PerformActionOnDevice(object device, bool newDevice = true, object additionData = null)
    {
      if (device is IDeviceSubrackItemIndexedInterface)
      {
        if (!newDevice)
        {
          IDeviceSubrackItemIndexedInterface targetData;
          if (systemFormulaTreeView.SelectedNode.Level > 0)
          {
            targetData = additionData as IDeviceSubrackItemIndexedInterface;
            var varSubrack = this.temporarySystemDevices.Find(x => x is ISubrackFrameInterface && (x as ISubrackFrameInterface).SubrackNumber == (targetData?.SubrackNumber ?? 0)) as ISubrackFrameInterface;
            dynamic out_EjectedDevice = null;
            varSubrack?.EjectDeviceFromPosition(targetData.SubrackItemNumber, out out_EjectedDevice);
            SystemDeviceConfiguration.TryAddDevice(out_EjectedDevice, this.temporarySystemDevices);
          }
          else
          {
            SystemDeviceConfiguration.TryRemoveDevice(device, this.temporarySystemDevices);
            SystemDeviceConfiguration.TryAddDevice(device, this.temporarySystemDevices);
          }
        }
        else
        if (SystemDeviceConfiguration.TryAddDevice(device, this.temporarySystemDevices))
        {

        }
      }
      else
      if (device is ISocketAdapterInterface)
      {
        if (newDevice) SystemDeviceConfiguration.TryAddDevice(device, this.temporarySystemDevices);
        SystemDeviceConfiguration.CheckAllSocketAdapterData(this.temporarySystemDevices);
      }
      else
      {
        if (newDevice) SystemDeviceConfiguration.TryAddDevice(device, this.temporarySystemDevices);
      }
      UpdateSystemFormulaTreeView();
    }
  }
}