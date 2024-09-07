using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistance.Repositories
{
    public class Entity<TId>  : IEntityStampTime
    {
        public TId Id { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? UpdateeDate { get; set; }
        public DateTime? InsertDate { get; set; }

        public Entity()
        {
            Id = default;
        }

        public Entity(TId id)
        {
            Id = id;
        }
    }
}
