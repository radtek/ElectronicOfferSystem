using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessData
{
    public partial class FileInfo
    {
        /// <summary>
        /// 绝对路径
        /// </summary>
        public string FullPath { get; set; }
        /// <summary>
        /// 项目
        /// </summary>
        public virtual Project Project { get; set; }
    }
}
