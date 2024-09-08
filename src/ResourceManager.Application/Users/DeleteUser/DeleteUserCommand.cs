using ResourceManager.Application.Abstractions.Messaging;

namespace ResourceManager.Application.Users.DeleteUser;

public sealed record DeleteUserCommand(Guid UserId) : ICommand;
