using System.ComponentModel;

namespace Feedback.Analyzer.Domain.Extensions;

/// <summary>
/// Contains extension methods related to enums.
/// </summary>
public static class EnumExtensions
{
    // Extension method to get the display name of an enum value
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