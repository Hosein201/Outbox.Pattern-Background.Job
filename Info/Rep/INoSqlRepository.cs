using Info.Model;
using Redis.OM;
using Redis.OM.Contracts;
using Redis.OM.Searching;
using System.Linq.Expressions;

namespace Info.Rep
{
    public interface INoSqlRepository<Entity>
        where Entity : class
    {
        Task<bool> AnyAsync(Expression<Func<Entity, bool>> expression);
        Task<int> CountAsync(Expression<Func<Entity, bool>> expression);
        Task DeleteAsync(Entity entity);
        Task<Entity> FindByIdAsync(string id);
        Task<IDictionary<string, Entity>> FindByIdsAsync(IEnumerable<string> ids);
        Task<Entity> FirstAsync(Expression<Func<Entity, bool>> expression);
        Task<Entity> FirstOrDefaultAsync(Expression<Func<Entity, bool>> expression);
        Task<List<Entity>> GetEntitiesAsync(Expression<Func<Entity, bool>> expression, Expression<Func<Entity, Entity>> selectItem);
        Task<List<Entity>> GetEntitiesAsync(Expression<Func<Entity, bool>> expression);
        List<Entity> GetEntitiesAsync();
        Task<List<string>> InsertAsync(IEnumerable<Entity> items);
        Task<string> InsertAsync(Entity entity);
        Task SaveAsync();
        Task<Entity> SingleAsync(Expression<Func<Entity, bool>> expression);
        Task<Entity> SingleOrDefaultAsync(Expression<Func<Entity, bool>> expression);
        Task UpdateAsync(Entity entity);
    }

    public class NoSqlRepository<Entity> : INoSqlRepository<Entity>
        where Entity : class
    {


        private readonly IRedisCollection<Entity> RedisCollection;

        public NoSqlRepository(RediSearchConfiguration rediSearchConfig)
        {
            var provider = new RedisConnectionProvider($"redis://{rediSearchConfig.Conn}");
           var redisConnection = provider.Connection;
            // redisConnection.DropIndex(typeof(Entity));
             redisConnection.CreateIndex(typeof(Entity));
            RedisCollection = provider.RedisCollection<Entity>();
            // CreateIndex();

            //  RedisConnection.Dispose(); 
        }

        public async Task<List<Entity>> GetEntitiesAsync(Expression<Func<Entity, bool>> expression, Expression<Func<Entity, Entity>> selectItem)
        {
            return (List<Entity>)await RedisCollection.Where(expression).Select(selectItem).ToListAsync();
        }
        
        public  List<Entity> GetEntitiesAsync()
        {
            var x =RedisCollection.ToList();
            return x;
        }

        public async Task<List<Entity>> GetEntitiesAsync(Expression<Func<Entity, bool>> expression)
        {
            return (List<Entity>)await RedisCollection.Where(expression).Select(s => s).ToListAsync();
        }

        public async Task<int> CountAsync(Expression<Func<Entity, bool>> expression)
        {
            return await RedisCollection.CountAsync(expression);
        }

        public async Task<bool> AnyAsync(Expression<Func<Entity, bool>> expression)
        {
            return await RedisCollection.AnyAsync(expression);
        }

        public async Task<Entity> FirstAsync(Expression<Func<Entity, bool>> expression)
        {
            return await RedisCollection.FirstAsync(expression);
        }

        public async Task<Entity> FirstOrDefaultAsync(Expression<Func<Entity, bool>> expression)
        {
            return await RedisCollection.FirstOrDefaultAsync(expression);
        }

        public async Task<Entity> SingleAsync(Expression<Func<Entity, bool>> expression)
        {
            return await RedisCollection.SingleAsync(expression);
        }

        public async Task<Entity> SingleOrDefaultAsync(Expression<Func<Entity, bool>> expression)
        {
            return await RedisCollection.SingleOrDefaultAsync(expression);
        }

        public async Task<IDictionary<string, Entity>> FindByIdsAsync(IEnumerable<string> ids)
        {
            return await RedisCollection.FindByIdsAsync(ids);
        }

        public async Task<Entity> FindByIdAsync(string id)
        {
            return await RedisCollection.FindByIdAsync(id);
        }

        public async Task<List<string>> InsertAsync(IEnumerable<Entity> items)
        {
            return await RedisCollection.InsertAsync(items);
        }

        public async Task<string> InsertAsync(Entity entity)
        {
            return await RedisCollection.InsertAsync(entity);
        }

        public async Task UpdateAsync(Entity entity)
        {
            await RedisCollection.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Entity entity)
        {
            await RedisCollection.DeleteAsync(entity);
        }

        public async Task SaveAsync()
        {
            await RedisCollection.SaveAsync();
        }
    }
}

