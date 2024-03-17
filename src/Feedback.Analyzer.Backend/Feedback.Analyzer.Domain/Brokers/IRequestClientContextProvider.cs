namespace Feedback.Analyzer.Domain.Brokers;

/// <summary>
/// Defines request context data provider
/// </summary>
public interface IRequestContextProvider
{
    /// <summary>
    /// Gets current logged in user Id
    /// </summary>
    /// <returns>Id of logged in user for the request</returns>
    Guid GetUserId();

    /// <summary>
    /// Gets current user access token
    /// </summary>
    string? GetAccessToken();
}