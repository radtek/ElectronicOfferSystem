using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessData
{
    public partial class Applicant
    {
        public Applicant()
        {
            XB = "1"; // 男性
            ZJLX = "1"; // 身份证
            GJDQ = "142"; // 中华人民共和国
            HJSZSS = "510000"; // 四川
            SSHY = "1"; // 交邮
            FRZJLX = "1"; // 身份证
            SQRLX = "1"; // 个人
            GYFS = "0"; // 单独所有
            SQRLB = "1"; // 权利人
            BDCDYLX = "1"; // 实测户
            SFCZR = "1"; // 是
        }

        /// <summary>
        /// 项目
        /// </summary>
        public virtual Project Project { get; set; }
    }
}
