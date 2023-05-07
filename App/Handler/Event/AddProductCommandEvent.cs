using App.Request.Event;
using MediatR;

namespace App.Handler.Event
{
    public class AddProductCommandEventHandler : INotificationHandler<AddProductCommandEvent>
    {
        public Task Handle(AddProductCommandEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
