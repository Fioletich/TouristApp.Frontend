using TouristApp.Blazor.Models;

namespace TouristApp.Blazor.Services.TouristRoutes;

public class ClientTouristRouteService(HttpClient httpClient) : ITouristRouteService {
    private readonly HttpClient _httpClient = httpClient;

    public async Task<TouristRoute[]> GetAll() {
        return await _httpClient.GetFromJsonAsync<TouristRoute[]>("TouristRoute/GetAll");
    }

    public async Task<TouristRoute> Get(Guid id) {
        return await _httpClient.GetFromJsonAsync<TouristRoute>($"TouristRoute/Get?Id={id}");
    }

    public async Task<Guid> Post(TouristRoute touristRoute) {
        return Guid.Parse((await (await _httpClient
            .PostAsync($"TouristRoute/Post?" +
                       $"RouteId={touristRoute.RouteId}" +
                       $"&PinpointId={touristRoute.PinpointId}", null))
            .Content.ReadFromJsonAsync<string>()));
    }

    public async Task Delete(Guid id) {
        await _httpClient.DeleteAsync($"TouristRoute/Delete?Id={id}");
    }

    public async Task Update(TouristRoute touristRoute) {
        await _httpClient
            .PostAsync($"TouristRoute/Post?" + 
                       $"Id={touristRoute.Id}" +
                       $"RouteId={touristRoute.RouteId}" +
                       $"PinpointId={touristRoute.PinpointId}", null);
    }
}