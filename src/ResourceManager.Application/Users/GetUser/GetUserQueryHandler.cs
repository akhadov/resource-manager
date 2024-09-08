using ResourceManager.Application.Abstractions.Messaging;
using ResourceManager.Domain.Users;
using ResourceManager.SharedKernel;

namespace ResourceManager.Application.Users.GetUser;

internal sealed class GetUserQueryHandler(
    IUserRepository userRepository) : IQueryHandler<GetUserQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

        var result = new UserResponse(
            user.Id,
            user.Name,
            user.Username,
            user.Actor,
            user.Level);

        return result;
    }
}
