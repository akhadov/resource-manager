using ResourceManager.Application.Abstractions.Messaging;
using ResourceManager.Application.Users.GetUser;

namespace ResourceManager.Application.Users.GetUsers;

public sealed record GetUsersQuery : IQuery<IEnumerable<UserResponse>>;
