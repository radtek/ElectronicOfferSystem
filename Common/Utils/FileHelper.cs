using System.IO;
using BusinessData.Dal;
using Common.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public class FileHelper
    {
        public static void DelFile(BusinessData.FileInfo fileInfo)
        {
            try
            {
                // 删除文件
                string path = ReadConfigInfo();
                FileInfo file = new FileInfo(path + "\\" + fileInfo.Path + "\\" + fileInfo.ID + fileInfo.Extension);
                if (file.Exists)
                {
                    file.Delete();
                }

                // 删除数据
                FileInfoDal fileInfoDal = new FileInfoDal();
                fileInfoDal.Del(fileInfo);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public static void DelDir(BusinessData.Project project)
        {
            try
            {
                // 删除目录
                string basePath = ReadConfigInfo();
                string path = basePath + "\\" + project.ID;
                DirectoryInfo di = new DirectoryInfo(path);
                if (di.Exists)
                {
                    Directory.Delete(path, true);
                }

                // 删除数据
                FileInfoDal fileInfoDal = new FileInfoDal();
                fileInfoDal.DelBy(t => t.ProjectID == project.ID);

            }
            catch (Exception)
            {

                throw;
            }

          

        }

        /// <summary>
        /// 读取本地配置信息
        /// </summary>
        private static string ReadConfigInfo()
        {
            string cfgINI = AppDomain.CurrentDomain.BaseDirectory + LocalConfiguration.INI_CFG;
            
            if (System.IO.File.Exists(cfgINI))
            {
                IniFileHelper ini = new IniFileHelper(cfgINI);
                 return ini.IniReadValue("Project", "FilePath");
            }
            return string.Empty;
        }
    }
}
