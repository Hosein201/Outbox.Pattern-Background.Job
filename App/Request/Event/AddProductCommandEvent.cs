using MediatR;

namespace App.Request.Event
{
    public class AddProductCommandEvent : INotification
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
    }
}