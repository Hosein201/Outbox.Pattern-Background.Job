using MediatR;

namespace App.Request.Command
{
    public class AddProductCommand : IRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
    }
}