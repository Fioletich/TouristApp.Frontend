using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Primitives;
using TouristApp.Blazor.Models;
using TouristApp.Blazor.Models.Pinpoints;

namespace TouristApp.Blazor.Utils;

public static class Mapper {
    public static JSPinpoint MapJsPinpoint(Pinpoint pinpoint) {
        var jsPinpoint = new JSPinpoint()
        {
            Coords = new[] { Convert.ToDecimal(pinpoint.XCoordinate), Convert.ToDecimal(pinpoint.YCoordinate) },
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
            result += Convert.ToDecimal(parts[0]);
            result += (Convert.ToDecimal(parts[1]) / (decimal)Math.Pow(10, parts[1].Length));
        }
        
        return result;
    }
    
    public static void ReloadPage(this NavigationManager manager)
    {
        manager.NavigateTo(manager.Uri, true);
    }
}