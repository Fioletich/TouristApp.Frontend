using TouristApp.Blazor.Models;

namespace TouristApp.Blazor.Services.Featureds;

public interface IFeaturedService {
    public Task<Featured[]> GetAll();

    public Task<Featured> Get(Guid id);

    public Task<Guid> Post(Featured featured);

    public Task Delete(Guid id);
}