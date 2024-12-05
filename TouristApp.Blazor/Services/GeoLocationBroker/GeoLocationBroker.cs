using Microsoft.JSInterop;
using TouristApp.Blazor.Exceptions;
using TouristApp.Blazor.Models;

namespace TouristApp.Blazor.Services.GeoLocationBroker;

public class GeoLocationBroker : IGeoLocationBroker
{
    public Coordinate Coordinates { get; private set; }
    
    private readonly IJSRuntime _jsRuntime;
    private readonly DotNetObjectReference<GeoLocationBroker> _dotNetObjectReference;

    public GeoLocationBroker(IJSRuntime jsRuntime)
    {
        this._jsRuntime = jsRuntime;

        _dotNetObjectReference = DotNetObjectReference.Create(this);
    }

    public async ValueTask RequestGeoLocationAsync(bool enableHighAccuracy, int maximumAgeInMilliseconds)
    {
        var dotNetObjectReference = this._dotNetObjectReference;

        await _jsRuntime.InvokeVoidAsync(identifier: "getCurrentPosition",
            dotNetObjectReference,
            enableHighAccuracy,
            maximumAgeInMilliseconds);
    }

    public async ValueTask RequestGeoLocationAsync()
    {
        await RequestGeoLocationAsync(enableHighAccuracy: true, maximumAgeInMilliseconds: 0);
    }

    public event Func<Coordinate, ValueTask> CoordinatesChanged = default!;

    public event Func<GeolocationPositionError, ValueTask> OnGeolocationPositionError = default!;

    [JSInvokable]
    public async Task OnSuccessAsync(Coordinate coordinates) {
        await CoordinatesChanged.Invoke(coordinates).AsTask();
    }

    [JSInvokable]
    public async Task OnErrorAsync(GeolocationPositionError error)
    {
        await OnGeolocationPositionError.Invoke(error);
    }
    
}