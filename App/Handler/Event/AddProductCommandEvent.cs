using App.Request.Event;
using Info.Rep;
using MediatR;

namespace App.Handler.Event
{
    public class AddProductCommandEventHandler : INotificationHandler<AddProductCommandEvent>
    {
        private readonly INoSqlRepository<AddProductCommandEvent> _noSqlRepository;

        public AddProductCommandEventHandler(INoSqlRepository<AddProductCommandEvent> noSqlRepository)
        {
            _noSqlRepository = noSqlRepository;
        }

        public async Task Handle(AddProductCommandEvent notification, CancellationToken cancellationToken)
        {
            await _noSqlRepository.InsertAsync(notification);
            await _noSqlRepository.SaveAsync();
        }
    }
}