using System;
using System.Configuration;
using System.IO;

namespace Common.Utils.Office
{
    /// <summary>
    /// Word操作
    /// </summary>
    public class WordOperator : WordBase
    {
        #region Ctor

        public WordOperator()
        {
        }

        #endregion

        #region Override

        /// <summary>
        /// 设置参数值
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected override bool OnSetParamValue(object data)
        {
            return true;
        }

        #endregion

        #region Static

        /// <summary>
        /// 初始化有效文件名
        /// </summary>
        /// <param name="fileName">文件名称</param>
        public static string InitalizeValideFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return "";
            }
            char[] path = System.IO.Path.GetInvalidPathChars();
            char[] name = System.IO.Path.GetInvalidFileNameChars();
            string filePath = fileName;
            try
            {
                for (int i = 0; i < path.Length; i++)
                {
                    filePath = filePath.Replace(path[i].ToString(), "");
                }
                string dirPath = System.IO.Path.GetDirectoryName(filePath) + "\\";
                string nameFile = filePath.Replace(dirPath, "");
                if (nameFile == filePath)
                {
                    int index = filePath.LastIndexOf("\\");
                    if (index > 0)
                    {
                        nameFile = filePath.Substring(index + 1);
                    }
                }
                for (int i = 0; i < name.Length; i++)
                {
                    nameFile = nameFile.Replace(name[i].ToString(), "");
                }
                path = null;
                name = null;
                return dirPath + nameFile;
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            return fileName;
        }

        /// <summary>
        /// 初始化文件目录
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        public static void InitalzieDirectory(string fileName)
        {
            string dir = System.IO.Path.GetDirectoryName(fileName);
            try
            {
                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 初始化默认目录
        /// </summary>
        /// <returns></returns>
        public static string InitalzieDefatultDirectory()
        {
            string filePath = WordOperator.SystemDefaultDirectory;
            if (string.IsNullOrEmpty(filePath) || !System.IO.Directory.Exists(filePath))
            {
                filePath = System.IO.Path.GetTempPath();
            }
            int index = filePath.LastIndexOf("\\");
            if ((index + 1) != filePath.Length)
            {
                filePath += "\\";
            }
            return filePath;
        }

        #endregion

        #region Setting

        public const string SYSTEMDEFAULTDIRECTORY = "SystemDefaultDirectory";//系统默认目录
        public const string SHEETCOLUMNAUTOFIT = "SheetColumnAutoFit";//列自适应

        /// <summary>
        /// 系统默认目录
        /// </summary>
        public static string SystemDefaultDirectory
        {
            get
            {
                string filePath = System.IO.Path.GetTempPath();
                string value = GetSpecialAppSettingValue(SYSTEMDEFAULTDIRECTORY, filePath);
                return value;
            }
            set
            {
                SetSpecialAppSettingValue(SYSTEMDEFAULTDIRECTORY, value);
            }
        }

        /// <summary>
        /// 列自适应
        /// </summary>
        public static bool SheetColumnAutoFit
        {
            get
            {
                string value = GetSpecialAppSettingValue(SHEETCOLUMNAUTOFIT, "false");
                bool autoFit = false;
                Boolean.TryParse(value, out autoFit);
                return autoFit;
            }
            set
            {
                SetSpecialAppSettingValue(SHEETCOLUMNAUTOFIT, value.ToString());
            }
        }

        /// <summary>
        /// 获取配置节值
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void SetSpecialAppSettingValue(string key, string value)
        {
            bool isExist = AppSettingIsExist(key);
            if (!isExist)
            {
                CreateAppSetting(key, value);
                return;
            }
            SetAppSettingValue(key, value);//设置配置节值
        }

        /// <summary>
        /// 获取特殊配置节值
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static string GetSpecialAppSettingValue(string key, string value)
        {
            bool isExist = AppSettingIsExist(key);
            if (isExist)
            {
                return GetAppSettingValue(key);
            }
            CreateAppSetting(key, value);
            return value;
        }

        /// <summary>
        /// 判断配置节是否存在
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        public static bool AppSettingIsExist(string key)
        {
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings[key]))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 创建AppSetting
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="value">AppSetting值</param>
        public static void CreateAppSetting(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[key]))
            {
                return;
            }
            configuration.AppSettings.Settings.Add(key, value);
            RefreshAndSaveSection(configuration);
        }

        /// <summary>
        /// 获取配置节值
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        public static string GetAppSettingValue(string key)
        {
            bool isExist = AppSettingIsExist(key);
            if (!isExist)
            {
                return "";
            }
            string val = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(val))
            {
                return "";
            }
            val = val.Trim();
            if (val == ",")
            {
                return "";
            }
            return val;
        }

        /// <summary>
        /// 获取配置节值
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void SetAppSettingValue(string key, string value)
        {
            bool isExist = AppSettingIsExist(key);
            if (!isExist)
            {
                return;
            }
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            RefreshAndSaveSection(configuration);
        }

        /// <summary>
        /// 刷新并保存修改项
        /// </summary>
        /// <param name="config">配置对象</param>
        public static void RefreshAndSaveSection(Configuration config)
        {
            string configPath = config.FilePath;
            FileInfo fileInfo = new FileInfo(configPath);
            fileInfo.Attributes = FileAttributes.Normal;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        #endregion
    }
}
