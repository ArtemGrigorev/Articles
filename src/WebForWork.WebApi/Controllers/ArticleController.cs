using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using WebForWork.Application.Commands;
using WebForWork.WebApi.Models;

namespace WebForWork.WebApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ArticleController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateResponseModel>> CreateArticleAsync([FromBody] CreateRequestModel createRequestModel, CancellationToken cancellationToken)
        {
            var command = new CreateArticleCommand() { Name = "Test", Tags = new Collection<string> { "test1", "test2" } };
            var result = await _mediator.Send(command, cancellationToken);

            return new CreateResponseModel();
        }
    }
}
