namespace TouristApp.Blazor.Models.Pinpoints;

public class StringPinpoint {
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? AudioUrl { get; set; }
    public string XCoordinate { get; set; }
    public string YCoordinate { get; set; }
}