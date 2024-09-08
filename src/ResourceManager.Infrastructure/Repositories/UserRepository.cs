using Microsoft.EntityFrameworkCore;
using ResourceManager.Domain.Users;
using ResourceManager.Infrastructure.Database;

namespace ResourceManager.Infrastructure.Repositories;

internal sealed class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Users.ToListAsync(cancellationToken);
    }

    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public void Insert(User user)
    {
        context.Users.Add(user);
    }

    public void Update(User user)
    {
        context.Users.Update(user);
    }

    public void Delete(User user)
    {
        context.Users.Remove(user);
    }

    public Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return context.Users.FirstOrDefaultAsync(u => u.Username.Equals(username), cancellationToken);
    }
}
