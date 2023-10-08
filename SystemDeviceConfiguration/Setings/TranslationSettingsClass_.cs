using BaseInterfaces;

namespace SystemDeviceConfiguration
{
  /// <summary>
  /// Класс текстовых данных уровня взаимодействия с аппаратурой для перевода на другие языки.
  /// </summary>
  public partial class TranslationSettingsClass: ITranslationInterface
  {
    #region // Текст отображаемый в окнах настроек программного обеспечения
    /// <summary>
    /// Текст, отображаемый в меню для выделения группы настроек заполнения конфигурации оборудования.
    /// </summary>
    internal static string SettingsType_systemDevices = "Конфигурация";
    /// <summary>
    /// Текст, отображаемый в заголовке формы настроек при выборе вкладки настроек заполнения конфигурации оборудования.
    /// </summary>
    internal static string SettingsCaption_systemDevices = "Настройки заполнения конфигурации оборудования";

    #region группа текстов для GetSettinDataForSystemDevices

    /// <summary>
    /// Расширение для файлов конфигураций оборудования (внутренняя структура - XML).
    /// </summary>
    public static string ConfigurationFileExtension = "Расширение файлов конфигурации оборудования";
    /// <summary>
    /// Наименование файла конфигурации по-умолчанию, если название файла не задано.
    /// </summary>
    public static string DefaultFileConfigurationFileName = "Наименование файла конфигурации по-умолчанию, если имя не задано";
    /// <summary>
    /// Имя загружнного файла конфигурации, либо null.
    /// </summary>
    public static string LoadedConfigurationFileName = "Имя загружаемого файла конфигурации при загрузке системы";
    #endregion
    #endregion
  }
}
