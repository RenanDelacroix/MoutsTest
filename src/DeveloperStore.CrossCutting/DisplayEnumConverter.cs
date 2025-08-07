using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DeveloperStore.CrossCutting
{
    public class DisplayEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var name = reader.GetString();
            foreach (var field in typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var display = field.GetCustomAttribute<DisplayAttribute>();
                if ((display?.Name ?? field.Name) == name)
                {
                    return (T)field.GetValue(null)!;
                }
            }

            throw new JsonException($"Unable to convert \"{name}\" to Enum \"{typeof(T)}\"");
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            var field = typeof(T).GetField(value.ToString()!);
            var display = field?.GetCustomAttribute<DisplayAttribute>();
            writer.WriteStringValue(display?.Name ?? value.ToString());
        }
    }
}
