using SD_Restaurant.Core.Entities;

namespace SD_Restaurant.Core.Repositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role?> GetByNameAsync(string name);
        Task<bool> NameExistsAsync(string name);
    }
} 