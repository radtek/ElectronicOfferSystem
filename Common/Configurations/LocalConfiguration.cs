using Common.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Configurations
{
    public class LocalConfiguration
    {
        /// <summary>
        /// 配置文件
        /// </summary>
        public const string INI_CFG = "config\\user.ini";

        /// <summary>
        /// 获取本地样式参数
        /// </summary>
        /// <returns></returns>
        public static string GetSkin()
        {
            string cfgINI = AppDomain.CurrentDomain.BaseDirectory + INI_CFG;
            if (File.Exists(cfgINI))
            {
                IniFileHelper ini = new IniFileHelper(cfgINI);
                string SkinName = ini.IniReadValue("Skin", "Skin");
                return SkinName;
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// 设置样式
        /// </summary>
        /// <param name="SkinName"></param>
        public static void SetKin(string SkinName)
        {
            string cfgINI = AppDomain.CurrentDomain.BaseDirectory + INI_CFG;
            IniFileHelper ini = new IniFileHelper(cfgINI);
            ini.IniWriteValue("Skin", "Skin", SkinName);
        }
    }
}
