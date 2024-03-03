using System.ComponentModel;
using AutoMapper;

namespace Feedback.Analyzer.Infrastructure.Common.Mappers;

public class EnumDescriptionConverter<TEnum> : IValueConverter<TEnum, string> where TEnum : struct, Enum 
{
    public string Convert(TEnum source, ResolutionContext context)
    {
        var fieldInfo = source.GetType().GetField(source.ToString());
        if (fieldInfo == null) return source.ToString();  // Fallback if not found

        var descriptionAttribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
        return descriptionAttribute?.Description ?? source.ToString(); 
    }
}