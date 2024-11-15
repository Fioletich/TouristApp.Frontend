namespace TouristApp.Blazor.Services.Routes;

public interface IRouteService {
    public Task<Models.Route[]> GetAll();

    public Task<Models.Route> Get(Guid id);

    public Task<Guid> Post(Models.Route route);

    public Task Delete(Guid id);

    public Task Update(Models.Route route);
}