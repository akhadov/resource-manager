using ResourceManager.Application.Abstractions.Messaging;
using ResourceManager.Application.Users.GetUser;
using ResourceManager.Domain.Users;
using ResourceManager.SharedKernel;

namespace ResourceManager.Application.Users.GetUsers;

internal sealed class GetUsersQueryHandler(
    IUserRepository userRepository)
    : IQueryHandler<GetUsersQuery, IEnumerable<UserResponse>>
{
    public async Task<Result<IEnumerable<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAllAsync(cancellationToken);

        var result = users.Select
            (u => new UserResponse(
                u.Id,
                u.Name,
                u.Username,
                u.Actor,
                u.Level))
            .ToList();

        return result;
    }
}
