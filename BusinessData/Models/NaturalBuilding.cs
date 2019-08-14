using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessData
{
    public partial class NaturalBuilding
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NaturalBuilding()
        {
            this.LogicalBuildings = new HashSet<LogicalBuilding>();
            this.Floors = new HashSet<Floor>();
            this.Households = new HashSet<Household>();

            // 初始下拉框数据
            FWJG = "1"; // 钢结构
            GHYT = "10"; // 住宅
            ZT = "1";  // 有效
        }

        public virtual Project Project { get; set; }

        /// <summary>
        /// 逻辑幢集合
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LogicalBuilding> LogicalBuildings { get; set; }
        /// <summary>
        /// 层集合
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Floor> Floors { get; set; }
        /// <summary>
        /// 户集合
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Household> Households { get; set; }
    }
}
