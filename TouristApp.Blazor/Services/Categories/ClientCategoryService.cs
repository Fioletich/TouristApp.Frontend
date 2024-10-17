using TouristApp.Blazor.Models;

namespace TouristApp.Blazor.Services.Categories;

public class ClientCategoryService(HttpClient httpClient) : ICategoryService {
    private readonly HttpClient _httpClient = httpClient;

    public async Task<Category[]> GetAll() {
        return await _httpClient.GetFromJsonAsync<Category[]>("Category/GetAll");
    }

    public async Task<Category> Get(Guid id) {
        return await _httpClient.GetFromJsonAsync<Category>($"Category/Get?Id={id}");
    }

    public async Task<Guid> Post(Category category) {
        return Guid.Parse((await (await _httpClient
            .PostAsync($"Category/Post?Name={category.Name}&Description={category.Description}", null
                )).Content.ReadFromJsonAsync<string>()));
    }

    public async Task Delete(Guid id) {
        await _httpClient.DeleteAsync($"Category/Delete?Id={id}");
    }
    
    public async Task Update(Category category) {
        await _httpClient.PutAsync($"Category/Put?" +
                                   $"Id={category.Id}" +
                                   $"&Name={category.Name}" +
                                   $"&Description={category.Description}", null);
    }
}