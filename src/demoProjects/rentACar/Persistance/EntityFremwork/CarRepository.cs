using Core.Persistance.Repositories;
using Domain.Entity;
using Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.EntityFremwork
{
    public class CarRepository : EfRepositoryBase<Car, Guid, BaseDbContext>
    {
        public CarRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
