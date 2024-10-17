using TouristApp.Blazor.Models;

namespace TouristApp.Blazor.Services.Categories;

public interface ICategoryService {
    public Task<Category[]> GetAll();
    
    public Task<Category> Get(Guid id);
    
    public Task<Guid> Post(Category category);

    public Task Delete(Guid id);

    public Task Update(Category category);
}