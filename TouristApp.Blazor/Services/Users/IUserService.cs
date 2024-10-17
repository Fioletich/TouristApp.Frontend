using TouristApp.Blazor.Models;

namespace TouristApp.Blazor.Services.Users;

public interface IUserService {
    public Task<User[]> GetAll();

    public Task<User> Get(Guid id);

    public Task<Guid> Post(User user);

    public Task Delete(Guid id);

    public Task Put(User user);
}