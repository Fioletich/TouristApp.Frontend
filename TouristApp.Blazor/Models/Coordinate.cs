namespace TouristApp.Blazor.Models;

public struct Coordinate
{
    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public double Accuracy { get; set; }
}