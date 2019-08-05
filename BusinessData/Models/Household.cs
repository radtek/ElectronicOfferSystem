using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessData
{
    public partial class Household
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Household()
        {
            this.Obligees = new HashSet<Obligee>();

            // 初始下拉框
            MJDW = "1"; // 平方米
            //HXJG = "1"; // 平层
            //HX = "1"; // 一居室
            //FWYT1 = "10"; // 住宅
            //FWYT2 = "10"; // 住宅
            //FWYT3 = "10"; // 住宅
            //FWJG = "1"; // 钢结构
            //FWXZ = "0"; // 市场化商品房
            //FWLX = "1"; // 住宅
            //FWCB = "8100"; // 国有房产
            ZT = "1"; // 有效

        }
        /// <summary>
        /// 权利人
        /// </summary>
        public virtual Project Project { get; set; }
        /// <summary>
        /// 自然幢
        /// </summary>
        public virtual NaturalBuilding NaturalBuilding { get; set; }
        /// <summary>
        /// 权利人集合
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Obligee> Obligees { get; set; }
    }
}
