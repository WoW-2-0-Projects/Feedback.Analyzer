using Feedback.Analyzer.Application.Common.PromptCategories.Commands;
using Feedback.Analyzer.Application.Common.PromptCategories.Queries;
using Feedback.Analyzer.Application.Common.Prompts.Commands;
using Feedback.Analyzer.Application.Common.Prompts.Events;
using Feedback.Analyzer.Application.Common.Prompts.Models;
using Feedback.Analyzer.Application.Common.Prompts.Queries;
using Feedback.Analyzer.Application.Common.PromptsHistory.Commands;
using Feedback.Analyzer.Application.Common.PromptsHistory.Models;
using Feedback.Analyzer.Application.Common.PromptsHistory.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.Analyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PromptsController(IMediator mediator) : ControllerBase
{
    #region Prompts

    [HttpGet]
    public async ValueTask<IActionResult> GetPrompts([FromQuery] PromptGetQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("{promptId:guid}")]
    public async ValueTask<IActionResult> GetPromptById([FromRoute] Guid promptId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(
            new PromptGetByIdQuery
            {
                PromptId = promptId
            },
            cancellationToken
        );
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreatePrompt([FromBody] PromptCreateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePrompt([FromBody] PromptUpdateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{promptId:guid}")]
    public async ValueTask<IActionResult> DeletePromptById([FromRoute] Guid promptId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(
            new PromptDeleteByIdCommand
            {
                PromptId = promptId
            },
            cancellationToken
        );
        return result ? Ok() : BadRequest();
    }

    #endregion

    #region Prompt categories
    
    [HttpGet("categories/{categoryId:guid}")]
    public async ValueTask<IActionResult> GetPromptCategories([FromRoute] Guid categoryId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new PromptCategoryGetByIdQuery
            {
                CategoryId = categoryId
            }
            , cancellationToken);
        return result is not null ? Ok(result) : NotFound();
    }


    [HttpGet("categories")]
    public async ValueTask<IActionResult> GetPromptCategories([FromQuery] PromptCategoryGetQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return result.Any() ? Ok(result) : NotFound();
    }

    #endregion

    #region Prompt Results

    [HttpGet("results/{categoryId:guid}")]
    public async ValueTask<IActionResult> GetPromptResultById([FromRoute] Guid categoryId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(
            new PromptResultGetByCategoryIdQuery
            {
                CategoryId = categoryId
            },
            cancellationToken
        );
        return result.Any() ? Ok(result) : NoContent();
    }
    
    [HttpPut("categories/{categoryId:guid}/selected/{promptId:guid}")]
    public async ValueTask<IActionResult> UpdateSelectedPrompt([FromRoute] Guid categoryId, Guid promptId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(
            new UpdateSelectedPromptCommand
            {
                CategoryId = categoryId,
                PromptId = promptId,
            },
            cancellationToken
        );
        return result ? Ok() : BadRequest();
    }

    #endregion

    [HttpGet("{promptId:guid}/histories")]
    public async ValueTask<IActionResult> GetPromptExecutionHistory([FromRoute] Guid promptId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new PromptsHistoryGetQuery
            {
                Filter = new PromptsHistoryFilter
                {
                    PromptId = promptId
                }
            },
            cancellationToken
        );
        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpPost("histories")]
    public async ValueTask<IActionResult> Create(
        [FromBody] PromptsHistoryCreateCommand promptsHistoryCreateCommand,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.Send(promptsHistoryCreateCommand, cancellationToken);
        return Ok(result);
    }
}