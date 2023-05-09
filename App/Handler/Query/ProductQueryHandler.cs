using App.Request.Event;
using App.Request.Query;
using Info.Rep;
using MediatR;

namespace App.Handler.Query
{
    public class ProductQueryHandler : IRequestHandler<ProductQuery, List<AddProductCommandEvent>>
    {
        private readonly INoSqlRepository<AddProductCommandEvent> _noSqlRepository;

        public ProductQueryHandler(INoSqlRepository<AddProductCommandEvent> noSqlRepository)
        {
            _noSqlRepository = noSqlRepository;
        }


        public async Task<List<AddProductCommandEvent>> Handle(ProductQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_noSqlRepository.GetEntitiesAsync());
        }
    }
}