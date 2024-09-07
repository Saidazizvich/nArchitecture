
using Core.Persistance.Repositories;
using Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata;
using Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Persistance.EntityFremwork
{
    public class BrandRepository: EfRepositoryBase<Brand, Guid, BaseDbContext>
    {
        public BrandRepository(BaseDbContext context) : base(context)
        {
        }
    }

   
}
