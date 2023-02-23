using System.Text.Json.Serialization;

namespace Entities.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Genre
{
    Comedy = 1,
    Action,
    Adventure,
    Horror,
    ScienceFiction,
    Anime
}
