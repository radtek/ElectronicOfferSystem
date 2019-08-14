using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Common.Utils
{
    public static class ConfigUtil
    {

        private static string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + @"AutoUpdater\AutoUpdater.exe.config";
        private static string ServerIP;


        public static string GetValue(string key)
        {
            if (System.IO.File.Exists(ConfigPath))
            {
                ExeConfigurationFileMap ecf = new ExeConfigurationFileMap();
                ecf.ExeConfigFilename = ConfigPath;
                Configuration config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(ecf, ConfigurationUserLevel.None);
                var keys = config.AppSettings.Settings.AllKeys.ToList();
                if (keys == null || keys.Count == 0)
                    return string.Empty;

                if (keys.Contains(key))
                {
                    return config.AppSettings.Settings[key].Value.ToString();
                }
                return string.Empty;
            }
            else
            {
                throw new Exception("配置文件不存在，请检查！");
                //MessageBox.Show("配置文件不存在，请检查！");
                //return string.Empty;
            }
        }


        public static void SaveConfig(string key, string value)
        {
            ExeConfigurationFileMap ecf = new ExeConfigurationFileMap();
            ecf.ExeConfigFilename = ConfigPath;
            Configuration config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(ecf, ConfigurationUserLevel.None);
            if (config.AppSettings.Settings[key] != null)
            {
                config.AppSettings.Settings[key].Value = value;
            }
            else
            {
                config.AppSettings.Settings.Add(key, value);
            }

            config.Save(ConfigurationSaveMode.Modified);
            //ConfigurationManager.RefreshSection("appSettings");//重新加载新的配置文件   
        }
        
    }



}
