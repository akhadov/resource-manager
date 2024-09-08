using ResourceManager.Application.Abstractions.Data;
using ResourceManager.Application.Abstractions.Messaging;
using ResourceManager.Domain.Users;
using ResourceManager.SharedKernel;

namespace ResourceManager.Application.Users.DeleteUser;

internal sealed class DeleteUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteUserCommand>
{
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

        userRepository.Delete(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
