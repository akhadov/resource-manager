using ResourceManager.Domain.Users;

namespace ResourceManager.Application.Users.UpdateUser;

public sealed record UpdateUserRequest(
    string Name,
    string Username,
    Actor Actor,
    Level Level);
