using TouristApp.Blazor.Models;

namespace TouristApp.Blazor.Services.PinpointCategoryService;

public class ClientPinpointCategoryService(HttpClient httpClient) : IPinpointCategoryService {
    private readonly HttpClient _httpClient = httpClient;

    public async Task<PinpointCategory[]> GetAll() {
        return await _httpClient.GetFromJsonAsync<PinpointCategory[]>("PinpointCategory/GetAll");
    }

    public async Task<PinpointCategory> Get(Guid id) {
        return await _httpClient.GetFromJsonAsync<PinpointCategory>($"PinpointCategory/Get?Id={id}");
    }

    public async Task<Guid> Post(PinpointCategory category) {
        return Guid.Parse((await (await _httpClient
                .PostAsync($"PinpointCategory/Post?" +
                           $"CategoryId={category.CategoryId}" +
                           $"&PinpointId={category.PinpointId}", null))
            .Content.ReadFromJsonAsync<string>()));
    }

    public async Task Delete(Guid id) {
        await _httpClient.DeleteAsync($"PinpointCategory/Delete?Id={id}");
    }
}