using System.Collections.Immutable;
using Feedback.Analyzer.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;

namespace Feedback.Analyzer.Persistence.Extensions;

public static class EfCoreExtensions
{
    public static SetPropertyCalls<T> MapToSetPropertyCalls<T>(
        this SetPropertyCalls<T> setPropertyCalls,
        IImmutableList<(Func<T, object> propertySelector, Func<T, object> valueSelector)> setPropertyExecutors
    )
    {
        foreach (var propertyExecutor in setPropertyExecutors)
            setPropertyCalls.SetProperty(propertyExecutor.propertySelector, propertyExecutor.valueSelector);

        return setPropertyCalls;
    }

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