using Data;

namespace Info.Rep
{
    public interface IOutboxMessageRepository
    {
        Task AddAsync(OutboxMessage entity, bool isSave, CancellationToken cancellationToken);
    }

    public class OutboxMessageRepository : IOutboxMessageRepository
    {
        private readonly AppDbContext _appDbContext;

        public OutboxMessageRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(OutboxMessage entity, bool isSave, CancellationToken cancellationToken)
        {
            await _appDbContext.AddAsync(entity, cancellationToken);
            if (isSave)
                await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
