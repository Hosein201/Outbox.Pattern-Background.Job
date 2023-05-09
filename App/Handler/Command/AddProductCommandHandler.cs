using App.Request.Command;
using App.Request.Event;
using Info.Rep;
using MediatR;
using Newtonsoft.Json;

namespace App.Handler.Command
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public AddProductCommandHandler(IProductRepository productRepository, IOutboxMessageRepository outboxMessageRepository)
        {
            _productRepository = productRepository;
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task<Unit> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Data.Product
            {
                CreateTime = request.CreateTime,
                Description = request.Description,
                Name = request.Name
            };

            await _productRepository.AddAsync(product, false, cancellationToken);

            await _outboxMessageRepository.AddAsync(new Data.OutboxMessage
            {
                Content = JsonConvert.SerializeObject(new AddProductCommandEvent
                {
                    CreateTime = product.CreateTime,
                    Description = product.Description,
                    Name = product.Name
                }),
                CreateTime = DateTime.UtcNow,
                ProcessdDate = null,
                Type = typeof(AddProductCommandEvent).FullName + "," + typeof(AddProductCommandEvent).Assembly.GetName().Name

            }, true, cancellationToken);

            return Unit.Value;
        }
    }
}