using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    [Serializable]
    public class Business<T> where T : class, new()
    {
        public string Name { get; set; }
        public T BusinessObject { get; set; }

    }
}
