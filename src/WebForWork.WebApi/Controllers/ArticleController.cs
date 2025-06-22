using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using WebForWork.Application.Commands.CreateArticle;
using WebForWork.Application.Commands.GetArticle;
using WebForWork.Application.Commands.UpdateArticle;
using WebForWork.Domain.Events;
using WebForWork.WebApi.Configurations;
using WebForWork.WebApi.Configurations.ArticleValidator;
using WebForWork.WebApi.Models;
using WebForWork.WebApi.Models.Article;

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
        private readonly GetArticleValidator _validatorGetModel;
        public ArticleController(IMediator mediator, 
            IMapper mapper, 
            CreateArticleValidator validatorCreateModel,
            UpdateArticleValidator validatorUpdateModel,
            GetArticleValidator validatorGetModel)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validatorCreateModel = validatorCreateModel ?? throw new ArgumentNullException(nameof(validatorCreateModel));
            _validatorUpdateModel = validatorUpdateModel ?? throw new ArgumentNullException(nameof(validatorUpdateModel));
            _validatorGetModel = validatorGetModel ?? throw new ArgumentNullException(nameof(validatorGetModel));
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
        public async Task<ActionResult<UpdateResponseModel>> UpdateArticleAsync([FromBody] UpdateRequestModel updateRequestModel, CancellationToken cancellationToken)
        {

            var validationResult = await _validatorUpdateModel.ValidateAsync(updateRequestModel, cancellationToken);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }
            var command = _mapper.Map<UpdateArticleCommand>(updateRequestModel);
            await _mediator.Send(command, cancellationToken);

          //  var result = _mapper.Map<UpdateResponseModel>(resultCommand);
            return Ok(new UpdateResponseModel() {Message = "Статья обновлена" });
            /* if (string.IsNullOrEmpty(result.MessageError))
                 return Ok(result);

             return StatusCode(StatusCodes.Status500InternalServerError, result);//BadRequest(result);*/
        }


      
        [HttpGet]
        [Route("api/article/{id}")]
        [ProducesResponseType(typeof(GetResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetResponseModel>> GetArticleAsync(string id, CancellationToken cancellationToken)
        {
            var getRequestModel = new GetRequestModel() { Id = id };
            var validationResult = await _validatorGetModel.ValidateAsync(getRequestModel, cancellationToken);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }
            var command = _mapper.Map<GetArticleCommand>(getRequestModel);
            var resultCommand = await _mediator.Send(command, cancellationToken);
            var result = _mapper.Map<GetResponseModel>(resultCommand);

            if (string.IsNullOrEmpty(result.MessageError))
                 return Ok(result);

             return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }
}
