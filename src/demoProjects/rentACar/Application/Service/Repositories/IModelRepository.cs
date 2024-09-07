using Core.Persistance.Repositories;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Repositories
{
    public interface IModelRepository : IAsyncRepository<Model,Guid>
    {
    }
}
