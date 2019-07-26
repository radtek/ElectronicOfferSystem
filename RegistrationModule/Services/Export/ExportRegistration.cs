using BusinessData;
using Common.Models;
using Common.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationModule.Services.Export
{
    public class ExportRegistration
    {
        public string SaveFileName { get; set; }
        public List<string> ErrorMsg { get; set; }
        public Project Project { get; set; }
        public ExportRegistration()
        {
            ErrorMsg = new List<string>();
        }

        public void write()
        {
            try
            {
                // 封装信息
                RegistrationResult registrationResult = new RegistrationResult
                {
                    Xmmc = Project.ProjectName,
                    Kfsmc = Project.DeveloperName,
                    CaseNum = Project.Transfer.ContractNum,
                    Sqrs = Project.Applicants.ToList()
                };


                // 导出属性信息文件 
                RegistrationResultToFile(registrationResult);
            }
            catch (Exception ex)
            {
                ErrorMsg.Add("导出属性信息文件异常：" + ex.Message);
                return;
            }
            try
            {
                // 导出附件
                ExportFile();
            }
            catch (Exception ex)
            {
                ErrorMsg.Add("导出附件异常：" + ex.Message);
                return;
            }

            try
            {
                // 压缩成报盘文件
                ZipHelper zipHelper = new ZipHelper();
                zipHelper.ZipFileFromDirectory(SaveFileName, SaveFileName + ".bpf", 5);
                // 删除目录及其下所有文件
                DelectDir(SaveFileName);
            }
            catch (Exception ex)
            {
                ErrorMsg.Add("压缩文件异常：" + ex.Message);
                return;
            }

        }



        private void RegistrationResultToFile(RegistrationResult registrationResult)
        {
            // 转成json字符串
            JObject jObject = new JObject();
            String jsonString = JsonConvert.SerializeObject(registrationResult);
            // 将属性信息写入登记信息文件
            //String root = OutPath + "\\" + Xmmc;
            SaveFileName += "\\" + Project.ProjectName;
            if (!Directory.Exists(SaveFileName))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(SaveFileName);
                directoryInfo.Create();
            }
            FileStream fs = new FileStream(SaveFileName + "\\登记信息.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(jsonString);
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        /// <summary>
        /// 导出附件
        /// </summary>
        private void ExportFile()
        {
            // 获取附件保存位置基础路径
            string baseAddress = @"D:\vs-workspace\Test";
            string srcPath = baseAddress + "\\" + Project.ID;
            string aimPath = SaveFileName;
            // 开始复制
            CopyDir(srcPath, aimPath);

        }

        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="srcPath"></param>
        /// <param name="aimPath"></param>
        private void CopyDir(string srcPath, string aimPath)
        {
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加
                if (aimPath[aimPath.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                {
                    aimPath += System.IO.Path.DirectorySeparatorChar;
                }
                // 判断目标目录是否存在如果不存在则新建
                if (!System.IO.Directory.Exists(aimPath))
                {
                    System.IO.Directory.CreateDirectory(aimPath);
                }
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                // 如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
                // string[] fileList = Directory.GetFiles（srcPath）；
                string[] fileList = System.IO.Directory.GetFileSystemEntries(srcPath);
                // 遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                    if (System.IO.Directory.Exists(file))
                    {
                        CopyDir(file, aimPath + System.IO.Path.GetFileName(file));
                    }
                    // 否则直接Copy文件
                    else
                    {
                        System.IO.File.Copy(file, aimPath + System.IO.Path.GetFileName(file), true);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="srcPath"></param>
        public static void DelectDir(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
                Directory.Delete(srcPath);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
