using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;

namespace LLServer.Mappers;

public static class ReflectionMapper
{
    public static TOutput Map<TInput, TOutput>(TInput input, TOutput target)
    {
        Type inputType = typeof(TInput);
        Type targetType = typeof(TOutput);
        
        PropertyInfo[] inputProperties = inputType.GetProperties();
        PropertyInfo[] outputProperties = targetType.GetProperties();
        
        foreach (PropertyInfo inputProperty in inputProperties)
        {
            PropertyInfo? outputProperty = outputProperties.FirstOrDefault(x => x.Name == inputProperty.Name);
            if (outputProperty == null)
            {
                continue;
            }
            
            //ignore property if it has the [JsonIgnore] or [Key] attribute
            if (inputProperty.GetCustomAttribute<JsonIgnoreAttribute>() != null ||
                inputProperty.GetCustomAttribute<KeyAttribute>() != null)
            {
                continue;
            }

            object? inputValue = inputProperty.GetValue(input);
            if (inputValue == null)
            {
                continue;
            }
            outputProperty.SetValue(target, inputValue);
        }

        return target;
    }
}