namespace Feedback.Analyzer.Domain.Brokers;

/// <summary>
/// Defines request context data provider
/// </summary>
public interface IRequestContextProvider
{
    /// <summary>
    /// Gets current logged in user Id if user is authenticated
    /// </summary>
    /// <returns>Id of user if user is authenticated, otherwise null</returns>
    Guid? GetUserId();
}