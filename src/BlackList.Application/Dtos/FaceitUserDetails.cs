using System.Text.Json.Serialization;

namespace BlackList.Application.Dtos;

public class FaceitUserDetails
{
    [JsonPropertyName("player_id")]
    public Guid PlayerId => default;
}