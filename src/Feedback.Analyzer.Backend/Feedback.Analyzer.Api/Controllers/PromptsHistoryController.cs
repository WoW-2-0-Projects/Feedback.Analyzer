using Feedback.Analyzer.Application.Common.PromptsHistory.Commands;
using Feedback.Analyzer.Application.Common.PromptsHistory.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Feedback.Analyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PromptsHistoryController(IMediator mediator) : ControllerBase
{

}