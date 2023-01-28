using System.Text.Json;
using System.Text.Json.Serialization;
using ReversiApi.Enums;

namespace ReversiApi.Converter;

public class SpelBordConverter: JsonConverter<Kleur[,]>
{
    public override Kleur[,] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, Kleur[,] bord, JsonSerializerOptions options) {
        writer.WriteStartObject();
        for (int row = 0; row < 8; row++) {
            for (int col = 0; col < 8; col++) {
                Kleur kleur = bord[row, col];
                writer.WriteNumber(row + "," + col, kleur.GetHashCode());
            }
        }

        writer.WriteEndObject();
    }
}