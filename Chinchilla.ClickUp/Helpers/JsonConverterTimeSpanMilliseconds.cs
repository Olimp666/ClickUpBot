using Newtonsoft.Json;
using System;

namespace Chinchilla.ClickUp.Helpers
{

    /// <summary>
    /// Json Converter class of TimeSpan object to / from milliseconds time
    /// </summary>
    public class JsonConverterTimeSpanMilliseconds : JsonConverter
    {

        /// <summary>
        /// Check if can convert this type of object. (Can only convert TimeSpan)
        /// </summary>
        /// <param name="objectType">object Type</param>
        /// <returns>Bool if can convert this object</returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(TimeSpan).IsAssignableFrom(objectType);
        }

        /// <summary>
        /// Read long with milliseconds and convert to TimeSpan
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (existingValue != null) return TimeSpan.FromMilliseconds(long.Parse((string)reader.Value));
            return null;
        }

        /// <summary>
        /// Get TimeSpan and serialize it to long for Unix method
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, ((TimeSpan)value).TotalMilliseconds);
        }

    }

}