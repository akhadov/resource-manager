using ResourceManager.Application.Users.GetUser;

namespace ResourceManager.UI.Services.Interfaces;

public interface IUserService
{
    Task<List<UserResponse>?> GetUsers();
    Task<UserResponse> GetUser(Guid id);
}
