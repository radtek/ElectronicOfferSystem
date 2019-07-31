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
            this.Mortgages = new HashSet<Mortgage>();
            this.Sequestrations = new HashSet<Sequestration>();
            this.Applicants = new HashSet<Applicant>();
            this.FileInfos = new HashSet<FileInfo>();
            this.Transfer = new Transfer();
        }

        /// <summary>
        /// 转移信息
        /// </summary>
        public virtual Transfer Transfer { get; set; }

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
        /// <summary>
        /// 抵押信息集合
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mortgage> Mortgages { get; set; }
        /// <summary>
        /// 查封信息集合
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sequestration> Sequestrations { get; set; }
        /// <summary>
        /// 申请人集合
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Applicant> Applicants { get; set; }
        /// <summary>
        /// 附件信息集合
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FileInfo> FileInfos { get; set; }
    }
}

