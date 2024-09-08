using ResourceManager.Domain.Users;

namespace ResourceManager.Application.Users.CreateUser;

public sealed record CreateUserRequest(
    string Name,
    string Username,
    Actor Actor,
    Level Level);
