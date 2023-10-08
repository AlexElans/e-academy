using System;
using System.Collections.Generic;

namespace SystemDeviceConfiguration
{
  partial class SystemDeviceConfiguration
  {
    /// <summary>
    /// Класс данных с настройками конфигурации оборудования системы.
    /// </summary>
    [Serializable]
    public partial class ExternalSettingsClass
    {
      /// <summary>
      ///Конструктор класса данных с настройками уровня взаимодействия с аппаратурой.
      /// </summary>
      public ExternalSettingsClass()
      {
      }

      #region // Настройки, относящиеся к прочим параметрам конфигурации аппаратуры
      /// <summary>
      /// Расширение для файлов конфигураций оборудования (внутренняя структура - XML).
      /// </summary>
      public string ConfigurationFileExtension
      {
        get
        {
          return SystemDeviceConfiguration.ConfigurationFileExtension;
        }
        set
        {
          SystemDeviceConfiguration.ConfigurationFileExtension = value;
        }
      }
      /// <summary>
      /// Наименование файла конфигурации по-умолчанию, если название файла не задано.
      /// </summary>
      public string DefaultFileConfigurationFileName
      {
        get
        {
          return SystemDeviceConfiguration.DefaultFileConfigurationFileName;
        }
        set
        {
          SystemDeviceConfiguration.DefaultFileConfigurationFileName = value;
        }
      }
      /// <summary>
      /// Наименование директории по-умолчанию для загрузки файлов.
      /// </summary>
      public string ConfigHardwareDirectoryPath
      {
        get
        {
          return SystemDeviceConfiguration.ConfigHardwareDirectoryPath;
        }
        set
        {
          SystemDeviceConfiguration.ConfigHardwareDirectoryPath = value;
        }
      }
      /// <summary>
      /// Флаг использования обращения к устройствам коммутации через виртуальные адреса.
      /// </summary>
      public bool UseVirtualAddressing
      {
        get
        {
          return SystemDeviceConfiguration.UseVirtualAddressing;
        }
        set
        {
          SystemDeviceConfiguration.UseVirtualAddressing = value;
        }
      }
      /// <summary>
      /// Имя загружнного файла конфигурации, либо null.
      /// </summary>
      public string LoadedConfigurationFileName
      {
        get
        {
          return SystemDeviceConfiguration.LoadedConfigurationFileName;
        }
        set
        {
          SystemDeviceConfiguration.LoadedConfigurationFileName = value;
        }
      }
      #endregion

      /// <summary>
      /// Предоставляет настройки заполнения конфигурации оборудования.
      /// </summary>
      /// <param name="settingsTypeName">Текст, отображаемый на отдельной вкладке настроек.</param>
      /// <param name="settingsCaption">Текст, отображаемый в заголовке формы настроек при выборе текущей вкладки.</param>
      /// <returns>Возвращает данные текста вкладки, заголовка формы для текущей вкладки и список содержащий тип данных и строковое представление наименования публичного свойста класса настроек либо заголовок группы настроек, следующих далее.</returns>
      public static BaseClasses.TrioBaseClass<string, string, List<BaseClasses.PairBaseClass<BaseClasses.SettingsBaseClass.ItemSettingType, string>>> GetSettinDataForSystemDevices(string settingsTypeName = null, string settingsCaption = null)
      {
        if (string.IsNullOrWhiteSpace(settingsTypeName) || string.IsNullOrWhiteSpace(settingsCaption))
        {
          settingsTypeName = nameof(TranslationSettingsClass.SettingsType_systemDevices);
          settingsCaption = nameof(TranslationSettingsClass.SettingsCaption_systemDevices);
        }
        BaseClasses.TrioBaseClass<string, string, List<BaseClasses.PairBaseClass<BaseClasses.SettingsBaseClass.ItemSettingType, string>>> returnedData = new BaseClasses.TrioBaseClass<string, string, List<BaseClasses.PairBaseClass<BaseClasses.SettingsBaseClass.ItemSettingType, string>>>
            (settingsTypeName, settingsCaption,
            new List<BaseClasses.PairBaseClass<BaseClasses.SettingsBaseClass.ItemSettingType, string>>
            {
                        new BaseClasses.PairBaseClass<BaseClasses.SettingsBaseClass.ItemSettingType, string>(BaseClasses.SettingsBaseClass.ItemSettingType.SettingItemName, nameof(LoadedConfigurationFileName                                     )),
                        new BaseClasses.PairBaseClass<BaseClasses.SettingsBaseClass.ItemSettingType, string>(BaseClasses.SettingsBaseClass.ItemSettingType.SettingItemName, nameof(ConfigurationFileExtension                                      )),
                        new BaseClasses.PairBaseClass<BaseClasses.SettingsBaseClass.ItemSettingType, string>(BaseClasses.SettingsBaseClass.ItemSettingType.SettingItemName, nameof(DefaultFileConfigurationFileName                                )),
            });
        return returnedData;
      }

    }
  }
}