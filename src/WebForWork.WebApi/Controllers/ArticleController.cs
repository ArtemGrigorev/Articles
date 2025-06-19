using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using WebForWork.Application.Commands;
using WebForWork.WebApi.Configurations;
using WebForWork.WebApi.Models;

namespace WebForWork.WebApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly CreateArticleValidator _validatorModel;
        public ArticleController(IMediator mediator, IMapper mapper, CreateArticleValidator validatorModel)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validatorModel = validatorModel ?? throw new ArgumentNullException(nameof(validatorModel));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateResponseModel>> CreateArticleAsync([FromBody] CreateRequestModel createRequestModel, CancellationToken cancellationToken)
        {

            var validationResult = await _validatorModel.ValidateAsync(createRequestModel, cancellationToken);
            if (!validationResult.IsValid) 
            {
                validationResult.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }
            var commantTest = _mapper.Map<CreateArticleCommand>(createRequestModel);

            var command = new CreateArticleCommand() { Name = "Test", Tags = new Collection<string> { "test1", "test2" } };
            var result = await _mediator.Send(command, cancellationToken);

            return new CreateResponseModel();
        }
    }
}
