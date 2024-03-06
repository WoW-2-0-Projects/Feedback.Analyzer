using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore.Query;

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
}