using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebForWork.Application.Commands.GetChaptersCatalog;
using WebForWork.Application.Commands.GetChapterWithArticles;
using WebForWork.WebApi.Configurations;
using WebForWork.WebApi.Configurations.ChapterValidators;
using WebForWork.WebApi.Models.Chapter;

namespace WebForWork.WebApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class ChapterController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly GetArticlesInChapterValidator _validatorGetModel;
        private readonly GetCatalogChapterValidator _validatorCatalogModel;
        public ChapterController(IMediator mediator,
                                 IMapper mapper,
                                 GetArticlesInChapterValidator validatorGetModel,
                                 GetCatalogChapterValidator validatorCatalogModel)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validatorGetModel = validatorGetModel ?? throw new ArgumentNullException(nameof(validatorGetModel));
            _validatorCatalogModel = validatorCatalogModel ?? throw new ArgumentNullException(nameof(validatorCatalogModel));
        }
        
        [HttpGet]
        [Route("api/chapter_and_articles/{id}")]
        [ProducesResponseType(typeof(GetResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetResponseModel>> GetArticlesInChapterAsync(string id, CancellationToken cancellationToken)
        {
             var getRequestModel = new GetRequestModel() { Id = id };
             var validationResult = await _validatorGetModel.ValidateAsync(getRequestModel, cancellationToken);
             if (!validationResult.IsValid)
             {
                 validationResult.AddToModelState(ModelState);
                 return ValidationProblem(ModelState);
             }
             var command = _mapper.Map<GetArticlesInChapterCommand>(getRequestModel);
             var resultCommand = await _mediator.Send(command, cancellationToken);
             var result = _mapper.Map<GetResponseModel>(resultCommand);

             if (string.IsNullOrEmpty(result.MessageError))
                 return Ok(result);

             return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
        [HttpGet]
        [Route("api/catalog/{page}")]
        [ProducesResponseType(typeof(GetCatalogResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetCatalogResponseModel>> GetCatalogAsync(string page, CancellationToken cancellationToken)
        {
            var getCatalogRequestModel = new GetCatalogRequestModel() { Page = page };
            var validationResult = await _validatorCatalogModel.ValidateAsync(getCatalogRequestModel, cancellationToken);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return ValidationProblem(ModelState);
            }
           
            var command = _mapper.Map<GetChaptersCatalogCommand>(getCatalogRequestModel);
            var resultCommand = await _mediator.Send(command, cancellationToken);
            var result = _mapper.Map<GetCatalogResponseModel>(resultCommand);

            if (string.IsNullOrEmpty(result.MessageError))
                return Ok(result);

            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }
}
