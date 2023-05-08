using App.Request.Command;
using App.Request.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OutboxPattern.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post([FromBody] AddProductCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            await _mediator.Send(new ProductQuery(), cancellationToken);
            return Ok();
        }
    }
}