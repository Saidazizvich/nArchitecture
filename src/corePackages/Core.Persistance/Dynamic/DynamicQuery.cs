using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistance.Dynamic
{
    public class DynamicQuery
    {
        public IEnumerable<Sort>? Sorts { get; set; }
        public Filter Filter { get; set; }

        public DynamicQuery(IEnumerable<Sort>? sorts, Filter filter)
        {
            Sorts = sorts;
            Filter = filter;
        }
    }
}
