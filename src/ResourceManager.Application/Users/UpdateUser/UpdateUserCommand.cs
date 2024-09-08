using ResourceManager.Application.Abstractions.Messaging;
using ResourceManager.Domain.Users;

namespace ResourceManager.Application.Users.UpdateUser;

public sealed record UpdateUserCommand(
    Guid UserId,
    string Name,
    string Username,
    Actor Actor,
    Level Level) : ICommand<Guid>;
