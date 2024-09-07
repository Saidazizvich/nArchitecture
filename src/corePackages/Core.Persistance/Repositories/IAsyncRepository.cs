using Core.Persistance.Dynamic;
using Core.Persistance.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistance.Repositories
{
    public interface IAsyncRepository<TEntity,TId> : IQuery<TEntity> where TEntity : Entity<TId>
     {
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> perdicate,
       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, // join islemi
        bool withDelete = false, // bu yani soft delete silinmis olan larni geltirimi yo geltimeymi true olsa geltiracak false olsa geltirmiyacak
          bool enableTracking = true, // EntityEntry
           CancellationToken cancellationToken = default);

        Task<Pagnite<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                   Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                   int index = 0, int size = 10, bool enableTracking = true,
                                   bool withDelete = false,
                                   CancellationToken cancellationToken = default);

        Task<Pagnite<TEntity>> GetListAsyncDynamic(DynamicQuery dynamicQuary, Expression<Func<TEntity, bool>>? predicate = null, // kurtarici yontem
                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                 Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, // join connection
                                 int index = 0, int size = 10, bool enableTracking = true, bool withDelete = false,
                                 CancellationToken cancellationToken = default);


        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> perdicate = null, // bu esa varmi yoki diye bakiyor dikkat anyasync 
             bool withDelete = false, // bu yani soft delete silinmis olan larni geltirimi yo geltimeymi true olsa geltiracak false olsa geltirmiyacak
               bool enableTracking = true,
                CancellationToken cancellationToken = default);

        Task<TEntity> AddAsync(TEntity entities); // tek eklemek

        Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities); //addrange biza ekelemek imkonini verecek bir method bir dan fazla veri eklemek coklu eklemek

        Task<TEntity> UpdateAsync(TEntity entities);   // tek guncellemek

        Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities); //addrange biza ekelemek imkonini verecek bir method bir method bir dan fazla veri eklemek coklu guncellemek permant = kalici

        Task<TEntity> DeleteAsync(TEntity entities, bool parmant = false); // t tablo silindi tek silmek parment = kalici

        Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities, bool permanet = false); // soft dellete yani yumsak silmek  cokl silmek
                                                                                                           // sadece silinmis olan t tablosi gelecek dikkat 
    }
}
