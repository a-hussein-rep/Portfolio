using Microsoft.EntityFrameworkCore;

using TaskHub.API.Data;
using TaskHub.API.Dtos;
using TaskHub.API.Models;

namespace TaskHub.API.Repositories
{
    public class UsersRepository
    {
        private readonly AppDbContext dbContext;

        public UsersRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        internal async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await dbContext.Users.ToListAsync();
        }

        internal async Task<User?> GetUserByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return await dbContext.Users
                .Include(u => u.Tasks)
                .Include(u => u.Projects)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        internal async Task<User> CreateUserAsync(UserDto dto)
        {
            var user = await dbContext.Users.AddAsync(
                new User
                {
                    Name = dto.Name,
                    Email = dto.Email
                }
            );

            await dbContext.SaveChangesAsync();

            return user.Entity;
        }

        internal async Task UpdateUserAsync(int id, UserDto dto)
        {
            var user = (await dbContext.Users.FindAsync(id))!;
            
            user.Name = dto.Name;
            user.Email = dto.Email;

            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
        }

        internal async Task DeleteUserAsync(User user)
        {
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
        }
    }
}
