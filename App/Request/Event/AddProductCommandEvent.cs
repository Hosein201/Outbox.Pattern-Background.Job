using MediatR;
using Redis.OM.Modeling;

namespace App.Request.Event
{
    [Document(StorageType = StorageType.Json,IndexName = "product-idx")]
    public class AddProductCommandEvent : INotification
    {
        [Searchable]
        public string Name { get; set; }
        [Searchable]
        public string Description { get; set; }
        [Indexed(Sortable = true)]
        public DateTime CreateTime { get; set; }
    }
}