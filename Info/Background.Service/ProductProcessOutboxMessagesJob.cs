using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;

namespace Info.Background.Service
{
    [DisallowConcurrentExecution]
    public class ProductProcessOutboxMessagesJob : IJob
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMediator _mediator;

        public ProductProcessOutboxMessagesJob(AppDbContext appDbContext, IMediator mediator)
        {
            _appDbContext = appDbContext;
            _mediator = mediator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var outboxMessages = await _appDbContext.OutboxMessages                                                   
                                                    .Where(w => w.ProcessdDate == null)
                                                    .Take(20)
                                                    .ToListAsync(context.CancellationToken);

            foreach (var outboxMessage in outboxMessages)
            {
                var @event = JsonConvert.DeserializeObject(outboxMessage.Content, Type.GetType(outboxMessage.Type));

                if (@event is null)
                    continue;

                outboxMessage.ProcessdDate = DateTime.UtcNow;

                await _mediator.Publish(@event, context.CancellationToken);
            }

            await _appDbContext.SaveChangesAsync(context.CancellationToken);
        }
    }
}
