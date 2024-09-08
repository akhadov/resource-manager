using ResourceManager.Application.Abstractions.Data;
using ResourceManager.Application.Abstractions.Messaging;
using ResourceManager.Domain.Users;
using ResourceManager.SharedKernel;

namespace ResourceManager.Application.Users.CreateUser;

internal sealed class CreateUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var username = await userRepository.GetByUsernameAsync(request.Username);

        var user = User.Create(
            request.Name,
            request.Username,
            request.Actor,
            request.Level);

        userRepository.Insert(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
