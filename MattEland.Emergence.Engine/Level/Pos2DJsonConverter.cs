using System;

namespace MattEland.Emergence.Engine.Level
{
    public class Pos2DJsonConverter : JsonConverter<Pos2D>
    {
        public override void WriteJson(JsonWriter writer, Pos2D value, JsonSerializer serializer)
        {
            writer.WriteValue(value.SerializedValue);
        }

        public override Pos2D ReadJson(JsonReader reader, Type objectType, Pos2D existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return Pos2D.FromString((string)reader.Value);
        }
    }    
}