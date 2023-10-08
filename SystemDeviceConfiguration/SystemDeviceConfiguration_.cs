using System;
using System.Collections.Generic;

namespace SystemDeviceConfiguration
{
  /// <summary>
  /// Класс данных конфигурации оборудования системы.
  /// </summary>
  public static partial class SystemDeviceConfiguration
  {
    /// <summary>
    /// Данные конфигурации оборудования.
    /// </summary>
    private static List<object> CurrentSystemDevices = new List<object>();
    /// <summary>
    /// Перечень типов данных, котоные могут быть сериализованы.
    /// </summary>
    private static Type[] availableSerializableTypes;
    /// <summary>
    /// Путь к директории с имеющимися файлами конфигураций оборудования.
    /// </summary>
    private static string configHardwareDirectoryPath;
    /// <summary>
    /// Путь к директории, в которой располагаются DLL-файлы подключаемого оборудования.
    /// </summary>
    private static string deviceDLLDirectoryPath;
    /// <summary>
    /// Имя загружнного файла конфигурации, либо null.
    /// </summary>
    private static string loadedConfigurationFileName = string.Empty;
    /// <summary>
    /// Событие загрузки данных конфигурации оборудования из файла.
    /// </summary>
    public static event EventHandler<BaseClasses.EventArgsBaseClass> LoadConfigurationFileChanged = null;
  }
}