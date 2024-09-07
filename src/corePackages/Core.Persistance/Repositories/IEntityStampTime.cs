using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistance.Repositories
{
    public interface IEntityStampTime
    {
        DateTime? DeleteDate { get; set; }  
        DateTime? UpdateeDate { get; set; }  
        DateTime? InsertDate { get; set; }  

    }
}
