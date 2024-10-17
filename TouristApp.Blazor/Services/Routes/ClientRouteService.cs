namespace TouristApp.Blazor.Services.Routes;

public class ClientRouteService(HttpClient httpClient) : IRouteService {
    private readonly HttpClient _httpClient = httpClient;

    public async Task<Models.Route[]> GetAll() {
        return await _httpClient.GetFromJsonAsync<Models.Route[]>("Route/GetAll");
    }

    public async Task<Models.Route> Get(Guid id) {
        return await _httpClient.GetFromJsonAsync<Models.Route>($"Route/Get?id={id}");
    }

    public async Task<Guid> Post(Models.Route route) {
        return Guid.Parse((await (await _httpClient
            .PostAsync($"Route/Post?Name={route.Name}&Description={route.Description}", null
            )).Content.ReadFromJsonAsync<string>()));
    }

    public async Task Delete(Guid id) {
        await _httpClient.DeleteAsync($"Route/Delete?Id={id}");
    }

    public async Task Update(Models.Route route) {
        await _httpClient.PutAsync($"Route/Put?" +
                                   $"Id={route.Id}" +
                                   $"&Name={route.Name}" +
                                   $"&Description={route.Description}", null);
    }
}