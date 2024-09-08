namespace ResourceManager.Domain.Users;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    void Insert(User user);
    void Update(User user);
    void Delete(User user);
}
