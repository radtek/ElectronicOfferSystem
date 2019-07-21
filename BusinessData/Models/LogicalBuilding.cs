using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessData
{
    public partial class LogicalBuilding
    {
        public virtual Project Project { get; set; }
        public virtual NaturalBuilding NaturalBuilding { get; set; }
    }
}
