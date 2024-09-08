using ResourceManager.Application.Abstractions.Messaging;

namespace ResourceManager.Application.Users.GetUser;

public sealed record GetUserQuery(Guid UserId) : IQuery<UserResponse>;
