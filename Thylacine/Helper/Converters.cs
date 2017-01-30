using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Helper
{
    public class TimestampConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) { return objectType == typeof(DateTime); }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) return null;
            if (reader.Value is DateTime) return (DateTime)reader.Value;


            ulong seconds = 0;
            if (reader.Value is Int64)
                seconds = (ulong)(Int64)reader.Value;
            else
                seconds = ulong.Parse((string)reader.Value, NumberStyles.None, CultureInfo.InvariantCulture);
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(seconds);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            double time = (TimeZoneInfo.ConvertTimeToUtc((DateTime)value) - new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
            int seconds = (int) Math.Round(time);
            writer.WriteValue(time.ToString(CultureInfo.InvariantCulture));
        }
    }

    public class SnowflakeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) { return objectType == typeof(ulong); }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) return null;
            return ulong.Parse((string)reader.Value, NumberStyles.None, CultureInfo.InvariantCulture);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        { 
            writer.WriteValue(((ulong)value).ToString(CultureInfo.InvariantCulture));
        }
    }

    public class SnowflakeArrayConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(IEnumerable<ulong[]>);
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var result = new List<ulong>();
            if (reader.TokenType == JsonToken.StartArray)
            {
                reader.Read();
                while (reader.TokenType != JsonToken.EndArray)
                {
                    result.Add(ulong.Parse((string)reader.Value, CultureInfo.InvariantCulture));
                    reader.Read();
                }
            }
            return result.ToArray();
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
                writer.WriteNull();
            else
            {
                writer.WriteStartArray();
                var a = (ulong[])value;
                for (int i = 0; i < a.Length; i++)
                    writer.WriteValue(a[i].ToString(CultureInfo.InvariantCulture));
                writer.WriteEndArray();
            }
        }
    }
}
