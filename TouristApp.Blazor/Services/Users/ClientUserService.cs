using TouristApp.Blazor.Models;

namespace TouristApp.Blazor.Services.Users;

public class ClientUserService(HttpClient httpClient) : IUserService {
    private readonly HttpClient _httpClient = httpClient;

    public async Task<User[]> GetAll() {
        return await _httpClient.GetFromJsonAsync<User[]>("User/GetAll");
    }

    public async Task<User> Get(Guid id) {
        return await _httpClient.GetFromJsonAsync<User>($"User/Get?Id={id}");
    }

    public async Task<Guid> Post(User user) {
        return Guid.Parse((await (await _httpClient.PostAsync($"User/Post?" +
                                                              $"Login={user.Login}" +
                                                              $"&Password={user.Password}" +
                                                              $"$PhoneNumber={user.PhoneNumber}" +
                                                              $"&Bio={user.Bio}" +
                                                              $"&Country={user.Country}" +
                                                              $"&City={user.City}", null))
            .Content.ReadFromJsonAsync<string>()));
    }

    public async Task Delete(Guid id) {
        await _httpClient.DeleteAsync($"User/Delete?Id={id}");
    }

    public async Task Put(User user) {
        await _httpClient.PutAsync($"User/Post?" +
                                   $"Id={user.Id}" +
                                   $"&Login={user.Login}" +
                                   $"&Password={user.Password}" +
                                   $"$PhoneNumber={user.PhoneNumber}" +
                                   $"&Bio={user.Bio}" +
                                   $"&Country={user.Country}" +
                                   $"&City={user.City}", null);
    }
}