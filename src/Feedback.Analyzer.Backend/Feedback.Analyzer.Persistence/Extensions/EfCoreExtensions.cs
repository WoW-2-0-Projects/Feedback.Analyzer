using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Persistence.Extensions;

/// <summary>
/// Contains EF core internal logic extensions.
/// </summary>
public static class EfCoreExtensions
{
    public static IQueryable<TSource> ApplyTrackingMode<TSource>(this IQueryable<TSource> source, QueryTrackingMode trackingMode) where TSource : class
    {
        return trackingMode switch
        {
            QueryTrackingMode.AsTracking => source,
            QueryTrackingMode.AsNoTracking => source.AsNoTracking(),
            QueryTrackingMode.AsNoTrackingWithIdentityResolution => source.AsNoTrackingWithIdentityResolution(),
            _ => source
        };
    }
}