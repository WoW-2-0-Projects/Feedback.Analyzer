namespace Feedback.Analyzer.Domain.Brokers;

/// <summary>
/// Provides methods to retrieve client context information for the current request.
/// </summary>
public interface IRequestClientContextProvider
{
    /// <summary>
    /// Gets the unique identifier of the user associated with the current request.
    /// </summary>
    /// <returns>The unique identifier of the user.</returns>
    Guid GetUserId();

    /// <summary>
    /// Gets the access token associated with the current request, if available.
    /// </summary>
    /// <returns>The access token associated with the request, or <c>null</c> if not available.</returns>
    string? GetAccessToken();
}