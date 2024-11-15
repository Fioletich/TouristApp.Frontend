using System.Globalization;
using Microsoft.AspNetCore.Components;
using TouristApp.Blazor.Models.Pinpoints;

namespace TouristApp.Blazor.Utils;

public static class Mapper {
    public static JSPinpoint MapJsPinpoint(Pinpoint pinpoint) {
        var jsPinpoint = new JSPinpoint()
        {
            Coords = new[] { Convert.ToDecimal(pinpoint.XCoordinate, CultureInfo.InvariantCulture), Convert.ToDecimal(pinpoint.YCoordinate, CultureInfo.InvariantCulture) },
            Info = $"{pinpoint.Name}: {pinpoint.Description}",
            Images = new[] { "" }
        };

        return jsPinpoint;
    }

    public static decimal ConvertStringToDecimal(string number) {
        decimal result = 0.0m;

        var parts = number.Split('.');

        if (parts.Length > 1)
        {
            result += Convert.ToDecimal(parts[0], CultureInfo.InvariantCulture);
            result += (Convert.ToDecimal(parts[1], CultureInfo.InvariantCulture) / (decimal)Math.Pow(10, parts[1].Length));
        }
        
        return result;
    }
    
    public static void ReloadPage(this NavigationManager manager)
    {
        manager.NavigateTo(manager.Uri, true);
    }

    public static JSPinpoint[] OrderByCoords(this JSPinpoint[] jsPinpoints) {
        var firstPoint = jsPinpoints[0];

        var ordered = new List<JSPinpoint>();
        ordered.Add(jsPinpoints[0]);

        var notOrdered = jsPinpoints.ToList();
        notOrdered.RemoveAt(0);

        var closestPointIndex = 0;
        var minDistance = decimal.MaxValue;

        for (int i = 0; i < ordered.Count; i++)
        {
            for (int j = 0; j < notOrdered.Count; j++)
            {
                var distance = SquareDistance(ordered[i], notOrdered[j]);

                if (distance != 0 && distance < minDistance)
                {
                    minDistance = distance;
                    closestPointIndex = j;
                }
            }

            if (notOrdered.Count == 0)
                continue;

            ordered.Add(notOrdered[closestPointIndex]);
            notOrdered.RemoveAt(closestPointIndex);
            minDistance = decimal.MaxValue;
        }

        return ordered.ToArray();
    }

    private static decimal SquareDistance(JSPinpoint point1, JSPinpoint point2) {
        return ((point1.Coords[0] - point2.Coords[0]) * (point1.Coords[0] - point2.Coords[0])) +
               ((point1.Coords[1] - point2.Coords[1]) * (point1.Coords[1] - point2.Coords[1]));
    }
    
   
}