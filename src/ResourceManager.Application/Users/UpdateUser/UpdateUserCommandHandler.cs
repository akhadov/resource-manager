
using ResourceManager.Application.Abstractions.Data;
using ResourceManager.Application.Abstractions.Messaging;
using ResourceManager.Domain.Users;
using ResourceManager.SharedKernel;

namespace ResourceManager.Application.Users.UpdateUser;

internal class UpdateUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

        user.Update(
            request.Name,
            request.Username,
            request.Actor,
            request.Level);

        userRepository.Update(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
