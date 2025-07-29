using Microsoft.EntityFrameworkCore;
using SD_Restaurant.Core.Entities;
using SD_Restaurant.Core.Repositories;
using SD_Restaurant.Infrastructure.Data;

namespace SD_Restaurant.Infrastructure.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(RestaurantDbContext context) : base(context)
        {
        }

        public async Task<Role?> GetByNameAsync(string name)
        {
            return await _context.Roles
                .Include(r => r.UserRoles)
                .ThenInclude(ur => ur.User)
                .FirstOrDefaultAsync(r => r.Name == name);
        }

        public async Task<IEnumerable<Role>> GetRolesWithUsersAsync()
        {
            return await _context.Roles
                .Include(r => r.UserRoles)
                .ThenInclude(ur => ur.User)
                .ToListAsync();
        }

        public async Task<bool> IsNameUniqueAsync(string name, int? excludeRoleId = null)
        {
            return !await _context.Roles
                .AnyAsync(r => r.Name == name && (!excludeRoleId.HasValue || r.Id != excludeRoleId.Value));
        }

        public async Task<bool> NameExistsAsync(string name)
        {
            return await _context.Roles.AnyAsync(r => r.Name == name);
        }
    }
} 