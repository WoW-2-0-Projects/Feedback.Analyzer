using Feedback.Analyzer.Domain.Common.Queries;
using Feedback.Analyzer.Domain.Entities;

namespace Feedback.Analyzer.Application.Common.Identity.Queries;

/// <summary>
/// Represents get current user query
/// </summary>
public class GetCurrentUserQuery : IQuery<Client>;