using TouristApp.Blazor.Models;

namespace TouristApp.Blazor.Services.TouristRoutes;

public interface ITouristRouteService {
    public Task<TouristRoute[]> GetAll();

    public Task<TouristRoute> Get(Guid id);

    public Task<Guid> Post(TouristRoute touristRoute);

    public Task Delete(Guid id);

    public Task Update(TouristRoute touristRoute);
}