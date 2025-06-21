using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using WebForWork.Application.Commands;
using WebForWork.Domain.Events;
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
            var commant = _mapper.Map<CreateArticleCommand>(createRequestModel);

           // var commandTets = new CreateArticleCommand() { Name = "Test", Tags = new Collection<string> { "test1", "test2" } };
            var resultCommand = await _mediator.Send(commant, cancellationToken);
            var result = _mapper.Map<CreateResponseModel>(resultCommand);
            if (string.IsNullOrEmpty(result.MessageError))
                return Ok(result);

            return StatusCode(StatusCodes.Status500InternalServerError,result);//BadRequest(result);
        }
    }
}
