using Core.Persistance.Dynamic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistance.Paging.ExtensionsIoC
{
    public static class IQuerablePagniteEtensions
    {
        public static async Task<Pagnite<T>> ToPagniteAsync<T>(this IQueryable<T> source, int index, int size , CancellationToken cancellationToken = default)
        {
            int count = await source.CountAsync(cancellationToken).ConfigureAwait(false);  // tekli 

            List<T> items = await source.Skip(index * size).Take(size).ToListAsync(cancellationToken).ConfigureAwait(false); // list 

            Pagnite<T> list = new()
            {
                Index = index,
                Count = count,
                Items = items,
                Size = size,
                Pages = (int)Math.Ceiling(count / (double)size) // 0.1 0.2 0.3  

            };
            return list;
        }
           
    }
}
