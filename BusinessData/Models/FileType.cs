using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessData.Models
{
    /// <summary>
    /// 附件类型
    /// </summary>
    public partial class FileType
    {
        public ObservableCollection<FileType> Children { get; set; }



        public FileType(string name, params FileType[] children)
        {
            Children = new ObservableCollection<FileType>(children);
        }
    }
}
