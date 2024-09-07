using Core.Persistance.Dynamic;
using Core.Persistance.Dynamic.ExtensionsIoC;
using Core.Persistance.Paging;
using Core.Persistance.Paging.ExtensionsIoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Core.Persistance.Repositories;

public class EfRepositoryBase<TEntity, TId, TContext> : IAsyncRepository<TEntity, TId> where TEntity : Entity<TId> where TContext : DbContext
{
    protected readonly TContext Context;

    public EfRepositoryBase(TContext context)
    {
        Context = context;
    }

    public IQueryable<TEntity> Query => Context.Set<TEntity>();

    public async Task<TEntity> AddAsync(TEntity entities)
    {
        entities.InsertDate = DateTime.UtcNow;
        Context.Entry(entities).State = EntityState.Added;
        await Context.SaveChangesAsync();
        return entities;
    }

    public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities)
    {
        foreach (var entity in entities) // list foreach
            entity.InsertDate = DateTime.Now;
        await Context.AddRangeAsync(entities);
        Context.SaveChangesAsync();
        return entities;
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> perdicate = null, bool withDelete = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = Query;
        if (!enableTracking) // true islemi dikkat
            query = query.AsNoTracking();
        //. AsNoTracking yöntemi, Entity Framework’ün takip (tracking) mekanizmasını devre dışı bırakır. Bu da sorgu performansını artırabilir ve bazı bellek tüketimi sorunlarını azaltabilir.
        if (withDelete)
            query = query.IgnoreQueryFilters();
        // Örneğin, silinmiş kayıtların görünmemesini sağlamak veya belirli bir kullanıcıya ait verileri filtrelemek için kullanılabilirler.
        if (perdicate != null)
            query = query.Where(perdicate);
        // burda esa sorgu islemi yapiyor 
        return await query.AnyAsync(cancellationToken);
    }

    public async Task<TEntity> DeleteAsync(TEntity entities, bool parmant = false)
    {
        //SetEntityAsDeleteAsync(entities, parmant);
        await Context.SaveChangesAsync();
        return entities;
    }

    public async Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities, bool permanet = false)
    {
        foreach (var item in entities)

            await SetEntityAsSoftDeleteAsync(item, permanet);

        await Context.SaveChangesAsync();
        return entities;
    }

  

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> perdicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool withDelete = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> entities = Query;
        if (!enableTracking)
            entities = entities.AsNoTracking(); // chang tracking bellekdan alib crud islemina hazirliyoruz dikkat 
        if (include != null)
            entities = include(entities); // inner join left join right join
        if (withDelete)
            entities = entities.IgnoreQueryFilters(); // global sorgu 
        return await entities.FirstOrDefaultAsync(perdicate, cancellationToken);
    }

    public async Task<Pagnite<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool enableTracking = true, bool withDelete = false, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> entities = Query;
        if (!enableTracking)
            entities = entities.AsNoTracking(); // chang tracking bellekdan alib crud islemina hazirliyoruz dikkat 
        if (include != null)
            entities = include(entities); // inner join left join right join
        if (withDelete)
            entities = entities.IgnoreQueryFilters(); // global sorgu 
        if (predicate != null)
            entities = entities.Where(predicate); // bu bool yani tablo olarak bakiyor varsa true yoksa false gelir
        if (orderBy != null)
            return await orderBy(entities).ToPagniteAsync(index, size, cancellationToken);// a z  // z a asc, desc 
        return await entities.ToPagniteAsync(index, size, cancellationToken); // sorgul islmi olacak
    }

    public async Task<Pagnite<TEntity>> GetListAsyncDynamic(DynamicQuery dynamicQuary, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool enableTracking = true, bool withDelete = false, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> entities = Query.ToDynamic(dynamicQuary);
        if (!enableTracking)
            entities = entities.AsNoTracking(); // chang tracking bellekdan alib crud islemina hazirliyoruz dikkat 
        if (include != null)
            entities = include(entities); // inner join left join right join
        if (withDelete)
            entities = entities.IgnoreQueryFilters(); // global sorgu 
        if (predicate != null)
            entities = entities.Where(predicate); // bu bool yani tablo olarak bakiyor varsa true yoksa false gelir
        if (orderBy != null)
            return await orderBy(entities).ToPagniteAsync(index, size, cancellationToken);// a z  // z a asc, desc 
        return await entities.ToPagniteAsync(index, size, cancellationToken); // sorgul islmi olacak
    }

    public async Task<TEntity> UpdateAsync(TEntity entities)
    {
        entities.UpdateeDate = DateTime.UtcNow;
        Context.Entry(entities).State = EntityState.Modified;
        Context.Update(entities);
        Context.SaveChanges();
        return entities;
    }

    public async Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities)
    {
        foreach (var entity in entities) // list foreach
            entity.InsertDate = DateTime.Now;
         Context.Update(entities);
        Context.SaveChangesAsync();
        return  entities;
    }

    protected async Task SetEntityAsSoftDeleteAsync(TEntity item, bool permanet)
    {
        if (!permanet) // kalici olmak durumu
        {
            CheckHasOneToOneRealtion(item); // BIRE BIR ENTITY SILMEK ISLEMIs kalici olsa 1
            await setEntityAsSoftDeleteAsync(item); // kalici olmasa 2
        }
        else
        {
             Context.Remove(item);  
        }
    }

    protected async Task setEntityAsSoftDeleteAsync(IEntityStampTime item)
    {
        if (item.DeleteDate.HasValue) return; item.DeleteDate = DateTime.UtcNow;
        //IsOnDependent  yazilmisdir

        var navigations = Context
           .Entry(item)
           .Metadata.GetNavigations().Where(x => x is { ForeignKey.DeleteBehavior: DeleteBehavior.ClientCascade or DeleteBehavior.Cascade });
        
           

        foreach (INavigation? navigation in navigations)
        {
            if (navigation.DeclaringEntityType.IsOwned()) //TargetEntityType yazilmisdir
                continue;
            if(navigation.PropertyInfo == null)
                 continue;
            
            object? navvalue = navigation.PropertyInfo.GetValue(item, null);
            if(navigation.IsCollection())
            {
                if(navvalue == null)
                {
                    IQueryable queryable = Context.Entry(item).Collection(navigation.PropertyInfo.Name).Query();
                    navvalue = await GetRelationLoaderQuery(queryable, navigationPropertType: navigation.PropertyInfo.GetType()).ToListAsync(); 
                    if (navvalue == null)
                        continue;
                }

                foreach (IEntityStampTime navValueItem in (IEnumerable)navvalue)
                    await setEntityAsSoftDeleteAsync(navValueItem);
            }
            else
            {
                if(navvalue == null)
                {
                    IQueryable queryable = Context.Entry(item).Reference(navigation.PropertyInfo.Name).Query();
                    navvalue = await GetRelationLoaderQuery(queryable, navigationPropertType: navigation.PropertyInfo.GetType()).FirstOrDefaultAsync();
                    if (navvalue == null)
                        continue;
                }

                await setEntityAsSoftDeleteAsync((IEntityStampTime)navvalue);   
            }
        }

        Context.Update(item);
    }

    protected IQueryable<object> GetRelationLoaderQuery(IQueryable queryable, Type navigationPropertType)
    {
        Type queryProviderType  = queryable.Provider.GetType();
        MethodInfo createQueryMethod = queryProviderType.GetMethods().First(m => m is { Name: nameof(queryable.Provider.CreateQuery), IsGenericMethod: true })
            ?.MakeGenericMethod(navigationPropertType) ?? throw new InvalidOperationException("CreateQuery<TElement> method is not found in IQureyableProvider");

        var queryProviderQuery = (IQueryable<object>)createQueryMethod.Invoke(queryable.Provider, new object[] { queryable.Expression })!;
        return queryProviderQuery.Where(x => !((IEntityStampTime)x).DeleteDate.HasValue);
    }

    protected void CheckHasOneToOneRealtion(TEntity item)
    {
        bool hasOneToOneRealtion = Context.Entry(item).Metadata.GetForeignKeys().All(                                  // burda esa one to one olursa yakaliyor 
            x => x.DependentToPrincipal?.IsCollection() == true || x.PrincipalToDependent?.IsCollection() == true || x.DependentToPrincipal?.ForeignKey.DeclaringEntityType.ClrType == item.GetType()
        ) == false;

        if (hasOneToOneRealtion)
            throw new InvalidOperationException(
            "Entity One to One realtionship.Soft delete Causes problems if you create entry again by same foreign"
            );
        
    }

    protected async Task SetEntityDeleteAsync(IEnumerable<TEntity> entities, bool permanet)
    {
        foreach(TEntity entity in entities)
            await SetEntityDeleteAsync(entities, permanet);
    }
}










