using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessData
{
    public partial class Project
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Project()
        {
            this.LogicalBuildings = new HashSet<LogicalBuilding>();
            this.Floors = new HashSet<Floor>();
            this.Households = new HashSet<Household>();
            this.Obligees = new HashSet<Obligee>();
        }
        /// <summary>
        /// 自然幢集合
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NaturalBuilding> NaturalBuildings { get; set; }
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
        /// <summary>
        /// 权利人集合
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Obligee> Obligees { get; set; }
    }
}
