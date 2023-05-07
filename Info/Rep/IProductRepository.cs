using Data;

namespace Info.Rep
{
    public interface IProductRepository
    {
        Task AddAsync(Product entity, bool isSave, CancellationToken cancellationToken);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Product entity, bool isSave, CancellationToken cancellationToken)
        {
            await _appDbContext.AddAsync(entity, cancellationToken);
            if (isSave)
                await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
