using Feedback.Analyzer.Domain.Common.Query;

namespace Feedback.Analyzer.Persistence.Extensions;

/// <summary>
/// Provides extension methods for applying query specifications to IQueryable and IEnumerable collections.
/// </summary>
public static class LinqExtensions
{
    /// <summary>
    /// Applies pagination to an IQueryable data source.
    /// </summary>
    /// <typeparam name="TSource">The type of elements in the data source.</typeparam>
    /// <param name="sources">The IQueryable data source to paginate.</param>
    /// <param name="paginationOptions">Pagination options such as page token and page size.</param>
    /// <returns>An IQueryable representing the paginated subset of the original data source.</returns>
    public static IQueryable<TSource> ApplyPagination<TSource>(this IQueryable<TSource> sources,
        FilterPagination paginationOptions)
    {
        return sources.Skip((int)((paginationOptions.PageToken - 1) * paginationOptions.PageSize))
            .Take((int)paginationOptions.PageSize);
    }
    
    /// <summary>
    /// Asynchronously finds the first element in a sequence that satisfies a specified condition.
    /// </summary>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    /// <param name="source">The sequence to search.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the first element in the sequence that satisfies the condition, or a default value if no such element is found.
    /// </returns>
    public static async Task<T?> FirstOrDefaultAsync<T>(
        this IEnumerable<T> source,
        Func<T, Task<bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(predicate);

        if(cancellationToken.IsCancellationRequested) return default;
        
        foreach (var item in source)
            if (await predicate(item))
                return item;

        return default;
    }
}
