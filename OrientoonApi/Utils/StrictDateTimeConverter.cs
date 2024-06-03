using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;
using System.Globalization;
using System.Runtime.Serialization;

namespace OrientoonApi.Utils
{
    public class StrictDateTimeConverter : JsonConverter<DateTime>
    {
        private const string DateFormat = "dd/MM/yyyy HH:mm:ss";

        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String)
            {
                throw new JsonSerializationException("Invalid token type");
            }

            var dateString = (string)reader.Value;
            if (DateTime.TryParseExact(dateString, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                return date;
            }
            throw new JsonSerializationException($"A data deve estar no formato {DateFormat}");
        }

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(DateFormat, CultureInfo.InvariantCulture));
        }
    }
}
