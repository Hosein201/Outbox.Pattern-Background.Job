using App.Request.Query;
using MediatR;

namespace App.Handler.Query
{
    public class ProductQueryHandler : IRequestHandler<ProductQuery>
    {
        public Task Handle(ProductQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
