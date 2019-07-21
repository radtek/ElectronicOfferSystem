using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessData
{
    public partial class Floor
    {
        /// <summary>
        /// 项目
        /// </summary>
        public virtual Project Project { get; set; }
        /// <summary>
        /// 自然幢
        /// </summary>
        public virtual NaturalBuilding NaturalBuilding { get; set; }
    }
}
