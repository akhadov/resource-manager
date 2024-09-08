using ResourceManager.Domain.Users;

namespace ResourceManager.Application.Users.GetUser;

public sealed record UserResponse(
    Guid Id,
    string Name,
    string Username,
    Actor Actor,
    Level Level);
