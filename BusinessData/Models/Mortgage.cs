using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessData
{
    public partial class Mortgage
    {

        public virtual Project Project { get; set; }
        public virtual Household Household { get; set; }
    }
}
