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
        private readonly CreateArticleValidator _validatorCreateModel;
        private readonly UpdateArticleValidator _validatorUpdateModel;
        public ArticleController(IMediator mediator, 
            IMapper mapper, 
            CreateArticleValidator validatorCreateModel,
            UpdateArticleValidator validatorUpdateModel)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validatorCreateModel = validatorCreateModel ?? throw new ArgumentNullException(nameof(validatorCreateModel));
            _validatorUpdateModel = validatorUpdateModel ?? throw new ArgumentNullException(nameof(validatorUpdateModel));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateResponseModel>> CreateArticleAsync([FromBody] CreateRequestModel createRequestModel, CancellationToken cancellationToken)
        {

            var validationResult = await _validatorCreateModel.ValidateAsync(createRequestModel, cancellationToken);
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

        [HttpPut]
        [ProducesResponseType(typeof(UpdateResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateResponseModel>> CreateArticleAsync([FromBody] UpdateRequestModel updateRequestModel, CancellationToken cancellationToken)
        {

            var validationResult = await _validatorUpdateModel.ValidateAsync(updateRequestModel, cancellationToken);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }
             var command = _mapper.Map<UpdateArticleCommand>(updateRequestModel);

          /*  var commandTests = new UpdateArticleCommand() {
                Id = Guid.Parse("3054f219-758f-4366-8a06-b7345a58ad93"),
                Name = "Test",
                Tags = new Collection<string> { "test2", "test1", "test3" }
            };
          */
            await _mediator.Send(command, cancellationToken);

          //  var result = _mapper.Map<UpdateResponseModel>(resultCommand);
            return Ok(new UpdateResponseModel() {Message = "Статья обновленна" });
            /* if (string.IsNullOrEmpty(result.MessageError))
                 return Ok(result);

             return StatusCode(StatusCodes.Status500InternalServerError, result);//BadRequest(result);*/
        }


        [HttpGet]
        [ProducesResponseType(typeof(UpdateResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateResponseModel>> CreateArticleAsync([FromBody] UpdateRequestModel updateRequestModel, CancellationToken cancellationToken)
        {

            var validationResult = await _validatorUpdateModel.ValidateAsync(updateRequestModel, cancellationToken);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }
            var command = _mapper.Map<UpdateArticleCommand>(updateRequestModel);

            /*  var commandTests = new UpdateArticleCommand() {
                  Id = Guid.Parse("3054f219-758f-4366-8a06-b7345a58ad93"),
                  Name = "Test",
                  Tags = new Collection<string> { "test2", "test1", "test3" }
              };
            */
            await _mediator.Send(command, cancellationToken);

            //  var result = _mapper.Map<UpdateResponseModel>(resultCommand);
            return Ok(new UpdateResponseModel() { Message = "Статья обновленна" });
            /* if (string.IsNullOrEmpty(result.MessageError))
                 return Ok(result);

             return StatusCode(StatusCodes.Status500InternalServerError, result);//BadRequest(result);*/
        }

    }
}
