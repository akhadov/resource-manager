using ResourceManager.Application.Abstractions.Messaging;
using ResourceManager.Domain.Users;

namespace ResourceManager.Application.Users.CreateUser;

public sealed record CreateUserCommand(
    string Name,
    string Username,
    Actor Actor,
    Level Level) : ICommand<Guid>;
