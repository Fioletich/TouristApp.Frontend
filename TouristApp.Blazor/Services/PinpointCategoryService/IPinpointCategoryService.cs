using TouristApp.Blazor.Models;

namespace TouristApp.Blazor.Services.PinpointCategoryService;

public interface IPinpointCategoryService {
    public Task<PinpointCategory[]> GetAll();
    
    public Task<PinpointCategory> Get(Guid id);

    public Task<Guid> Post(PinpointCategory category);

    public Task Delete(Guid id);
}