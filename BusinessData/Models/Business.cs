using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessData.Models
{
    public class Business
    {
        public string Name { get; set; }
        public NaturalBuilding NaturalBuilding { get; set; }
        public LogicalBuilding LogicalBuilding { get; set; }
        public Floor Floor { get; set; }
        public Household Household { get; set; }
        public Obligee Obligee { get; set; }

    }
}
