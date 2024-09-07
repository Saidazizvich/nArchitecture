using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistance.Dynamic
{
    public class Sort
    {
        public string Field { get; set; }
        public string Asc_Desc { get; set; }

        public Sort() 
        {
            Field =string.Empty;    
            Asc_Desc = string.Empty;    
        }

        public Sort(string field, string asc_Desc)
        {
            Field=field;    
            Asc_Desc = asc_Desc;
        }
    }
}
