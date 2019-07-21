using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessData
{
    public partial class Obligee
    {
        public Obligee()
        {
            ZJZL = "1"; // 身份证
            GJ = "142"; // 中华人民共和国
            XB = "1"; // 男性
            QLRLX = "1"; // 个人
            QLLX = "1"; // 集体土地所有权
            GYFS = "0"; // 单独所有
            FRZJLX = "1"; // 个人
            DLRZJLX = "1"; // 个人
        }
        public virtual Project Project { get; set; }
        public virtual Household Household { get; set; }
    }
}
