using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistance.Paging
{
    public class Pagnite<T>
    {
        public Pagnite()
        {
            Items = Array.Empty<T>();   
        }

        public int Size { get; set; } // sayfada kac data boyut 
        public int Index { get; set; } // hangi sayfa 1 2 3 4 5 6 6
        public int Count { get; set; } // kayit sayisi 
        public int Pages { get; set; } // toplam kac sayfa var 

        public IList<T> Items { get; set; } // datamiz ne  --- 1 

        public bool HasPerviours => Index > 0; // oncaki sayfa 
        public bool HasNext => Index + 1 < Count;     // sonraki sayfa geciyor dikkat 
    }
}
