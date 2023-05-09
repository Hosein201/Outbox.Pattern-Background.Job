using App.Request.Event;
using MediatR;

namespace App.Request.Query
{
    public class ProductQuery : IRequest<List<AddProductCommandEvent>>
    {
    }
}