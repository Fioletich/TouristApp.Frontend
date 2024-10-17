using TouristApp.Blazor.Models;

namespace TouristApp.Blazor.Services.Featureds;

public class ClientFeaturedService(HttpClient httpClient) : IFeaturedService {
    private readonly HttpClient _httpClient = httpClient;

    public Task<Featured[]> GetAll() {
        return _httpClient.GetFromJsonAsync<Featured[]>("Featured/GetAll");
    }

    public async Task<Featured> Get(Guid id) {
        return await _httpClient.GetFromJsonAsync<Featured>($"Featured/Get?Id={id}");
    }

    public async Task<Guid> Post(Featured featured) {
        return Guid.Parse((await (await _httpClient.PostAsync($"Featured/Post?" + 
                                                              $"TouristRouteId={featured.TouristRouteId}" +
                                                              $"UserId={featured.UserId}", null))
            .Content.ReadFromJsonAsync<string>()));
    }

    public async Task Delete(Guid id) {
        await _httpClient.DeleteAsync($"Featured/Delete?Id={id}");
    }
}