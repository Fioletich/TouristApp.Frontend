using TouristApp.Blazor.Models.Pinpoints;

namespace TouristApp.Blazor.Services.Pinpoints;

public interface IPinpointService {
    public Task<Pinpoint[]> GetAll();

    public Task<Pinpoint> Get(Guid id);

    public Task<Guid> Post(StringPinpoint pinpoint);

    public Task Delete(Guid id);

    public Task Update(StringPinpoint pinpoint);
}