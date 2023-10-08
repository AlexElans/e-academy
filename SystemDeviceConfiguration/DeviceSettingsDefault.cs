using BaseInterfaces.DevicesLibrary;
using System;
using System.Windows.Forms;

namespace SystemDeviceConfiguration
{
  /// <summary>
  /// Класс пользовательского элемента управления изменения настроек устройства.
  /// </summary>
  public partial class DeviceSettingsDefault: UserControl, IDeviceSettingsAbstract
  {
    /// <summary>
    /// Событие, вызываемое при завершении сохранения данных.
    /// </summary>
    public event EventHandler DataInvalidate;
    /// <summary>
    /// Данные устройства
    /// </summary>
    private object referencesData { get; set; }
    /// <summary>
    /// Конструктор пользовательского элемента управления изменения настроек указанного устройства.
    /// </summary>
    /// <param name="deviceData">Данные устройства.</param>
    public DeviceSettingsDefault(object deviceData)
    {
      InitializeComponent();
      referencesData = deviceData;
      InvalidateDeviceSettings();
    }
    /// <summary>
    /// Заполняет элементы управления из предоставленных данных.
    /// </summary>
    private void InvalidateDeviceSettings()
    {
      var varReferencesData = referencesData as IDeviceInterface;
      if (varReferencesData != null)
      {
        DeviceNameTextBox.Value = varReferencesData.DeviceName;
        DeviceFullNameTextBox.Value = varReferencesData.DeviceFullName;
        ServiceInformationTextBox.Value = varReferencesData.ServiceInformation;
        CommentaryTextBox.Value = varReferencesData.Commentary;
        DeviceDescriptionTextBox.Text = varReferencesData.Description;
        DeviceDescriptionTextBox.SelectionStart = 0;
      }
      varReferencesData = null;
      Invalidate();
    }
    /// <summary>
    /// Вызывается при нажатии на кнопку сохранения настроек.
    /// </summary>
    /// <param name="sender">Объект, вызвавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private void SaveSettingsDeviceButton_Click(object sender, EventArgs e)
    {
      var varReferencesData = referencesData as IDeviceInterface;
      if (varReferencesData != null)
      {
        varReferencesData.DeviceName = DeviceNameTextBox.Value;
        varReferencesData.DeviceFullName = DeviceFullNameTextBox.Value;
        varReferencesData.ServiceInformation = ServiceInformationTextBox.Value;
        varReferencesData.Commentary = CommentaryTextBox.Value;
      }
      varReferencesData = null;
      InvalidateDeviceSettings();
      DataInvalidate?.Invoke(null, EventArgs.Empty);
    }
  }
}