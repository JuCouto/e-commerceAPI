using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace APIVenda.Net.Validations
{
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private const string Format = "dd/MM/yyyy";
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.GetString().Equals(""))
                return DateTime.Parse("01/01/0001");

            if (DateTime.TryParseExact(reader.GetString(),Format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var date))

                return date;

            throw new JsonException("A data deve estar no seguinte formato: dd/MM/yyyy Ex: 12/09/2022");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {

            writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
        }
        
    }

}
