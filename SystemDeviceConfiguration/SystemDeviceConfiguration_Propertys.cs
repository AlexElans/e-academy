using System;
using System.Collections.Generic;

namespace SystemDeviceConfiguration
{
  partial class SystemDeviceConfiguration
  {
    /// <summary>
    /// Расширение для файлов конфигураций оборудования (внутренняя структура - XML).
    /// </summary>
    public static string ConfigurationFileExtension { get; set; } = ".configDevice";
    /// <summary>
    /// Наименование файла конфигурации по-умолчанию, если название файла не задано.
    /// </summary>
    public static string DefaultFileConfigurationFileName { get; set; } = "Проект";
    /// <summary>
    /// Путь к директории с имеющимися файлами конфигураций оборудования.
    /// </summary>
    public static string ConfigHardwareDirectoryPath
    {
      get
      {
        return System.IO.Directory.Exists(configHardwareDirectoryPath) ? configHardwareDirectoryPath : System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
      }
      set
      {
        SetConfigHardwareDirectoryPath(value);
      }
    }
    /// <summary>
    /// Путь к директории, в которой располагаются DLL-файлы подключаемого оборудования.
    /// </summary>
    public static string DeviceDLLDirectoryPath
    {
      get
      {
        return System.IO.Directory.Exists(deviceDLLDirectoryPath) ? deviceDLLDirectoryPath : System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
      }
      set
      {
        SetDeviceDLLDirectoryPath(value);
      }
    }
    /// <summary>
    /// Ссылка на текущий экземпляр конфигурации оборудования.
    /// </summary>
    public static List<object> Current => CurrentSystemDevices;
    /// <summary>
    /// Флаг использования обращения к устройствам коммутации через виртуальные адреса.
    /// </summary>
    public static bool UseVirtualAddressing { get; set; }
    /// <summary>
    /// Имя загружнного файла конфигурации, либо null.
    /// </summary>
    public static string LoadedConfigurationFileName
    {
      get
      {
        return loadedConfigurationFileName;
      }
      set
      {
        SetLoadedConfigurationFile(value);
        LoadConfigurationFileChanged?.Invoke(null, new BaseClasses.EventArgsBaseClass(loadedConfigurationFileName));
      }

    }
  }
}