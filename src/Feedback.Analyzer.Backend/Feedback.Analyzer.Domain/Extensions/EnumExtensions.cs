using System.ComponentModel;

namespace Feedback.Analyzer.Domain.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum value)
    {
        var type = value.GetType();
        var name = Enum.GetName(type, value);
        if (name == null)
            return string.Empty;
        
        var field = type.GetField(name);
        if (field == null)
            return string.Empty;
        
        var attr = field.GetCustomAttributes(false).OfType<DescriptionAttribute>().FirstOrDefault();
        return attr?.Description ?? string.Empty;
    }
}