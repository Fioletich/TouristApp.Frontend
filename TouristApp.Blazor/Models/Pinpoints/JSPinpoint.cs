using System.Text.Json.Serialization;

namespace TouristApp.Blazor.Models.Pinpoints;

public class JSPinpoint {
    [JsonPropertyName("coords")]
    public decimal[] Coords { get; set; }
    [JsonPropertyName("info")]
    public string Info { get; set; }
    [JsonPropertyName("images")]
    public string[] Images { get; set; }
}