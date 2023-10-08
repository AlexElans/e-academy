using BaseInterfaces.DevicesLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SystemDeviceConfiguration
{
  partial class SystemDeviceConfiguration
  {
    /// <summary>
    /// Конструктор класса данных конфигурации оборудования системы.
    /// </summary>
    static SystemDeviceConfiguration()
    {
      GenerateAvailableDevicesTypes();
    }
    /// <summary>
    /// Устанавливает пути к папкам с конфигурацией оборудования и библиотеками подключаемых устройств.
    /// </summary>
    /// <param name="in_configHardwareDirectoryPath">Путь к директории с имеющимися файлами конфигураций оборудования.</param>
    /// <param name="in_deviceDLLDirectoryPath">Путь к директории, в которой располагаются DLL-файлы подключаемого оборудования.</param>
    public static void SetPaths(string in_configHardwareDirectoryPath = null, string in_deviceDLLDirectoryPath = null)
    {
      SetConfigHardwareDirectoryPath(in_configHardwareDirectoryPath);
      SetDeviceDLLDirectoryPath(in_deviceDLLDirectoryPath);
    }
    /// <summary>
    /// Устанавливает путь к директории с имеющимися файлами конфигураций оборудования.
    /// </summary>
    /// <param name="in_configHardwareDirectoryPath">Путь к директории с имеющимися файлами конфигураций оборудования.</param>
    private static void SetConfigHardwareDirectoryPath(string in_configHardwareDirectoryPath)
    {
      if (!string.IsNullOrWhiteSpace(in_configHardwareDirectoryPath) && Directory.Exists(in_configHardwareDirectoryPath)) configHardwareDirectoryPath = in_configHardwareDirectoryPath;
      else
        configHardwareDirectoryPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
    }
    /// <summary>
    /// Устанавливает путь к директории, в которой располагаются DLL-файлы подключаемого оборудования.
    /// </summary>
    /// <param name="in_deviceDLLDirectoryPath">Путь к директории, в которой располагаются DLL-файлы подключаемого оборудования.</param>
    private static void SetDeviceDLLDirectoryPath(string in_deviceDLLDirectoryPath)
    {
      if (!string.IsNullOrWhiteSpace(in_deviceDLLDirectoryPath) && Directory.Exists(in_deviceDLLDirectoryPath)) deviceDLLDirectoryPath = in_deviceDLLDirectoryPath;
      else
        deviceDLLDirectoryPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
    }
    /// <summary>
    /// Генерирует список доступных типов устройств для дельнейшего создания их экземпляров.
    /// </summary>
    /// <param name="in_deviceDLLDirectoryPath">Путь к директории, в которой располагаются DLL-файлы подключаемого оборудования.</param>
    private static void GenerateAvailableDevicesTypes(string in_deviceDLLDirectoryPath = null)
    {
      if (!Directory.Exists(deviceDLLDirectoryPath)) SetDeviceDLLDirectoryPath(in_deviceDLLDirectoryPath);
      string[] availableDLLs = Directory.GetFiles(deviceDLLDirectoryPath, "*.dll", SearchOption.AllDirectories);
      List<Type> availableTypes = new List<Type>();
      Assembly assemblyData;
      for (int i = 0; i < availableDLLs.Length; i++)
      {
        assemblyData = Assembly.LoadFrom(availableDLLs[i]);
        availableTypes.AddRange(assemblyData.GetExportedTypes());
      }
      availableSerializableTypes = availableTypes.Distinct().Where(x => x.IsClass && x.IsPublic && x.IsSerializable).ToArray();
      availableDLLs = null;
      availableTypes = null;
      assemblyData = null;
    }
    /// <summary>
    /// Предоставляет список типов, обозначенных в качестве устройств.
    /// </summary>
    /// <param name="in_deviceDLLDirectoryPath">Путь к директории, в которой располагаются DLL-файлы подключаемого оборудования.</param>
    /// <returns>Возвращает список типов, обозначенных в качестве устройств.</returns>
    public static List<Type> GetAvailableDeviceTypes(string in_deviceDLLDirectoryPath = null)
    {
      List<Type> availableDeviceTypes = new List<Type>();
      Type ExportedType;
      if (!Directory.Exists(deviceDLLDirectoryPath)) SetDeviceDLLDirectoryPath(in_deviceDLLDirectoryPath);
      if (availableSerializableTypes == null || availableSerializableTypes.Length == 0) GenerateAvailableDevicesTypes(in_deviceDLLDirectoryPath);
      for (int i = 0; i < availableSerializableTypes?.Length; i++)
      {
        ExportedType = availableSerializableTypes[i];
        if (ExportedType.GetInterface(nameof(IDeviceInterface)) != null)
        {
          availableDeviceTypes.Add(ExportedType);
        }
      }
      try
      {
        return availableDeviceTypes;
      }
      finally
      {
        availableDeviceTypes = null;
        ExportedType = null;
      }
    }
    /// <summary>
    /// Генерирует список доступных устройств для их возможного назначения в качество составных частей системы.
    /// </summary>
    /// <param name="in_deviceDLLDirectoryPath">Путь к директории, в которой располагаются DLL-файлы подключаемого оборудования.</param>
    /// <returns>Возвращает список доступных устройств для их возможного назначения в качество составных частей системы.</returns>
    public static List<object> GenerateAvailableDevicesListBox(string in_deviceDLLDirectoryPath = null)
    {
      if (!Directory.Exists(deviceDLLDirectoryPath)) SetDeviceDLLDirectoryPath(in_deviceDLLDirectoryPath);
      if (availableSerializableTypes == null || availableSerializableTypes.Length == 0) GenerateAvailableDevicesTypes(in_deviceDLLDirectoryPath);
      var availableDevices = new List<object>();
      Type ExportedType;
      bool generating = true;
      object DeviceInstance = null;
      for (int i = 0; i < availableSerializableTypes?.Length; i++)
      {
        ExportedType = availableSerializableTypes[i];
        if (ExportedType.GetInterface(nameof(IDeviceInterface)) != null)
        {
          try
          {
            DeviceInstance = Activator.CreateInstance(ExportedType, new object[] { generating });
            availableDevices.Add(DeviceInstance);
          }
          catch
          {
            try
            {
              DeviceInstance = Activator.CreateInstance(ExportedType);
              availableDevices.Add(DeviceInstance);
            }
            catch (Exception e)
            {
              throw new TargetInvocationException($"Cant create device object of {nameof(IDeviceInterface)}", e);
            }
          }
        }
      }
      ExportedType = null;
      try
      {
        return availableDevices;
      }
      finally
      {
        availableDevices = null;
      }
    }
    /// <summary>
    /// Производит сохранение пути к файлу для автоматической загрузки конфигурации системы.
    /// </summary>
    /// <param name="in_FileName">Наименование файла для автоматической загрузки конфигурации системы.</param>
    private static void SetLoadedConfigurationFile(string in_FileName)
    {
      loadedConfigurationFileName = Path.Combine(configHardwareDirectoryPath, in_FileName);
      if (!File.Exists(loadedConfigurationFileName)) loadedConfigurationFileName = string.Empty;
      else loadedConfigurationFileName = Path.GetFileName(loadedConfigurationFileName);
    }
    /// <summary>
    /// Сохраняет данные конфигурации оборудования системы в заданную директорию, с заданным наименованием сохраняемого файла.
    /// </summary>
    /// <param name="in_savedDirectory">Путь к директории для сохранения данных конфигурации оборудования.</param>
    /// <param name="in_savedFileName">Наименование сохраняемого файла конфигурации оборудоваия.</param>
    /// <param name="temporarySystemDevices">Данные редактируемой конфигурации оборудования, либо null если требуется сохранить текущие настройки.</param>
    public static void SaveConfiguration(string in_savedDirectory = null, string in_savedFileName = null, List<object> temporarySystemDevices = null)
    {
      if (temporarySystemDevices == null)
      {
        temporarySystemDevices = Current;
      }
      string fullFilePath = null;
      if (string.IsNullOrWhiteSpace(in_savedFileName)) in_savedFileName = DefaultFileConfigurationFileName;
      System.Xml.Serialization.XmlSerializer ser = null;
      in_savedFileName = $"{Path.GetFileNameWithoutExtension(in_savedFileName)}{ConfigurationFileExtension}";
      if (!string.IsNullOrWhiteSpace(in_savedDirectory))
        fullFilePath = (Path.GetDirectoryName($"{in_savedDirectory?.TrimEnd(Path.DirectorySeparatorChar)}{Path.DirectorySeparatorChar}") ?? Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)?.TrimEnd(Path.DirectorySeparatorChar)) + Path.DirectorySeparatorChar + in_savedFileName;
      else
        fullFilePath += in_savedFileName;
      try
      {
        using (TextWriter writer = new StreamWriter(fullFilePath))
        {
          ser = new System.Xml.Serialization.XmlSerializer(CurrentSystemDevices.GetType(), availableSerializableTypes);
          ser.Serialize(writer, temporarySystemDevices);
          writer.Close();
          writer.Dispose();
        }
        CurrentSystemDevices = temporarySystemDevices;
        LoadedConfigurationFileName = Path.GetFileName(fullFilePath);
      }
      catch
      {
        LoadedConfigurationFileName = null;
        throw;
      }
      finally
      {
        fullFilePath = null;
        ser = null;
      }
    }
    /// <summary>
    /// Производит проверку возможности загрузки указанного файла в качестве файла конфигурации оборудования.
    /// </summary>
    /// <param name="in_FilePath">Путь к файлу конфигурации оборудования.</param>
    /// <returns>True - если файл существует и может быть загружен в качестве файла конфигурации оборудования, иначе - False</returns>
    public static bool CanLoadConfiguration(string in_FilePath)
    {
      Stream fs = null;
      System.Xml.XmlReader reader;
      System.Xml.Serialization.XmlSerializer serializer;
      try
      {
        if (File.Exists(in_FilePath))
        {
          fs = new FileStream(in_FilePath, FileMode.Open);
          reader = new System.Xml.XmlTextReader(fs);
          serializer = new System.Xml.Serialization.XmlSerializer(CurrentSystemDevices.GetType(), availableSerializableTypes);
          if (serializer.CanDeserialize(reader))
          {
            return true;
          }
        }
        return false;
      }
      finally
      {
        fs?.Close();
        fs?.Dispose();
        fs = null;
        reader = null;
        serializer = null;
      }
    }
    /// <summary>
    /// Загружает данные конфигурации оборудования системы из заданной директории, с заданным наименованием загружаемого файла с настройками.
    /// </summary>
    /// <param name="in_loadedDirectory">Путь к директории.</param>
    /// <param name="in_loadedFileName">Наименование файла для загрузки.</param>
    /// <param name="LoadToTemporary">Флаг указания загрузки конфигурации в качестве временных данных.</param>
    /// <returns>Возвращает данные загруженной конфигурации оборудования.</returns>
    public static List<object> LoadConfiguration(string in_loadedDirectory, string in_loadedFileName, bool LoadToTemporary = false)
    {
      string fullFilePath = string.Empty;
      System.Xml.Serialization.XmlSerializer ser = null;
      List<object> temporarySystemDevices = null;
      if (!string.IsNullOrWhiteSpace(in_loadedFileName))
      {
        if (!Path.HasExtension(in_loadedFileName)) in_loadedFileName += ConfigurationFileExtension;
        if (Path.GetExtension(in_loadedFileName) == ConfigurationFileExtension)
        {
          if (!string.IsNullOrWhiteSpace(in_loadedDirectory))
          {
            fullFilePath = (Path.GetDirectoryName($"{in_loadedDirectory?.TrimEnd(Path.DirectorySeparatorChar)}{Path.DirectorySeparatorChar}") ?? Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)?.TrimEnd(Path.DirectorySeparatorChar)) + Path.DirectorySeparatorChar + in_loadedFileName;
          }
          else
            fullFilePath += in_loadedFileName;
          if (availableSerializableTypes == null || availableSerializableTypes.Length == 0) GenerateAvailableDevicesTypes();
          if (CanLoadConfiguration(fullFilePath))
          {
            try
            {
              using (FileStream myFileStream = new FileStream(fullFilePath, FileMode.Open))
              {
                ser = new System.Xml.Serialization.XmlSerializer(CurrentSystemDevices.GetType(), availableSerializableTypes);
                temporarySystemDevices = (List<object>)ser.Deserialize(myFileStream);
                myFileStream?.Close();
                myFileStream?.Dispose();
              }
              if (!LoadToTemporary)
              {
                CurrentSystemDevices = temporarySystemDevices;
                temporarySystemDevices = null;
                LoadedConfigurationFileName = Path.GetFileName(fullFilePath);
              }
              CheckAllSocketAdapterData();
            }
            catch
            {
              if (!LoadToTemporary)
              {
                LoadedConfigurationFileName = string.Empty;
              }
              throw;
            }
          }
        }
      }

      fullFilePath = null;
      ser = null;
      return temporarySystemDevices;
    }
    /// <summary>
    /// Определяет был ли сохранён загружаемый файл конфигурации оборудования или нет.
    /// </summary>
    /// <param name="in_loadedDirectory">Путь к директории с файлом конфигурации оборудования.</param>
    /// <returns>True - файл конфигурации оборудования был успешно сохранён, иначе - False.</returns>
    public static bool LoadedConfigurationFileExist(string in_loadedDirectory)
    {
      string fullFilePath = string.Empty;
      try
      {
        fullFilePath = Path.GetDirectoryName($"{in_loadedDirectory?.TrimEnd(Path.DirectorySeparatorChar)}{Path.DirectorySeparatorChar}") + Path.DirectorySeparatorChar;
        if (Directory.Exists(fullFilePath))
        {
          fullFilePath += LoadedConfigurationFileName;
          return File.Exists(fullFilePath);
        }
        return false;
      }
      finally
      {
        fullFilePath = null;
      }
    }
    /// <summary>
    /// Производит попытку удалить устройство из состава системы.
    /// </summary>
    /// <param name="in_device">Данные устройства, которое требуется удалить.</param>
    /// <param name="temporarySystemDevices">список с временными данными устройств системы.</param>
    /// <returns>True - удаление успешно произведено, иначе - False.</returns>
    public static bool TryRemoveDevice(dynamic in_device, List<object> temporarySystemDevices = null)
    {
      if (temporarySystemDevices == null)
      {
        temporarySystemDevices = Current;
      }
      int DeviceIndex = -1;
      if ((DeviceIndex = temporarySystemDevices.FindIndex(x => x.Equals(in_device))) > -1)
      {
        temporarySystemDevices.RemoveAt(DeviceIndex);
        return true;
      }
      return false;
    }
    /// <summary>
    /// Производит попытку добавить устройство в качество составной части системы.
    /// </summary>
    /// <param name="in_device">Данные устройства, которое требуется добавить.</param>
    /// <param name="temporarySystemDevices">список с временными данными устройств системы.</param>
    /// <returns>True - добавление успешно произведено, иначе - False.</returns>
    public static bool TryAddDevice(dynamic in_device, List<object> temporarySystemDevices = null)
    {
      if (temporarySystemDevices == null)
      {
        temporarySystemDevices = Current;
      }
      bool SuccesfullyAdded = false;
      dynamic EjectedDevice = null;
      int deviceIndex = -1;
      try
      {
        if (in_device is ISubrackFrameInterface)
        {
          var deviceISubrackFrameInterface = in_device as ISubrackFrameInterface;
          if (deviceISubrackFrameInterface.SubrackNumber > 0 && (deviceIndex = temporarySystemDevices.FindIndex(x => x is ISubrackFrameInterface && (x as ISubrackFrameInterface).SubrackNumber == deviceISubrackFrameInterface.SubrackNumber)) < 0)
          {
            temporarySystemDevices.Add(in_device);
            SuccesfullyAdded = true;
          }
          deviceISubrackFrameInterface = null;
          return SuccesfullyAdded;
        }
        if (in_device is IDeviceSubrackItemIndexedInterface)
        {
          var deviceIDeviceSubrackItemIndexedInterface = in_device as IDeviceSubrackItemIndexedInterface;
          if (deviceIDeviceSubrackItemIndexedInterface.SubrackNumber > 0 && (deviceIndex = temporarySystemDevices.FindIndex(x => x is ISubrackFrameInterface && (x as ISubrackFrameInterface).SubrackNumber == deviceIDeviceSubrackItemIndexedInterface.SubrackNumber)) > -1)
          {
            var deviceISubrackFrameInterface = temporarySystemDevices[deviceIndex] as ISubrackFrameInterface;
            if (deviceISubrackFrameInterface.SetDeviceToPosition(deviceIDeviceSubrackItemIndexedInterface.SubrackItemNumber, ref in_device, out EjectedDevice))
            {
              if (EjectedDevice != null)
              {
                (EjectedDevice as IDeviceSubrackItemIndexedInterface).SubrackItemNumber = 0;
                (EjectedDevice as IDeviceSubrackItemIndexedInterface).SubrackNumber = 0;
                temporarySystemDevices.Add(EjectedDevice);
              }
              SuccesfullyAdded = true;
            }
            else
            {
              (in_device as IDeviceSubrackItemIndexedInterface).SubrackItemNumber = 0;
              (in_device as IDeviceSubrackItemIndexedInterface).SubrackNumber = 0;
              temporarySystemDevices.Add(in_device);
              SuccesfullyAdded = true;
            }
            deviceISubrackFrameInterface = null;
          }
          else
          {
            (in_device as IDeviceSubrackItemIndexedInterface).SubrackItemNumber = 0;
            (in_device as IDeviceSubrackItemIndexedInterface).SubrackNumber = 0;
            temporarySystemDevices.Add(in_device);
            SuccesfullyAdded = true;
          }
          deviceIDeviceSubrackItemIndexedInterface = null;
          return SuccesfullyAdded;
        }
        if (in_device is IDeviceInterface)
        {
          if (in_device is IDeviceIDGeneratorInterface)
          {
            int newInternalID;
            do
            {
              newInternalID = (in_device as IDeviceIDGeneratorInterface).GenerateInternalID();
            }
            while (temporarySystemDevices.Any(x => (x is IDeviceIDGeneratorInterface) && (x as IDeviceIDGeneratorInterface).InternalID == newInternalID));
            (in_device as IDeviceIDGeneratorInterface).InternalID = newInternalID;
          }
          temporarySystemDevices.Add(in_device);
          SuccesfullyAdded = true;
          return SuccesfullyAdded;
        }
        return SuccesfullyAdded;
      }
      finally
      {
        EjectedDevice = null;
      }
    }
    /// <summary>
    /// Предоставляет список всех коммутационных точек текщей конфигурации оборудования.
    /// </summary>
    /// <returns>Возвращает список всех коммутационных точек текщей конфигурации оборудования.</returns>
    public static List<BaseClasses.PointsAddress> GetAllCommutationPointsList()
    {
      List<BaseClasses.PointsAddress> AllPointsASKList = new List<BaseClasses.PointsAddress>();
      List<object> targetDataList;
      List<BaseClasses.PairBaseClass<int, dynamic>> commutationDevices;
      List<BaseClasses.DesignatedConnector> temporaryConnectorList;
      BaseClasses.Connector temporaryConnector;
      IDeviceSubrackItemIndexedInterface temporarySubrackData;
      try
      {
        if (UseVirtualAddressing)
        {
          targetDataList = CurrentSystemDevices.FindAll(x => x is ISocketAdapterInterface);
          for (int i = 0; i < targetDataList?.Count; i++)
          {
            temporaryConnectorList = (targetDataList[i] as ISocketAdapterInterface).RealConnectors?.Cast<BaseClasses.DesignatedConnector>()?.ToList();
            for (int i2 = 0; i2 < temporaryConnectorList?.Count; i2++)
            {
              if (temporaryConnectorList[i2].Tag is IDeviceSubrackItemIndexedInterface)
              {
                temporarySubrackData = temporaryConnectorList[i2].Tag as IDeviceSubrackItemIndexedInterface;
                temporaryConnector = temporaryConnectorList[i2].connectorData;
                for (int i3 = 0; i3 < temporaryConnector?.ContactCount; i3++)
                {
                  AllPointsASKList.Add(new BaseClasses.PointsAddress(temporarySubrackData.SubrackNumber, temporarySubrackData.DeviceNumber, temporaryConnector[i3].Number));
                }
              }
            }
          }
        }
        else
        {
          targetDataList = CurrentSystemDevices.FindAll(x => x is ISubrackFrameInterface);
          for (int i = 0; i < targetDataList?.Count; i++)
          {
            commutationDevices = (targetDataList[i] as ISubrackFrameInterface).DeviceChainIndexed.FindAll(x => x is BaseClasses.PairBaseClass<int, dynamic> && (x as BaseClasses.PairBaseClass<int, dynamic>).Value2 is ICommutatorInterface)?.Cast<BaseClasses.PairBaseClass<int, dynamic>>()?.ToList();
            for (int i2 = 0; i2 < commutationDevices?.Count; i2++)
            {
              temporarySubrackData = (commutationDevices[i2].Value2 as IDeviceSubrackItemIndexedInterface);
              if (temporarySubrackData.DeviceNumber > 0)
              {
                var commatationData = (commutationDevices[i2].Value2 as ICommutatorInterface);
                for (int i3 = 0; i3 < commatationData?.ConnectorList?.Count; i3++)
                {
                  temporaryConnector = (commatationData.ConnectorList[i3] as BaseClasses.DesignatedConnector).connectorData;
                  for (int i4 = 0; i4 < temporaryConnector?.ContactCount; i4++)
                  {
                    AllPointsASKList.Add(new BaseClasses.PointsAddress(temporarySubrackData.SubrackNumber, temporarySubrackData.DeviceNumber, temporaryConnector[i4].Number));
                  }
                }
              }
            }
          }
        }
        return AllPointsASKList;
      }
      finally
      {
        AllPointsASKList = null;
        targetDataList = null;
        commutationDevices = null;
        temporaryConnectorList = null;
        temporaryConnector = null;
        temporarySubrackData = null;
      }
    }
    /// <summary>
    /// Проверяет данные каждой переходной панели (виртуальные данные на уникальность, проверяет, чтобы каждый разъём переходной панели был подключен только к одному разъёму какого-либо коммутационного устройства).
    /// </summary>
    /// <returns>True - во время првоерки переходных панелей были обнаружены проблемы, иначе - False.</returns>
    public static bool CheckAllSocketAdapterData(List<object> temporarySystemDevices = null)
    {
      if (temporarySystemDevices == null)
      {
        temporarySystemDevices = Current;
      }
      List<object> SocketAdapterList = temporarySystemDevices.FindAll(x => x is ISocketAdapterInterface);
      bool ContainVirtualData = false;
      bool ContainProblem = false;
      List<dynamic> temporaryVirtualDataConnectors;
      List<dynamic> AllVirtualDataConnectors = new List<dynamic>();
      IDeviceSubrackItemIndexedInterface a;
      IDeviceSubrackItemIndexedInterface b;
      StringBuilder ErrorMessage = new StringBuilder();
      try
      {
        UseVirtualAddressing = false;
        if (SocketAdapterList?.Count > 0)
        {
          for (int i = 0; i < SocketAdapterList.Count; i++)
          {
            temporaryVirtualDataConnectors = (SocketAdapterList[i] as ISocketAdapterInterface).GetVirtualDataConnectors();
            if (temporaryVirtualDataConnectors?.Count > 0)
            {
              ContainVirtualData = true;
              ContainProblem = !AllVirtualDataConnectors.TrueForAll(x => { a = (x as BaseClasses.DesignatedConnector).Tag as IDeviceSubrackItemIndexedInterface; return temporaryVirtualDataConnectors.TrueForAll(y => { b = (y as BaseClasses.DesignatedConnector).Tag as IDeviceSubrackItemIndexedInterface; return (a?.SubrackNumber != b?.SubrackNumber && a?.SubrackItemNumber != b?.SubrackItemNumber); }); });
              if (ContainProblem)
              {
                UseVirtualAddressing = false;
                ErrorMessage.AppendLine("Найдены повторяющиеся виртуальные адреса");
                return ContainProblem;
              }
            }
          }
        }
        if (ContainVirtualData) UseVirtualAddressing = true;
        if (!UseVirtualAddressing)
        {
          List<BaseClasses.PointsAddress> UsedPoints = GetAllCommutationPointsList();
          List<BaseClasses.DesignatedConnector> temporaryConnectorList;
          List<BaseClasses.PointsAddress> ClosedPointsList;
          BaseClasses.PointsAddress temporaryPointsAddress;
          for (int i = 0; i < SocketAdapterList.Count; i++)
          {
            temporaryConnectorList = (SocketAdapterList[i] as ISocketAdapterInterface).RealConnectors?.Cast<BaseClasses.DesignatedConnector>()?.ToList();
            for (int i2 = 0; i2 < temporaryConnectorList?.Count; i2++)
            {
              ClosedPointsList = temporaryConnectorList[i2].TagAddition as List<BaseClasses.PointsAddress>;
              for (int i3 = 0; i3 < ClosedPointsList?.Count; i3++)
              {
                temporaryPointsAddress = ClosedPointsList[i3];
                if (UsedPoints.Contains(temporaryPointsAddress)) UsedPoints.Remove(temporaryPointsAddress);
                else
                {
                  ContainProblem = true;
                  ErrorMessage.AppendLine($"Адрес точки {temporaryPointsAddress} уже был использован, повторое применение в переходной панели ID{(SocketAdapterList[i] as IDeviceIDGeneratorInterface).InternalID} в разъёме {temporaryConnectorList[i2].ToString()} недопустимо");
                }
              }
            }
          }
        }
        return ContainProblem;
      }
      finally
      {
        BaseClasses.MessageBoxClass message = new BaseClasses.MessageBoxClass();
        if (ContainProblem) message.ShowDialog("Конфигурация оборудования", ErrorMessage.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, "OK", "OK", "OK");
        SocketAdapterList = null;
        temporaryVirtualDataConnectors = null;
        AllVirtualDataConnectors = null;
      }
    }
    /// <summary>
    /// Производит поиск встроенных устройств, соответствующих заданному типу.
    /// </summary>
    /// <param name="searcedType">Тип искомых встроенных устройств.</param>
    /// <returns>Возвращает список устройств, соответствующих заданному типу.</returns>
    public static List<BaseClasses.PairBaseClass<dynamic, int>> GetAllDevicesByType(Type searcedType)
    {
      List<dynamic> integralDevices = new List<dynamic>();
      List<BaseClasses.PairBaseClass<dynamic, int>> returnedDevices = new List<BaseClasses.PairBaseClass<dynamic, int>>();
      int temporarySubrackNumber;
      for (int i = 0; i < CurrentSystemDevices?.Count; i++)
      {
        if (searcedType.IsClass && (CurrentSystemDevices[i].GetType() as Type).Equals(searcedType))
        {
          returnedDevices.Add(new BaseClasses.PairBaseClass<dynamic, int>(CurrentSystemDevices[i], 0));
        }
        else
        if (searcedType.IsInterface && (CurrentSystemDevices[i].GetType() as Type).GetInterfaces().Any(x => x.Equals(searcedType)))
        {
          returnedDevices.Add(new BaseClasses.PairBaseClass<dynamic, int>(CurrentSystemDevices[i], 0));
        }
        else
        if (CurrentSystemDevices[i] is ISubrackFrameInterface)
        {
          integralDevices = (CurrentSystemDevices[i] as ISubrackFrameInterface).GetIntegralDevices(searcedType);
          temporarySubrackNumber = (CurrentSystemDevices[i] as ISubrackFrameInterface).SubrackNumber;
          for (int i2 = 0; i2 < integralDevices?.Count; i2++)
          {
            returnedDevices.Add(new BaseClasses.PairBaseClass<dynamic, int>(integralDevices[i2], temporarySubrackNumber));
          }
        }
      }
      return returnedDevices;
    }
  }
}