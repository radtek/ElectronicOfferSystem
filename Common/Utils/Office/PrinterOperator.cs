using System;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace Common.Utils.Office
{
    /// <summary>
    /// 打印机类
    /// </summary>
    public class PrinterOperator
    {
        [System.Runtime.InteropServices.DllImport("winspool.drv", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        private static extern long SetDefaultPrinter(string printerName);//设置默认打印机

        private static PrintDocument printDocument = new PrintDocument();//打印文档

        /// <summary>  
        /// 获取本机默认打印机名称  
        /// </summary>  
        public static String DefaultPrinter
        {
            get { return printDocument.PrinterSettings.PrinterName; }
        }

        /// <summary>  
        /// 获取本机的打印机列表。列表中的第一项就是默认打印机。  
        /// </summary>  
        public static List<String> GetLocalPrinters()
        {
            List<String> printers = new List<string>();
            printers.Add(DefaultPrinter); // 默认打印机始终出现在列表的第一项  
            foreach (String printerName in PrinterSettings.InstalledPrinters)
            {
                if (printers.Contains(printerName))
                {
                    continue;
                }
                printers.Add(printerName);
            }
            return printers;
        }

        /// <summary>
        /// 设置默认打印机
        /// </summary>
        public static void SetLocalDefaultPrinter(string printerName)
        {
            SetDefaultPrinter(printerName);
        }
    }
}
