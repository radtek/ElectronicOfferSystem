using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessData
{
    public partial class Mortgage
    {
        public Mortgage()
        {
            ZJLX = "1"; // 身份证
            DYFS = "1"; // 一般抵押
            FRZJLX = "1";
            DYRZJLX = "1";
            DYBDCLX = "1"; // 土地
            CZFS = "01"; // 共同持证
        }
        public virtual Project Project { get; set; }
        public virtual Household Household { get; set; }
    }
}
