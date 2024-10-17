using TouristApp.Blazor.Models;
using TouristApp.Blazor.Models.Pinpoints;
using TouristApp.Blazor.Utils;

namespace TouristApp.Blazor.Services.Pinpoints;

public class ClientPinpointService(HttpClient httpClient) : IPinpointService {
    private readonly HttpClient _httpClient = httpClient;

    public async Task<Pinpoint[]> GetAll() {
        return await _httpClient.GetFromJsonAsync<Pinpoint[]>("Pinpoint/GetAll");
    }

    public async Task<Pinpoint> Get(Guid id) {
        return await _httpClient.GetFromJsonAsync<Pinpoint>($"Pinpoint/Get?Id={id}");
    }

    public async Task<Guid> Post(StringPinpoint pinpoint) {
        return Guid.Parse((await (await _httpClient
            .PostAsync($"Pinpoint/Post?" + 
                       $"&Name={pinpoint.Name}" +
                       $"&Description={pinpoint.Description}" +
                       $"&XCoordinate={pinpoint.XCoordinate}" + 
                       $"&YCoordinate={pinpoint.YCoordinate}"
                , null
            )).Content.ReadFromJsonAsync<string>()));
    }

    public async Task Delete(Guid id) {
        await _httpClient.DeleteAsync($"Pinpoint/Delete?Id={id}");
    }

    public async Task Update(StringPinpoint pinpoint) {
        await _httpClient
            .PutAsync($"Pinpoint/Put?" +
                      $"Id={pinpoint.Id}" +
                      $"&Name={pinpoint.Name}" +
                      $"&Description={pinpoint.Description}" +
                      $"&XCoordinate={pinpoint.XCoordinate}" +
                      $"&YCoordinate={pinpoint.YCoordinate}"
                , null
            );
    }
}